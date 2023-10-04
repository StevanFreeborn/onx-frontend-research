import AuthContainer from '../../components/authentication/AuthContainer';
import LoginForm from '../../components/authentication/LoginForm';

export default function LoginPage() {
  return (
    <AuthContainer>
      <LoginForm />
    </AuthContainer>
  );
}
