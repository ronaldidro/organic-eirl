import { orderService } from "@/services/orderService";
import type { Order, PagedResult } from "@/types/order";
import { useEffect, useState } from "react";

interface UseOrdersResult {
  orders: PagedResult<Order> | undefined;
  loading: boolean;
  pageNumber: number;
  setPageNumber: (page: number) => void;
  deleteOrder: (id: number) => Promise<void>;
  loadOrders: () => Promise<void>;
}

export function useOrders(
  initialPage: number = 1,
  pageSize: number = 5
): UseOrdersResult {
  const [orders, setOrders] = useState<PagedResult<Order>>();
  const [loading, setLoading] = useState(true);
  const [pageNumber, setPageNumber] = useState(initialPage);

  const loadOrders = async () => {
    try {
      setLoading(true);
      const result = await orderService.getAll(pageNumber, pageSize);
      setOrders(result);
    } catch (error) {
      console.error("Error loading orders:", error);
    } finally {
      setLoading(false);
    }
  };

  const deleteOrder = async (id: number) => {
    await orderService.delete(id);

    if (pageNumber > 1) {
      setPageNumber(1);
    } else {
      await loadOrders();
    }
  };

  useEffect(() => {
    loadOrders();
  }, [pageNumber]);

  return {
    orders,
    loading,
    pageNumber,
    setPageNumber,
    deleteOrder,
    loadOrders,
  };
}
