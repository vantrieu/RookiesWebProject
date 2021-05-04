import { ChangeEvent, FormEvent, Fragment, useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { AppState } from '../../store';
import { loadCategories } from '../../store/Categories/actions';
import { Category } from '../../store/Categories/types';
import { Product } from '../../store/Products/types';
import { history } from '../../helpers';
import { productService } from '../../services/product.service';
import { ShowNotify } from '../../store/Notify/actions';

const EditProduct = (props: any) => {
    const id = props.match.params.id;
    const dispatch = useDispatch();
    const [selectImages, setSelectimages] = useState(Array<string>());
    const [formData, setFormData] = useState(new FormData());
    const categories = useSelector<AppState>((state) => state.categories.categories) as Array<Category>;
    const products = useSelector<AppState>((state) => state.products.items) as Array<Product>;

    const getProductById = (id: number, listProduct: Array<Product>) => {
        var result = listProduct.find(p => {
            return p.id === id
        });
        return result;
    }

    const [product] = useState(getProductById(Number(id), products) as Product);

    const getCategoryId = () => {
        var temp = categories.find(c => c.name === product?.categoryName);
        return temp ? temp.id : '';
    };

    const [formInput, setFormInput] = useState({
        name: product?.name,
        description: product?.description,
        quantities: product?.quantities,
        price: product?.price,
        categoryId: getCategoryId()
    });

    useEffect(() => {
        dispatch(loadCategories());
        var temp = Array<string>();
        if (product?.productFileImages) {
            product?.productFileImages.map((image) => {
                return temp.push(`${process.env.REACT_APP_API_URL}${image}`)
            })
        }
        setSelectimages(temp);
    }, [dispatch])

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
        console.log(formInput)
        if (name && description && price && quantities && categoryId) {
            const formDataSubmit = formData;
            formDataSubmit.append('name', formInput.name);
            formDataSubmit.append('description', formInput.description);
            formDataSubmit.append('price', formInput.price.toString());
            formDataSubmit.append('quantities', formInput.quantities.toString());
            formDataSubmit.append('categoryId', formInput.categoryId.toString());
            await productService.UpdateProductById(id, formDataSubmit);
            dispatch(ShowNotify('Chỉnh sửa sản phẩm thành công!'));
            history.goBack()
        }

    };

    const renderOption = () => {
        return categories.map((item, index) => {
            return (item.name === product?.categoryName) ?
                (<option key={index} value={item.id} defaultValue={item.id}>{item.name}</option>) :
                (<option key={index} value={item.id}>{item.name}</option>)
        })
    }

    return (
        <Fragment>
            <h1 className='h3 mb-4 text-gray-800'>Chỉnh sửa sản phẩm</h1>
            <div className='card'>
                <div className='card-header'>Thông tin sản phẩm</div>
                <div className='card-body'>
                    <form onSubmit={handleSubmit}>
                        <div className='form-group'>
                            <label>Tên sản phẩm</label>
                            <input type='text' className="form-control" onChange={handleChange} name='name' defaultValue={product?.name} placeholder='Nhập tên sản phẩm...' />
                        </div>
                        <div className='form-group'>
                            <label>Mô tả</label>
                            <input className="form-control" onChange={handleChange} defaultValue={product?.description} name='description' />
                        </div>
                        <div className='form-group row'>
                            <div className='form-group col'>
                                <label>Số lượng</label>
                                <input type='number' onChange={handleChange} defaultValue={product?.quantities} className="form-control" name='quantities' />
                            </div>
                            <div className='form-group col'>
                                <label>Giá</label>
                                <input type='number' onChange={handleChange} defaultValue={product?.price} className="form-control" name='price' />
                            </div>
                            <div className='form-group col'>
                                <label>Loại</label>
                                <select className='col' style={{ height: '50%' }} name="categoryId" onChange={selectHandleChange}>
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

export default EditProduct;
