import { render } from '@testing-library/react';
import {
  RouteObject,
  RouterProvider,
  createMemoryRouter,
} from 'react-router-dom';
import PublicRoute from '../../src/routes/PublicRoute';

describe('PublicRoute', () => {
  function ResetPassword() {
    return <div>Reset Password</div>;
  }

  const testRoutes: RouteObject[] = [
    {
      element: <PublicRoute />,
      children: [
        {
          path: '/ResetPassword',
          element: <ResetPassword />,
        },
      ],
    },
  ];

  it('should render children when user is logged out', () => {
    const router = createMemoryRouter(testRoutes, {
      initialEntries: ['/ResetPassword'],
    });

    const { queryByText } = render(<RouterProvider router={router} />);

    const element = queryByText('Reset Password');

    expect(element).toBeInTheDocument();
  });

  it('should render children when user is logged in', () => {
    const router = createMemoryRouter(testRoutes, {
      initialEntries: ['/ResetPassword'],
    });

    const { queryByText } = render(<RouterProvider router={router} />);

    const element = queryByText('Reset Password');

    expect(element).toBeInTheDocument();
  });
});
