export const LOGIN_REQUEST = 'LOGIN_REQUEST';
export const LOGIN_SUCCESS = 'LOGIN_SUCCESS';
export const LOGIN_FAILURE = 'LOGIN_FAILURE';

export const LOAD_CURRENT_LOGIN_USER_REQUEST = 'LOAD_CURRENT_LOGIN_USER_REQUEST';
export const LOAD_CURRENT_LOGIN_USER_SUCCESS = 'LOAD_CURRENT_LOGIN_USER_SUCCESS';
export const LOAD_CURRENT_LOGIN_USER_FAILURE = 'LOAD_CURRENT_LOGIN_USER_FAILURE';

export const LOG_OUT = 'LOG_OUT';

export interface AuthenticatedUser {
    userId: string;
    fullName: string;
    email: string;
    phoneNumber: string;
    lockoutEnd: boolean;
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

interface LoadCurrentLoginUserRequest {
    type: typeof LOAD_CURRENT_LOGIN_USER_REQUEST;
}

interface LoadCurrentLoginUserSuccess {
    type: typeof LOAD_CURRENT_LOGIN_USER_SUCCESS;
    payload: {
        user: AuthenticatedUser;
    };
}

interface LoadCurrentLoginUserFailure {
    type: typeof LOAD_CURRENT_LOGIN_USER_FAILURE;
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
    | Logout
    | LoadCurrentLoginUserRequest
    | LoadCurrentLoginUserSuccess
    | LoadCurrentLoginUserFailure;