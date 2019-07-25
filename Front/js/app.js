import HomeController from './controllers/homeController.js';
import DictionaryController from './controllers/dictionaryController.js';

class App {
    constructor() {
    }

    init() {
        this.homeController = new HomeController();
        this.dictionaryController = new DictionaryController(this.homeController);
        this.addEventListenersForNavigationalButtons();
    }

    addEventListenersForNavigationalButtons() {
        document.getElementById("homeLink").addEventListener("click", () => this.homeController.displayPage());
        document.getElementById("dictionaryLink").addEventListener("click", () => this.dictionaryController.changePage());
        document.getElementById("historyLink").addEventListener("click", () => this.renderer.changePage("History page"));
    }
}

const app = new App();

window.addEventListener('load', () => {
    app.init();
});

