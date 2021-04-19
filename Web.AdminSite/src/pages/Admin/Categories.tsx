import { Fragment, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { AppState } from '../../store';
import { deleteCategory, loadCategories } from '../../store/Categories/actions';
import { Category } from '../../store/Categories/types';
import { confirmAlert } from 'react-confirm-alert'; // Import
import 'react-confirm-alert/src/react-confirm-alert.css';

const Categories = () => {
    const categories = useSelector<AppState>((state) => state.categories.categories) as Array<Category>;
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(loadCategories())
    }, [dispatch])

    const submit = (category: Category) => {
        confirmAlert({
            title: 'Cảnh báo!',
            message: `Bạn muốn xóa loại sản phẩm: ${category.name}`,
            buttons: [
                {
                    label: 'Xóa',
                    onClick: () => dispatch(deleteCategory(category.id))
                },
                {
                    label: 'Hủy',
                    onClick: () => {}
                }
            ]
        });
    };

    return (
        <Fragment>
            <div className="row">
                <div className="col-6">
                </div>
                <div className="col-6 text-right mb-2">
                    <Link className="btn btn-success" to='/add-category'>
                        <i className="fas fa-plus" />
                        &nbsp; Thêm mới loại sản phẩm
                    </Link>
                </div>
            </div>
            <div className="card shadow mb-4">
                <div className="card-header py-3">
                    <h6 className="m-0 font-weight-bold text-primary">Danh sách loại sản phẩm</h6>
                </div>
                <div className="card-body">
                    <div className="table-responsive">
                        <table className="table table-bordered" id="dataTable" width="100%" cellSpacing={0}>
                            <thead>
                                <tr>
                                    <th>Mã loại sản phẩm</th>
                                    <th>Tên loại sản phẩm</th>
                                    <th>Mô tả</th>
                                    <th>Quản lý</th>
                                </tr>
                            </thead>
                            <tbody>
                                {categories.map((item, index) => {
                                    return (
                                        <tr key={index}>
                                            <td>{item.id}</td>
                                            <td>{item.name}</td>
                                            <td>{item.description}</td>
                                            <td>
                                                <Link className="btn btn-success mr-1" to={'/category/' + item.id.toString()}>
                                                    <i className="fas fa-edit" />
                                                    &nbsp; Cập nhật
                                                </Link>
                                                <button className="btn btn-danger ml-1" onClick={() => submit(item)}>
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
                </div>
            </div>
        </Fragment>
    )
}

export default Categories;