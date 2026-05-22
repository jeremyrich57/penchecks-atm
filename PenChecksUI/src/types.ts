export type Account = {
  id: string;
  name: string;
  balance: number;
};

export type TransactionType = "deposit" | "withdraw" | "transfer";

export type Transaction = {
  id: string;
  type: TransactionType;
  amount: number;
  accountId: string;
  accountName: string;
  toAccountId?: string;
  toAccountName?: string;
  timestamp: number;
  balanceAfter: number;
};
