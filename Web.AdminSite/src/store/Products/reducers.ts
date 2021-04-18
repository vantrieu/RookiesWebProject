import {
    LOAD_PRODUCTS_FAILURE,
    LOAD_PRODUCTS_REQUEST,
    LOAD_PRODUCTS_SUCCESS,
    ProductsActionTypes,
    ProductsState,
    ProductState,
    LOAD_PRODUCT_REQUEST,
    ProductActionTypes,
    LOAD_PRODUCT_SUCCESS,
    LOAD_PRODUCT_FAILURE
} from './types';

const initialState: ProductsState = {
    items: [],
    totalCount: 0,
    pageSize: 0,
    currentPage: 0,
    totalPages: 0,
    previousPage: "No",
    nextPage: "No",
    loading: false,
    error: null
}

const productsReducer = (
    state: ProductsState = initialState,
    action: ProductsActionTypes
): ProductsState => {
    switch (action.type) {
        case LOAD_PRODUCTS_REQUEST: {
            return {
                ...state,
                loading: true
            };
        }
        case LOAD_PRODUCTS_SUCCESS: {
            return {
                ...state,
                items: action.payload.items,
                totalCount: action.payload.totalCount,
                pageSize: action.payload.pageSize,
                currentPage: action.payload.currentPage,
                totalPages: action.payload.totalPages,
                previousPage: action.payload.previousPage,
                nextPage: action.payload.nextPage,
                loading: false,
                error: null
            };
        }
        case LOAD_PRODUCTS_FAILURE: {
            return {
                ...state,
                loading: false,
                error: action.payload.error
            };
        }
        default:
            return state;
    }
}

const initialProductState: ProductState = {
    item: {},
    loading: false,
    error: null
}

const productReducer = (
    state: ProductState = initialProductState,
    action: ProductActionTypes
): ProductState => {
    switch (action.type) {
        case LOAD_PRODUCT_REQUEST: {
            return {
                ...state,
                loading: true
            };
        }
        case LOAD_PRODUCT_SUCCESS: {
            return {
                ...state,
                item: action.payload.item,
                loading: false,
                error: null
            };
        }
        case LOAD_PRODUCT_FAILURE: {
            return {
                ...state,
                loading: false,
                error: action.payload.error
            };
        }
        default:
            return {...state};
    }
}

export { productsReducer, productReducer};