//Rotas do sistema, aqui acessamos cada página/page criada e definimos a prinicipal(que é o Dashboard)
import { Routes, Route } from "react-router-dom";
import Layout from "@/components/Layout";
import DashboardPage from "@/pages/DashboardPage";
import PersonsPage from "@/pages/PersonsPage";
import CategoriesPage from "@/pages/CategoriesPage";
import TransactionsPage from "@/pages/TransactionsPage";
import NotFound from "@/pages/NotFound";

const AppRoutes = () => (
  <Layout>
    <Routes>
      <Route path="/" element={<DashboardPage />} />
      <Route path="/pessoas" element={<PersonsPage />} />
      <Route path="/categorias" element={<CategoriesPage />} />
      <Route path="/transacoes" element={<TransactionsPage />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  </Layout>
);

export default AppRoutes;
