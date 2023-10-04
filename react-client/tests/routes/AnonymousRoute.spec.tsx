import { render } from '@testing-library/react';
import {
  RouteObject,
  RouterProvider,
  createMemoryRouter,
} from 'react-router-dom';
import { describe, vi } from 'vitest';
import AnonymousRoute from '../../src/routes/AnonymousRoute';

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

describe('AnonymousRoute', () => {
  function Login() {
    return <div>login</div>;
  }

  function Home() {
    return <div>home</div>;
  }

  const testRoutes: RouteObject[] = [
    {
      element: <AnonymousRoute />,
      children: [
        {
          path: '/Login',
          element: <Login />,
        },
      ],
    },
    {
      path: '/',
      element: <Home />,
    },
  ];

  afterEach(() => {
    vi.resetAllMocks();
  });

  it('should render children when user is logged out', () => {
    useUserContextMock.mockReturnValue({
      userState: null,
    });

    const router = createMemoryRouter(testRoutes, {
      initialEntries: ['/Login'],
    });

    const { queryByText } = render(<RouterProvider router={router} />);

    const element = queryByText('login');

    expect(element).toBeInTheDocument();
  });

  it('should redirect to root when user is logged in', () => {
    useUserContextMock.mockReturnValue({
      userState: {
        id: '1',
        token: 'token',
      },
    });

    const router = createMemoryRouter(testRoutes, {
      initialEntries: ['/Login'],
    });

    const { queryByText } = render(<RouterProvider router={router} />);

    const element = queryByText('home');

    expect(element).toBeInTheDocument();
  });
});
