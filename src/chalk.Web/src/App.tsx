import { HelmetProvider } from "react-helmet-async";
import { Provider } from "react-redux";
import { BrowserRouter, Route, Routes } from "react-router";
import { Toaster } from "sonner";

import DashboardLayout from "@/pages/layouts/DashboardLayout.tsx";
import Layout from "@/pages/layouts/Layout.tsx";
import PrivateLayout from "@/pages/layouts/PrivateLayout.tsx";

import Courses from "@/pages/Courses.tsx";
import Dashboard from "@/pages/Dashboard.tsx";
import Landing from "@/pages/Landing.tsx";
import Login from "@/pages/Login.tsx";
import NotFound from "@/pages/NotFound.tsx";
import Organizations from "@/pages/Organizations.tsx";
import Register from "@/pages/Register.tsx";
import Settings from "@/pages/Settings.tsx";

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
            <Route element={<PrivateLayout />}>
              <Route path="/dashboard" element={<DashboardLayout />}>
                <Route index element={<Dashboard />} />
              </Route>
              <Route path="/settings" element={<Settings />} />
              <Route path="/courses" element={<Courses />} />
              <Route path="/organizations" element={<Organizations />} />
            </Route>
            <Route path="*" element={<NotFound />} />
          </Routes>
        </BrowserRouter>
        <Toaster />
      </HelmetProvider>
    </Provider>
  );
}
