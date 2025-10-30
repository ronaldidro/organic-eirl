import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { useOrders } from "@/hooks/useOrders";
import { useState } from "react";
import { Link } from "react-router-dom";
import { toast } from "sonner";
import { DeleteConfirmationDialog } from "../shared/DeleteConfirmationDialog";
import { Pagination } from "../shared/Pagination";

export function OrderList() {
  const [deleteDialog, setDeleteDialog] = useState<{
    open: boolean;
    orderId?: number;
  }>({
    open: false,
  });

  const { orders, loading, pageNumber, setPageNumber, deleteOrder } =
    useOrders();

  const openDeleteDialog = (orderId: number) => {
    setDeleteDialog({ open: true, orderId });
  };

  const closeDeleteDialog = () => {
    setDeleteDialog({ open: false });
  };

  const handleDelete = async () => {
    if (!deleteDialog.orderId) return;

    try {
      await deleteOrder(deleteDialog.orderId);
      toast.success("Pedido eliminado correctamente");
    } catch (error) {
      console.error("Error deleting order:", error);
      toast.error("Error al eliminar el pedido");
    } finally {
      closeDeleteDialog();
    }
  };

  if (loading) {
    return <div className="text-center py-8">Cargando pedidos...</div>;
  }

  return (
    <>
      <Card>
        <CardHeader>
          <CardTitle>Pedidos Registrados</CardTitle>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>ID</TableHead>
                <TableHead>Cliente</TableHead>
                <TableHead>Fecha</TableHead>
                <TableHead>Total</TableHead>
                <TableHead>Acciones</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {orders?.items.length === 0 ? (
                <TableRow>
                  <TableCell
                    colSpan={5}
                    className="text-center py-8 text-gray-500"
                  >
                    No hay pedidos registrados
                  </TableCell>
                </TableRow>
              ) : (
                orders?.items.map((order) => (
                  <TableRow key={order.id}>
                    <TableCell className="font-medium">#{order.id}</TableCell>
                    <TableCell>{order.customerName}</TableCell>
                    <TableCell>
                      {new Date(order.orderDate).toLocaleDateString()}
                    </TableCell>
                    <TableCell className="font-medium">
                      S/ {order.totalPrice.toFixed(2)}
                    </TableCell>
                    <TableCell>
                      <div className="flex space-x-2">
                        <Button asChild variant="outline" size="sm">
                          <Link to={`/pedidos/${order.id}`}>Ver</Link>
                        </Button>
                        <Button
                          variant="destructive"
                          size="sm"
                          onClick={() => openDeleteDialog(order.id)}
                        >
                          Eliminar
                        </Button>
                      </div>
                    </TableCell>
                  </TableRow>
                ))
              )}
            </TableBody>
          </Table>

          <Pagination
            pageNumber={pageNumber}
            totalPages={orders?.totalPages ?? 0}
            onPageChange={setPageNumber}
          />
        </CardContent>
      </Card>

      <DeleteConfirmationDialog
        open={deleteDialog.open}
        onOpenChange={closeDeleteDialog}
        onConfirm={handleDelete}
      />
    </>
  );
}
