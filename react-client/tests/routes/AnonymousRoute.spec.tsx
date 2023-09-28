import { render } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { describe } from 'vitest';
import { UserContextProvider } from '../../src/context/UserContext';
import AnonymousRoute from '../../src/routes/AnonymousRoute';

describe('AnonymousRoute', () => {
  it('should render without crashing', () => {
    const { getByText } = render(
      <UserContextProvider>
        <MemoryRouter>
          <AnonymousRoute />
        </MemoryRouter>
      </UserContextProvider>
    );

    expect(getByText('Public Layout')).toBeInTheDocument();
  });

  it('should render without crashing', () => {
    const { queryByText } = render(
      <UserContextProvider initialState={{ id: '', token: '' }}>
        <MemoryRouter>
          <AnonymousRoute />
        </MemoryRouter>
      </UserContextProvider>
    );

    const element = queryByText('Public Layout');

    expect(element).not.toBeInTheDocument();
  });
});
