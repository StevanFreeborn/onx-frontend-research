import {
  Navigate,
  Route,
  Routes,
  createBrowserRouter,
  createRoutesFromElements,
} from 'react-router-dom';
import AnonymousRoute from './routes/AnonymousRoute';
import ProtectedRoute from './routes/ProtectedRoute';
import PublicRoute from './routes/PublicRoute';

export const routes = createRoutesFromElements(
  <Routes>
    <Route path="/Public">
      <Route element={<AnonymousRoute />}>
        <Route index element={<Navigate to="/Public/Login" replace />} />
        <Route path="Login" />
      </Route>
      <Route element={<PublicRoute />}>
        <Route path="ForgotPassword" />
        <Route path="ResetPassword" />
      </Route>
    </Route>
    <Route element={<ProtectedRoute />}>
      <Route path="/" element={<div>Hello</div>}></Route>
    </Route>
  </Routes>
);

export const router = createBrowserRouter(routes);
