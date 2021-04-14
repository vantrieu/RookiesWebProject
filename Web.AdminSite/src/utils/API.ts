import axios from 'axios';
import env from 'react-dotenv';

// axios.interceptors.request.use(function (config) {
//     const token = JSON.parse(localStorage.getItem('user')).access_token;
//     if(token){
//         config.headers.Authorization =  token;
//     }
//     return config;
// });

export default axios.create({
    baseURL: `${env.API_URL}`
})