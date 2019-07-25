import html from '../services/stringToHtmlParser.js';
import ErrorAlert from '../components/errorAlert.js';
import WordItem from '../components/wordItem.js';
import SearchForm from '../components/searchForm.js';

const searchPage = (dictionaryModel) => {
    const view = html`    
        <section>
            <h2>Dictionary</h2>
            ${SearchForm()}
            <br/>
            ${dictionaryModel.error 
                ? ErrorAlert(dictionaryModel.error)
                : ''}
            <ul class="list-group">
                ${dictionaryModel.words
                    ? dictionaryModel.words.map(word => WordItem(word))
                    : ''
                }
            </ul>
        </section>`;
    return view;
}

export default searchPage;