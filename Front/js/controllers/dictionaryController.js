import Renderer from '../renderer.js';
import getPageOfWords from '../services/getPageOfWords.js';
import getSearchedWords from '../services/getSearchedWords.js';
import deleteWord from '../services/deleteWord.js';
import addNewWord from '../services/addNewWord.js';
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

        document.getElementById("searchForm").addEventListener("submit", (e) => this.handleSearch(e));
        document.getElementById("addWordForm").addEventListener("submit", (e) => this.handleAddWord(e));

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

    async handleAddWord(e) {
        e.preventDefault();
        const newWordField = document.getElementById("newWordValue");
        const newWordValue = newWordField.value;
        newWordField.value = "";

        await addNewWord(newWordValue)
            .then()
            .catch(err => {
                this.mapResponseToModel(err);
            })
        this.dictionaryModel.words = null;
        this.changePage();
    }

    mapResponseToModel(data) {
        this.dictionaryModel.words = data.words ? data.words : this.dictionaryModel.words;
        this.dictionaryModel.currentPage = data.page ? data.page : this.dictionaryModel.currentPage;
        this.dictionaryModel.error = data.response ? data.response.data : this.dictionaryModel.error;
    }

    changePage() {
        if (!this.dictionaryModel.words) {
            getPageOfWords(this.currentPage)
                .then(res => {
                    this.mapResponseToModel(res.data);
                })
                .catch(err => {
                    this.mapResponseToModel(err);
                })
                .then(() => {
                    this.renderer.changePage(dictionaryView(this.dictionaryModel));
                    this.setupEventListeners();
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