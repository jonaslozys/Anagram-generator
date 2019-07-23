const html = (literal, ...cooked) => {
    let result = '';
    cooked.forEach((cook, i) => {
        let lit = literal[i];
        result += lit;
        result += cook;
    })

    result += literal[literal.length - 1];
    return result;
}

const homePage = html
    `
    <section>
        <form action="localhost:3000">
            <label>Enter word</label>
            <input name="word" type="text" placeholder="word" id="userInput">
            <button type="submit">Get anagrams</button>
        </form>
    </section>
    `

export default homePage;
