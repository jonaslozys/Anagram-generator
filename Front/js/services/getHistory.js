import config from './config.js';

const getHistory = () => new Promise((resolve, reject) => {
    axios.get(`${config.APIURL}/history`)
            .then(res => resolve(res))
            .catch(err => reject(err));
})

export default getHistory;