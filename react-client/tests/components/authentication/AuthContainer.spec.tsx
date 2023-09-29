import { render } from '@testing-library/react';
import AuthContainer from '../../../src/components/authentication/AuthContainer';

describe('AuthContainer', () => {
  it('should render with children', () => {
    const Child = () => <div>Child</div>;
    const { getByTestId, getByText, getByAltText } = render(
      <AuthContainer>
        <Child />
      </AuthContainer>
    );

    const layout = getByTestId('authContainer');
    const logo = getByAltText('company logo');
    const copyRight = getByText('Copyright', {
      exact: false,
    });
    const patent = getByText('U.S. Patent No', {
      exact: false,
    });
    const child = getByText('Child');

    expect(layout).toBeInTheDocument();
    expect(logo).toBeInTheDocument();
    expect(copyRight).toBeInTheDocument();
    expect(patent).toBeInTheDocument();
    expect(child).toBeInTheDocument();
  });
});
