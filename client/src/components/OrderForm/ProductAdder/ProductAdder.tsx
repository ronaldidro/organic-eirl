import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { useProducts } from "@/hooks/useProducts";
import type { OrderDetailItem } from "@/types/order";
import { useState } from "react";
import { toast } from "sonner";
import { PriceInput } from "./PriceInput";
import { ProductSelector } from "./ProductSelector";
import { QuantityInput } from "./QuantityInput";

interface ProductAdderProps {
  onAddProduct: (product: OrderDetailItem) => void;
  disabled?: boolean;
}

export function ProductAdder({ onAddProduct, disabled }: ProductAdderProps) {
  const { data: products } = useProducts();
  const [selectedProductId, setSelectedProductId] = useState<number>(0);
  const [quantity, setQuantity] = useState<string>("1");
  const [unitPrice, setUnitPrice] = useState<string>("");

  const handleAddProduct = () => {
    const numQuantity = Number(quantity);
    const numUnitPrice = Number(unitPrice);

    if (!selectedProductId || numQuantity <= 0 || numUnitPrice <= 0) {
      toast.error("Por favor complete todos los campos del producto");
      return;
    }

    const productSelected = products.find(
      (product) => product.id === selectedProductId
    );

    const productItem: OrderDetailItem = {
      productId: selectedProductId,
      productDescription: productSelected?.description || "",
      quantity: numQuantity,
      unitPrice: numUnitPrice,
      subtotal: Number((numQuantity * numUnitPrice).toFixed(2)),
    };

    onAddProduct(productItem);

    setSelectedProductId(0);
    setQuantity("1");
    setUnitPrice("");
  };

  const isAddButtonDisabled =
    disabled || !selectedProductId || !quantity || !unitPrice;

  return (
    <div className="space-y-4 p-4 border rounded-lg bg-gray-50">
      <Label className="text-lg font-semibold">Agregar Producto</Label>

      <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
        <div className="md:col-span-2">
          <ProductSelector
            value={selectedProductId}
            onProductChange={setSelectedProductId}
            disabled={disabled}
          />
        </div>

        <QuantityInput
          value={quantity}
          onQuantityChange={setQuantity}
          disabled={disabled}
        />

        <PriceInput
          value={unitPrice}
          onPriceChange={setUnitPrice}
          disabled={disabled}
        />
      </div>

      <Button
        type="button"
        onClick={handleAddProduct}
        disabled={isAddButtonDisabled}
        className="w-full"
      >
        Agregar Producto
      </Button>
    </div>
  );
}
