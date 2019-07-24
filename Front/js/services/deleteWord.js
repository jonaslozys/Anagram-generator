import config from './config.js';

const deleteWord = (wordId) => new Promise((resolve, reject) => {
    axios.delete(`${config.APIURL}/dictionary/delete/${wordId}`)
        .then(res => resolve(res))
        .catch(err => reject(err));
})


export default deleteWord;