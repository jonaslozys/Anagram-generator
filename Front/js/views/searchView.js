import html from '../services/stringToHtmlParser.js';
import ErrorAlert from '../components/errorAlert.js';
import WordItem from '../components/wordItem.js';

const searchPage = (dictionaryModel) => {
    const view = html`    
        <section>
            <h2>Dictionary</h2>
            <form class="form-inline">
                <div class="form-group"> 
                    <input name="word" type="text" placeholder="Search" id="wordInput" class="form-control col-md-8">
                    <button type="submit" class="btn btn-primary" id="buttonGetAnagrams">Find Words</button>
                <div>
            </form>
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