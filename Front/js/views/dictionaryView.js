import html from '../services/stringToHtmlParser.js';

const dictionaryPage = (words = []) => {
    
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

            <form class="form-inline">
                <div class="form-group"> 
                    <input name="word" type="text" placeholder="Search" id="wordInput" class="form-control col-md-8">
                    <button type="submit" class="btn btn-primary" id="buttonGetAnagrams">Find Words</button>
                <div>
            </form>
            <br/>

            <div id="anagrams">
                <ul>
                    ${words.map(word => 
                        `<li>
                            ${word}
                            <button class="btn btn-danger">Delete word</button>
                            <button class="btn btn-info">Edit word</button>
                        <li>`
                    )}
                <ul>
            </div>
        </section>`;
    return view;
}

export default dictionaryPage;