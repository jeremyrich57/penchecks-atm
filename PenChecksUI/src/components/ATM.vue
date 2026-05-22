<template>
  <div class="main-container">
    <h1>PenChecks ATM</h1>
    <div>
      Welcome
      <b
        ><i>{{ customer?.name }}!</i></b
      >
      Deposit, withdraw, and transfer funds
    </div>
    <v-alert
      v-if="error"
      type="error"
      class="mt-3"
      closable
      @click:close="error = null"
    >
      {{ error }}
    </v-alert>
    <Accounts />
    <div class="d-flex flex-wrap ga-5 mt-4">
      <Transaction class="flex-fill" style="min-width: 250px" />
      <History class="flex-fill" style="min-width: 250px" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from "vue";
import Accounts from "./Accounts.vue";
import Transaction from "./Transaction.vue";
import History from "./History.vue";
import { useAtm } from "../composables/useAtm";

const { customer, error, refresh } = useAtm();

onMounted(refresh);
</script>

<style scoped>
.main-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem 1.5rem 3rem;
}
</style>
