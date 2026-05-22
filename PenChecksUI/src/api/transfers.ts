import { useFetch } from "../composables/useFetch";
import type { TransferResponse } from "../types";

export const transfer = (
  fromAccountId: string,
  toAccountId: string,
  amount: number
) =>
  useFetch<TransferResponse>("/api/transfers", {
    method: "POST",
    body: JSON.stringify({ fromAccountId, toAccountId, amount }),
  });
