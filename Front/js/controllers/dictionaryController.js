import Renderer from '../renderer.js';
import getPageOfWords from '../services/getPageOfWords.js';
import dictionaryView from '../views/dictionaryView.js';
import DictionaryModel from '../models/dictionaryModel.js';

class DictionaryController{
    constructor() {
        this.renderer = new Renderer();
        this.currentPage = 11321312213;
        this.dictionaryModel = DictionaryModel;
    }

    mapResponseToModel(data) {
        this.dictionaryModel.words = data.words;
        this.dictionaryModel.currentPage = data.currentPage;
        this.dictionaryModel.error = data.response.statusText;
    }

    changePage() {
        if (!this.dictionaryModel.words) {
            getPageOfWords(this.currentPage)
            .then(res => {
                this.mapResponseToModel(res.data);
                this.renderer.changePage(dictionaryView(this.dictionaryModel));
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