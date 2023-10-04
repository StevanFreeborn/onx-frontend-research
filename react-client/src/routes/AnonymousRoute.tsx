import { ReactNode } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { PublicLayout } from '../components/layouts/PublicLayout';
import { useUserContext } from '../hooks/useUserContext';
import { client } from '../http/client';
import { authService } from '../services/authService';

export default function AnonymousRoute({ children }: { children?: ReactNode }) {
  const { userState } = useUserContext();
  const { refreshAccessToken } = authService(client());

  if (userState !== null) {
    if (userState.expiresAt > Date.now()) {
      return <Navigate to="/" replace />;
    }

    if (userState.expiresAt < Date.now()) {
      refreshAccessToken(userState.id).then(refreshResult => {
        if (refreshResult.isSuccess) {
          return <Navigate to="/" replace />;
        }
      });
    }
  }

  return <PublicLayout>{children ? children : <Outlet />}</PublicLayout>;
}
