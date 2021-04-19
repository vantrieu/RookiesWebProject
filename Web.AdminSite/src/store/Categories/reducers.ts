import { 
    CategoriesActionTypes, 
    CategoriesState, 
    Category, 
    DELETE_CATEGORY, 
    LOAD_CATEGORIES_FAILURE, 
    LOAD_CATEGORIES_REQUEST, 
    LOAD_CATEGORIES_SUCCESS 
} from "./types";


const initialState: CategoriesState = {
    categories: [],
    loading: false,
    error: null
}

const RefreshItems = (categoriesState: CategoriesState, category: Category) => {
    const listCategory = [...categoriesState.categories];
    const index = listCategory?.findIndex(u => u.id === category.id);
    listCategory?.splice(index, 1);
    return listCategory;
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
        case DELETE_CATEGORY: {
            return {
                ...state,
                categories: RefreshItems(state, action.payload.item)
            }
        }
        default:
            return state;
    }
}

export { categoriesReducer };