import { Dispatch } from "redux";
import { 
    NotifyActionTypes, 
    REFRESH_NOTIFY, 
    SHOW_NOTIFY
} from "./types";

export const ShowNotify = (message: string) => {
    return async (dispatch: Dispatch<NotifyActionTypes>) => {
        dispatch ({
            type: REFRESH_NOTIFY,
            payload: {
                message: null
            }
        });

        dispatch({
            type: SHOW_NOTIFY,
            payload: {
                message: message
            }
        });
    }
}