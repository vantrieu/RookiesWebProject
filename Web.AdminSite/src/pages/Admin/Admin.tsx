import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { getCurrentLoginUser } from '../../store/Account/actions';
import LeftMenu from './LeftMenu';
import TopNavBar from './TopNavBar';
import { BrowserRouter, Route } from 'react-router-dom';
import Home from './Home';
import Users from './Users';

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
                    </div>
                </div>
                <footer className="sticky-footer bg-white">
                    <div className="container my-auto">
                        <div className="copyright text-center my-auto">
                            <span>Copyright © Your Website 2020</span>
                        </div>
                    </div>
                </footer>
            </div>
        </BrowserRouter>
    )
}