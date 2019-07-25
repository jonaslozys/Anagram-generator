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

    async handleSearch(searchValue) {
        this.homeModel.searchWord = searchValue;
    
        await getAnagrams(searchValue)
                .then(res => this.mapResponseToModel(res.data))
                .catch(err => this.mapResponseToModel(err));
        this.displayPage();
    }

    mapResponseToModel(data) {
        if (data.response) {
            this.homeModel.anagrams = null;
            this.homeModel.error = data.response.data;
        } else {
            this.homeModel.error = null;
            this.homeModel.anagrams = data.anagrams ? data.anagrams : this.homeModel.anagrams;
        }
    }

    setupEventListeners() {
        document.getElementById("getAnagramsForm").addEventListener("submit", (e) => {
            e.preventDefault();
            const searchValue = document.getElementById("getAnagramsFormValue").value;
            document.getElementById("getAnagramsFormValue").value = "";
            this.handleSearch(searchValue);
        });
    }

    displayPage() {
        this.renderer.changePage(homeView(this.homeModel));
        this.setupEventListeners();
    }
    
}

export default HomeController;