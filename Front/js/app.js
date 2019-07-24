import Renderer from "./renderer.js";
import HomeController from './controllers/homeController.js';
import DictionaryController from './controllers/dictionaryController.js';
import homeView from './views/homeView.js';
import dictionaryView from './views/dictionaryView.js';
import getAnagrams from './services/getAnagrams.js';

class App {
    constructor() {
        this.renderer = new Renderer();
    }

    init() {
        this.renderer.render();
        this.renderer.changePage(homeView());
        this.homeController = new HomeController();
        this.dictionaryController = new DictionaryController();
        this.addEventListenersForNavigationalButtons();
    }

    addEventListenersForNavigationalButtons() {
        document.getElementById("homeLink").addEventListener("click", () => this.renderer.changePage(homeView()));
        document.getElementById("dictionaryLink").addEventListener("click", () => this.dictionaryController.changePage());
        document.getElementById("historyLink").addEventListener("click", () => this.renderer.changePage("History page"));
    }
}

const app = new App();

window.addEventListener('load', () => {
    app.init()
});

