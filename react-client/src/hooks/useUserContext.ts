import jwtDecode from 'jwt-decode';
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

  function logUserOut() {
    dispatchUserAction({ type: 'LOGOUT' });
    navigate('/Public/Login');
  }

  function logUserIn(jwtToken: string) {
    const { sub } = jwtDecode<{ sub: string }>(jwtToken);
    const user: User = { id: sub, token: jwtToken };
    dispatchUserAction({ type: 'LOGIN', payload: user });
  }

  return { userState, logUserOut, logUserIn };
}
