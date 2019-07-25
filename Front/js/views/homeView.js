import html from '../services/stringToHtmlParser.js';
import getAnagarmsForm from '../components/getAnagramsForm.js';

const homePage = (anagrams = []) => {
    const result =  html`
    <section>
        ${getAnagarmsForm()}
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
