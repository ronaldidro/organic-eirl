import { Label } from "@/components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { useProducts } from "@/hooks/useProducts";

interface ProductSelectorProps {
  value: number;
  onProductChange: (productId: number) => void;
  disabled?: boolean;
}

export function ProductSelector({
  value,
  onProductChange,
  disabled,
}: ProductSelectorProps) {
  const { data: products, loading } = useProducts();

  return (
    <div className="space-y-2">
      <Label htmlFor="product-select">Producto</Label>
      <Select
        value={value > 0 ? value.toString() : ""}
        onValueChange={(val) => onProductChange(Number(val))}
        disabled={disabled || loading}
      >
        <SelectTrigger id="product-select" className="w-full">
          <SelectValue
            placeholder={loading ? "Cargando..." : "Seleccionar producto"}
          />
        </SelectTrigger>
        <SelectContent>
          {products.map((product) => (
            <SelectItem key={product.id} value={product.id.toString()}>
              {product.description}
            </SelectItem>
          ))}
        </SelectContent>
      </Select>
    </div>
  );
}
