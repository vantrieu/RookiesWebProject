import { userService } from './../../services/user.service';
import { AccountActionTypes, LOGIN_FAILURE, LOGIN_REQUEST, LOGIN_SUCCESS, LOG_OUT, LOAD_CURRENT_LOGIN_USER_FAILURE, LOAD_CURRENT_LOGIN_USER_REQUEST, LOAD_CURRENT_LOGIN_USER_SUCCESS } from './types';
import { Dispatch } from "react";
import { history } from '../../helpers';

export const login = (username: string, password: string, from: string) => {
    return async (dispatch: Dispatch<AccountActionTypes>) => {
        dispatch({
            type: LOGIN_REQUEST,
            payload: {
                username: username,
                password: password
            }
        });

        try {
            const response = await userService.login(username, password);
            const res = await userService.CheckRole(response.access_token);
            if(res === 200){
                dispatch({
                    type: LOGIN_SUCCESS,
                    payload: response,
                });
                history.push(from);
            }
            else {
                dispatch({
                    type: LOGIN_FAILURE,
                    payload: { error: 'Login failure!' },
                });
            }
        } catch (error) {
            dispatch({
                type: LOGIN_FAILURE,
                payload: { error: error.toString() },
            });
        }
    }
}

export const logout = (): AccountActionTypes => {
    return { type: LOG_OUT };
};

export const getCurrentLoginUser = () => {
    return async (dispatch: Dispatch<AccountActionTypes>) => {
        dispatch({
            type: LOAD_CURRENT_LOGIN_USER_REQUEST,
        });
        try {
            const response = await userService.getCurrentLoginUser();
            dispatch({
                type: LOAD_CURRENT_LOGIN_USER_SUCCESS,
                payload: { user: response },
            });
        } catch (error) {
            dispatch({
                type: LOAD_CURRENT_LOGIN_USER_FAILURE,
                payload: { error: error.toString() },
            });
        }
    };
};