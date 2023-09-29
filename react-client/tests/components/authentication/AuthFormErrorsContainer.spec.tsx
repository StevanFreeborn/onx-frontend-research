import { render } from '@testing-library/react';
import AuthFormErrorsContainer from '../../../src/components/authentication/AuthFormErrorsContainer';

describe('AuthFormErrorsContainer', () => {
  it('should not render any elements when empty errors array is passed', () => {
    const { queryByTestId } = render(<AuthFormErrorsContainer errors={[]} />);
    const errorsContainer = queryByTestId('authErrorsContainer');

    expect(errorsContainer).not.toBeInTheDocument();
  });

  it('should render with errors', () => {
    const errors = ['Error 1', 'Error 2'];
    const { getByTestId, getByText } = render(
      <AuthFormErrorsContainer errors={errors} />
    );
    const errorsContainer = getByTestId('authErrorsContainer');
    const error1 = getByText('Error 1');
    const error2 = getByText('Error 2');

    expect(errorsContainer).toBeInTheDocument();
    expect(error1).toBeInTheDocument();
    expect(error2).toBeInTheDocument();
  });
});
