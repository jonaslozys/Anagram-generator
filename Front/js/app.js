import HomeController from './controllers/homeController.js';
import DictionaryController from './controllers/dictionaryController.js';
import HistoryController from './controllers/historyController.js';

class App {
    constructor() {
    }

    init() {
        this.homeController = new HomeController();
        this.dictionaryController = new DictionaryController(this.homeController);
        this.historyController = new HistoryController();
        this.addEventListenersForNavigationalButtons();
    }

    addEventListenersForNavigationalButtons() {
        document.getElementById("homeLink").addEventListener("click", () => this.homeController.displayPage());
        document.getElementById("dictionaryLink").addEventListener("click", () => this.dictionaryController.changePage());
        document.getElementById("historyLink").addEventListener("click", () => this.historyController.displayHistory());
    }
}

const app = new App();

window.addEventListener('load', () => {
    app.init();
});

