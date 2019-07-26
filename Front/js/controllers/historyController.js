import Renderer from '../renderer.js';
import getHistory from '../services/getHistory.js';
import historyModel from '../models/historyModel.js';
import historyView from '../views/historyView.js';

class HistoryController {
    constructor() {
        this.renderer = new Renderer();
        this.historyModel = historyModel;
    }

    async getSearchHistory() {
        await getHistory()
                .then(res => this.mapResponseToModel(res.data))
                .catch(err => this.mapResponseToModel(err));
    }

    mapResponseToModel(data) {
        console.log(data);
        this.historyModel.historyLogs = data.historyLogs ? data.historyLogs : this.historyModel.historyLogs;
        console.log(this.historyModel)
    }

    async displayHistory() {
        await this.getSearchHistory();
        this.renderer.changePage(historyView(this.historyModel));
    }

}

export default HistoryController;