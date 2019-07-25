const WordItem = (word) =>
    `
        <a class="list-group-item" id=${word.id} href="#">${word.word}</a>
        <button id="deleteWord" class="btn btn-danger" value=${word.id}>Delete word</button>
        <button id="editWord" class="btn btn-info" value=${word.id}>Edit word</button>
    `;

export default WordItem;