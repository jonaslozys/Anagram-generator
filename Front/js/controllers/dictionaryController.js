import Renderer from '../renderer.js';
import getPageOfWords from '../services/getPageOfWords.js';
import dictionaryView from '../views/dictionaryView.js';
import DictionaryModel from '../models/dictionaryModel.js';

class DictionaryController{
    constructor() {
        this.renderer = new Renderer();
        this.currentPage = 1;
        this.eventListeners = null;
        this.dictionaryModel = DictionaryModel;
    }

    setupEventListeners() {
        this.eventListeners = [];
        const buttons = document.querySelectorAll(".page-item");
        buttons.forEach(button => {
            button.addEventListener("click", (e) => {
                e.preventDefault();
                this.handlePaginationClick(e.target.value);
            });
        });
    }

    handlePaginationClick(pageNumber) {
        this.currentPage = pageNumber;
        this.dictionaryModel.currentPage = pageNumber;
        this.dictionaryModel.words = null;
        this.changePage();
    }

    mapResponseToModel(data) {
        this.dictionaryModel.words = data.words;
        this.dictionaryModel.currentPage = data.page;
        this.dictionaryModel.error = data.response ? data.response.statusText : null;
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
        }
    }
    
}

export default DictionaryController;