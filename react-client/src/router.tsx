import { Navigate, RouteObject, createBrowserRouter } from 'react-router-dom';
import NotFound from './components/navigation/errors/NotFound';
import PublicNotFound from './components/navigation/errors/PublicNotFound';
import ContentPage from './pages/protected/ContentPage';
import DashboardPage from './pages/protected/DashboardPage';
import ReportsPage from './pages/protected/ReportsPage';
import ForgotPasswordPage from './pages/public/ForgotPasswordPage';
import LoginPage from './pages/public/LoginPage';
import ResetPasswordPage from './pages/public/ResetPasswordPage';
import AnonymousRoute from './routes/AnonymousRoute';
import ProtectedRoute from './routes/ProtectedRoute';
import PublicRoute from './routes/PublicRoute';

export const routes: RouteObject[] = [
  {
    path: '/Public',
    children: [
      {
        element: <AnonymousRoute />,
        children: [
          {
            index: true,
            element: <Navigate to="/Public/Login" replace />,
          },
          {
            path: 'Login',
            element: <LoginPage />,
          },
          {
            path: '*',
            element: <PublicNotFound />,
          },
        ],
      },
      {
        element: <PublicRoute />,
        children: [
          {
            path: 'ForgotPassword',
            element: <ForgotPasswordPage />,
          },
          {
            path: 'ResetPassword',
            element: <ResetPasswordPage />,
          },
          {
            path: '*',
            element: <PublicNotFound />,
          },
        ],
      },
    ],
  },
  {
    element: <ProtectedRoute />,
    children: [
      {
        index: true,
        element: <Navigate to="/Dashboard" replace />,
      },
      {
        path: 'Dashboard',
        element: <DashboardPage />,
      },
      {
        path: 'Content',
        element: <ContentPage />,
      },
      {
        path: 'Report',
        element: <ReportsPage />,
      },
      {
        path: '*',
        element: <NotFound />,
      },
    ],
  },
];

export const router = createBrowserRouter(routes);
