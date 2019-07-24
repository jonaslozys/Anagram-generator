import Renderer from '../renderer.js';
import getPageOfWords from '../services/getPageOfWords.js';
import deleteWord from '../services/deleteWord.js';
import dictionaryView from '../views/dictionaryView.js';
import DictionaryModel from '../models/dictionaryModel.js';

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
    
}

export default DictionaryController;