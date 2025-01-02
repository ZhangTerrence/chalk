import { HelmetProvider } from "react-helmet-async";
import { Provider } from "react-redux";
import { BrowserRouter, Route, Routes } from "react-router";
import { Toaster } from "sonner";

import DashboardLayout from "@/pages/layouts/DashboardLayout.tsx";
import Layout from "@/pages/layouts/Layout.tsx";

import Dashboard from "@/pages/Dashboard.tsx";
import Landing from "@/pages/Landing.tsx";
import Login from "@/pages/Login.tsx";
import NotFound from "@/pages/NotFound.tsx";
import Register from "@/pages/Register.tsx";

import { ProtectedRoute } from "@/components/ProtectedRoute.tsx";

import { store } from "@/redux/store.ts";

export default function App() {
  const context = {};

  return (
    <Provider store={store}>
      <HelmetProvider context={context}>
        <BrowserRouter>
          <Routes>
            <Route element={<Layout />}>
              <Route index element={<Landing />} />
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
            </Route>
            <Route
              path="/dashboard"
              element={
                <ProtectedRoute>
                  <DashboardLayout />
                </ProtectedRoute>
              }
            >
              <Route index element={<Dashboard />} />
            </Route>
            <Route path="*" element={<NotFound />} />
          </Routes>
        </BrowserRouter>
        <Toaster />
      </HelmetProvider>
    </Provider>
  );
}
