import React, { Fragment, useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { AppState } from '../../store';
import { Order } from '../../store/Orders/types';
import { confirmOrder, loadOrders } from '../../store/Orders/actions';
import { Link } from 'react-router-dom';

const Orders = () => {
    const orders = useSelector<AppState>((state) => state.orders.orders) as Array<Order>;
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(loadOrders());
    }, [dispatch])

    return (
        <Fragment>
            <div className="card shadow mb-4">
                <div className="card-header py-3">
                    <h6 className="m-0 font-weight-bold text-primary">Danh sách đơn hàng</h6>
                </div>
                <div className="card-body">
                    <div className="table-responsive">
                        <table className="table table-bordered" id="dataTable" width="100%" cellSpacing={0}>
                            <thead>
                                <tr>
                                    <th>Mã đơn hàng</th>
                                    <th>Tên khách hàng</th>
                                    <th>Số điện thoại</th>
                                    <th>Ngày đặt hàng</th>
                                    <th>Tình trạng đơn hàng</th>
                                    <th>Chi tiết</th>
                                </tr>
                            </thead>
                            <tbody>
                                {orders.map((item, index) => {
                                    return (
                                        <tr key={index}>
                                            <td>{item.orderId}</td>
                                            <td>{item.fullname}</td>
                                            <td>{item.phoneNumber}</td>
                                            <td>{item.orderDate}</td>
                                            <td>
                                                {
                                                    item.status ?
                                                        <label className='btn btn-primary'>
                                                            <i className="far fa-check-circle" />
                                                            &nbsp; Đã xác nhận
                                                        </label> :
                                                        <button className='btn btn-danger' onClick={() => dispatch(confirmOrder(item.orderId))}>
                                                            <i className="far fa-window-close" />
                                                            &nbsp; Chưa xác nhận
                                                        </button>
                                                }
                                            </td>
                                            <td>
                                                <Link className="btn btn-success mr-1" to={`/order-detail/${item.orderId}`}>
                                                    <i className="fas fa-info-circle" />
                                                    &nbsp; Xem chi tiết
                                                </Link>
                                            </td>
                                        </tr>
                                    )
                                })}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </Fragment>
    )
}

export default Orders;