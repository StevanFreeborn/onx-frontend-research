import { ReactNode } from 'react';
import Sidebar from '../navigation/Sidebar/Sidebar';
import styles from './ProtectedLayout.module.css';

export function ProtectedLayout({ children }: { children: ReactNode }) {
  return (
    <div className={styles.container}>
      <Sidebar />
      <main className={styles.main}>{children}</main>
    </div>
  );
}
