import { useEffect, useState } from 'react';
import { client } from '../../../http/client';
import { SystemStatus, statusService } from '../../../services/statusService';
import styles from './SidebarFooter.module.css';

export default function SidebarFooter({
  isCollapsed,
  arrowButtonClickHandler,
}: {
  isCollapsed: boolean;
  arrowButtonClickHandler: () => void;
}) {
  const [status, setStatus] = useState(SystemStatus.None);
  const { getStatus } = statusService(client());

  useEffect(() => {
    const interval = setInterval(async () => {
      const statusResult = await getStatus();
      if (statusResult.isSuccess) {
        setStatus(statusResult.value.status);
      }
    }, 15000);

    getStatus().then(statusResult => {
      if (statusResult.isSuccess) {
        setStatus(statusResult.value.status);
      }
    });

    return () => clearInterval(interval);
  }, []);

  function getStatusColor() {
    switch (status) {
      case SystemStatus.Critical:
        return 'red';
      case SystemStatus.Major:
        return 'orange';
      case SystemStatus.Minor:
        return 'yellow';
      case SystemStatus.Maintenance:
        return 'blue';
      case SystemStatus.None:
      default:
        return '#2fcc66';
    }
  }

  return (
    <footer
      className={isCollapsed ? styles.containerCollapsed : styles.container}
    >
      <div
        onClick={arrowButtonClickHandler}
        title="Collapse"
        className={styles.footerItem}
      >
        <button type="button" className={styles.footerIconContainer}>
          {isCollapsed ? (
            <svg
              className={styles.footerIcon}
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 24 24"
            >
              <path d="M4,15V9H12V4.16L19.84,12L12,19.84V15H4Z" />
            </svg>
          ) : (
            <svg
              className={styles.footerIcon}
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 24 24"
            >
              <path d="M20,9V15H12V19.84L4.16,12L12,4.16V9H20Z" />
            </svg>
          )}
        </button>
      </div>
      <div title="Help" className={styles.footerItem}>
        <a
          href="/Help/OnspringDocumentation.htm"
          className={styles.footerIconContainer}
        >
          <svg
            className={styles.footerIcon}
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
          >
            <path d="M11,18H13V16H11V18M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12A10,10 0 0,0 12,2M12,20C7.59,20 4,16.41 4,12C4,7.59 7.59,4 12,4C16.41,4 20,7.59 20,12C20,16.41 16.41,20 12,20M12,6A4,4 0 0,0 8,10H10A2,2 0 0,1 12,8A2,2 0 0,1 14,10C14,12 11,11.75 11,15H13C13,12.75 16,12.5 16,10A4,4 0 0,0 12,6Z" />
          </svg>
        </a>
      </div>
      <div title="All Systems Operational" className={styles.footerItem}>
        <div className={styles.footerIconContainer}>
          <div
            className={styles.statusIcon}
            style={{
              backgroundColor: getStatusColor(),
            }}
          ></div>
        </div>
      </div>
    </footer>
  );
}
