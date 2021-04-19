import { api } from '../helpers';

const GetAllOrder = async (): Promise<any> => {
    return await api.get<any>('/api/v1/Order/get-all').then((response) => {
        return response.data;
    });
}

const ConfirmOrder = async (id: number): Promise<any> => {
    return await api.put<any>(`/api/v1/Order/confirm?orderId=${id}`).then((response) => {
        return response.status;
    });
}

export const orderService = {
    GetAllOrder,
    ConfirmOrder
}