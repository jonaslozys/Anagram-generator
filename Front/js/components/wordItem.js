const WordIntem = (word) => {
    const wordItem = 
    `
        <li class="list-group-item" id=${word.id}>${word.word}</li>
        <button class="btn btn-danger">Delete word</button>
        <button class="btn btn-info">Edit word</button>
    `
    return wordItem;
}

export default WordIntem;