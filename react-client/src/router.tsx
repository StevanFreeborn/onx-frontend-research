import { Navigate, RouteObject, createBrowserRouter } from 'react-router-dom';
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
          },
        ],
      },
      {
        element: <PublicRoute />,
        children: [
          {
            path: 'ForgotPassword',
          },
          {
            path: 'ResetPassword',
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
