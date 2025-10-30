import type { ProductDto } from "../types/product";
import { api } from "./api";

export const productService = {
  getAll: async (): Promise<ProductDto[]> => {
    const response = await api.get("/products");
    return response.data;
  },
};
