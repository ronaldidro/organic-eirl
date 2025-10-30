import { OrderForm } from "@/components/OrderForm/OrderForm";
import { Button } from "@/components/ui/button";
import { orderService } from "@/services/orderService";
import type { CreateOrderRequest, Order } from "@/types/order";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "sonner";

export function ViewOrder() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [order, setOrder] = useState<Order | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (id) {
      loadOrder(Number(id));
    }
  }, [id]);

  const loadOrder = async (orderId: number) => {
    try {
      const orderData = await orderService.getById(orderId);
      setOrder(orderData);
    } catch (error) {
      console.error("Error loading order:", error);
      toast.error("Error al cargar el pedido");
      navigate("/");
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return <div className="text-center py-8">Cargando pedido...</div>;
  }

  if (!order) {
    return (
      <div className="text-center py-8">
        <p>Pedido no encontrado</p>
        <Button onClick={() => navigate("/")} className="mt-4">
          Volver a la lista
        </Button>
      </div>
    );
  }

  const initialData: CreateOrderRequest = {
    orderDate: order.orderDate?.split("T")[0],
    customerId: order.customerId,
    totalPrice: order.totalPrice,
    orderDetails: order.orderDetails.map((detail) => ({
      productId: detail.productId,
      productDescription: detail.productDescription,
      quantity: detail.quantity,
      unitPrice: detail.unitPrice,
      subtotal: detail.subtotal,
    })),
  };

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-gray-900">Pedido #{order.id}</h1>
        <Button onClick={() => navigate("/")} variant="outline">
          Volver a la lista
        </Button>
      </div>
      <OrderForm mode="view" initialData={initialData} />
    </div>
  );
}
