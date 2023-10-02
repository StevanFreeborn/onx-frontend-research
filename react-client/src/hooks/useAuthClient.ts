import { client } from '../http/client';
import { useUserContext } from './useUserContext';

export function useAuthClient() {
  const { userState } = useUserContext();

  return client({
    authHeader: {
      Authorization: `Bearer ${userState?.token}`,
    },
  });
}
