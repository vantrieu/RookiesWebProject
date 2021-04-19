import React, { Fragment, useState } from 'react'
import { useSelector } from 'react-redux';
import { AppState } from '../../store';
import { Order } from '../../store/Orders/types';

const OrderDetail = (props: any) => {
    const id = props.match.params.id;
    const orders = useSelector<AppState>((state) => state.orders.orders) as Array<Order>;

    const getOrderById = (id: number, listOrder: Array<Order>) => {
        var result = listOrder.find(o => {
            return o.orderId === id
        });
        return result;
    }

    const [order] = useState(getOrderById(Number(id), orders));

    const handleTotal = () => {
        let total = 0;
        order?.products.forEach( item => {
            total = total + (item.total * item.price);
        })
        return total.toString();
    }

    return (
        <Fragment>
            <div className="card shadow mb-4">
                <div className="card-header py-3">
                    <h6 className="m-0 font-weight-bold text-primary">Chi tiết đơn hàng - {order?.orderId}</h6>
                </div>
                <div className="card-body">
                    <div className="table-responsive">
                        <table className="table table-bordered" id="dataTable" width="100%" cellSpacing={0}>
                            <thead>
                                <tr>
                                    <th>STT</th>
                                    <th>Mã sản phẩm</th>
                                    <th>Tên sản phẩm</th>
                                    <th>Số lượng</th>
                                    <th>Giá</th>
                                    <th>Thành tiền</th>
                                </tr>
                            </thead>
                            <tbody>
                                {order?.products.map((item, index) => {
                                    return (
                                        <tr key={index}>
                                            <td>{index + 1}</td>
                                            <td>{item.productId}</td>
                                            <td>{item.productName}</td>
                                            <td>{item.total}</td>
                                            <td>{item.price}</td>
                                            <td>{item.total * item.price}</td>
                                        </tr>
                                    )
                                })}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div style={{float: 'right'}}>
                <h4><b>Thành tiền: {handleTotal()} VNĐ</b></h4>
            </div>
        </Fragment>
    )
}

export default OrderDetail;