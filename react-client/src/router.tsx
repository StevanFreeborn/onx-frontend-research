import { Navigate, RouteObject, createBrowserRouter } from 'react-router-dom';
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
        ],
      },
    ],
  },
  {
    element: <ProtectedRoute />,
    children: [
      {
        path: '/',
        element: <div>Hello</div>,
      },
    ],
  },
];

export const router = createBrowserRouter(routes);
