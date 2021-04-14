import { applyMiddleware, combineReducers, compose, createStore } from "redux";
import { accountReducer } from "./Account/reducers";
import thunkMiddleware from "redux-thunk";
import { persistStore, persistReducer } from 'redux-persist';
import storage from 'redux-persist/lib/storage';

const persistConfig = {
    key: 'root',
    storage,
    whitelist: ['account']
}

const rootReducer = combineReducers({
    account: accountReducer
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

export { store, persistedStore };