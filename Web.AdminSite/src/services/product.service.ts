import { api } from '../helpers';

const GetProducts = async (pageNumber: number, pageSize: number): Promise<any> => {
    return await api.get<any>(`/api/v1/Product?pageNumber=${pageNumber}&pageSize=${pageSize}`).then((response) => {
        return response.data;
    });
}

const CreateProduct = async (data: FormData): Promise<any> => {
    return await api.post<any>('/api/v1/Product', data).then((response) => {
        return response.data;
    });
}

const GetProductById = async (id: number): Promise<any> => {
    return await api.get<any>(`/api/v1/Product/${id}`).then((response) => {
        return response.data;
    });
}

const UpdateProductById = async (id: number,data: FormData): Promise<any> => {
    return await api.put<any>(`/api/v1/Product/${id}`, data).then((response) => {
        console.log(response.status)
        return response.data;
    });
}

export const productService = {
    GetProducts,
    CreateProduct,
    GetProductById,
    UpdateProductById
}