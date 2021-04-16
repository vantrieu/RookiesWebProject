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
            dispatch({
                type: LOGIN_SUCCESS,
                payload: response,
            });
            const statusCode = await userService.CheckRole();
            if (statusCode !== 200) {
                dispatch({
                    type: LOG_OUT
                })
                history.push('/');
            }
            history.push(from);
        } catch (error) {
            dispatch({
                type: LOGIN_FAILURE,
                payload: { error: error.toString() },
            });
        }
    }
}

export const logout = (): AccountActionTypes => {
    userService.logout();
    return { type: LOG_OUT }
}

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