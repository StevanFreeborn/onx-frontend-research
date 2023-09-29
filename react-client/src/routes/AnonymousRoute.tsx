import { ReactNode } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { PublicLayout } from '../components/layouts/PublicLayout';
import { useUserContext } from '../hooks/useUserContext';

export default function AnonymousRoute({ children }: { children?: ReactNode }) {
  const { userState } = useUserContext();

  if (userState !== null) {
    // TODO: check if user access token is expired
    // if expired, then try to refresh it
    // if refresh succeeds, then redirect to root
    // if refresh fails, log them out and allow
    // them to continue to the page.
    console.log('here');
    return <Navigate to="/" replace />;
  }

  return <PublicLayout>{children ? children : <Outlet />}</PublicLayout>;
}
