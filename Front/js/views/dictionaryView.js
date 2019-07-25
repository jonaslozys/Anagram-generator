import html from '../services/stringToHtmlParser.js';
import ErrorAlert from '../components/errorAlert.js';
import Pagination from '../components/pagination.js';
import WordItem from '../components/wordItem.js';
import SearchForm from '../components/searchForm.js';
import AddWordForm from '../components/addWordForm.js';

const dictionaryPage = (dictionaryModel) => {
    const view = html`    
        <section>
            <h2>Dictionary</h2>
            ${AddWordForm()}
            <br/>
            ${SearchForm()}
            <br/>
            ${dictionaryModel.error 
                ? ErrorAlert(dictionaryModel.error)
                : ''}
            ${Pagination(dictionaryModel.currentPage)}
            <ul class="list-group">
                ${dictionaryModel.words
                    ? dictionaryModel.words.map(word => WordItem(word))
                    : ''
                }
            </ul>
        </section>`;
    return view;
}

export default dictionaryPage;