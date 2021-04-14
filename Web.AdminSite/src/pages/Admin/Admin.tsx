import { Fragment, useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { getCurrentLoginUser } from '../../store/Account/actions';
import LeftMenu from './LeftMenu';
import TopNavBar from './TopNavBar';

export const Admin = () => {
    const dispatch = useDispatch();
    useEffect(() => {
        dispatch(getCurrentLoginUser());
    }, []);
    
    return (
        <Fragment>
            <LeftMenu />
            <div id="content-wrapper" className="d-flex flex-column">
                <div id="content">
                    <TopNavBar />
                    <div className="container-fluid">
                        <h1>Render SPA!</h1>
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
        </Fragment>
    )
}