import { productService } from './../../services/product.service';
import { 
    ProductsActionTypes, 
    LOAD_PRODUCTS_REQUEST, 
    LOAD_PRODUCTS_SUCCESS,
    LOAD_PRODUCTS_FAILURE
} from './types';
import { Dispatch } from "react";

export const loadProduct = (pageNumber: number, pageSize: number) => {
    return async (dispatch: Dispatch<ProductsActionTypes>) => {
        dispatch({
            type: LOAD_PRODUCTS_REQUEST,
            payload: {
                loading: true
            }
        });
        try {
            const response = await productService.GetProducts(pageNumber, pageSize);
            dispatch({
                type: LOAD_PRODUCTS_SUCCESS,
                payload: {
                    items: response.items,
                    totalCount: response.totalCount,
                    pageSize: response.pageSize,
                    currentPage: response.currentPage,
                    totalPages: response.totalPages,
                    previousPage: response.previousPage,
                    nextPage: response.nextPage,
                    loading: false,
                    error: null
                }
            });
        } catch (error) {
            dispatch({
                type: LOAD_PRODUCTS_FAILURE,
                payload: {
                    loading: false,
                    error: error.toString()
                }
            });
        }
    }
}