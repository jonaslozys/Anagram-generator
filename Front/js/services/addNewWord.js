import config from './config.js';

const addNewWord = (newWordValue) => new Promise((resolve, reject) => {
    newWordValue = newWordValue.trim();
    if (newWordValue.length > 0) {
        axios.post(`${config.APIURL}/dictionary/add/${newWordValue}`)
            .then(res => resolve(res))
            .catch(err => reject(err));
    }
})


export default addNewWord;