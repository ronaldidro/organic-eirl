import { Button } from "@/components/ui/button";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import type { OrderDetailItem } from "@/types/order";

interface ProductTableProps {
  products: OrderDetailItem[];
  onRemoveProduct: (index: number) => void;
  onUpdateProduct: (index: number, product: OrderDetailItem) => void;
  disabled?: boolean;
}

export function ProductTable({
  products,
  onRemoveProduct,
  onUpdateProduct,
  disabled,
}: ProductTableProps) {
  if (products.length === 0) {
    return (
      <div className="text-center py-8 text-gray-500 border rounded-lg">
        No hay productos agregados
      </div>
    );
  }

  const handleQuantityChange = (index: number, newQuantity: number) => {
    const product = products[index];
    const updatedProduct: OrderDetailItem = {
      ...product,
      quantity: newQuantity,
      subtotal: newQuantity * product.unitPrice,
    };
    onUpdateProduct(index, updatedProduct);
  };

  const handleUnitPriceChange = (index: number, newUnitPrice: number) => {
    const product = products[index];
    const updatedProduct: OrderDetailItem = {
      ...product,
      unitPrice: newUnitPrice,
      subtotal: product.quantity * newUnitPrice,
    };
    onUpdateProduct(index, updatedProduct);
  };

  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead>Producto</TableHead>
          <TableHead>Cantidad</TableHead>
          <TableHead>Precio Unit.</TableHead>
          <TableHead>Subtotal</TableHead>
          {!disabled && <TableHead>Acciones</TableHead>}
        </TableRow>
      </TableHeader>
      <TableBody>
        {products.map((product, index) => (
          <TableRow key={index}>
            <TableCell className="font-medium">
              {product.productDescription || `Producto ${product.productId}`}
            </TableCell>
            <TableCell>
              <input
                type="number"
                value={product.quantity}
                onChange={(e) =>
                  handleQuantityChange(index, Number(e.target.value))
                }
                min="1"
                className="w-20 border rounded px-2 py-1"
                disabled={disabled}
              />
            </TableCell>
            <TableCell>
              <input
                type="number"
                value={product.unitPrice}
                onChange={(e) =>
                  handleUnitPriceChange(index, Number(e.target.value))
                }
                min="0"
                step="0.01"
                className="w-24 border rounded px-2 py-1"
                disabled={disabled}
              />
            </TableCell>
            <TableCell className="font-medium">
              S/ {product.subtotal.toFixed(2)}
            </TableCell>
            {!disabled && (
              <TableCell>
                <Button
                  type="button"
                  variant="destructive"
                  size="sm"
                  onClick={() => onRemoveProduct(index)}
                  disabled={disabled}
                >
                  Retirar
                </Button>
              </TableCell>
            )}
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}
