export interface OrderDetailItem {
  productId: number;
  productDescription?: string;
  quantity: number;
  unitPrice: number;
  subtotal: number;
}

export interface OrderDetail {
  productId: number;
  productDescription?: string;
  quantity: number;
  unitPrice: number;
  subtotal: number;
}

export interface Order {
  id: number;
  orderDate: string;
  customerId: number;
  customerName: string;
  totalPrice: number;
  created: string;
  orderDetails: OrderDetail[];
}

export interface CreateOrderRequest {
  orderDate: string;
  customerId: number;
  totalPrice: number;
  orderDetails: OrderDetailItem[];
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  hasPrevious: boolean;
  hasNext: boolean;
}
