import React, { Fragment, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { AppState } from '../../store';
import { ProductsState } from '../../store/Products/types';
import { loadProduct } from '../../store/Products/actions';
import env from 'react-dotenv';

const Products = () => {
    const products = useSelector<AppState>((state) => state.products) as ProductsState;
    const dispatch = useDispatch();
    useEffect(() => {
        dispatch(loadProduct(1));
    }, [dispatch])
    return (
        <Fragment>
            <div className="row">
                <div className="col-6">
                    <h2 className="text-info">Danh sách sản phẩm</h2>
                </div>
                <div className="col-6 text-right">
                    <button className="btn btn-success"><i className="fas fa-plus" />&nbsp; Thêm sản phẩm mới</button>
                </div>
            </div>
            <div className="card-body">
                <div className="table-responsive">
                    <table className="table table-bordered" id="dataTable" width="100%" cellSpacing={0}>
                        <thead>
                            <tr>
                                <th>Hình ảnh</th>
                                <th>Tên sản phẩm</th>
                                <th>Số lượng</th>
                                <th>Giá</th>
                                <th>Ngày tạo</th>
                                <th>Ngày cập nhật</th>
                                <th>Quản lý</th>
                            </tr>
                        </thead>
                        <tbody>
                            {products.items.map((product, index) => {
                                return (
                                    <tr key={index}>
                                        <td>
                                            <img style={{ width: '50px', height: '75px' }} src={env.API_URL + product.productFileImages[0]} alt={product.name} />
                                        </td>
                                        <td>{product.name}</td>
                                        <td>{product.quantities}</td>
                                        <td>{product.price}</td>
                                        <td>{product.createdDate}</td>
                                        <td>{product.updatedDate}</td>
                                        <td>
                                            <button className="btn btn-success mr-1">Cập nhật</button>
                                            <button className="btn btn-danger ml-1">Xóa</button>
                                        </td>
                                    </tr>
                                )
                            })}
                        </tbody>
                    </table>
                </div>
            </div>
        </Fragment>

    )
}

export default Products;

