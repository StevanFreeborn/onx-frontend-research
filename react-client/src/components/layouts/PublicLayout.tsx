import { CSSProperties, ReactNode } from 'react';
import dew from '../../assets/images/backgrounds/dew.jpg';
import kromlau from '../../assets/images/backgrounds/kromlau.jpg';
import marsh from '../../assets/images/backgrounds/marsh.jpg';

export function PublicLayout({ children }: { children: ReactNode }) {
  const backgroundImages = [dew, kromlau, marsh];
  const backgroundImage =
    backgroundImages[Math.floor(Math.random() * backgroundImages.length)];

  const styles: CSSProperties = {
    background: `url(${backgroundImage})`,
    height: '100vh',
    backgroundPosition: 'center center',
    backgroundRepeat: 'no-repeat',
    backgroundSize: 'cover',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
  };

  return (
    <main data-testid="publicLayout" style={styles}>
      {children}
    </main>
  );
}
