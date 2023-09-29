import { BiSolidLock } from 'react-icons/bi';
import { ImUser } from 'react-icons/im';
import { Link } from 'react-router-dom';
import AuthFormErrorsContainer from './AuthFormErrorsContainer';
import { AuthInput } from './AuthInput';
import styles from './LoginForm.module.css';

export default function LoginForm() {
  const username = '';
  const password = '';
  const errors: string[] = [];

  function handleFormSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    console.log('Form submitted');
  }

  function handleUsernameChange(event: React.ChangeEvent<HTMLInputElement>) {
    console.log('Username changed to', event.target.value);
  }

  function handlePasswordChange(event: React.ChangeEvent<HTMLInputElement>) {
    console.log('Password changed to', event.target.value);
  }

  return (
    <form onSubmit={handleFormSubmit}>
      <AuthInput
        Icon={ImUser}
        type="text"
        placeholder="Username"
        value={username}
        changeHandler={handleUsernameChange}
      />
      <AuthInput
        Icon={BiSolidLock}
        type="password"
        placeholder="Password"
        value={password}
        changeHandler={handlePasswordChange}
      />
      <div className={styles.actionsContainer}>
        <Link className={styles.forgotPasswordLink} to="/Public/ForgotPassword">
          Forgot Password?
        </Link>
        <button className={styles.loginButton} type="submit">
          Login
        </button>
      </div>
      <AuthFormErrorsContainer errors={errors} />
    </form>
  );
}
