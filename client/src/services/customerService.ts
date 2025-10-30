import type { CustomerDto } from "../types/customer";
import { api } from "./api";

export const customerService = {
  getAll: async (): Promise<CustomerDto[]> => {
    const response = await api.get("/customers");
    return response.data;
  },
};
