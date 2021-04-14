import env from 'react-dotenv';

const login = (username: string, password: string) => {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ username, password })
    };
    return fetch(`${env.API_URL}/accounts/login`, requestOptions)
        .then(handleResponse)
        .then((response) => {
            sessionStorage.setItem('user', JSON.stringify(response.items));
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