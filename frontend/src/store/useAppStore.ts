//Aqui nós temos um Store global usando Zustand
//Bastante útil para atualizarmos informações e deixarmos o componentes das páginas dinâmicas
import { create } from "zustand";
import type {
  Person,
  PersonFormData,
  Category,
  CategoryFormData,
  Transaction,
  TransactionFormData,
} from "@/types";
import { personService } from "@/services/personService";
import { categoryService } from "@/services/categoryService";
import { transactionService } from "@/services/transactionService";

interface AppState {
  persons: Person[];
  categories: Category[];
  transactions: Transaction[];
  loadingPersons: boolean;
  loadingCategories: boolean;
  loadingTransactions: boolean;
  error: string | null;

  fetchPersons: () => Promise<void>;
  addPerson: (data: PersonFormData) => Promise<void>;
  updatePerson: (id: string, data: PersonFormData) => Promise<void>;
  deletePerson: (id: string) => Promise<void>;

  fetchCategories: () => Promise<void>;
  addCategory: (data: CategoryFormData) => Promise<void>;

  fetchTransactions: () => Promise<void>;
  addTransaction: (data: TransactionFormData) => Promise<void>;

  clearError: () => void;
}

export const useAppStore = create<AppState>((set) => ({
  persons: [],
  categories: [],
  transactions: [],
  loadingPersons: false,
  loadingCategories: false,
  loadingTransactions: false,
  error: null,

  clearError: () => set({ error: null }),

  //Chamando o serviço de Pessoas

fetchPersons: async () => {
  set({ loadingPersons: true });

  try {
    const persons = await personService.getAll();
    set({ persons });
  } catch (error: any) {
    set({ error: error.message });
  } finally {
    set({ loadingPersons: false });
  }
},

addPerson: async (data) => {
  set({ loadingPersons: true });

  try {
    const newPerson = await personService.create(data);

    set((s) => ({
      persons: [...s.persons, newPerson],
    }));
  } catch (error: any) {
    set({ error: error.message });
  } finally {
    set({ loadingPersons: false });
  }
},

updatePerson: async (id, data) => {
  set({ loadingPersons: true });

  try {
    const updated = await personService.update(id, data);

    set((s) => ({
      persons: s.persons.map((p) =>
        p.id === id ? updated : p
      ),
    }));
  } catch (error: any) {
    set({ error: error.message });
  } finally {
    set({ loadingPersons: false });
  }
},

deletePerson: async (id) => {
  set({ loadingPersons: true });

  try {
    await personService.remove(id);

    set((s) => ({
      persons: s.persons.filter((p) => p.id !== id),
      transactions: s.transactions.filter((t) => t.personId !== id),
    }));
  } catch (error: any) {
    set({ error: error.message });
  } finally {
    set({ loadingPersons: false });
  }
},

  //Chamando o serviço de Categorias

fetchCategories: async () => {
  set({ loadingCategories: true });

  try {
    const categories = await categoryService.getAll();
    set({ categories });
  } catch (error: any) {
    set({ error: error.message });
  } finally {
    set({ loadingCategories: false });
  }
},

addCategory: async (data) => {
  set({ loadingCategories: true });

  try {
    const newCategory = await categoryService.create(data);

    set((s) => ({
      categories: [...s.categories, newCategory],
    }));
  } catch (error: any) {
    set({ error: error.message });
  } finally {
    set({ loadingCategories: false });
  }
},

  //Chamando o serviço de Transações

fetchTransactions: async () => {
  set({ loadingTransactions: true });

  try {
    const transactions = await transactionService.getAll();
    set({ transactions });
  } catch (error: any) {
    set({ error: error.message });
  } finally {
    set({ loadingTransactions: false });
  }
},

addTransaction: async (data) => {
  set({ loadingTransactions: true });

  try {
    const newTransaction = await transactionService.create(data);

    set((s) => ({
      transactions: [...s.transactions, newTransaction],
    }));
  } catch (error: any) {
    set({ error: error.message });
  } finally {
    set({ loadingTransactions: false });
  }
},
}));
