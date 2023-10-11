import { useEffect, useRef, useState } from 'react';
import { BiLogOutCircle, BiSolidUser } from 'react-icons/bi';
import { FaUserCog } from 'react-icons/fa';
import { Link } from 'react-router-dom';
import defaultProfilePicture from '../../../assets/images/defaults/profile-picture.png';
import logo from '../../../assets/images/logos/testing-demo-logo-180-47.svg';
import { useAuthClient } from '../../../hooks/useAuthClient';
import { useUserContext } from '../../../hooks/useUserContext';
import { authService } from '../../../services/authService';
import styles from './SidebarHeader.module.css';

export default function SidebarHeader({
  isCollapsed,
}: {
  isCollapsed: boolean;
}) {
  const client = useAuthClient();
  const { logout } = authService(client);
  const { logUserOut } = useUserContext();
  const [isProfileModalOpen, setIsProfileModalOpen] = useState(false);
  const modalRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    function handleClickOutside(event: MouseEvent) {
      if (
        isProfileModalOpen &&
        modalRef.current &&
        modalRef.current.contains(event.target as Node) === false
      ) {
        setIsProfileModalOpen(false);
      }
    }

    document.addEventListener('click', handleClickOutside);
    return () => document.removeEventListener('click', handleClickOutside);
  }, [isProfileModalOpen]);

  const username = 'John Doe';
  const role = 'Software Engineer';

  function handleProfilePictureClick() {
    setIsProfileModalOpen(!isProfileModalOpen);
  }

  function handleProfileClick() {
    alert('Should open profile modal');
    setIsProfileModalOpen(false);
  }

  function handlePreferencesClick() {
    alert('Should open preferences modal');
    setIsProfileModalOpen(false);
  }

  async function handleLogoutClick() {
    const logoutResult = await logout();

    if (logoutResult.isFailed) {
      alert('Error logging out');
      return;
    } else {
      logUserOut();
    }

    setIsProfileModalOpen(false);
  }

  return (
    <header className={styles.container}>
      <div
        className={
          isCollapsed ? styles.logoContainerCollapsed : styles.logoContainer
        }
      >
        <Link to="/">
          <img alt="testing-demo-logo" src={logo} className={styles.logo} />
        </Link>
      </div>
      <div
        className={
          isCollapsed
            ? styles.profileContainerCollapsed
            : styles.profileContainer
        }
      >
        <div ref={modalRef} style={{ position: 'relative' }}>
          <button
            onClick={handleProfilePictureClick}
            type="button"
            className={styles.profilePictureButton}
          >
            <img alt="default-profile" src={defaultProfilePicture} />
          </button>
          {isProfileModalOpen ? (
            <div className={styles.profileModal}>
              <ul>
                {isCollapsed ? (
                  <>
                    <li className={styles.profileModalInfo}>
                      <div className={styles.profileModalUsername}>
                        {username}
                      </div>
                      <div className={styles.profileModalCurrentRole}>
                        {role}
                      </div>
                    </li>
                    <hr />
                  </>
                ) : null}
                <li>
                  <button type="button" onClick={handleProfileClick}>
                    <BiSolidUser size={20} />
                    <span>Profile</span>
                  </button>
                </li>
                <li>
                  <button type="button" onClick={handlePreferencesClick}>
                    <FaUserCog size={20} />
                    <span>Preferences</span>
                  </button>
                </li>
                <hr />
                <li>
                  <button type="button" onClick={handleLogoutClick}>
                    <BiLogOutCircle size={20} />
                    <span>Logout</span>
                  </button>
                </li>
              </ul>
            </div>
          ) : null}
        </div>
        {isCollapsed ? null : (
          <div className={styles.profileInfoContainer}>
            <div className={styles.userName}>{username}</div>
            <div className={styles.currentRole}>{role}</div>
          </div>
        )}
      </div>
    </header>
  );
}
