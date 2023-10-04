import { useNavigate } from 'react-router-dom';
import styles from './NotFound.module.css';

export default function NotFound() {
  const navigate = useNavigate();

  function handlePreviousPageButtonClick() {
    navigate(-1);
  }

  return (
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
  );
}
