import { BrowserRouter, Route, Routes } from "react-router-dom";
import { MainLayout } from "./components/Layout/MainLayout";
import { CreateOrder } from "./pages/CreateOrder/CreateOrder";
import { Home } from "./pages/Home/Home";
import { ViewOrder } from "./pages/ViewOrder/ViewOrder";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<MainLayout />}>
          <Route index element={<Home />} />
          <Route path="pedidos">
            <Route path="nuevo" element={<CreateOrder />} />
            <Route path=":id" element={<ViewOrder />} />
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
