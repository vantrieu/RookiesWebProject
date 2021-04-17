export const LOAD_PRODUCTS_REQUEST = 'LOAD_PRODUCTS_REQUEST';
export const LOAD_PRODUCTS_SUCCESS = 'LOAD_PRODUCTS_SUCCESS';
export const LOAD_PRODUCTS_FAILURE = 'LOAD_PRODUCTS_FAILURE';

export interface Product {
    id: number,
    name: string,
    description: string,
    quantities: number,
    price: number,
    createdDate: Date,
    updatedDate: Date,
    categoryName: string,
    rates: Array<number>,
    productFileImages: Array<string>
}

export interface ProductsState {
    items: Array<Product>,
    totalCount: number,
    pageSize: number,
    currentPage: number,
    totalPages: number,
    previousPage: string,
    nextPage: string,
    loading: boolean;
    error: string | null;
}

export interface LoadProductsRequest {
    type: typeof LOAD_PRODUCTS_REQUEST,
    payload: {
        loading: boolean;
    }
}

export interface LoadProductsSuccess {
    type: typeof LOAD_PRODUCTS_SUCCESS,
    payload: {
        items: Array<Product>;
        totalCount: number;
        pageSize: number;
        currentPage: number;
        totalPages: number;
        previousPage: string;
        nextPage: string;
        loading: boolean;
        error: string | null;
    }
}

export interface LoadProductsFailure {
    type: typeof LOAD_PRODUCTS_FAILURE,
    payload: {
        loading: boolean,
        error: string;
    }
}

export type ProductsActionTypes = 
    | LoadProductsFailure
    | LoadProductsRequest
    | LoadProductsSuccess;