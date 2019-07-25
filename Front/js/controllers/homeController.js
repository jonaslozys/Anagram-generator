import Renderer from '../renderer.js';
import getAnagrams from '../services/getAnagrams.js';
import homeView from '../views/homeView.js';

class HomeController{
    constructor() {
        this.renderer = new Renderer();
        this.displayPage();
    }

    handleSubmit(e) {
        e.preventDefault();
        const word = document.getElementById("wordInput").value;
        document.getElementById("wordInput").value = "";
    
        getAnagrams(word)
            .then(anagrams => this.renderer.changePage(homeView(anagrams.data)))
            .catch(err => console.log(err));
    }

    setupEventListeners() {
        this.submitButton = document.getElementById("buttonGetAnagrams")
            .addEventListener("click", (e) => this.handleSubmit(e));
    }

    displayPage() {
        this.renderer.changePage(homeView());
    }
    
}

export default HomeController;