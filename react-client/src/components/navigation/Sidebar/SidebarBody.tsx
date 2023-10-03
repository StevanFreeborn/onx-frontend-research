import { useEffect, useRef, useState } from 'react';
import { Link } from 'react-router-dom';
import styles from './SidebarBody.module.css';

export default function SidebarBody({ isCollapsed }: { isCollapsed: boolean }) {
  return (
    <nav className={styles.container}>
      <div className={styles.topContainer}>
        <SideBarSearch isCollapsed={isCollapsed} />
        <div
          title="Dashboards"
          className={
            isCollapsed ? styles.navItemActiveCollapsed : styles.navItemActive
          }
        >
          <div className={styles.navItemIconContainer}>
            <svg
              className={styles.navItemIcon}
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 24 24"
            >
              <path d="M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12A10,10 0 0,0 12,2M12,4A8,8 0 0,1 20,12C20,14.4 19,16.5 17.3,18C15.9,16.7 14,16 12,16C10,16 8.2,16.7 6.7,18C5,16.5 4,14.4 4,12A8,8 0 0,1 12,4M14,5.89C13.62,5.9 13.26,6.15 13.1,6.54L11.81,9.77L11.71,10C11,10.13 10.41,10.6 10.14,11.26C9.73,12.29 10.23,13.45 11.26,13.86C12.29,14.27 13.45,13.77 13.86,12.74C14.12,12.08 14,11.32 13.57,10.76L13.67,10.5L14.96,7.29L14.97,7.26C15.17,6.75 14.92,6.17 14.41,5.96C14.28,5.91 14.15,5.89 14,5.89M10,6A1,1 0 0,0 9,7A1,1 0 0,0 10,8A1,1 0 0,0 11,7A1,1 0 0,0 10,6M7,9A1,1 0 0,0 6,10A1,1 0 0,0 7,11A1,1 0 0,0 8,10A1,1 0 0,0 7,9M17,9A1,1 0 0,0 16,10A1,1 0 0,0 17,11A1,1 0 0,0 18,10A1,1 0 0,0 17,9Z" />
            </svg>
          </div>
          {isCollapsed ? null : (
            <div className={styles.navLink}>Dashboards</div>
          )}
        </div>
        <Link title="Reports" to="/Report" className={styles.navLink}>
          <div
            className={isCollapsed ? styles.navItemCollapsed : styles.navItem}
          >
            <div className={styles.navItemIconContainer}>
              <svg
                className={styles.navItemIcon}
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
              >
                <path d="M22,21H2V3H4V19H6V10H10V19H12V6H16V19H18V14H22V21Z" />
              </svg>
            </div>
            {isCollapsed ? null : <div className={styles.navLink}>Reports</div>}
          </div>
        </Link>
        <Link title="Content" to="/Content" className={styles.navLink}>
          <div
            className={isCollapsed ? styles.navItemCollapsed : styles.navItem}
          >
            <div className={styles.navItemIconContainer}>
              <svg
                className={styles.navItemIcon}
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
              >
                <path d="M15,7H20.5L15,1.5V7M8,0H16L22,6V18A2,2 0 0,1 20,20H8C6.89,20 6,19.1 6,18V2A2,2 0 0,1 8,0M4,4V22H20V24H4A2,2 0 0,1 2,22V4H4Z" />
              </svg>
            </div>
            {isCollapsed ? null : <div className={styles.navLink}>Content</div>}
          </div>
        </Link>
      </div>
      <div className={styles.bottomContainer}>
        <Link title="Administration" to="/Admin" className={styles.navLink}>
          <div
            className={isCollapsed ? styles.navItemCollapsed : styles.navItem}
          >
            <div className={styles.gearIconContainer}>
              <svg
                className={styles.gearIcon}
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
              >
                <path d="M12,15.5A3.5,3.5 0 0,1 8.5,12A3.5,3.5 0 0,1 12,8.5A3.5,3.5 0 0,1 15.5,12A3.5,3.5 0 0,1 12,15.5M19.43,12.97C19.47,12.65 19.5,12.33 19.5,12C19.5,11.67 19.47,11.34 19.43,11L21.54,9.37C21.73,9.22 21.78,8.95 21.66,8.73L19.66,5.27C19.54,5.05 19.27,4.96 19.05,5.05L16.56,6.05C16.04,5.66 15.5,5.32 14.87,5.07L14.5,2.42C14.46,2.18 14.25,2 14,2H10C9.75,2 9.54,2.18 9.5,2.42L9.13,5.07C8.5,5.32 7.96,5.66 7.44,6.05L4.95,5.05C4.73,4.96 4.46,5.05 4.34,5.27L2.34,8.73C2.21,8.95 2.27,9.22 2.46,9.37L4.57,11C4.53,11.34 4.5,11.67 4.5,12C4.5,12.33 4.53,12.65 4.57,12.97L2.46,14.63C2.27,14.78 2.21,15.05 2.34,15.27L4.34,18.73C4.46,18.95 4.73,19.03 4.95,18.95L7.44,17.94C7.96,18.34 8.5,18.68 9.13,18.93L9.5,21.58C9.54,21.82 9.75,22 10,22H14C14.25,22 14.46,21.82 14.5,21.58L14.87,18.93C15.5,18.67 16.04,18.34 16.56,17.94L19.05,18.95C19.27,19.03 19.54,18.95 19.66,18.73L21.66,15.27C21.78,15.05 21.73,14.78 21.54,14.63L19.43,12.97Z" />
              </svg>
            </div>
          </div>
        </Link>
      </div>
    </nav>
  );
}

function SideBarSearch({ isCollapsed }: { isCollapsed: boolean }) {
  return isCollapsed ? <SearchModalButton /> : <SearchInput />;
}

function SearchInput({ containerPadding }: { containerPadding?: number }) {
  return (
    <div
      style={{ padding: containerPadding }}
      className={styles.searchContainer}
    >
      <input type="text" placeholder="Search All Content"></input>
      <div className={styles.searchIconContainer}>
        <svg
          className={styles.searchIcon}
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 24 24"
        >
          <path d="M9.5,3A6.5,6.5 0 0,1 16,9.5C16,11.11 15.41,12.59 14.44,13.73L14.71,14H15.5L20.5,19L19,20.5L14,15.5V14.71L13.73,14.44C12.59,15.41 11.11,16 9.5,16A6.5,6.5 0 0,1 3,9.5A6.5,6.5 0 0,1 9.5,3M9.5,5C7,5 5,7 5,9.5C5,12 7,14 9.5,14C12,14 14,12 14,9.5C14,7 12,5 9.5,5Z" />
        </svg>
      </div>
    </div>
  );
}

function SearchModalButton() {
  const [isInputDisplayed, setIsInputDisplayed] = useState(false);
  const containerRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    function handleClickOutside(e: globalThis.MouseEvent) {
      if (
        containerRef.current &&
        containerRef.current.contains(e.target as Node) === false
      ) {
        setIsInputDisplayed(false);
      }
    }

    document.addEventListener('click', handleClickOutside);
    return () => document.removeEventListener('click', handleClickOutside);
  }, [containerRef, setIsInputDisplayed, isInputDisplayed]);

  function handleButtonClick() {
    setIsInputDisplayed(!isInputDisplayed);
  }

  return (
    <div ref={containerRef} className={styles.searchContainerCollapsed}>
      <button
        onClick={handleButtonClick}
        className={styles.searchIconContainerCollapsed}
      >
        <svg
          className={styles.searchIcon}
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 24 24"
        >
          <path d="M9.5,3A6.5,6.5 0 0,1 16,9.5C16,11.11 15.41,12.59 14.44,13.73L14.71,14H15.5L20.5,19L19,20.5L14,15.5V14.71L13.73,14.44C12.59,15.41 11.11,16 9.5,16A6.5,6.5 0 0,1 3,9.5A6.5,6.5 0 0,1 9.5,3M9.5,5C7,5 5,7 5,9.5C5,12 7,14 9.5,14C12,14 14,12 14,9.5C14,7 12,5 9.5,5Z" />
        </svg>
      </button>
      <div
        className={
          isInputDisplayed ? styles.searchSliderSlide : styles.searchSlider
        }
      >
        <SearchInput containerPadding={0} />
      </div>
    </div>
  );
}
