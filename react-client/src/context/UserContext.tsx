import { Dispatch, createContext, useReducer } from 'react';
const USER_KEY = 'onxAuth';

export type User = {
  id: string;
  expiresAt: number;
  token: string;
};

type UserAction =
  | {
      type: 'LOGIN';
      payload: User;
    }
  | {
      type: 'LOGOUT';
    };

type UserContextType = {
  userState: User | null;
  dispatchUserAction: Dispatch<UserAction>;
};

type UserContextProviderProps = {
  children: React.ReactNode;
  reducer?: (state: User | null, action: UserAction) => User | null;
  initialState?: User | null;
};

function getUserFromLocalStorage(): User | null {
  const user = localStorage.getItem(USER_KEY);
  return user === null ? null : JSON.parse(user);
}

function userReducer(state: User | null, action: UserAction) {
  switch (action.type) {
    case 'LOGIN':
      localStorage.setItem(USER_KEY, JSON.stringify(action.payload));
      return action.payload;
    case 'LOGOUT':
      localStorage.removeItem(USER_KEY);
      return null;
    default:
      return state;
  }
}

export const UserContext = createContext<UserContextType | undefined>(
  undefined
);

export function UserContextProvider({
  children,
  reducer = userReducer,
  initialState = getUserFromLocalStorage(),
}: UserContextProviderProps) {
  const [userState, dispatchUserAction] = useReducer(reducer, initialState);

  return (
    <UserContext.Provider value={{ userState, dispatchUserAction }}>
      {children}
    </UserContext.Provider>
  );
}
