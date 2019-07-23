import Renderer from "./renderer.js";
import HomeController from './controllers/homeController.js';

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


window.addEventListener('load', () => {
    app.init()
    new HomeController();
});

document.getElementById("homeLink").addEventListener("click", () => view.changePage(homeView()));
document.getElementById("dictionaryLink").addEventListener("click", () => view.changePage("Dictionary page"));
document.getElementById("historyLink").addEventListener("click", () => view.changePage("History page"));