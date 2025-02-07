export interface Order {
    orderId: number,
    orderDate: string,
    buyerEmail: string,
    shippingAddress: ShippingAddress,
    deliveryMethod: string,
    deliveryFee: number,
    paymentSummary: PaymentSummary,
    orderItems: OrderItem[],
    subTotal: number,
    discount?: number,
    status: string,
    total: number,
    paymentIntentId: string,
  }
  
  export interface ShippingAddress {
    name: string,
    line1: string,
    line12?: string,
    country: string,
    city: string,
    state: string,
    postalCode: string,
  }
  
  export interface PaymentSummary {
    last4: number,
    brand: string,
    expMonth: number,
    expYear: number,
  }
  
  export interface OrderItem {
    productId: number,
    productName: string,
    pictureUrl: string,
    price: number,
    quantity: number,
  }

  export interface OrderToCreate {
    cartId: string,
    deliveryMethodId: number,
    shippingAddress: ShippingAddress,
    paymentSummary: PaymentSummary,
    discount?: number
  }