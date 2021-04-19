import {
    DELETE_PRODUCT,
    LOAD_PRODUCTS_FAILURE,
    LOAD_PRODUCTS_REQUEST,
    LOAD_PRODUCTS_SUCCESS,
    ProductsActionTypes,
    ProductsState,
    Product
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

const RefreshItems = (productsState: ProductsState, product: Product) => {
    const lstProduct = [...productsState.items];
    const index = lstProduct?.findIndex(u => u.id === product.id);
    lstProduct?.splice(index, 1);
    return lstProduct;
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
        case DELETE_PRODUCT: {
            return {
                ...state,
                items: RefreshItems(state, action.payload.item)
            }
        }
        default:
            return state;
    }
}

export { productsReducer };