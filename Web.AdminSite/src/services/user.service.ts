import { api } from '../helpers';

const login = async (username: string, password: string): Promise<any> => {
    var urlencoded = new URLSearchParams();
    urlencoded.append("grant_type", "password");
    urlencoded.append("username", username);
    urlencoded.append("password", password);
    urlencoded.append("client_id", "react");
    urlencoded.append("client_secret", "secret");

    return await api.post('/connect/token', urlencoded)
        .then(response => {
            sessionStorage.setItem('user', JSON.stringify(response.data));
            return response.data;
        })
        .catch(error => {
            return Promise.reject(error);
        })
}

const getCurrentLoginUser = async (): Promise<any> => {
    return await api.get<any>('/api/v1/User/info').then((response) => {
      return response.data;
    });
}

const logout = () => {
    sessionStorage.removeItem('user');
}

export const userService = {
    login,
    logout,
    getCurrentLoginUser
}