import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

interface QuantityInputProps {
  value: string;
  onQuantityChange: (quantity: string) => void;
  disabled?: boolean;
}

export function QuantityInput({
  value,
  onQuantityChange,
  disabled,
}: QuantityInputProps) {
  return (
    <div className="space-y-2">
      <Label htmlFor="quantity">Cantidad</Label>
      <Input
        id="quantity"
        type="number"
        placeholder="Cantidad"
        value={value}
        onChange={(e) => onQuantityChange(e.target.value)}
        min="1"
        disabled={disabled}
      />
    </div>
  );
}
