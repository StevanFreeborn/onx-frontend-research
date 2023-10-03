import jwtDecode from 'jwt-decode';
import { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { User, UserContext } from '../context/UserContext';
import { client } from '../http/client';
import { authService } from '../services/authService';

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

  async function refreshAccessToken(originalRequest: Request) {
    const { refreshAccessToken } = authService(client());
    const unauthorizedResponse = new Response(null, { status: 401 });

    if (userState === null) {
      logUserOut();
      return unauthorizedResponse;
    }

    const refreshResult = await refreshAccessToken(userState.id);

    if (refreshResult.isFailed) {
      logUserOut();
      return unauthorizedResponse;
    }

    logUserIn(refreshResult.value.token);

    originalRequest.headers.set(
      'Authorization',
      `Bearer ${refreshResult.value.token}`
    );

    return await fetch(originalRequest);
  }

  return { userState, logUserOut, logUserIn, refreshAccessToken };
}
