import { useState } from 'react';
import styles from './Sidebar.module.css';
import SidebarBody from './SidebarBody';
import SidebarFooter from './SidebarFooter';
import SidebarHeader from './SidebarHeader';

export default function Sidebar({}) {
  const [isCollapsed, setIsCollapsed] = useState(false);

  function handleArrowButtonClick() {
    setIsCollapsed(!isCollapsed);
  }

  return (
    <aside className={isCollapsed ? styles.sidebarCollapsed : styles.sidebar}>
      <SidebarHeader isCollapsed={isCollapsed} />
      <SidebarBody isCollapsed={isCollapsed} />
      <SidebarFooter
        isCollapsed={isCollapsed}
        arrowButtonClickHandler={handleArrowButtonClick}
      />
    </aside>
  );
}
