import html from '../services/stringToHtmlParser.js';
import ErrorAlert from '../components/errorAlert.js';
import Loading from '../components/loading.js';
import HistoryItem from '../components/historyItem.js';

const historyPage = (historyModel) => {
    const result =  html`
    <section>
        <br/>
        <h1>Search History</h1>
        ${historyModel.error 
            ? ErrorAlert(historyModel.error)
            : ''
        }
        ${historyModel.loading 
            ? Loading()
            : ''
        }
        <ul class="list-group">
            ${historyModel.historyLogs
                ? historyModel.historyLogs.map(historyLog => HistoryItem(historyLog))
                : ''
            }
        </ul>
    </section>
    `
    return result;
}


export default historyPage;
