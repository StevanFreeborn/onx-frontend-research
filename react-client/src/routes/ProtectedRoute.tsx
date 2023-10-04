import { ReactNode } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { ProtectedLayout } from '../components/layouts/ProtectedLayout';
import { useUserContext } from '../hooks/useUserContext';
import { client } from '../http/client';
import { authService } from '../services/authService';

export default function ProtectedRoute({ children }: { children?: ReactNode }) {
  const { userState } = useUserContext();
  const { refreshAccessToken } = authService(client());

  if (userState === null) {
    return <Navigate to="/Public/Login" />;
  }

  if (userState.expiresAt < Date.now()) {
    refreshAccessToken(userState.id).then(refreshResult => {
      if (refreshResult.isFailed) {
        return <Navigate to="/Public/Login" />;
      }
    });
  }

  return <ProtectedLayout>{children ? children : <Outlet />}</ProtectedLayout>;
}
