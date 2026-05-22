import { useFetch } from "../composables/useFetch";
import type { Transaction } from "../types";

export const getTransactions = (accountId?: string) => {
  const qs = accountId ? `?accountId=${encodeURIComponent(accountId)}` : "";
  return useFetch<Transaction[]>(`/api/transactions${qs}`);
};
