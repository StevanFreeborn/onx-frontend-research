import { ReactNode } from 'react';

export function ProtectedLayout({ children }: { children: ReactNode }) {
  return (
    <div>
      <h1>Protected Layout</h1>
      {children}
    </div>
  );
}
