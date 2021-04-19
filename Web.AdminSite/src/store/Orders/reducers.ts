import {
    CONFIRM_ORDER,
    LOAD_ORDER_FAILURE,
    LOAD_ORDER_REQUEST,
    LOAD_ORDER_SUCCESS,
    OrdersActionTypes,
    OrdersState
} from './types';


const initialState: OrdersState = {
    orders: [],
    loading: false,
    error: null
}

const RefreshItems = (ordersState: OrdersState, id: number) => {
    const listOrder = [...ordersState.orders];
    const index = listOrder.findIndex(u => u.orderId === id);
    const temp = listOrder[index];
    temp.status = true;
    listOrder.splice(index, 1, temp);
    return listOrder;
}

const ordersReducer = (
    state: OrdersState = initialState,
    action: OrdersActionTypes
): OrdersState => {
    switch (action.type) {
        case LOAD_ORDER_REQUEST: {
            return {
                ...state,
                loading: true
            }
        }
        case LOAD_ORDER_SUCCESS: {
            return {
                ...state,
                loading: false,
                orders: action.payload.orders
            }
        }
        case LOAD_ORDER_FAILURE: {
            return {
                ...state,
                loading: false,
                error: action.payload.error
            }
        }
        case CONFIRM_ORDER: {
            return {
                ...state,
                orders: RefreshItems(state, action.payload.id)
            }
        }
        default:
            return state;
    }
}

export { ordersReducer };