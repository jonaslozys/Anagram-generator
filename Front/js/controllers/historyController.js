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
        this.displayHistory();
    }

    mapResponseToModel(data) {
        if (data.historyLogs) {
            this.historyModel.historyLogs = data.historyLogs ? data.historyLogs : this.historyModel.historyLogs;
            this.historyModel.loading = false;
        } else {
            this.historyModel.loading = true;
        }
    }

    async displayHistory() {
        if (!this.historyModel.historyLogs) {
            this.getSearchHistory();
            this.renderer.changePage(historyView(this.historyModel));
        } else {
            this.renderer.changePage(historyView(this.historyModel));
        }
    }

}

export default HistoryController;