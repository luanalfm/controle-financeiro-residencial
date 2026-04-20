//Aqui nós temos todas as validações do Zod, para validar os campos dos formulários
import { z } from "zod";

export const personSchema = z.object({
  name: z
    .string()
    .min(2, "Nome deve ter ao menos 2 caracteres")
    .max(200, "O nome deve ter no máximo 200 caracteres"),
  age: z
    .number({ message: "Idade deve ser um número" })
    .int("Idade deve ser um número inteiro")
    .min(1, "Idade mínima é 1")
    .max(150, "Idade inválida"),
});

export const categorySchema = z.object({
  description: z
    .string()
    .min(2, "Descrição deve ter ao menos 2 caracteres")
    .max(100, "Descrição muito longa"),
  purpose: z.enum({ despesa: "despesa", receita: "receita", ambas: "ambas" }, {
    message: "Selecione um propósito válido",
  }),
});

export const transactionSchema = z.object({
  description: z
    .string()
    .min(2, "Descrição deve ter ao menos 2 caracteres")
    .max(200, "Descrição muito longa"),
  amount: z
    .number({ message: "Valor deve ser um número" })
    .positive("Valor deve ser positivo"),
  type: z.enum({ despesa: "despesa", receita: "receita" }, {
    message: "Selecione um tipo válido",
  }),
  categoryId: z.string().min(1, "Selecione uma categoria"),
  personId: z.string().min(1, "Selecione uma pessoa"),
});

export type PersonSchemaType = z.infer<typeof personSchema>;
export type CategorySchemaType = z.infer<typeof categorySchema>;
export type TransactionSchemaType = z.infer<typeof transactionSchema>;
