import { applyMiddleware, combineReducers, compose, createStore } from "redux";
import { accountReducer } from "./Account/reducers";
import { productsReducer} from './Products/reducers';
import thunkMiddleware from "redux-thunk";
import { persistStore, persistReducer } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import { setAuthToken } from '../helpers';
import { usersReducer } from './Users/reducers';
import { categoriesReducer } from './Categories/reducers';

const persistConfig = {
    key: 'root',
    storage,
    whitelist: [
      'account',
      'user', 
      'products',
      'categories'
    ]
}

const rootReducer = combineReducers({
    account: accountReducer,
    user: usersReducer,
    products: productsReducer,
    categories: categoriesReducer
});

const persistedReducer = persistReducer(persistConfig, rootReducer)

declare global{
    interface Window {
        __REDUX_DEVTOOLS_EXTENSION_COMPOSE__?: typeof compose;
    }
}

const composeEnhancers = (typeof window !== 'undefined' && window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__) || compose;

export type AppState = ReturnType<typeof rootReducer>;

const configureStore = () => {
    const middlewares = [thunkMiddleware];
    const middlewareEnhancer = applyMiddleware(...middlewares);

    return createStore(persistedReducer, composeEnhancers(middlewareEnhancer));
}

const store = configureStore();
const persistedStore = persistStore(store);

let currentState = store.getState() as AppState;

store.subscribe(() => {
  // keep track of the previous and current state to compare changes
  let previousState = currentState;
  currentState = store.getState() as AppState;
  // if the token changes set the value in localStorage and axios headers
  if (previousState.account.access_token !== currentState.account.access_token) {
    const token = currentState.account.access_token;
    if (token) {
      setAuthToken(token);
    }
  }
});

export { store, persistedStore };