import { Client } from '../http/client';
import { Result } from './result';

export function authService(client: Client) {
  const baseUrl = import.meta.env.VITE_API_BASE_URL;
  const endpoints = {
    login: `${baseUrl}/auth/login`,
    logout: `${baseUrl}/auth/logout`,
    register: `${baseUrl}/auth/register`,
    refresh: `${baseUrl}/auth/refresh`,
  };

  return {
    async refresh(userId: string): Promise<Result<{ token: string }>> {
      const req = {
        url: endpoints.refresh,
        body: {
          userId,
        },
      };

      const res = await client.post(req);
      const body = await res.json();

      if (res.ok === false) {
        const errMsg = body?.title ?? 'Unable to refresh user token';
        const error = new Error(errMsg);
        return Result.failure(error);
      }

      return Result.success<{ token: string }>(body);
    },
    async login(
      username: string,
      password: string
    ): Promise<Result<{ token: string }>> {
      const req = {
        url: endpoints.login,
        body: {
          username,
          password,
        },
      };

      const res = await client.post(req);
      const body = await res.json();

      if (res.ok === false) {
        const errMsg = body?.title ?? 'Unable to login user';
        const error = new Error(errMsg);
        return Result.failure(error);
      }

      return Result.success<{ token: string }>(body);
    },
    async logout(): Promise<Result<true>> {
      const res = await client.post({ url: endpoints.logout });
      const body = await res.json();

      if (res.ok === false) {
        const errMsg = body?.title ?? 'Unable to logout user';
        const error = new Error(errMsg);
        return Result.failure(error);
      }

      return Result.success(true);
    },
    async register(
      email: string,
      password: string
    ): Promise<Result<{ id: string }>> {
      const req = {
        url: endpoints.register,
        body: {
          email,
          password,
        },
      };

      const res = await client.post(req);
      const body = await res.json();

      if (res.ok === false) {
        const body = await res.json();
        const errMsg = body?.title ?? 'Unable to register user';
        const error = new Error(errMsg);
        return Result.failure(error);
      }

      return Result.success<{ id: string }>(body);
    },
  };
}
