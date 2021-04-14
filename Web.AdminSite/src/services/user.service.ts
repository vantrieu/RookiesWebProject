import env from 'react-dotenv';

const login = (username: string, password: string) => {
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/x-www-form-urlencoded");

    var urlencoded = new URLSearchParams();
    urlencoded.append("grant_type", "password");
    urlencoded.append("username", username);
    urlencoded.append("password", password);
    urlencoded.append("client_id", "react");
    urlencoded.append("client_secret", "secret");

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: urlencoded
    };

    return fetch(`${env.API_URL}/connect/token`, requestOptions)
        .then(handleResponse)
        .then((response) => {
            sessionStorage.setItem('user', JSON.stringify(response));
            return response;
        })
}

const logout = () => {
    sessionStorage.removeItem('user');
}

const handleResponse = (response: any) => {
    return response.text().then((text: string) => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                logout();
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }
        return data;
    });
}

export const userService = {
    login,
    logout
}