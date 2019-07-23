import View from "./view.js";

class App {
    constructor() {
        this.view = new View();
    }

    init() {
        this.view.render();
        this.view.changePage("Home page");
    }
}

const app = new App();
app.init()

const view = new View();

document.getElementById("homeLink").addEventListener("click", () => view.changePage("Home page"));
document.getElementById("dictionaryLink").addEventListener("click", () => view.changePage("Dictionary page"));
document.getElementById("historyLink").addEventListener("click", () => view.changePage("History page"));




