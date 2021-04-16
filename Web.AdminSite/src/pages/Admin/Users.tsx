import React, { Fragment, useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { AppState } from '../../store';
import { loadUsers, lockUser, unLockUser } from '../../store/Users/actions';
import { User } from '../../store/Users/types';

const Users = () => {
    const users = useSelector<AppState>((state) => state.user.users) as Array<User>;
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(loadUsers());
    }, [dispatch])

    return (
        <Fragment>
            <div className="card shadow mb-4">
                <div className="card-header py-3">
                    <h6 className="m-0 font-weight-bold text-primary">Danh sách người dùng</h6>
                </div>
                <div className="card-body">
                    <div className="table-responsive">
                        <table className="table table-bordered" id="dataTable" width="100%" cellSpacing={0}>
                            <thead>
                                <tr>
                                    <th>Mã khách hàng</th>
                                    <th>Họ tên</th>
                                    <th>Email</th>
                                    <th>Số điện thoại</th>
                                    <th>Tình trạng tài khoản</th>
                                </tr>
                            </thead>
                            <tbody>
                                {users.map((user, index) => {
                                    return (
                                        <tr key={index}>
                                            <td>{user.userId}</td>
                                            <td>{user.fullName}</td>
                                            <td>{user.email}</td>
                                            <td>{user.phoneNumber}</td>
                                            <td>
                                                {
                                                    (user.lockoutEnd) ?
                                                        <button className="btn btn-danger" onClick={() => dispatch(unLockUser(user.userId))}>Mở khóa</button> :
                                                        <button className="btn btn-success" onClick={() => dispatch(lockUser(user.userId))}>Khóa</button>
                                                }
                                            </td>
                                        </tr>
                                    )
                                })}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </Fragment>
    )
}

export default Users;
