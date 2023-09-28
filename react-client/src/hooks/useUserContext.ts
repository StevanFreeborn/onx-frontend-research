import { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { User, UserContext } from '../context/UserContext';

export function useUserContext() {
  const userContext = useContext(UserContext);
  const navigate = useNavigate();

  if (userContext === undefined) {
    throw new Error('useUserContext must be used within a UserContextProvider');
  }

  const { userState, dispatchUserAction } = userContext;

  function logOut() {
    dispatchUserAction({ type: 'LOGOUT' });
    navigate('/Public/Login');
  }

  function logIn(user: User) {
    dispatchUserAction({ type: 'LOGIN', payload: user });
  }

  return { userState, logOut, logIn };
}
