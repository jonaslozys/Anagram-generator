import Renderer from '../renderer.js';
import getPageOfWords from '../services/getPageOfWords.js';
import dictionaryView from '../views/dictionaryView.js';

class DictionaryController{
    constructor() {
        //this.submitButton = document.getElementById("buttongetPageOfWords")
          //                          .addEventListener("click", (e) => this.handleSubmit(e));
        this.renderer = new Renderer();
        this.currentPage = 1;
    }

    changePage() {
        getPageOfWords(this.currentPage)
            .then(words => this.renderer.changePage(dictionaryView(words.data.words)))
            .catch(err => console.log(err));
    }
    
}

export default DictionaryController;