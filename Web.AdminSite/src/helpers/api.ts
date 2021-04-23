import axios from 'axios';
import { history } from './history';
import { logout } from '../store/Account/actions';
import { store } from '../store';

const api = axios.create({
  baseURL: `${process.env.REACT_APP_API_URL}`,
  // headers: {
  //   'Content-Type': 'application/json',
  // },
});

/**
 intercept any error responses from the api
 and check if the token is no longer valid.
 ie. Token has expired or user is no longer
 authenticated.
 logout the user if the token has expired
**/
api.interceptors.response.use(
  (res) => res,
  (err) => {
    if (err.response.status === 401) {
      store.dispatch(logout());
      history.push('/');
    }
    return Promise.reject(err);
  }
);

export { api };