<template>
  <div>
    <h3>Transaction History</h3>
    <v-table height="300px" fixed-header>
      <thead>
        <tr>
          <th>Date</th>
          <th>Type</th>
          <th>From Account</th>
          <th>To Account</th>
          <th>Amount</th>
        </tr>
      </thead>
      <tbody>
        <tr v-if="transactions.length === 0">
          <td colspan="5" class="text-center text-medium-emphasis">
            No transactions yet
          </td>
        </tr>
        <tr v-for="tx in transactions" :key="tx.id">
          <td>{{ formatDate(tx.timestamp) }}</td>
          <td>{{ typeLabel(tx.type) }}</td>
          <td>{{ fromAccountLabel(tx) }}</td>
          <td>{{ toAccountLabel(tx) }}</td>
          <td :class="amountClass(tx.type)">
            {{ amountPrefix(tx.type) }}${{ tx.amount.toFixed(2) }}
          </td>
        </tr>
      </tbody>
    </v-table>
  </div>
</template>

<script setup lang="ts">
import { useAtm } from "../composables/useAtm";
import type { Transaction, TransactionType } from "../types";

const { transactions } = useAtm();

function formatDate(iso: string) {
  return new Date(iso).toLocaleString();
}

function typeLabel(type: TransactionType) {
  switch (type) {
    case "deposit":
      return "Deposit";
    case "withdraw":
      return "Withdrawal";
    case "transfer":
      return "Transfer";
  }
}

function fromAccountLabel(tx: Transaction) {
  if (tx.type === "deposit") return "-";
  return tx.accountName || "-";
}

function toAccountLabel(tx: Transaction) {
  if (tx.type === "deposit") return tx.accountName || "-";
  if (tx.type === "transfer") return tx.toAccountName || "-";
  return "-";
}

function amountPrefix(type: TransactionType) {
  return type === "deposit" ? "+" : "-";
}

function amountClass(type: TransactionType) {
  if (type === "deposit") return "text-success text-title-medium";
  if (type === "withdraw") return "text-error text-title-medium";
  return "text-title-medium";
}
</script>

<style scoped></style>
