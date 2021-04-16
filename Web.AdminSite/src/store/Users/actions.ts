import { 
    UsersActionTypes, 
    LOAD_USERS_REQUEST, 
    LOAD_USERS_SUCCESS, 
    LOAD_USERS_FAILURE, 
    LOCK_USER_REQUEST, 
    LOCK_USER_FAILURE, 
    LOCK_USER_SUCCESS, 
    UNLOCK_USER_REQUEST, 
    UNLOCK_USER_SUCCESS,
    UNLOCK_USER_FAILURE
} from './types';
import { userService } from './../../services/user.service';
import { Dispatch } from "react";

export const loadUsers = () => {
    return async (dispatch: Dispatch<UsersActionTypes>) => {
        dispatch({
            type: LOAD_USERS_REQUEST,
            payload: {
                loading: true
            }
        });
        try {
            const response = await userService.getListUser();
            dispatch({
                type: LOAD_USERS_SUCCESS,
                payload: {
                    loading: false,
                    users: response
                }
            });
        } catch (error) {
            dispatch({
                type: LOAD_USERS_FAILURE,
                payload: {
                    loading: false,
                    error: error.toString()
                }
            });
        }
    }
}

export const lockUser = (id: string) => {
    return async (dispatch: Dispatch<UsersActionTypes>) => {
        dispatch({
            type: LOCK_USER_REQUEST
        });
        try {
            const response = await userService.lockUserById(id);
            dispatch({
                type: LOCK_USER_SUCCESS,
                payload: {
                    user: response
                }
            });
        } catch (error) {
            dispatch({
                type: LOCK_USER_FAILURE,
                payload: {
                    error: error.toString()
                }
            });
        }
    }
}

export const unLockUser = (id: string) => {
    return async (dispatch: Dispatch<UsersActionTypes>) => {
        dispatch({
            type: UNLOCK_USER_REQUEST
        });
        try {
            const response = await userService.unLockUserById(id);
            dispatch({
                type: UNLOCK_USER_SUCCESS,
                payload: {
                    user: response
                }
            });
        } catch (error) {
            dispatch({
                type: UNLOCK_USER_FAILURE,
                payload: {
                    error: error.toString()
                }
            });
        }
    }
}