import React, { ChangeEvent, FormEvent, Fragment, useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { categoryService } from '../../services/category.service';
import { AppState } from '../../store';
import { loadCategories } from '../../store/Categories/actions';
import { Category } from '../../store/Categories/types';
import { history } from '../../helpers';
import { ShowNotify } from '../../store/Notify/actions';

const EditCategory = (props: any) => {
    const id = props.match.params.id;
    const dispatch = useDispatch();
    const categories = useSelector<AppState>((state) => state.categories.categories) as Array<Category>;

    const [submitted, setSubmitted] = useState(false);

    const GetCategoryById = (id: Number, listCategory: Array<Category>) => {
        return listCategory.find(c => {
            return c.id === id
        });
    }

    const [category] = useState(GetCategoryById(Number(id), categories));

    const [formInput, setFormInput] = useState({
        name: category?.name,
        description: category?.description
    });

    useEffect(() => {
        dispatch(loadCategories());
    }, [dispatch])

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormInput((inputs) => ({ ...inputs, [name]: value }));
    };

    const { name, description } = formInput;
    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setSubmitted(true);
        if (name && description) {
            const result = await categoryService.UpdateCategory(Number(id), name, description);
            if (result === 200) {
                dispatch(ShowNotify('Chỉnh sửa thông tin loại sản phẩm thành công!'));
                history.goBack()
            }
        }
    };

    return (
        <Fragment>
            <h1 className='h3 mb-4 text-gray-800'>Chỉnh sửa loại sản phẩm</h1>
            <div className='card'>
                <div className='card-header'>Thông tin loại sản phẩm</div>
                <div className='card-body'>
                    <form onSubmit={handleSubmit}>
                        <div className='form-group'>
                            <label>Tên loại sản phẩm</label>
                            <div className="row m-2">
                                <input type='text' className={"form-control" + (submitted && !name ? ' is-invalid col-11' : '')} onChange={handleChange} defaultValue={category?.name} name='name' placeholder='Nhập tên loại sản phẩm...' />
                                {submitted && !name && (
                                    <div className='invalid-feedback col-1'>
                                        Tên là bắt buộc
                                    </div>
                                )}
                            </div>
                        </div>
                        <div className='form-group'>
                            <label>Mô tả</label>
                            <div className="row m-2">
                                <input className={"form-control" + (submitted && !name ? ' is-invalid col-11' : '')} onChange={handleChange} defaultValue={category?.description} name='description' placeholder='Nhập mô tả...' />
                                {submitted && !name && (
                                    <div className='invalid-feedback col-1'>
                                        Tên là bắt buộc
                                    </div>
                                )}
                            </div>
                        </div>
                        <div className='form-group'>
                            <button className='btn btn-primary mr-1' type='submit'>
                                <i className="fas fa-save" />
                                &nbsp; Lưu
                            </button>
                            <Link className='btn btn-danger ml-1' to="/categories">
                                <i className="fas fa-window-close" />
                                &nbsp; Hủy
                            </Link>
                        </div>
                    </form>
                </div>
            </div>
        </Fragment>
    )
}

export default EditCategory;