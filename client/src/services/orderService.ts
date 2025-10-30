import type { CreateOrderRequest, Order, PagedResult } from "../types/order";
import { api } from "./api";

export const orderService = {
  getAll: async (
    pageNumber: number = 1,
    pageSize: number = 5
  ): Promise<PagedResult<Order>> => {
    const response = await api.get(
      `/orders?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
    return response.data;
  },

  getById: async (id: number): Promise<Order> => {
    const response = await api.get(`/orders/${id}`);
    return response.data;
  },

  create: async (order: CreateOrderRequest): Promise<Order> => {
    const response = await api.post("/orders", order);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/orders/${id}`);
  },
};
