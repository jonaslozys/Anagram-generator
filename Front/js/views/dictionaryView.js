import html from '../services/stringToHtmlParser.js';
import ErrorAlert from '../components/errorAlert.js';
import Pagination from '../components/pagination.js';
import WordItem from '../components/wordItem.js';
import SearchForm from '../components/searchForm.js';

const dictionaryPage = (dictionaryModel) => {
    const view = html`    
        <section>
            <h2>Dictionary</h2>
            
            <form class="form-inline">
                <div class="form-group"> 
                    <input name="word" type="text" placeholder="New Word" id="wordInput" class="form-control col-md-6">
                    <button type="submit" class="btn btn-primary" id="buttonGetAnagrams">Add new Word</button>
                <div>
            </form>
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