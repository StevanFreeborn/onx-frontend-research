import { ReactNode } from 'react';

export function PublicLayout({ children }: { children: ReactNode }) {
  return (
    <div>
      <h1>Public Layout</h1>
      {children}
    </div>
  );
}
