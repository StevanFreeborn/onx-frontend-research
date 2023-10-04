import { useNavigate } from 'react-router-dom';
import AuthContainer from '../../authentication/AuthContainer';
import styles from './PublicNotFound.module.css';

export default function PublicNotFound() {
  const navigate = useNavigate();

  function handlePreviousPageButtonClick() {
    navigate(-1);
  }

  return (
    <AuthContainer>
      <div className={styles.container}>
        <h2 className={styles.heading}>
          Oops! The page you're trying to view doesn't exist
        </h2>
        <p className={styles.message}>
          Go back to the{' '}
          <button
            className={styles.previousButton}
            onClick={handlePreviousPageButtonClick}
            type="button"
          >
            previous page
          </button>
        </p>
      </div>
    </AuthContainer>
  );
}
