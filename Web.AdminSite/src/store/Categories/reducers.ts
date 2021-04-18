import { CategoriesActionTypes, CategoriesState, LOAD_CATEGORIES_FAILURE, LOAD_CATEGORIES_REQUEST, LOAD_CATEGORIES_SUCCESS } from "./types";


const initialState: CategoriesState = {
    categories: [],
    loading: false,
    error: null
}

const categoriesReducer = (
    state: CategoriesState = initialState,
    action: CategoriesActionTypes
): CategoriesState => {
    switch (action.type){
        case LOAD_CATEGORIES_REQUEST: {
            return {
                ...state,
                loading: true
            }
        }
        case LOAD_CATEGORIES_SUCCESS: {
            return {
                ...state,
                loading: false,
                categories: action.payload.categories
            }
        }
        case LOAD_CATEGORIES_FAILURE: {
            return {
                ...state,
                loading: false,
                error: action.payload.error
            }
        }
        default:
            return state;
    }
}

export { categoriesReducer };