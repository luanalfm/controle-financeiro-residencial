//Página de Transações, com as funções criar e listar.
import { useState, useMemo, useEffect } from "react";
import { useForm, useWatch } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useAppStore } from "@/store/useAppStore";
import { transactionSchema, type TransactionSchemaType } from "@/utils/schemas";
import { formatCurrency } from "@/lib/utils";
import Modal from "@/components/Modal";
import { FormInput, FormSelect } from "@/components/FormFields";
import EmptyState from "@/components/EmptyState";
import { ArrowLeftRight, Plus, AlertTriangle } from "lucide-react";
import { toast } from "sonner";

const TransactionsPage = () => {
  const { transactions, persons, categories, addTransaction } = useAppStore();
  const [modalOpen, setModalOpen] = useState(false);

  const {
    register,
    handleSubmit,
    reset,
    control,
    formState: { errors, isSubmitting },
  } = useForm<TransactionSchemaType>({
    resolver: zodResolver(transactionSchema),
  });

  // Observa mudanças em personId e type para aplicar a regra de menor idade
  const selectedPersonId = useWatch({ control, name: "personId" });
  const selectedType = useWatch({ control, name: "type" });

  const selectedPerson = persons.find((p) => p.id === selectedPersonId);

//Regra: menor de idade só pode criar despesa.
//Se o person selecionado for menor, desabilitamos "receita".
  const isMinor = selectedPerson ? selectedPerson.age < 18 : false;

//Regra: filtrar categorias compatíveis com o tipo selecionado.
//despesa → categories com "purpose" = "despesa" ou "ambas"
//receita → categories com "purpose" = "receita" ou "ambas"
  const filteredCategories = useMemo(() => {
    if (!selectedType) return categories;
    return categories.filter(
      (cat) => cat.purpose === selectedType || cat.purpose === "ambas"
    );
  }, [categories, selectedType]);

  const onSubmit = async (data: TransactionSchemaType) => {
    await addTransaction(data);
    toast.success("Transação criada com sucesso!");
    setModalOpen(false);
    reset();
  };

  const openModal = () => {
    reset({
      description: "",
      amount: undefined as unknown as number,
      type: undefined as unknown as "despesa",
      categoryId: "",
      personId: "",
    });
    setModalOpen(true);
  };

    const fetchTransactions = useAppStore((state) => state.fetchTransactions);
    const fetchPersons = useAppStore((state) => state.fetchPersons);
    const fetchCategories = useAppStore((state) => state.fetchCategories);
    
useEffect(() => {
  fetchTransactions();
  fetchPersons();       
  fetchCategories();    
}, [fetchTransactions, fetchPersons, fetchCategories]);

  //Para encontrar o nome por ID, já que a API retorna o id 
  const getPersonName = (id: string) =>
    persons.find((p) => p.id === id)?.name || "—";
  const getCategoryName = (id: string) =>
    categories.find((c) => c.id === id)?.description || "—";

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <h1 className="page-title mb-0">Transações</h1>
        <button
          onClick={openModal}
          className="flex items-center gap-2 px-4 py-2.5 bg-primary text-primary-foreground rounded-lg text-sm font-medium hover:opacity-90 transition-opacity"
        >
          <Plus size={16} />
          Nova Transação
        </button>
      </div>

      {transactions.length === 0 ? (
        <EmptyState
          icon={<ArrowLeftRight size={24} />}
          title="Nenhuma transação registrada"
          description="Registre receitas e despesas para acompanhar suas finanças."
          action={
            <button
              onClick={openModal}
              className="text-sm text-primary font-medium hover:underline"
            >
              Adicionar transação
            </button>
          }
        />
      ) : (
        <div className="data-table overflow-x-auto">
          <table className="w-full min-w-[600px]">
            <thead>
              <tr className="border-b border-border">
                <th className="text-left px-4 py-3 text-xs font-semibold text-muted-foreground uppercase tracking-wider">
                  Descrição
                </th>
                <th className="text-left px-4 py-3 text-xs font-semibold text-muted-foreground uppercase tracking-wider">
                  Tipo
                </th>
                <th className="text-left px-4 py-3 text-xs font-semibold text-muted-foreground uppercase tracking-wider">
                  Pessoa
                </th>
                <th className="text-left px-4 py-3 text-xs font-semibold text-muted-foreground uppercase tracking-wider">
                  Categoria
                </th>
                <th className="text-right px-4 py-3 text-xs font-semibold text-muted-foreground uppercase tracking-wider">
                  Valor
                </th>
              </tr>
            </thead>
            <tbody>
              {transactions.map((t) => (
                <tr
                  key={t.id}
                  className="border-b border-border last:border-0 hover:bg-muted/50 transition-colors"
                >
                  <td className="px-4 py-3 text-sm font-medium text-foreground">
                    {t.description}
                  </td>
                  <td className="px-4 py-3">
                    <span
                      className={`px-2.5 py-1 text-xs font-medium rounded-full ${
                        t.type === "receita"
                          ? "bg-income/10 text-income"
                          : "bg-expense/10 text-expense"
                      }`}
                    >
                      {t.type === "receita" ? "Receita" : "Despesa"}
                    </span>
                  </td>
                  <td className="px-4 py-3 text-sm text-muted-foreground">
                    {getPersonName(t.personId)}
                  </td>
                  <td className="px-4 py-3 text-sm text-muted-foreground">
                    {getCategoryName(t.categoryId)}
                  </td>
                  <td
                    className={`px-4 py-3 text-sm font-semibold text-right ${
                      t.type === "receita" ? "text-income" : "text-expense"
                    }`}
                  >
                    {t.type === "despesa" ? "- " : "+ "}
                    {formatCurrency(t.amount)}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {/* Modal nova transação */}
      <Modal
        open={modalOpen}
        onClose={() => setModalOpen(false)}
        title="Nova Transação"
      >
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <FormSelect
            label="Pessoa"
            placeholder="Selecione uma pessoa"
            error={errors.personId?.message}
            options={persons.map((p) => ({ value: p.id, label: `${p.name} (${p.age} anos)` }))}
            {...register("personId")}
          />

          {/* Aviso para menor de idade */}
          {isMinor && (
            <div className="flex items-center gap-2 px-3 py-2 rounded-lg bg-warning/10 text-warning text-xs font-medium">
              <AlertTriangle size={14} />
              Menores de 18 anos só podem criar despesas.
            </div>
          )}

          <FormSelect
            label="Tipo"
            placeholder="Selecione o tipo"
            error={errors.type?.message}
            options={
              isMinor
                ? [{ value: "despesa", label: "Despesa" }]
                : [
                    { value: "despesa", label: "Despesa" },
                    { value: "receita", label: "Receita" },
                  ]
            }
            {...register("type")}
          />

          <FormSelect
            label="Categoria"
            placeholder="Selecione uma categoria"
            error={errors.categoryId?.message}
            options={filteredCategories.map((c) => ({
              value: c.id,
              label: c.description,
            }))}
            {...register("categoryId")}
          />

          {filteredCategories.length === 0 && selectedType && (
            <p className="text-xs text-destructive">
              Nenhuma categoria disponível para este tipo de transação.
            </p>
          )}

          <FormInput
            label="Descrição"
            placeholder="Ex: Conta de luz"
            error={errors.description?.message}
            {...register("description")}
          />
          <FormInput
            label="Valor (R$)"
            type="number"
            step="0.01"
            min="0.01"
            placeholder="0,00"
            error={errors.amount?.message}
            {...register("amount", { valueAsNumber: true })}
          />

          <div className="flex justify-end gap-2 pt-2">
            <button
              type="button"
              onClick={() => setModalOpen(false)}
              className="px-4 py-2.5 text-sm font-medium text-muted-foreground hover:text-foreground rounded-lg border border-border hover:bg-muted transition-colors"
            >
              Cancelar
            </button>
            <button
              type="submit"
              disabled={isSubmitting}
              className="px-4 py-2.5 text-sm font-medium bg-primary text-primary-foreground rounded-lg hover:opacity-90 transition-opacity disabled:opacity-50"
            >
              {isSubmitting ? "Salvando..." : "Criar"}
            </button>
          </div>
        </form>
      </Modal>
    </div>
  );
};

export default TransactionsPage;
