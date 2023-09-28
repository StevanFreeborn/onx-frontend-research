import { ReactNode } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { ProtectedLayout } from '../components/layouts/ProtectedLayout';
import { useUserContext } from '../hooks/useUserContext';

export default function ProtectedRoute({ children }: { children?: ReactNode }) {
  const { userState } = useUserContext();

  // TODO: Also check if user access token is expired
  // if expired, then try to refresh it
  // if refresh fails, then redirect to login page
  if (userState === null) {
    return <Navigate to="/Public/Login" />;
  }

  return <ProtectedLayout>{children ? children : <Outlet />}</ProtectedLayout>;
}
