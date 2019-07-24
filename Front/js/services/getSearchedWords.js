import config from './config.js';

const getSearchedWords = (searchValue) => new Promise((resolve, reject) => {
    if (searchValue.length > 0) {
        axios.get(`${config.APIURL}/dictionary/find/${searchValue}`)
            .then(res => resolve(res))
            .catch(err => reject(err));
    }
})


export default getSearchedWords;