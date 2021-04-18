import { api } from '../helpers';

const GetProducts = async (pageNumber: number | null): Promise<any> => {
    pageNumber = pageNumber ? pageNumber : 1;
    return await api.get<any>(`/api/v1/Product?pageNumber=${pageNumber}`).then((response) => {
        return response.data;
    });
}

const CreateProduct = async (data: FormData): Promise<any> => {
    return await api.post<any>('/api/v1/Product', data).then((response) => {
        return response.data;
    });
}

export const productService = {
    GetProducts,
    CreateProduct
}