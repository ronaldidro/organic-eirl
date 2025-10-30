import { Button } from "@/components/ui/button";
import { Link, Outlet } from "react-router-dom";

export function MainLayout() {
  return (
    <div className="min-h-screen bg-gray-50">
      <header className="bg-white shadow-sm border-b">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex justify-between items-center h-16">
            <Link to="/" className="flex items-center space-x-2">
              <span className="text-xl font-bold text-gray-900">
                🛒 Organic EIRL
              </span>
            </Link>
            <nav className="flex space-x-4">
              <Button asChild variant="ghost">
                <Link to="/">Pedidos</Link>
              </Button>
              <Button asChild>
                <Link to="/pedidos/nuevo">Nuevo</Link>
              </Button>
            </nav>
          </div>
        </div>
      </header>

      <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <Outlet />
      </main>
    </div>
  );
}
