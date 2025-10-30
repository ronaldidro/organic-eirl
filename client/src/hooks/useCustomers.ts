import { customerService } from "@/services/customerService";
import type { CustomerDto } from "@/types/customer";
import { useApiData } from "./useApiData";

export function useCustomers() {
  return useApiData<CustomerDto>(customerService.getAll);
}
