import Renderer from "./renderer.js";
import HomeController from './controllers/homeController.js';

import homeView from './views/homeView.js';
import getAnagrams from './services/getAnagrams.js';

class App {
    constructor() {
        this.renderer = new Renderer();
    }

    init() {
        this.renderer.render();
        this.renderer.changePage(homeView());
        this.addEventListenersForNavigationalButtons();

    }

    addEventListenersForNavigationalButtons() {
        document.getElementById("homeLink").addEventListener("click", () => this.renderer.changePage(homeView()));
        document.getElementById("dictionaryLink").addEventListener("click", () => this.renderer.changePage("Dictionary page"));
        document.getElementById("historyLink").addEventListener("click", () => this.renderer.changePage("History page"));
    }
}

const app = new App();

window.addEventListener('load', () => {
    app.init()
    new HomeController();
});

