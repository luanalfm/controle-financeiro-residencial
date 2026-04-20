//As tipagens estão concetradas aqui

//Tipo de propósito de uma categoria 
export type CategoryPurpose = "despesa" | "receita" | "ambas";

//Tipo de transação 
export type TransactionType = "despesa" | "receita";

//Entidade Pessoa 
export interface Person {
  id: string;
  name: string;
  age: number;
}

//Payload para criação/edição de pessoa 
export interface PersonFormData {
  name: string;
  age: number;
}

//Entidade Categoria 
export interface Category {
  id: string;
  description: string;
  purpose: CategoryPurpose;
}

//Payload para criação de categoria 
export interface CategoryFormData {
  description: string;
  purpose: CategoryPurpose;
}

//Entidade Transação 
export interface Transaction {
  id: string;
  description: string;
  amount: number;
  type: TransactionType;
  categoryId: string;
  personId: string;
}

//Payload para criação de transação 
export interface TransactionFormData {
  description: string;
  amount: number;
  type: TransactionType;
  categoryId: string;
  personId: string;
}

//Resumo financeiro por pessoa 
export interface PersonSummary {
  person: Person;
  totalIncome: number;
  totalExpense: number;
  balance: number;
}

//Resumo financeiro geral 
export interface GeneralSummary {
  totalIncome: number;
  totalExpense: number;
  balance: number;
}
