export interface Product{
    id : string,
    name: string,
    description: string,
    category: string,
    availableQuantity: number,
    imageUrl: string,
    price: number,
    discount: number,
    specification?: string
}