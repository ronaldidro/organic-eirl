import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

interface PriceInputProps {
  value: string;
  onPriceChange: (price: string) => void;
  disabled?: boolean;
}

export function PriceInput({
  value,
  onPriceChange,
  disabled,
}: PriceInputProps) {
  return (
    <div className="space-y-2">
      <Label htmlFor="unit-price">Precio Unitario</Label>
      <Input
        id="unit-price"
        type="number"
        placeholder="0.00"
        value={value}
        onChange={(e) => onPriceChange(e.target.value)}
        min="0"
        step="0.05"
        disabled={disabled}
      />
    </div>
  );
}
