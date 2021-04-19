import { useState } from "react";
import { useDispatch, useSelector} from "react-redux";
import { logout } from "../../store/Account/actions";
import { AppState } from '../../store';
import { AuthenticatedUser } from "../../store/Account/types";

const TopNavBar = () => {
    const [isShowProfilemenuDropdown, setIsShowProfilemenuDropdown] = useState(false);
    const dispatch = useDispatch();
    const user = useSelector<AppState>((state) => state.account.user) as AuthenticatedUser;
    return (
        <div>
            <nav className="navbar navbar-expand navbar-light bg-white topbar mb-4 static-top shadow">
                <ul className="navbar-nav ml-auto">
                    {/* Nav Item - User Information */}
                    <li className={"nav-item dropdown no-arrow" + (isShowProfilemenuDropdown ? ' show' : '')}>
                        <label className="nav-link dropdown-toggle" id="userDropdown" role="button" data-toggle="dropdown" aria-haspopup="true"
                            aria-expanded= {isShowProfilemenuDropdown ? 'true' : 'false'} onClick={() => setIsShowProfilemenuDropdown(!isShowProfilemenuDropdown)}>
                            <span className="mr-2 d-none d-lg-inline text-gray-600 small">{user?.fullName}</span>
                            <img className="img-profile rounded-circle" src="/undraw_profile.svg" alt="undraw_profile" />
                        </label>
                        {/* Dropdown - User Information */}
                        <div className={"dropdown-menu dropdown-menu-right shadow animated--grow-in" + (isShowProfilemenuDropdown ? ' show' : '')} aria-labelledby="userDropdown">
                            <label className="dropdown-item" data-toggle="modal" data-target="#logoutModal" onClick={() => dispatch(logout())}>
                                <i className="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-gray-400" />
                                Logout
                            </label>
                        </div>
                    </li>
                </ul>
            </nav>
        </div>
    )
}

export default TopNavBar;