const getAnagrams = (word) => {
    if (word.length > 0) {
        axios.get(`http://localhost:5001?word=${word}`)
            .then(res => console.log(res))
            .catch(err => console.log(err));
    }
}


export default getAnagrams;