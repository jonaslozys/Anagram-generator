import config from './config.js';

const getAnagrams = (word) => new Promise((resolve, reject) => {
    word = word.trim();
    if (word.length > 0) {
        axios.get(`${config.APIURL}/anagrams/${word}`)
            .then(res => resolve(res))
            .catch(err => reject(err));
    }
})


export default getAnagrams;