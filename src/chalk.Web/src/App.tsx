import { store } from "@/redux/store.ts";
import { HelmetProvider } from "react-helmet-async";
import { Provider } from "react-redux";
import { BrowserRouter, Route, Routes } from "react-router";
import { Toaster } from "sonner";

import Dashboard from "@/pages/Dashboard.tsx";
import Landing from "@/pages/Landing.tsx";
import Login from "@/pages/Login.tsx";
import NotFound from "@/pages/NotFound.tsx";
import Register from "@/pages/Register.tsx";

import { ProtectedRoute } from "@/components/ProtectedRoute.tsx";

export default function App() {
  const context = {};

  return (
    <Provider store={store}>
      <HelmetProvider context={context}>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<Landing />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route
              path="/dashboard"
              element={
                <ProtectedRoute>
                  <Dashboard />
                </ProtectedRoute>
              }
            />
            <Route path="*" element={<NotFound />} />
          </Routes>
        </BrowserRouter>
        <Toaster />
      </HelmetProvider>
    </Provider>
  );
}
