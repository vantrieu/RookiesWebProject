import { UsersState, LOAD_USERS_REQUEST, LOAD_USERS_SUCCESS, UsersActionTypes, LOAD_USERS_FAILURE, LOCK_USER_REQUEST, LOCK_USER_FAILURE, LOCK_USER_SUCCESS, User, UNLOCK_USER_REQUEST, UNLOCK_USER_SUCCESS, UNLOCK_USER_FAILURE } from './types';

const initialState: UsersState = {
    users: [],
    loading: false,
    error: null
};

const RefreshState = (userState: UsersState, user: User) => {
    const lstUser = [...userState.users];
    const index = lstUser?.findIndex(u => u.userId === user.userId);
    lstUser?.splice(index, 1, user);
    return lstUser;
}

const usersReducer = (
    state: UsersState = initialState,
    action: UsersActionTypes
): UsersState => {
    switch (action.type) {
        case LOAD_USERS_REQUEST: {
            return {
                ...state,
                loading: action.payload.loading
            }
        }
        case LOAD_USERS_SUCCESS: {
            return {
                ...state,
                loading: action.payload.loading,
                users: action.payload.users
            };
        }
        case LOAD_USERS_FAILURE: {
            return {
                ...state,
                loading: action.payload.loading,
                error: action.payload.error
            }
        }
        case LOCK_USER_REQUEST: {
            return {
                ...state
            }
        }
        case LOCK_USER_SUCCESS: {
            return {
                ...state,
                loading: false,
                users: RefreshState(state, action.payload.user)
            }
        }
        case LOCK_USER_FAILURE: {
            return {
                ...state,
                error: action.payload.error
            }
        }
        case UNLOCK_USER_REQUEST: {
            return {
                ...state
            }
        }
        case UNLOCK_USER_SUCCESS: {
            return {
                ...state,
                loading: false,
                users: RefreshState(state, action.payload.user)
            }
        }
        case UNLOCK_USER_FAILURE: {
            return {
                ...state,
                error: action.payload.error
            }
        }
        default:
            return state;
    }
}

export { usersReducer };