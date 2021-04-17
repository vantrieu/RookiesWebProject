import React, { ChangeEvent, Fragment, useState } from 'react'
import { Link } from 'react-router-dom';

const AddProduct = () => {
    const [formInput, setFormInput] = useState({
        name: '',
        description: '',
        quantities: 0,
        price: 0,
        categoryId: '',
        images: []
    });

    const [formSubmitted, setFormSubmitted] = useState(false);
    const [selectImages, setSelectimages] = useState(Array<string>());
    const { name, description, quantities, price, categoryId, images } = formInput;

    const imageHandleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const fileArray = [];
        if (e.target.files) {
            for (let i = 0; i < e.target.files.length; i++) {
                fileArray.push(URL.createObjectURL(e.target.files[i]));
            }
            setSelectimages(fileArray)
            console.log(fileArray);
        }
    }

    return (
        <Fragment>
            <h1 className='h3 mb-4 text-gray-800'>Thêm mới sản phẩm</h1>
            <div className='card'>
                <div className='card-header'>Thông tin sản phẩm</div>
                <div className='card-body'>
                    <form>
                        <div className='form-group'>
                            <label>Tên sản phẩm</label>
                            <input type='text' className="form-control" name='name' placeholder='Nhập tên sản phẩm...' />
                        </div>
                        <div className='form-group'>
                            <label>Mô tả</label>
                            <textarea className="form-control" name='description' />
                        </div>
                        <div className='form-group row'>
                            <div className='form-group col'>
                                <label>Số lượng</label>
                                <input type='number' className="form-control" name='quantities' />
                            </div>
                            <div className='form-group col'>
                                <label>Giá</label>
                                <input type='number' className="form-control" name='last_name' />
                            </div>
                            <div className='form-group col'>
                                <label>Loại sản phẩm</label>
                                <input type='number' className="form-control" name='last_name' />
                            </div>
                        </div>
                        <div className='form-group'>
                            <label>Hình ảnh sản phẩm</label>
                            <input type='file' multiple className="form-control" onChange={imageHandleChange} />
                        </div>
                        <div className='form-group row' style={{ justifyContent: 'center' }}>
                            {selectImages.map((image, index) => {
                                return (
                                    <img className="col-2" key={index} src={image} alt="image preview" />
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