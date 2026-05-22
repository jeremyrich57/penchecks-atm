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
              :items="accountItems"
              item-title="name"
              item-value="id"
              required
            ></v-select>
            <v-number-input
              v-model="depositAmount"
              label="Amount"
              required
              :min="0"
              :precision="2"
            ></v-number-input>
            <v-btn
              type="submit"
              color="primary"
              block
              :loading="submitting"
              :disabled="!depositToAccount || depositAmount <= 0"
            >
              Deposit
            </v-btn>
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
              :items="accountItems"
              item-title="name"
              item-value="id"
              required
            ></v-select>
            <v-number-input
              v-model="withdrawAmount"
              label="Amount"
              required
              :min="0"
              :precision="2"
            ></v-number-input>
            <v-btn
              type="submit"
              color="primary"
              block
              :loading="submitting"
              :disabled="!withdrawFromAccount || withdrawAmount <= 0"
            >
              Withdraw
            </v-btn>
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
              :items="accountItems"
              item-title="name"
              item-value="id"
              required
            ></v-select>
            <v-select
              v-model="transferToAccount"
              label="To Account"
              :items="accountItems"
              item-title="name"
              item-value="id"
              required
            ></v-select>
            <v-number-input
              v-model="transferAmount"
              label="Amount"
              required
              :min="0"
              :precision="2"
            ></v-number-input>
            <v-btn
              type="submit"
              color="primary"
              block
              :loading="submitting"
              :disabled="
                !transferFromAccount ||
                !transferToAccount ||
                transferFromAccount === transferToAccount ||
                transferAmount <= 0
              "
            >
              Transfer
            </v-btn>
          </v-form>
        </v-sheet>
      </v-tabs-window-item>
    </v-tabs-window>
  </div>
</template>

<script setup lang="ts">
import { computed, ref, watch } from "vue";
import { useAtm } from "../composables/useAtm";

const { accounts, deposit, withdraw, transfer } = useAtm();

const accountItems = computed(() =>
  accounts.value.map((a) => ({ id: a.id, name: a.name }))
);

const tab = ref("deposit");
const submitting = ref(false);

const depositToAccount = ref<string | null>(null);
const depositAmount = ref(0);
const withdrawFromAccount = ref<string | null>(null);
const withdrawAmount = ref(0);
const transferFromAccount = ref<string | null>(null);
const transferToAccount = ref<string | null>(null);
const transferAmount = ref(0);

// pick sensible defaults once accounts load
watch(
  accounts,
  (list) => {
    if (list.length === 0) return;
    depositToAccount.value ??= list[0].id;
    withdrawFromAccount.value ??= list[0].id;
    transferFromAccount.value ??= list[0].id;
    transferToAccount.value ??= list[1]?.id ?? list[0].id;
  },
  { immediate: true }
);

async function submitDeposit() {
  if (!depositToAccount.value) return;
  submitting.value = true;
  try {
    await deposit(depositToAccount.value, depositAmount.value);
    depositAmount.value = 0;
  } finally {
    submitting.value = false;
  }
}

async function submitWithdraw() {
  if (!withdrawFromAccount.value) return;
  submitting.value = true;
  try {
    await withdraw(withdrawFromAccount.value, withdrawAmount.value);
    withdrawAmount.value = 0;
  } finally {
    submitting.value = false;
  }
}

async function submitTransfer() {
  if (!transferFromAccount.value || !transferToAccount.value) return;
  submitting.value = true;
  try {
    await transfer(
      transferFromAccount.value,
      transferToAccount.value,
      transferAmount.value
    );
    transferAmount.value = 0;
  } finally {
    submitting.value = false;
  }
}
</script>

<style scoped></style>
