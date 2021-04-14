import { AccountState, AccountActionTypes, LOGIN_REQUEST, LOGIN_SUCCESS, LOGIN_FAILURE, LOG_OUT } from './types';

const initialState: AccountState = {
    user: null,
    loading: false,
    error: null,
    token: null
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
                token: action.payload.token
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
                token: null
            }
        }
        default:
            return state;
    }
}

export {accountReducer};