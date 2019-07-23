import html from '../services/stringToHtmlParser.js';

const homePage = html
    `
    <section>
        <form>
            <div class="form-group"> 
                <label for="wordInput">Enter word</label>
                <input name="word" type="text" placeholder="word" id="wordInput" class="form-control col-md-2">
            <div>
            <br/>
            <button type="submit" class="btn btn-primary" id="buttonGetAnagrams">Get anagrams</button>
        </form>
    </section>
    `

export default homePage;
