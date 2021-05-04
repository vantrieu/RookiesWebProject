import { ChangeEvent, FormEvent, Fragment, useState } from 'react'
import { useDispatch } from 'react-redux';
import { Link } from 'react-router-dom';
import { history } from '../../helpers';
import { categoryService } from '../../services/category.service';
import { ShowNotify } from '../../store/Notify/actions';

const AddCategory = () => {
    const [formInput, setFormInput] = useState({
        name: '',
        description: ''
    });

    const [submitted, setSubmitted] = useState(false);
    const dispatch = useDispatch();

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormInput((inputs) => ({ ...inputs, [name]: value }));
    };

    const { name, description } = formInput;

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setSubmitted(true);
        if (name && description) {
            await categoryService.CreateCategory(name, description);
            dispatch(ShowNotify('Thêm mới loại sản phẩm thành công!'));
            history.goBack();
        }
    };

    return (
        <Fragment>
            <h1 className='h3 mb-4 text-gray-800'>Thêm mới loại sản phẩm</h1>
            <div className='card'>
                <div className='card-header'>Thông tin loại sản phẩm</div>
                <div className='card-body'>
                    <form onSubmit={handleSubmit}>
                        <div className='form-group'>
                            <label>Tên loại sản phẩm</label>
                            <div className="row m-2">
                                <input type='text' className={"form-control" + (submitted && !name ? ' is-invalid col-11' : '')} onChange={handleChange} name='name' placeholder='Nhập tên loại sản phẩm...' />
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
                                <input className={"form-control" + (submitted && !description ? ' is-invalid col-11' : '')} onChange={handleChange} name='description' placeholder='Nhập mô tả...' />
                                {submitted && !description && (
                                    <div className='invalid-feedback col-1' >
                                        Mô tả là bắt buộc
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

export default AddCategory;