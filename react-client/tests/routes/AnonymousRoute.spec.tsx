import { render } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { describe } from 'vitest';
import { UserContextProvider } from '../../src/context/UserContext';
import AnonymousRoute from '../../src/routes/AnonymousRoute';

describe('AnonymousRoute', () => {
  it('should render without crashing', () => {
    const { getByText } = render(
      <MemoryRouter>
        <UserContextProvider>
          <AnonymousRoute />
        </UserContextProvider>
      </MemoryRouter>
    );

    expect(getByText('Public Layout')).toBeInTheDocument();
  });
});
