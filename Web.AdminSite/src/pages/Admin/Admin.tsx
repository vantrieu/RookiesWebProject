import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { getCurrentLoginUser } from '../../store/Account/actions';
import LeftMenu from './LeftMenu';
import TopNavBar from './TopNavBar';
import { BrowserRouter, Route } from 'react-router-dom';
import Home from './Home';
import Users from './Users';
import Products from './Products';
import AddProduct from './AddProduct';
import EditProduct from './EditProduct';
import Categories from './Categories';
import AddCategory from './AddCategory';
import EditCategory from './EditCategory';
import Orders from './Orders';
import OrderDetail from './OrderDetail';

export const Admin = () => {
    const dispatch = useDispatch();
    useEffect(() => {
        dispatch(getCurrentLoginUser());
    }, [dispatch]);

    return (
        <BrowserRouter>
            <LeftMenu />
            <div id="content-wrapper" className="d-flex flex-column">
                <div id="content">
                    <TopNavBar />
                    <div className="container-fluid">
                        <Route exact path='/'>
                            <Home />
                        </Route>
                        <Route exact path='/users'>
                            <Users />
                        </Route>
                        <Route exact path='/products'>
                            <Products />
                        </Route>
                        <Route exact path='/add-product'>
                            <AddProduct />
                        </Route>
                        <Route exact path='/product/:id' component={EditProduct}>
                        </Route>
                        <Route exact path='/categories'>
                            <Categories />
                        </Route>
                        <Route exact path='/add-category'>
                            <AddCategory />
                        </Route>
                        <Route exact path='/category/:id' component={EditCategory}>
                        </Route>
                        <Route exact path='/orders'>
                            <Orders />
                        </Route>
                        <Route exact path='/order-detail/:id' component={OrderDetail}>
                        </Route>
                    </div>
                </div>
                <footer className="sticky-footer bg-white">
                    <div className="container my-auto">
                        <div className="copyright text-center my-auto">
                            <span>Copyright © 2020 by HEAVEN SHOP</span>
                        </div>
                    </div>
                </footer>
            </div>
        </BrowserRouter>
    )
}