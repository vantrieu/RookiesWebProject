/* eslint-disable jsx-a11y/anchor-is-valid */

import { useState } from "react";
import { Link } from "react-router-dom";

const LeftMenu = () => {
    const [toggled, setToggled] = useState(false);

    return (
        <ul className={"navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" + (toggled ? ' toggled' : '')} id="accordionSidebar">
            <Link className="sidebar-brand d-flex align-items-center justify-content-center" to="/">
                <div className="sidebar-brand-icon rotate-n-15">
                    <i className="fas fa-laugh-wink" />
                </div>
                <div className="sidebar-brand-text mx-3">HEAVEN SHOP</div>
            </Link>
            <hr className="sidebar-divider" />
            <div className="sidebar-heading">
                Quản trị hệ thống
            </div>
            <li className="nav-item">
                <Link className="nav-link" to='/users'>
                    <i className="fas fa-users" />
                    <span>Người dùng</span>
                </Link>
            </li>
            <li className="nav-item">
                <Link className="nav-link" to='/products'>
                    <i className="fas fa-fw fa-table" />
                    <span>Sản phẩm</span>
                </Link>
            </li>
            <li className="nav-item">
                <Link className="nav-link" to='/categories'>
                    <i className="fab fa-typo3" />
                    <span>Loại sản phẩm</span>
                </Link>
            </li>
            <li className="nav-item">
                <Link className="nav-link" to='/orders'>
                    <i className="fab fa-jedi-order" />
                    <span>Đơn hàng</span>
                </Link>
            </li>
            <hr className="sidebar-divider d-none d-md-block" />
            <div className="text-center d-none d-md-inline" onClick={() => setToggled(!toggled)}>
                <button className="rounded-circle border-0" id="sidebarToggle" />
            </div>
        </ul>
    )
}


export default LeftMenu;