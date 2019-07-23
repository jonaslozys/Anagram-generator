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

export default html;