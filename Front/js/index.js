import Renderer from "./renderer.js";
import homeView from './views/homeView.js';
import getAnagrams from './services/getAnagrams.js';

class App {
    constructor() {
        this.view = new Renderer();
    }

    init() {
        this.view.render();
        this.view.changePage(homeView());
    }
}

const app = new App();
const view = new Renderer();

let submitButton;

window.addEventListener('load', () => {
    app.init()
    submitButton = document.getElementById("buttonGetAnagrams").addEventListener("click", (e) => {
        e.preventDefault();
        const word = document.getElementById("wordInput").value;
        document.getElementById("wordInput").value = "";
    
        getAnagrams(word)
            .then(anagrams => view.changePage(homeView(anagrams.data)))
            .catch(err => console.log(err));
    });

});


document.getElementById("homeLink").addEventListener("click", () => view.changePage(homeView()));
document.getElementById("dictionaryLink").addEventListener("click", () => view.changePage("Dictionary page"));
document.getElementById("historyLink").addEventListener("click", () => view.changePage("History page"));










