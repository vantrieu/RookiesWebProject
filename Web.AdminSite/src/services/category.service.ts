import { api } from '../helpers';

const GetCategories = async (): Promise<any> => {
    return await api.get('/api/v1/Category').then((response) => {
        return response.data;
    })
}

export const categoryService = {
    GetCategories
}