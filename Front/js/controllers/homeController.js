import Renderer from '../renderer.js';
import getAnagrams from '../services/getAnagrams.js';
import homeModel from '../models/homeModel.js';
import homeView from '../views/homeView.js';

class HomeController{
    constructor() {
        this.renderer = new Renderer();
        this.homeModel = homeModel;
        this.displayPage();
    }

    async handleSearch(e) {
        e.preventDefault();
        const searchValue = document.getElementById("getAnagramsFormValue").value;
        document.getElementById("getAnagramsFormValue").value = "";
    
        await getAnagrams(searchValue)
                .then(res => this.mapResponseToModel(res.data))
                .catch(err => this.mapResponseToModel(res));
        this.displayPage();
    }

    mapResponseToModel(data) {
        console.log(data);
        this.homeModel.anagrams = data.anagrams ? data.anagrams : this.homeModel.anagrams;
        this.homeModel.error = data.response ? data.response : this.homeModel.error;
    }

    setupEventListeners() {
        document.getElementById("getAnagramsForm").addEventListener("submit", (e) => this.handleSearch(e));
    }

    displayPage() {
        this.renderer.changePage(homeView());
        this.setupEventListeners();
    }
    
}

export default HomeController;