<template>
  <div class="main-container">
    <h1>PenChecks ATM</h1>
    <div>Deposit, withdraw, and transfer funds</div>
    <Accounts />
    <div class="d-flex flex-wrap ga-5 mt-4">
      <Transaction class="flex-fill" style="min-width: 250px" />
      <History class="flex-fill" style="min-width: 250px" />
    </div>
  </div>
</template>

<script setup lang="ts">
import Accounts from "./Accounts.vue";
import Transaction from "./Transaction.vue";
import History from "./History.vue";
</script>

<style scoped>
.main-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem 1.5rem 3rem;
}
</style>
