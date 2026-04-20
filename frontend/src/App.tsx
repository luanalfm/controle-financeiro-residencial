//Aqui é basicamente a raiz de tudo, configuramos o uso dos providers e das rotas
import { BrowserRouter } from "react-router-dom";
import { Toaster as Sonner } from "@/components/ui/sonner";
import { TooltipProvider } from "@/components/ui/tooltip";
import AppRoutes from "@/routes/AppRoutes";

const App = () => (
    <TooltipProvider>
      <Sonner />
      <BrowserRouter>
        <AppRoutes />
      </BrowserRouter>
    </TooltipProvider>
);

export default App;
