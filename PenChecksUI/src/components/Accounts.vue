<template>
  <div>
    <h3>Accounts</h3>
    <div class="d-flex ga-3 flex-wrap">
      <v-card
        v-for="account in accounts"
        :key="account.id"
        :subtitle="account.name"
        class="flex-fill"
        style="min-width: 250px"
      >
        <v-card-text>
          <p class="text-headline-large">
            Balance: ${{ account.balance.toFixed(2) }}
          </p>
        </v-card-text>
      </v-card>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useAtm } from "../composables/useAtm";

const { accounts } = useAtm();
</script>

<style scoped></style>
