import config from './config.js';

const getPageOfWords = (pageNumber = 1) => new Promise((resolve, reject) => {
    axios.get(`${config.APIURL}/dictionary/page/${pageNumber}`)
            .then(res => resolve(res))
            .catch(err => reject(err));
})

export default getPageOfWords;