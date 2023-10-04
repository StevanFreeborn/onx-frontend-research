import { ReactNode } from 'react';
import { Outlet } from 'react-router-dom';
import { PublicLayout } from '../components/layouts/PublicLayout';

export default function PublicRoute({ children }: { children?: ReactNode }) {
  return <PublicLayout>{children ? children : <Outlet />}</PublicLayout>;
}
