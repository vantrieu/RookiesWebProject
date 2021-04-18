import React, { ChangeEvent, FormEvent, Fragment, useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { Link, Redirect } from 'react-router-dom';
import { history } from '../../helpers';
import { AppState } from '../../store';
import { loadCategories } from '../../store/Categories/actions';
import { Category } from '../../store/Categories/types';
import { productService } from './../../services/product.service'

const AddProduct = () => {
    const categories = useSelector<AppState>((state) => state.categories.categories) as Array<Category>;
    const dispatch = useDispatch();
    const [formSubmitted, setFormSubmitted] = useState(false);
    const [selectImages, setSelectimages] = useState(Array<string>());
    const [formData, setFormData] = useState(new FormData());
    const [formInput, setFormInput] = useState({
        name: '',
        description: '',
        quantities: 0,
        price: 0,
        categoryId: ''
    });

    useEffect(() => {
        dispatch(loadCategories())
    }, [dispatch]);

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormInput((inputs) => ({ ...inputs, [name]: value }));
    };

    const imageHandleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const fileArray = [];
        const formData = new FormData();
        if (e.target.files) {
            for (let i = 0; i < e.target.files.length; i++) {
                fileArray.push(URL.createObjectURL(e.target.files[i]));
                formData.append("images", e.target.files[i], e.target.files[i].name);
            }
            setSelectimages(fileArray);
        }
        setFormData(formData);
    }

    const selectHandleChange = (e: ChangeEvent<HTMLSelectElement>) => {
        setFormInput({
            ...formInput,
            categoryId: e.target.value
        });
    }

    const { name, description, price, quantities, categoryId } = formInput;

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setFormSubmitted(true);
        if (name && description && price && quantities && categoryId) {
            const formDataSubmit = formData;
            formDataSubmit.append('name', formInput.name);
            formDataSubmit.append('description', formInput.description);
            formDataSubmit.append('price', formInput.price.toString());
            formDataSubmit.append('quantities', formInput.quantities.toString());
            formDataSubmit.append('categoryId', formInput.categoryId.toString());
            await productService.CreateProduct(formData);
            history.goBack()
        }

    };

    const renderOption = () => {
        return categories.map((item, index) => {
            return <option key={index} value={item.id}>{item.name}</option>
        })
    }

    return (
        <Fragment>
            <h1 className='h3 mb-4 text-gray-800'>Thêm mới sản phẩm</h1>
            <div className='card'>
                <div className='card-header'>Thông tin sản phẩm</div>
                <div className='card-body'>
                    <form onSubmit={handleSubmit}>
                        <div className='form-group'>
                            <label>Tên sản phẩm</label>
                            <input type='text' className="form-control" onChange={handleChange} name='name' placeholder='Nhập tên sản phẩm...' />
                        </div>
                        <div className='form-group'>
                            <label>Mô tả</label>
                            <input className="form-control" onChange={handleChange} name='description' />
                        </div>
                        <div className='form-group row'>
                            <div className='form-group col'>
                                <label>Số lượng</label>
                                <input type='number' className="form-control" onChange={handleChange} name='quantities' />
                            </div>
                            <div className='form-group col'>
                                <label>Giá</label>
                                <input type='number' className="form-control" onChange={handleChange} name='price' />
                            </div>
                            <div className='form-group col'>
                                <label>Loại</label>
                                <select className='col bodered' style={{ height: '50%' }} name="categoryId" onChange={selectHandleChange}>
                                    {renderOption()}
                                </select>
                            </div>
                        </div>
                        <div className='form-group'>
                            <label>Hình ảnh sản phẩm</label>
                            <input type='file' multiple className="form-control" onChange={imageHandleChange} />
                        </div>
                        <div className='form-group row' style={{ justifyContent: 'center' }}>
                            {selectImages.map((item, index) => {
                                return (
                                    <img className="col-2" key={index} src={item} alt="preview" />
                                )
                            })}
                        </div>
                        <div className='form-group'>
                            <button className='btn btn-primary mr-1' type='submit'>
                                <i className="fas fa-save" />
                                &nbsp; Lưu
                            </button>
                            <Link className='btn btn-danger ml-1' to="/products">
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

export default AddProduct;