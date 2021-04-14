import {
    AccountState, AccountActionTypes, LOGIN_REQUEST, LOGIN_SUCCESS, LOGIN_FAILURE, LOG_OUT, LOAD_CURRENT_LOGIN_USER_FAILURE,
    LOAD_CURRENT_LOGIN_USER_REQUEST, LOAD_CURRENT_LOGIN_USER_SUCCESS
} from './types';

const initialState: AccountState = {
    user: null,
    loading: false,
    error: null,
    access_token: null,
    refresh_token: null,
    expires_in: 0
}

const accountReducer = (
    state: AccountState = initialState,
    action: AccountActionTypes
): AccountState => {
    switch (action.type) {
        case LOGIN_REQUEST: {
            return {
                ...state,
                loading: true
            };
        }
        case LOGIN_SUCCESS: {
            return {
                ...state,
                loading: false,
                access_token: action.payload.access_token,
                refresh_token: action.payload.refresh_token,
                expires_in: action.payload.expires_in
            };
        }
        case LOGIN_FAILURE: {
            return {
                ...state,
                loading: false,
                error: action.payload.error
            };
        }
        case LOG_OUT: {
            return {
                ...state,
                user: null,
                error: null,
                access_token: null,
                refresh_token: null,
                expires_in: 0
            }
        }
        case LOAD_CURRENT_LOGIN_USER_REQUEST: {
            return {
                ...state,
                loading: true
            };
        }
        case LOAD_CURRENT_LOGIN_USER_SUCCESS: {
            return {
                ...state,
                loading: false,
                user: action.payload.user,
            };
        }
        case LOAD_CURRENT_LOGIN_USER_FAILURE: {
            return {
                ...state,
                loading: false,
                error: action.payload.error,
            };
        }
        default:
            return state;
    }
}

export { accountReducer };