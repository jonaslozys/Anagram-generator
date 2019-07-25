import html from '../services/stringToHtmlParser.js';
import getAnagarmsForm from '../components/getAnagramsForm.js';
import AnagramItem from '../components/anagramItem.js';
import ErrorAlert from '../components/errorAlert.js';

const homePage = (homeModel) => {
    console.log(homeModel)
    const result =  html`
    <section>
        ${getAnagarmsForm()}
        <br/>
        ${homeModel.error 
            ? ErrorAlert(homeModel.error)
            : ''
        }
        ${homeModel.searchWord
            ? `<h2>Anagrams for <b>${homeModel.searchWord}</b></h2>`
            : ''
        }
        <ul class="list-group">
            ${homeModel.anagrams
                ? homeModel.anagrams.map(anagram => AnagramItem(anagram))
                : ''
            }
        </ul>
    </section>
    `
    return result;
}


export default homePage;
