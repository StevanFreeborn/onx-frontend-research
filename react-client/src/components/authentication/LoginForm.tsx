import { ChangeEvent, FormEvent, useReducer } from 'react';
import { BiSolidLock } from 'react-icons/bi';
import { ImUser } from 'react-icons/im';
import { Link } from 'react-router-dom';
import { useAuthClient } from '../../hooks/useAuthClient';
import { useUserContext } from '../../hooks/useUserContext';
import { authService } from '../../services/authService';
import AuthFormErrorsContainer from './AuthFormErrorsContainer';
import { AuthInput } from './AuthInput';
import styles from './LoginForm.module.css';

export default function LoginForm() {
  const { logUserIn } = useUserContext();
  const client = useAuthClient();
  const { login } = authService(client);

  type FormState = {
    username: string;
    password: string;
    errors: string[];
  };

  const initialFormState: FormState = {
    username: '',
    password: '',
    errors: [],
  };

  type FormActions =
    | { type: 'SET_USERNAME'; payload: { username: string } }
    | { type: 'SET_PASSWORD'; payload: { password: string } }
    | { type: 'SET_ERROR'; payload: { error: string } };

  const reducer = (state: FormState, action: FormActions) => {
    switch (action.type) {
      case 'SET_USERNAME':
        return {
          ...state,
          username: action.payload.username,
        };
      case 'SET_PASSWORD':
        return {
          ...state,
          password: action.payload.password,
        };
      case 'SET_ERROR':
        return {
          ...state,
          errors: [...state.errors, action.payload.error],
        };
      default:
        return state;
    }
  };

  const [formState, dispatch] = useReducer(reducer, initialFormState);

  async function handleFormSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    const errors: string[] = [];

    if (!formState.username) {
      errors.push('Username is required');
    }

    if (!formState.password) {
      errors.push('Password is required');
    }

    if (errors.length > 0) {
      errors.forEach(error => {
        dispatch({
          type: 'SET_ERROR',
          payload: { error },
        });
      });

      return;
    }

    const loginResult = await login(formState.username, formState.password);

    if (loginResult.isFailed) {
      dispatch({
        type: 'SET_ERROR',
        payload: { error: loginResult.error.message },
      });
      return;
    }

    logUserIn(loginResult.value.token);
  }

  function handleUsernameChange(event: ChangeEvent<HTMLInputElement>) {
    dispatch({
      type: 'SET_USERNAME',
      payload: { username: event.target.value },
    });
  }

  function handlePasswordChange(event: ChangeEvent<HTMLInputElement>) {
    dispatch({
      type: 'SET_PASSWORD',
      payload: { password: event.target.value },
    });
  }

  return (
    <form onSubmit={handleFormSubmit}>
      <AuthInput
        Icon={ImUser}
        type="text"
        placeholder="Username"
        value={formState.username}
        changeHandler={handleUsernameChange}
      />
      <AuthInput
        Icon={BiSolidLock}
        type="password"
        placeholder="Password"
        value={formState.password}
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
      <AuthFormErrorsContainer errors={formState.errors} />
    </form>
  );
}
