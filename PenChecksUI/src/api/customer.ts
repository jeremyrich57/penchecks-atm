import { useFetch } from "../composables/useFetch";
import type { Customer } from "../types";

export const getCustomer = () => useFetch<Customer>("/api/customer");
