import { client } from '../http/client';
import { useUserContext } from './useUserContext';

export function useAuthClient() {
  const { userState, refreshAccessToken } = useUserContext();

  return client({
    authHeader: {
      Authorization: `Bearer ${userState?.token}`,
    },
    unauthorizedResponseHandler: refreshAccessToken,
  });
}
