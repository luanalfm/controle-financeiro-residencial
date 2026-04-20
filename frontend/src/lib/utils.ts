import { clsx, type ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";

//para o tailwind
export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

//Para formatar o valor para R$
export function formatCurrency(value: number): string {
  return new Intl.NumberFormat("pt-BR", {
    style: "currency",
    currency: "BRL",
  }).format(value);
}
