import React, { ChangeEvent, FormEvent, useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { AppState } from '../../store';
import { useLocation } from 'react-router';
import { login, logout } from '../../store/Account/actions';

export const Login = () => {
    const [inputs, setInputs] = useState({
        username: '',
        password: '',
    });
    const [submitted, setSubmitted] = useState(false);

    const loading = useSelector<AppState>((state) => state.account.loading);

    const { username , password } = inputs;

    const dispatch = useDispatch();
    const location = useLocation();

    useEffect(() => {
        dispatch(logout());
    }, [dispatch]);

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setInputs((inputs) => ({ ...inputs, [name]: value }));
    };

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setSubmitted(true);
        if (username && password) {
            const { from }: any = location.state || { from: { pathname: '/' } };
            dispatch(login(username, password, from));
        }
    };

    return (
        <div className='container'>
            {/* Outer Row */}
            <div className='row justify-content-center'>
                <div className='col-xl-10 col-lg-12 col-md-9'>
                    <div className='card o-hidden border-0 shadow-lg my-5'>
                        <div className='card-body p-0'>
                            {/* Nested Row within Card Body */}
                            <div className='row'>
                                <div className='col-lg-6 d-none d-lg-block bg-login-image' />
                                <div className='col-lg-6'>
                                    <div className='p-5'>
                                        <div className='text-center'>
                                            <h1 className='h4 text-gray-900 mb-4'>Welcome Back!</h1>
                                        </div>
                                        <form className='user' onSubmit={handleSubmit}>
                                            <div className='form-group'>
                                                <input
                                                    type='text'
                                                    className={
                                                        'form-control form-control-user ' +
                                                        (submitted && !username ? 'is-invalid' : '')
                                                    }
                                                    id='exampleInputEmail'
                                                    aria-describedby='emailHelp'
                                                    onChange={handleChange}
                                                    placeholder='Enter Username...'
                                                    name='username'
                                                />
                                                {submitted && !username && (
                                                    <div className='invalid-feedback' style={{display: "flex", justifyContent: "center"}}>
                                                        Email is required
                                                    </div>
                                                )}
                                            </div>
                                            <div className='form-group'>
                                                <input
                                                    type='password'
                                                    className={
                                                        'form-control form-control-user ' +
                                                        (submitted && !username ? 'is-invalid' : '')
                                                    }
                                                    id='exampleInputPassword'
                                                    placeholder='Password'
                                                    onChange={handleChange}
                                                    name='password'
                                                    autoComplete="on"
                                                />
                                                {submitted && !password && (
                                                    <div className='invalid-feedback' style={{display: "flex", justifyContent: "center"}}>
                                                        Password is required
                                                    </div>
                                                )}
                                            </div>
                                            <div className='form-group' style={{display: "flex", justifyContent: "center"}}>
                                                <button className='btn btn-primary'>
                                                    {loading && (
                                                        <span className='spinner-border spinner-border-sm mr-1'></span>
                                                    )}
                          Login
                        </button>
                                            </div>
                                        </form>
                                        <hr />
                                        <div className='text-center'>
                                            <a className='small' href='forgot-password.html'>
                                                Forgot Password?
                      </a>
                                        </div>
                                        <div className='text-center'>
                                            <a className='small' href='register.html'>
                                                Create an Account!
                      </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};