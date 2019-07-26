import html from '../services/stringToHtmlParser.js';
import ErrorAlert from '../components/errorAlert.js';

const historyPage = (historyModel) => {
    const result =  html`
    <section>
        <br/>
        ${historyModel.error 
            ? ErrorAlert(historyModel.error)
            : ''
        }
        <h1>Search History</h1>
        <ul class="list-group">
            ${historyModel.historyLogs
                ? historyModel.historyLogs.map(historyLog => `<h1>${historyLog.wordSearched}</h1>`)
                : ''
            }
        </ul>
    </section>
    `
    return result;
}


export default historyPage;
