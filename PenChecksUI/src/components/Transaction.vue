<template>
  <div>
    <h3>New Transaction</h3>
    <v-tabs v-model="tab" class="mt-3">
      <v-tab value="deposit">Deposit</v-tab>
      <v-tab value="withdraw">Withdraw</v-tab>
      <v-tab value="transfer">Transfer</v-tab>
    </v-tabs>

    <v-tabs-window v-model="tab" class="mt-3">
      <v-tabs-window-item
        value="deposit"
        :transition="false"
        :reverse-transition="false"
      >
        <v-sheet>
          <v-form @submit.prevent="submitDeposit">
            <v-select
              v-model="depositToAccount"
              label="To Account"
              :items="['Checking', 'Savings']"
              required
            ></v-select>
            <v-number-input
              v-model="depositAmount"
              label="Amount"
              required
              :min="0"
              :precision="2"
            ></v-number-input>
            <v-btn type="submit" color="primary" block>Deposit</v-btn>
          </v-form>
        </v-sheet>
      </v-tabs-window-item>
      <v-tabs-window-item
        value="withdraw"
        :transition="false"
        :reverse-transition="false"
      >
        <v-sheet>
          <v-form @submit.prevent="submitWithdraw">
            <v-select
              v-model="withdrawFromAccount"
              label="From Account"
              :items="['Checking', 'Savings']"
              required
            ></v-select>
            <v-number-input
              v-model="withdrawAmount"
              label="Amount"
              required
              :min="0"
              :precision="2"
            ></v-number-input>
            <v-btn type="submit" color="primary" block>Withdraw</v-btn>
          </v-form>
        </v-sheet>
      </v-tabs-window-item>
      <v-tabs-window-item
        value="transfer"
        :transition="false"
        :reverse-transition="false"
      >
        <v-sheet>
          <v-form @submit.prevent="submitTransfer">
            <v-select
              v-model="transferFromAccount"
              label="From Account"
              :items="['Checking', 'Savings']"
              required
            ></v-select>
            <v-select
              v-model="transferToAccount"
              label="To Account"
              :items="['Checking', 'Savings']"
              required
            ></v-select>
            <v-number-input
              v-model="transferAmount"
              label="Amount"
              required
              :min="0"
              :precision="2"
            ></v-number-input>
            <v-btn type="submit" color="primary" block>Transfer</v-btn>
          </v-form>
        </v-sheet>
      </v-tabs-window-item>
    </v-tabs-window>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";

const tab = ref("deposit");
const depositToAccount = ref("Checking");
const depositAmount = ref(0);
const withdrawFromAccount = ref("Checking");
const withdrawAmount = ref(0);
const transferFromAccount = ref("Checking");
const transferToAccount = ref("Savings");
const transferAmount = ref(0);

const submitDeposit = () => {
  // Handle deposit submission logic here
  alert("Deposit submitted!");
};

const submitWithdraw = () => {
  // Handle withdrawal submission logic here
  alert("Withdrawal submitted!");
};

const submitTransfer = () => {
  // Handle transfer submission logic here
  alert("Transfer submitted!");
};
</script>

<style scoped></style>
