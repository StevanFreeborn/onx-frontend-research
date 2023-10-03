import { BiLogOutCircle, BiSolidUser } from 'react-icons/bi';
import { FaUserCog } from 'react-icons/fa';
import { Link } from 'react-router-dom';
import defaultProfilePicture from '../../../assets/images/defaults/profile-picture.png';
import logo from '../../../assets/images/logos/testing-demo-logo-180-47.svg';
import styles from './SidebarHeader.module.css';

export default function SidebarHeader({
  isCollapsed,
}: {
  isCollapsed: boolean;
}) {
  const username = 'John Doe';
  const role = 'Software Engineer';
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
      <div className={styles.profileContainer}>
        <button type="button" className={styles.profilePictureButton}>
          <img alt="default-profile" src={defaultProfilePicture} />
          <div className={styles.profileModal}>
            <ul>
              <li>
                <button type="button">
                  <BiSolidUser />
                  <span>Profile</span>
                </button>
              </li>
              <li>
                <button type="button">
                  <FaUserCog />
                  <span>Preferences</span>
                </button>
              </li>
              <li>
                <button type="button">
                  <BiLogOutCircle />
                  <span>Logout</span>
                </button>
              </li>
            </ul>
          </div>
        </button>
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
