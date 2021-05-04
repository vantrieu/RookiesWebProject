import { 
    NotifyActionTypes, 
    NotifyState, 
    REFRESH_NOTIFY, 
    SHOW_NOTIFY
} from "./types";

const initialState: NotifyState = {
    message: null
}

const notifyReducer = (
    state: NotifyState = initialState,
    action: NotifyActionTypes
): NotifyState => {
    switch (action.type) {
        case REFRESH_NOTIFY: {
            return {
                message: action.payload.message
            }
        }
        case SHOW_NOTIFY: {
            return {
                message: action.payload.message
            }
        }
        default:
            return state;
    }
}

export { notifyReducer };