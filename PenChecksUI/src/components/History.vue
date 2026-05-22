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
        <tr v-for="(tx, index) in transactions" :key="index">
          <td>{{ tx.date }}</td>
          <td>{{ tx.type }}</td>
          <td>{{ tx.fromAccount || "-" }}</td>
          <td>{{ tx.toAccount || "-" }}</td>
          <td
            v-if="tx.type === 'Deposit'"
            class="text-success text-title-medium"
          >
            +${{ tx.amount.toFixed(2) }}
          </td>
          <td
            v-else-if="tx.type === 'Withdrawal'"
            class="text-error text-title-medium"
          >
            -${{ tx.amount.toFixed(2) }}
          </td>
          <td v-else class="text-title-medium">-${{ tx.amount.toFixed(2) }}</td>
        </tr>
      </tbody>
    </v-table>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";

// TODO: Replace with real transaction data from backend
const transactions = ref([
  {
    date: "2024-06-01",
    type: "Deposit",
    fromAccount: "",
    toAccount: "Checking",
    amount: 500,
  },
  {
    date: "2024-06-02",
    type: "Withdrawal",
    fromAccount: "Checking",
    toAccount: "",
    amount: 100,
  },
  {
    date: "2024-06-03",
    type: "Transfer",
    fromAccount: "Checking",
    toAccount: "Savings",
    amount: 200,
  },
]);
</script>

<style scoped></style>
