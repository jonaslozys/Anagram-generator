import Renderer from '../renderer.js';
import getPageOfWords from '../services/getPageOfWords.js';
import getSearchedWords from '../services/getSearchedWords.js';
import deleteWord from '../services/deleteWord.js';
import dictionaryView from '../views/dictionaryView.js';
import DictionaryModel from '../models/dictionaryModel.js';
import searchPage from '../views/searchView.js';

class DictionaryController{
    constructor() {
        this.renderer = new Renderer();
        this.currentPage = 1;
        this.dictionaryModel = DictionaryModel;
    }

    setupEventListeners() {
        const paginationButtons = document.querySelectorAll(".page-item");
        paginationButtons.forEach(button => {
            button.addEventListener("click", (e) => {
                e.preventDefault();
                this.handlePaginationClick(e.target.value);
            });
        });

        const deleteButton = document.querySelectorAll("#deleteWord");
        deleteButton.forEach(button => {
            button.addEventListener("click", (e) => {
                e.preventDefault();
                this.handleDelete(e.target.value);
            })
        })

        const searchButton = document.getElementById("searchForm").addEventListener("submit", (e) => this.handleSearch(e));
    }

    handlePaginationClick(pageNumber) {
        this.currentPage = pageNumber;
        this.dictionaryModel.currentPage = pageNumber;
        this.dictionaryModel.words = null;
        this.changePage();
    }

    handleDelete(wordId) {
        deleteWord(wordId)
            .then(res => {
                    this.dictionaryModel.words.forEach((word, index) => {
                        if (word.id == wordId) {
                            this.dictionaryModel.words.splice(index, 1);
                        }
                    });
                    this.changePage();

            })
            .catch(err => {
                this.mapResponseToModel(err);
                this.changePage();
            });
    }

    async handleSearch(e) {
        e.preventDefault();
        const searchField = document.getElementById("searchValue");
        const searchValue = searchField.value;
        searchField.value = "";

        await getSearchedWords(searchValue)
            .then(res => {
                this.mapResponseToModel(res.data);
            })
            .catch(err => {
                this.mapResponseToModel(err);
                this.dictionaryModel.words = null;
            });
        this.openSearchResultsPage();
    }

    mapResponseToModel(data) {
        this.dictionaryModel.words = data.words ? data.words : this.dictionaryModel.words;
        this.dictionaryModel.currentPage = data.page ? data.page : this.dictionaryModel.currentPage;
        this.dictionaryModel.error = data.response ? data.response.data : null;
    }

    changePage() {
        if (!this.dictionaryModel.words) {
            getPageOfWords(this.currentPage)
                .then(res => {
                    this.mapResponseToModel(res.data);
                    this.renderer.changePage(dictionaryView(this.dictionaryModel));
                    this.setupEventListeners();
                })
                .catch(err => {
                    this.mapResponseToModel(err);
                    this.renderer.changePage(dictionaryView(this.dictionaryModel));
                });
        } else {
            this.renderer.changePage(dictionaryView(this.dictionaryModel));
            this.setupEventListeners();
        }
    }

    openSearchResultsPage() {
        this.renderer.changePage(searchPage(this.dictionaryModel));
        this.setupEventListeners();
    }
    
}

export default DictionaryController;