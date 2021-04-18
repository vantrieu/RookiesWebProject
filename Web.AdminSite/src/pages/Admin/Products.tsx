import React, { Fragment, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { AppState } from '../../store';
import { ProductsState } from '../../store/Products/types';
import { loadProduct } from '../../store/Products/actions';
import env from 'react-dotenv';
import { Link } from 'react-router-dom';
import Pagination from "react-js-pagination";

const Products = () => {
    const products = useSelector<AppState>((state) => state.products) as ProductsState;
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(loadProduct(1, 6));
    }, [dispatch])

    const handlePageChange = (pageNumber: number) => {
        dispatch(loadProduct(pageNumber, 6));
    }

    return (
        <Fragment>
            <div className="row">
                <div className="col-6">
                    <h2 className="text-info">Danh sách sản phẩm</h2>
                </div>
                <div className="col-6 text-right">
                    <Link className="btn btn-success" to='/add-product'>
                        <i className="fas fa-plus" />
                        &nbsp; Thêm sản phẩm mới
                    </Link>
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
                                            <Link className="btn btn-success mr-1" to={'/product/'+product.id.toString()}>
                                                <i className="fas fa-edit" />
                                                &nbsp; Cập nhật
                                            </Link>
                                            <button className="btn btn-danger ml-1">
                                                <i className="far fa-trash-alt" />
                                                &nbsp; Xóa
                                            </button>
                                        </td>
                                    </tr>
                                )
                            })}
                        </tbody>
                    </table>
                </div>
                <div style={{ display: 'flex', justifyContent: 'center' }}>
                    <Pagination
                        itemClass="page-item"
                        linkClass="page-link"
                        activePage={products.currentPage}
                        itemsCountPerPage={products.pageSize}
                        totalItemsCount={products.totalCount}
                        pageRangeDisplayed={5}
                        onChange={handlePageChange}
                    />
                </div>
            </div>
        </Fragment>

    )
}

export default Products;

