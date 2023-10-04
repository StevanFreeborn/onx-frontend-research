import { Client } from '../http/client';
import { Result } from './result';

export enum SystemStatus {
  None = 'none',
  Minor = 'minor',
  Major = 'major',
  Critical = 'critical',
  Maintenance = 'maintenance',
}

export function statusService(client: Client) {
  const endpoint = 'https://fyc7t33wg4l2.statuspage.io/api/v2/status.json';

  return {
    async getStatus(): Promise<Result<{ status: SystemStatus }>> {
      const res = await client.get({ url: endpoint });
      const body = await res.json();

      if (res.ok === false) {
        const errMsg = body?.title ?? 'Unable to get status';
        const error = new Error(errMsg);
        return Result.failure(error);
      }

      return Result.success<{ status: SystemStatus }>({
        status: body.status.indicator,
      });
    },
  };
}
