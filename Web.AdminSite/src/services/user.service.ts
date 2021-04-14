import API from '../utils/API';

const login = (username: string, password: string) => {
    var urlencoded = new URLSearchParams();
    urlencoded.append("grant_type", "password");
    urlencoded.append("username", username);
    urlencoded.append("password", password);
    urlencoded.append("client_id", "react");
    urlencoded.append("client_secret", "secret");

    console.log(API)
    return API.post('/connect/token', urlencoded)
        .then(response => {
            sessionStorage.setItem('user', JSON.stringify(response.data));
            return response.data;
        })
        .catch(error => {
            return Promise.reject(error);
        })
}

const logout = () => {
    sessionStorage.removeItem('user');
}

export const userService = {
    login,
    logout
}