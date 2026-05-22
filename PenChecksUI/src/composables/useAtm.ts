import { computed, ref } from "vue";
import * as accountsApi from "../api/accounts";
import * as customerApi from "../api/customer";
import * as transactionsApi from "../api/transactions";
import * as transfersApi from "../api/transfers";
import type { Customer, Transaction } from "../types";

const customer = ref<Customer | null>(null);
const transactions = ref<Transaction[]>([]);
const loading = ref(false);
const error = ref<string | null>(null);

const accounts = computed(() => customer.value?.accounts ?? []);

async function refresh() {
  loading.value = true;
  error.value = null;
  try {
    const [c, t] = await Promise.all([
      customerApi.getCustomer(),
      transactionsApi.getTransactions(),
    ]);
    customer.value = c;
    transactions.value = t;
  } catch (e) {
    error.value = (e as Error).message;
  } finally {
    loading.value = false;
  }
}

async function deposit(accountId: string, amount: number) {
  await accountsApi.deposit(accountId, amount);
  await refresh();
}

async function withdraw(accountId: string, amount: number) {
  await accountsApi.withdraw(accountId, amount);
  await refresh();
}

async function transfer(
  fromAccountId: string,
  toAccountId: string,
  amount: number
) {
  await transfersApi.transfer(fromAccountId, toAccountId, amount);
  await refresh();
}

export function useAtm() {
  return {
    customer,
    accounts,
    transactions,
    loading,
    error,
    refresh,
    deposit,
    withdraw,
    transfer,
  };
}
