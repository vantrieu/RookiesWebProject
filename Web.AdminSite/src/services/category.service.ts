import { api } from '../helpers';

const GetCategories = async (): Promise<any> => {
    return await api.get('/api/v1/Category').then((response) => {
        return response.data;
    })
}

const CreateCategory = async (name: string, des: string): Promise<any> => {
    return await api.post('/api/v1/Category', {
        name: name,
        description: des
    }).then((response) => {
        return response.data;
    })
}

const UpdateCategory = async (id: number, name: string, des: string): Promise<any> => {
    return await api.put(`/api/v1/Category/id=${id}`, {
        name: name,
        description: des
    }).then((response) => {
        return response.status;
    });
}

const DeleteCategory = async (id: number): Promise<any> => {
    return await api.delete(`/api/v1/Category/id=${id}`).then((response) => {
        return response.data;
    });
}

export const categoryService = {
    GetCategories,
    CreateCategory,
    UpdateCategory,
    DeleteCategory
}