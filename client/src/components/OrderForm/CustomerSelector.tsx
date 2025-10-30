import { Label } from "@/components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { useCustomers } from "@/hooks/useCustomers";

interface CustomerSelectorProps {
  value: number;
  onChange: (customerId: number) => void;
  disabled?: boolean;
}

export function CustomerSelector({
  value,
  onChange,
  disabled,
}: CustomerSelectorProps) {
  const { data: customers, loading } = useCustomers();

  return (
    <div className="space-y-2">
      <Label htmlFor="customer">Cliente</Label>
      <Select
        value={value > 0 ? value?.toString() : ""}
        onValueChange={(val) => onChange(Number(val))}
        disabled={disabled || loading}
      >
        <SelectTrigger className="w-full">
          <SelectValue
            placeholder={loading ? "Cargando..." : "Seleccionar cliente"}
          />
        </SelectTrigger>
        <SelectContent>
          {customers.map((customer) => (
            <SelectItem key={customer.id} value={customer.id.toString()}>
              {customer.fullName}
            </SelectItem>
          ))}
        </SelectContent>
      </Select>
    </div>
  );
}
