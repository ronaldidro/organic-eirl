import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { useOrderProducts } from "@/hooks/useOrderProducts";
import { orderService } from "@/services/orderService";
import type { CreateOrderRequest } from "@/types/order";
import { yupResolver } from "@hookform/resolvers/yup";
import axios from "axios";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";
import * as yup from "yup";
import { CustomerSelector } from "./CustomerSelector";
import { OrderSummary } from "./OrderSummary";
import { ProductAdder } from "./ProductAdder/ProductAdder";
import { ProductTable } from "./ProductTable";

const orderSchema = yup.object({
  customerId: yup
    .number()
    .required("Cliente es requerido")
    .min(1, "Debe seleccionar un cliente"),
  orderDate: yup
    .string()
    .required("Fecha del pedido es requerida")
    .test("is-valid-date", "Fecha debe ser válida", (value) => {
      if (!value) return false;
      return !isNaN(new Date(value).getTime());
    })
    .test("not-past", "La fecha no puede ser pasada", (value) => {
      if (!value) return true;

      const [year, month, day] = value.split("-").map(Number);
      const inputDate = new Date(year, month - 1, day);

      const today = new Date();
      today.setHours(0, 0, 0, 0);

      return inputDate >= today;
    }),
});

interface OrderFormProps {
  mode: "create" | "view";
  initialData?: CreateOrderRequest;
}

export function OrderForm({ mode, initialData }: OrderFormProps) {
  const navigate = useNavigate();
  const {
    products,
    total,
    handleAddProduct,
    handleRemoveProduct,
    handleUpdateProduct,
  } = useOrderProducts(initialData?.orderDetails);
  const [submitting, setSubmitting] = useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    watch,
  } = useForm({
    resolver: yupResolver(orderSchema),
    defaultValues: {
      customerId: initialData?.customerId || 0,
      orderDate: initialData?.orderDate || "",
    },
  });

  const customerId = watch("customerId");

  const onSubmit = async (data: { customerId: number; orderDate: string }) => {
    const totalPrice = products.reduce(
      (sum, product) => sum + product.subtotal,
      0
    );

    const orderData: CreateOrderRequest = {
      orderDate: data.orderDate,
      customerId: data.customerId,
      totalPrice: Number(totalPrice.toFixed(2)),
      orderDetails: products,
    };

    try {
      setSubmitting(true);
      await orderService.create(orderData);
      toast.success("Pedido creado exitosamente");
      navigate("/");
    } catch (error: unknown) {
      console.error("Error creating order:", error);
      if (axios.isAxiosError(error) && error.response?.data?.errors) {
        toast.error((error.response.data.errors as string[]).join(", "));
      } else {
        toast.error("Error al crear el pedido");
      }
    } finally {
      setSubmitting(false);
    }
  };

  const isViewMode = mode === "view";

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Información del Pedido</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label className="text-sm font-medium">Fecha del Pedido</label>
              <input
                type="date"
                {...register("orderDate")}
                className="w-full border rounded px-3 py-2 mt-1"
                disabled={isViewMode}
              />
              {errors.orderDate && (
                <p className="text-red-500 text-sm">
                  {errors.orderDate.message}
                </p>
              )}
            </div>
            <div>
              <CustomerSelector
                value={customerId}
                onChange={(value) =>
                  setValue("customerId", value, { shouldValidate: true })
                }
                disabled={isViewMode}
              />
              {errors.customerId && (
                <p className="text-red-500 text-sm">
                  {errors.customerId.message}
                </p>
              )}
            </div>
          </div>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Productos</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          {!isViewMode && (
            <ProductAdder
              onAddProduct={handleAddProduct}
              disabled={isViewMode}
            />
          )}

          <ProductTable
            products={products}
            onRemoveProduct={handleRemoveProduct}
            onUpdateProduct={handleUpdateProduct}
            disabled={isViewMode}
          />
        </CardContent>
      </Card>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        <div className="lg:col-span-2"></div>
        <div>
          <OrderSummary total={total} />
        </div>
      </div>

      {!isViewMode && (
        <div className="flex justify-end space-x-4">
          <Button
            type="button"
            variant="outline"
            onClick={() => navigate("/")}
            disabled={submitting}
          >
            Cancelar
          </Button>
          <Button type="submit" disabled={submitting}>
            {submitting ? "Creando..." : "Crear Pedido"}
          </Button>
        </div>
      )}

      {isViewMode && (
        <div className="flex justify-end">
          <Button type="button" variant="outline" onClick={() => navigate("/")}>
            Volver
          </Button>
        </div>
      )}
    </form>
  );
}
