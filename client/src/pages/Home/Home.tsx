import { OrderList } from "@/components/OrderList/OrderList";

export function Home() {
  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Lista de Pedidos</h1>
      </div>
      <OrderList />
    </div>
  );
}
