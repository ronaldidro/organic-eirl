import { OrderForm } from "@/components/OrderForm/OrderForm";

export function CreateOrder() {
  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-gray-900">Nuevo Pedido</h1>
      </div>
      <OrderForm mode="create" />
    </div>
  );
}
