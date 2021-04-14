import { useSelector } from 'react-redux';
import { Route, RouteProps } from 'react-router-dom';
import { Redirect } from 'react-router';
import { Login } from '../pages/Account/Login';
import { AppState } from '../store';
import { AccountState } from '../store/Account/types';

export const AccountRoute = ({ children, ...rest }: RouteProps): JSX.Element => {
    const account: AccountState = useSelector((state: AppState) => state.account);
    return (
        <Route
          {...rest}
          render={() =>
            account.access_token ? (
              <Redirect to={{ pathname: '/admin/home' }} />
            ) : (
              <Login />
            )
          }
        ></Route>
      );
}
