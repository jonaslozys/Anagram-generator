import html from '../services/stringToHtmlParser.js';

const homePage = (anagrams = []) => {
    const result =  html`
    <section>
        <form>
            <div class="form-group"> 
                <label for="wordInput">Enter word</label>
                <input name="word" type="text" placeholder="word" id="wordInput" class="form-control col-md-2">
            <div>
            <br/>
            <button type="submit" class="btn btn-primary" id="buttonGetAnagrams">Get anagrams</button>
        </form>
        <div id="anagrams">
            <ul>
                ${anagrams.map(anagram => `<li>${anagram}</li>`)}
            <ul>
        </div>
    </section>
    `
    return result;
}


export default homePage;
