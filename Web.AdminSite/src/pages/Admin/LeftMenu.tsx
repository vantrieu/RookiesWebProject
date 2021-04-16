/* eslint-disable jsx-a11y/anchor-is-valid */

import { Link } from "react-router-dom";

const LeftMenu = () => {
    return (
        <ul className="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" id="accordionSidebar">
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
                    <i className="fas fa-fw fa-chart-area" />
                    <span>Người dùng</span>
                </Link>
            </li>
            <li className="nav-item">
                <a className="nav-link" href="tables.html">
                    <i className="fas fa-fw fa-table" />
                    <span>Tables</span>
                </a>
            </li>
            <hr className="sidebar-divider d-none d-md-block" />
            <div className="text-center d-none d-md-inline">
                <button className="rounded-circle border-0" id="sidebarToggle" />
            </div>
        </ul>
    )
}


export default LeftMenu;