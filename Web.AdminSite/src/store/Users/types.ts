export const LOAD_USERS_REQUEST = 'LOAD_USERS_REQUEST';
export const LOAD_USERS_SUCCESS = 'LOAD_USERS_SUCCESS';
export const LOAD_USERS_FAILURE = 'LOAD_USERS_FAILURE';

export const LOCK_USER_REQUEST = 'LOCK_USER_REQUEST';
export const LOCK_USER_SUCCESS = 'LOCK_USER_SUCCESS';
export const LOCK_USER_FAILURE = 'LOCK_USER_FAILURE';

export const UNLOCK_USER_REQUEST = 'UNLOCK_USER_REQUEST';
export const UNLOCK_USER_SUCCESS = 'UNLOCK_USER_SUCCESS';
export const UNLOCK_USER_FAILURE = 'UNLOCK_USER_FAILURE';

export interface User {
    userId: string;
    fullName: string;
    email: string;
    phoneNumber: string;
    lockoutEnd: Date;
}

export interface LoadUsersRequest {
    type: typeof LOAD_USERS_REQUEST;
    payload: {
        loading: boolean
    }
};

export interface LoadUsersSuccess {
    type: typeof LOAD_USERS_SUCCESS;
    payload: {
        loading: boolean,
        users: Array<User>
    }
}

export interface LoadUsersFailure {
    type: typeof LOAD_USERS_FAILURE;
    payload: {
        loading: boolean,
        error: string;
    };
}

export interface UsersState {
    users: Array<User>;
    loading: boolean;
    error: string | null;
}

export interface LockUserRequest {
    type: typeof LOCK_USER_REQUEST
}

export interface LockUserSuccess {
    type: typeof LOCK_USER_SUCCESS,
    payload: {
        user: User
    }
}

export interface LockUserFailure {
    type: typeof LOCK_USER_FAILURE,
    payload: {
        error: string | null
    }
}

export interface UnLockUserRequest {
    type: typeof UNLOCK_USER_REQUEST
}

export interface UnLockUserSuccess {
    type: typeof UNLOCK_USER_SUCCESS,
    payload: {
        user: User
    }
}

export interface UnLockUserFailure {
    type: typeof UNLOCK_USER_FAILURE,
    payload: {
        error: string | null
    }
}

export type UsersActionTypes =
    | LoadUsersRequest
    | LoadUsersSuccess
    | LoadUsersFailure
    | LockUserFailure
    | LockUserRequest
    | LockUserSuccess
    | UnLockUserRequest
    | UnLockUserSuccess
    | UnLockUserFailure;