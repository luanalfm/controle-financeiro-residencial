//Página de dashboard contendo os retornos das consultas
import { useEffect, useState } from "react";
import StatCard from "@/components/StatCard";
import EmptyState from "@/components/EmptyState";
import { formatCurrency } from "@/lib/utils";
import {
  TrendingUp,
  TrendingDown,
  Wallet,
  LayoutDashboard,
} from "lucide-react";
import { consultasService } from "@/services/searchService";


const DashboardPage = () => {
  const [pessoaData, setPessoaData] = useState<any>(null);
  const [categoriaData, setCategoriaData] = useState<any>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [pessoas, categorias] = await Promise.all([
          consultasService.getTotaisPorPessoa(),
          consultasService.getTotaisPorCategoria(),
        ]);

        setPessoaData(pessoas);
        setCategoriaData(categorias);
      } catch (err) {
        console.error("Erro ao carregar dashboard:", err);
      }
    };

    fetchData();
  }, []);

  const general = pessoaData?.totalGeral;

  return (
    <div>
      <h1 className="page-title">Dashboard</h1>

      {/* 🔹 Resumo geral */}
      <div className="grid grid-cols-1 sm:grid-cols-3 gap-4 mb-8">
        <StatCard
          title="Receitas Totais"
          value={general?.totalReceitas ?? 0}
          icon={<TrendingUp size={18} />}
          variant="income"
        />
        <StatCard
          title="Despesas Totais"
          value={general?.totalDespesas ?? 0}
          icon={<TrendingDown size={18} />}
          variant="expense"
        />
        <StatCard
          title="Saldo Geral"
          value={general?.saldoLiquido ?? 0}
          icon={<Wallet size={18} />}
          variant="balance"
        />
      </div>

      {/* 🔹 Resumo por pessoa */}
      <h2 className="text-lg font-semibold text-foreground mb-4">
        Resumo por Pessoa
      </h2>

      {pessoaData?.porPessoa?.length === 0 ? (
        <EmptyState
          icon={<LayoutDashboard size={24} />}
          title="Nenhum dado ainda"
          description="Cadastre pessoas e transações para ver o resumo financeiro."
        />
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {pessoaData?.porPessoa.map((p: any) => (
            <div key={p.pessoaId} className="stat-card">
              <div className="flex items-center gap-2 mb-4">
                <div className="w-8 h-8 rounded-full bg-primary/10 flex items-center justify-center text-primary font-semibold text-sm">
                  {p.nome.charAt(0).toUpperCase()}
                </div>
                <div>
                  <p className="font-semibold text-foreground text-sm">
                    {p.nome}
                  </p>
                </div>
              </div>

              <div className="space-y-2 text-sm">
                <div className="flex justify-between">
                  <span className="text-muted-foreground">Receitas</span>
                  <span className="text-income">
                    {formatCurrency(p.totalReceitas)}
                  </span>
                </div>

                <div className="flex justify-between">
                  <span className="text-muted-foreground">Despesas</span>
                  <span className="text-expense">
                    {formatCurrency(p.totalDespesas)}
                  </span>
                </div>

                <div className="border-t border-border pt-2 flex justify-between">
                  <span className="text-muted-foreground font-medium">
                    Saldo
                  </span>
                  <span className="font-bold text-primary">
                    {formatCurrency(p.saldo)}
                  </span>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

      {/* 🔹 Resumo por categoria */}
      <h2 className="text-lg font-semibold text-foreground mt-10 mb-4">
        Resumo por Categoria
      </h2>

      {categoriaData?.porCategoria?.length === 0 ? (
        <EmptyState
          icon={<LayoutDashboard size={24} />}
          title="Nenhuma categoria com dados"
          description="Cadastre transações para ver o resumo por categoria."
        />
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {categoriaData?.porCategoria.map((c: any) => (
            <div key={c.descricaoCategoria} className="stat-card">
              <p className="font-semibold text-sm mb-3">
                {c.descricaoCategoria}
              </p>

              <div className="space-y-2 text-sm">
                <div className="flex justify-between">
                  <span className="text-muted-foreground">Receitas</span>
                  <span className="text-income">
                    {formatCurrency(c.totalReceitas)}
                  </span>
                </div>

                <div className="flex justify-between">
                  <span className="text-muted-foreground">Despesas</span>
                  <span className="text-expense">
                    {formatCurrency(c.totalDespesas)}
                  </span>
                </div>

                <div className="border-t pt-2 flex justify-between">
                  <span>Saldo</span>
                  <span className="font-bold">
                    {formatCurrency(c.saldo)}
                  </span>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default DashboardPage;