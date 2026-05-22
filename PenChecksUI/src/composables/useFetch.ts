const BASE = import.meta.env.VITE_API_BASE_URL ?? "";

export async function useFetch<T>(
  path: string,
  opts: RequestInit = {}
): Promise<T> {
  const res = await fetch(`${BASE}${path}`, {
    headers: { "Content-Type": "application/json", ...opts.headers },
    ...opts,
  });

  if (!res.ok) {
    let message = res.statusText;
    try {
      const body = await res.json();
      if (body?.message) message = body.message;
    } catch {
      // response had no JSON body
    }
    throw new Error(message);
  }

  if (res.status === 204) return undefined as T;
  return (await res.json()) as T;
}
