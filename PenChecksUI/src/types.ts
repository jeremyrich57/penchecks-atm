export type Account = {
  id: string;
  name: string;
  balance: number;
};

export type Customer = {
  id: string;
  name: string;
  accounts: Account[];
};

export type TransactionType = "deposit" | "withdraw" | "transfer";

export type Transaction = {
  id: string;
  type: TransactionType;
  amount: number;
  accountId: string;
  accountName: string;
  toAccountId?: string | null;
  toAccountName?: string | null;
  timestamp: string;
};

export type TransferResponse = {
  from: Account;
  to: Account;
};
