export const SHOW_NOTIFY = 'SHOW_NOTIFY';
export const REFRESH_NOTIFY = 'REFRESH_NOTIFY';

interface ShowNotify {
    type: typeof SHOW_NOTIFY;
    payload: {
        message: string
    };
}

interface RefreshNotify {
    type: typeof REFRESH_NOTIFY;
    payload: {
        message: null
    };
}

export interface NotifyState {
    message: string | null;
}

export type NotifyActionTypes = 
    | ShowNotify
    | RefreshNotify;