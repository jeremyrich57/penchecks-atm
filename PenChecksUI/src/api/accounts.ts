import { useFetch } from "../composables/useFetch";
import type { Account } from "../types";

export const getAccounts = () => useFetch<Account[]>("/api/accounts");

export const getAccount = (id: string) =>
  useFetch<Account>(`/api/accounts/${id}`);

export const deposit = (id: string, amount: number) =>
  useFetch<Account>(`/api/accounts/${id}/deposit`, {
    method: "POST",
    body: JSON.stringify({ amount }),
  });

export const withdraw = (id: string, amount: number) =>
  useFetch<Account>(`/api/accounts/${id}/withdraw`, {
    method: "POST",
    body: JSON.stringify({ amount }),
  });
