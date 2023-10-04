export type Client = {
  get: ({
    url,
    config,
  }: {
    url: string;
    config?: RequestInit;
  }) => Promise<Response>;
  post: <T>({
    url,
    config,
    body,
  }: {
    url: string;
    config?: RequestInit;
    body?: T;
  }) => Promise<Response>;
  put: <T>({
    url,
    config,
    body,
  }: {
    url: string;
    config?: RequestInit;
    body?: T;
  }) => Promise<Response>;
  delete: ({
    url,
    config,
  }: {
    url: string;
    config?: RequestInit;
  }) => Promise<Response>;
};

type ClientConfig = {
  authHeader?: Record<string, string> | undefined;
  unauthorizedResponseHandler?: (originalRequest: Request) => Promise<Response>;
};

export function client(clientConfig?: ClientConfig): Client {
  async function request(url: string, config?: RequestInit): Promise<Response> {
    const requestConfig = {
      ...config,
      headers: {
        ...config?.headers,
        ...clientConfig?.authHeader,
      },
      credentials: 'include' as RequestCredentials,
    };

    const request = new Request(url, requestConfig);
    const response = await fetch(request);

    if (response.status === 401 && clientConfig?.unauthorizedResponseHandler) {
      return await clientConfig.unauthorizedResponseHandler(request);
    }

    return response;
  }

  async function get({ url, config }: { url: string; config?: RequestInit }) {
    const requestConfig = { ...config, method: 'GET' };
    return await request(url, requestConfig);
  }

  async function post<T>({
    url,
    config,
    body,
  }: {
    url: string;
    config?: RequestInit;
    body?: T;
  }) {
    const requestConfig = {
      ...config,
      method: 'POST',
      body: JSON.stringify(body),
      headers: {
        ...config?.headers,
        'Content-Type': 'application/json',
      },
    };
    return await request(url, requestConfig);
  }

  async function put<T>({
    url,
    config,
    body,
  }: {
    url: string;
    config?: RequestInit;
    body?: T;
  }) {
    const requestConfig = {
      ...config,
      method: 'PUT',
      body: JSON.stringify(body),
      headers: {
        ...config?.headers,
        'Content-Type': 'application/json',
      },
    };
    return await request(url, requestConfig);
  }

  async function del({ url, config }: { url: string; config?: RequestInit }) {
    const requestConfig = { ...config, method: 'DELETE' };
    return await request(url, requestConfig);
  }

  return {
    get: get,
    post: post,
    put: put,
    delete: del,
  };
}
