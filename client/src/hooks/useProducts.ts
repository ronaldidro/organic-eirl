import { productService } from "@/services/productService";
import type { ProductDto } from "@/types/product";
import { useApiData } from "./useApiData";

export function useProducts() {
  return useApiData<ProductDto>(productService.getAll);
}
