const getPageOfWords = (pageNumber = 1) => new Promise((resolve, reject) => {
    axios.get(`https://localhost:8001/api/dictionary/page/${pageNumber}`)
            .then(res => resolve(res))
            .catch(err => reject(err));
})

export default getPageOfWords;