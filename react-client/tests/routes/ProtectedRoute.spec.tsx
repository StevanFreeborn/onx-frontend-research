import { render } from '@testing-library/react';
import {
  RouteObject,
  RouterProvider,
  createMemoryRouter,
} from 'react-router-dom';
import { vi } from 'vitest';
import ProtectedRoute from '../../src/routes/ProtectedRoute';

const { useUserContextMock } = vi.hoisted(() => {
  return {
    useUserContextMock: vi.fn(),
  };
});

vi.mock('../../src/hooks/useUserContext', () => {
  return {
    useUserContext: useUserContextMock,
  };
});

describe('ProtectedRoute', () => {
  function Login() {
    return <div>login</div>;
  }

  function Home() {
    return <div>home</div>;
  }

  const testRoutes: RouteObject[] = [
    {
      element: <ProtectedRoute />,
      children: [
        {
          path: '/',
          element: <Home />,
        },
      ],
    },
    {
      path: '/Public/Login',
      element: <Login />,
    },
  ];

  afterEach(() => {
    vi.resetAllMocks();
  });

  it('should render children when user is logged in', () => {
    useUserContextMock.mockReturnValue({
      userState: {
        id: 1,
        token: 'token',
      },
    });

    const router = createMemoryRouter(testRoutes, {
      initialEntries: ['/'],
    });

    const { queryByText } = render(<RouterProvider router={router} />);

    const element = queryByText('home');

    expect(element).toBeInTheDocument();
  });

  it('should redirect to login when user is logged out', () => {
    useUserContextMock.mockReturnValue({
      userState: null,
    });

    const router = createMemoryRouter(testRoutes, {
      initialEntries: ['/'],
    });

    const { queryByText } = render(<RouterProvider router={router} />);

    const element = queryByText('login');

    expect(element).toBeInTheDocument();
  });
});
