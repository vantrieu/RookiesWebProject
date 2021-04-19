import { categoryService } from './../../services/category.service';
import { Dispatch } from "react";
import { 
    CategoriesActionTypes, 
    DELETE_CATEGORY, 
    LOAD_CATEGORIES_FAILURE, 
    LOAD_CATEGORIES_REQUEST,
    LOAD_CATEGORIES_SUCCESS
} from "./types";

export const loadCategories = () => {
    return async (dispatch: Dispatch<CategoriesActionTypes>) => {
        dispatch({
            type: LOAD_CATEGORIES_REQUEST,
            payload: {
                loading: true
            }
        });
        try {
            const response = await categoryService.GetCategories();
            dispatch({
                type: LOAD_CATEGORIES_SUCCESS,
                payload: {
                    loading: false,
                    categories: response
                }
            });
        } catch (error) {
            dispatch({
                type: LOAD_CATEGORIES_FAILURE,
                payload: {
                    loading: false,
                    error: error.toString()
                }
            });
        }
    }
}

export const deleteCategory = (id: number) => {
    return async (dispatch: Dispatch<CategoriesActionTypes>) => {
        const response = await categoryService.DeleteCategory(id);
        dispatch({
            type: DELETE_CATEGORY,
            payload: {
                item: response
            }
        });
    }
}