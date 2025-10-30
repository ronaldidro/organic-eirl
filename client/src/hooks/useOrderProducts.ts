import type { OrderDetailItem } from "@/types/order";
import { useState } from "react";

export function useOrderProducts(initialProducts?: OrderDetailItem[]) {
  const [products, setProducts] = useState(initialProducts || []);

  const handleAddProduct = (product: OrderDetailItem) => {
    setProducts((prev) => [...prev, product]);
  };

  const handleRemoveProduct = (index: number) => {
    setProducts((prev) => prev.filter((_, i) => i !== index));
  };

  const handleUpdateProduct = (
    index: number,
    updatedProduct: OrderDetailItem
  ) => {
    setProducts((prev) =>
      prev.map((product, i) => (i === index ? updatedProduct : product))
    );
  };

  const total = products.reduce((sum, product) => sum + product.subtotal, 0);

  return {
    products,
    total,
    handleAddProduct,
    handleRemoveProduct,
    handleUpdateProduct,
  };
}
