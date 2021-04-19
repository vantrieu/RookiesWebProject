export const LOAD_ORDER_REQUEST = 'LOAD_ORDER_REQUEST';
export const LOAD_ORDER_SUCCESS = 'LOAD_ORDER_SUCCESS';
export const LOAD_ORDER_FAILURE = 'LOAD_ORDER_FAILURE';

export const CONFIRM_ORDER = 'CONFIRM_ORDER';

export interface OrderDetail {
    productId: number,
    productName: string,
    total: number,
    price: number
}

export interface Order {
    orderId: number,
    fullname: string,
    phoneNumber: string,
    orderDate: Date,
    status: boolean,
    products: Array<OrderDetail>
}

export interface OrdersState {
    orders: Array<Order>;
    loading: boolean;
    error: string | null;
}

interface LoadOrderRequest {
    type: typeof LOAD_ORDER_REQUEST,
    payload: {
        loading: boolean
    }
}

interface LoadOrderSeccess {
    type: typeof LOAD_ORDER_SUCCESS,
    payload: {
        loading: boolean,
        orders: Array<Order>
    }
}

interface LoadOrderFailure {
    type: typeof LOAD_ORDER_FAILURE,
    payload: {
        loading: boolean,
        error: string
    }
}

interface ConfirmOrder {
    type: typeof CONFIRM_ORDER,
    payload: {
        id: number
    }
}

export type OrdersActionTypes = 
    | LoadOrderRequest
    | LoadOrderSeccess
    | LoadOrderFailure
    | ConfirmOrder;