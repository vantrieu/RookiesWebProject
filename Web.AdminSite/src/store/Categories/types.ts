export const LOAD_CATEGORIES_REQUEST = 'LOAD_CATEGORIES_REQUEST';
export const LOAD_CATEGORIES_SUCCESS = 'LOAD_CATEGORIES_SUCCESS';
export const LOAD_CATEGORIES_FAILURE = 'LOAD_CATEGORIES_FAILURE';

export interface Category {
    id: number;
    name: string;
    description: string;
}

interface LoadCategoriesRequest {
    type: typeof LOAD_CATEGORIES_REQUEST,
    payload: {
        loading: boolean
    }
}

interface LoadCategoriesSuccess {
    type: typeof LOAD_CATEGORIES_SUCCESS,
    payload: {
        loading: boolean,
        categories: Array<Category>
    }
}

interface LoadCategoriesFailure {
    type: typeof LOAD_CATEGORIES_FAILURE,
    payload: {
        loading: boolean,
        error: string
    }
}

export interface CategoriesState {
    categories: Array<Category>;
    loading: boolean;
    error: string | null;
}

export type CategoriesActionTypes =
| LoadCategoriesRequest
| LoadCategoriesSuccess
| LoadCategoriesFailure;