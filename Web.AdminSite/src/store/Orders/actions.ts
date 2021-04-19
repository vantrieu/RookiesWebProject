import { orderService } from './../../services/order.service';
import {
    CONFIRM_ORDER,
    LOAD_ORDER_FAILURE,
    LOAD_ORDER_REQUEST,
    LOAD_ORDER_SUCCESS,
    OrdersActionTypes
} from './types';
import { Dispatch } from "react";

export const loadOrders = () => {
    return async (dispatch: Dispatch<OrdersActionTypes>) => {
        dispatch({
            type: LOAD_ORDER_REQUEST,
            payload: {
                loading: true
            }
        })
        try {
            const response = await orderService.GetAllOrder();
            dispatch({
                type: LOAD_ORDER_SUCCESS,
                payload: {
                    loading: false,
                    orders: response
                }
            })
        } catch (error) {
            dispatch({
                type: LOAD_ORDER_FAILURE,
                payload: {
                    loading: false,
                    error: error.toString()
                }
            })
        }
    }
}

export const confirmOrder = (id: number) => {
    return async (dispatch: Dispatch<OrdersActionTypes>) => {
        const response = await orderService.ConfirmOrder(id);
        if (response === 200) {
            dispatch({
                type: CONFIRM_ORDER,
                payload: {
                    id: id
                }
            });
        }
    }
}