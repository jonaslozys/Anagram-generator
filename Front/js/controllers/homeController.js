import Renderer from '../renderer.js';
import getAnagrams from '../services/getAnagrams.js';
import homeView from '../views/homeView.js';

class HomeController{
    constructor() {
        this.submitButton = document.getElementById("buttonGetAnagrams")
                                    .addEventListener("click", (e) => this.handleSubmit(e));
        this.renderer = new Renderer();
    }

    handleSubmit(e) {
        e.preventDefault();
        const word = document.getElementById("wordInput").value;
        document.getElementById("wordInput").value = "";
    
        getAnagrams(word)
            .then(anagrams => this.renderer.changePage(homeView(anagrams.data)))
            .catch(err => console.log(err));
    }
    
}

export default HomeController;