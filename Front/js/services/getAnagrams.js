const getAnagrams = (word) => new Promise((resolve, reject) => {
    if (word.length > 0) {
        axios.get(`https://localhost:8001/api/anagrams/${word}`)
            .then(res => resolve(res))
            .catch(err => reject(err));
    }
})


export default getAnagrams;