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
        <form action="http://google.com">
            <div class="form-group"> 
                <label for="wordInput">Enter word</label>
                <input name="word" type="text" placeholder="word" id="wordInput" class="form-control col-md-2">
            <div>
            <br/>
            <button type="submit" class="btn btn-primary">Get anagrams</button>
        </form>
    </section>
    `

export default homePage;
