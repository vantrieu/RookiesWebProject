import './styles/sb-admin-2.min.css';
import './assets/fontawesome-free/css/all.min.css';
import { Router, Switch } from 'react-router-dom';
import { PrivateRoute } from './components';
import { Login } from './pages/Account';
import { Admin } from './pages/Admin/Admin';
import { AccountRoute } from './components/AccountRoute';
import { history } from './helpers';

function App() {
  return (
    <div className="App" id="wrapper">
      <Router history={history}>
        <Switch>
          <PrivateRoute>
            <AccountRoute  path='/login'>
              <Login />
            </AccountRoute>
            <PrivateRoute  path='/'>
              <Admin />
            </PrivateRoute>
          </PrivateRoute>
        </Switch>
      </Router>
    </div>
  );
}

export default App;
