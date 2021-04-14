export const LOGIN_REQUEST = 'LOGIN_REQUEST';
export const LOGIN_SUCCESS = 'LOGIN_SUCCESS';
export const LOGIN_FAILURE = 'LOGIN_FAILURE';

export const LOG_OUT = 'LOG_OUT';

export interface AuthenticatedUser {
    _id: string;
    first_name: string;
    last_name: string;
    username: string;
    avatar: string;
}

interface LoginRequest {
    type: typeof LOGIN_REQUEST;
    payload: {
        username: string;
        password: string;
    };
}

interface LoginSuccess {
    type: typeof LOGIN_SUCCESS;
    payload: {
        access_token: string;
        refresh_token: string;
        expires_in: number;
    };
}

interface LoginFailure {
    type: typeof LOGIN_FAILURE;
    payload: {
        error: string;
    };
}

interface Logout {
    type: typeof LOG_OUT;
}

export interface AccountState {
    user: AuthenticatedUser | null;
    loading: boolean;
    error: string | null;
    access_token: string | null;
    refresh_token: string | null;
    expires_in: number | 0;    
}

export type AccountActionTypes = 
    | LoginRequest
    | LoginSuccess
    | LoginFailure
    | Logout;