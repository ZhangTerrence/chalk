import { HelmetProvider } from "react-helmet-async";
import { Provider } from "react-redux";
import { BrowserRouter, Route, Routes } from "react-router";
import { Toaster } from "sonner";

import CoursesPage from "@/pages/(private)/courses/page.tsx";
import DashboardLayout from "@/pages/(private)/dashboard/layout.tsx";
import DashboardPage from "@/pages/(private)/dashboard/page.tsx";
import PrivateLayout from "@/pages/(private)/layout.tsx";
import OrganizationsPage from "@/pages/(private)/organizations/page.tsx";
import SettingsPage from "@/pages/(private)/settings/page.tsx";
import RootLayout from "@/pages/layout.tsx";
import LoginPage from "@/pages/login/page.tsx";
import NotFoundPage from "@/pages/notFound";
import LandingPage from "@/pages/page.tsx";
import RegisterPage from "@/pages/register/page.tsx";

import { store } from "@/redux/store.ts";

export default function App() {
  const context = {};

  return (
    <Provider store={store}>
      <HelmetProvider context={context}>
        <BrowserRouter>
          <Routes>
            <Route element={<RootLayout />}>
              <Route index element={<LandingPage />} />
              <Route path="/login" element={<LoginPage />} />
              <Route path="/register" element={<RegisterPage />} />
            </Route>
            <Route element={<PrivateLayout />}>
              <Route path="/dashboard" element={<DashboardLayout />}>
                <Route index element={<DashboardPage />} />
              </Route>
              <Route path="/settings" element={<SettingsPage />} />
              <Route path="/courses" element={<CoursesPage />} />
              <Route path="/organizations" element={<OrganizationsPage />} />
            </Route>
            <Route path="*" element={<NotFoundPage />} />
          </Routes>
        </BrowserRouter>
        <Toaster />
      </HelmetProvider>
    </Provider>
  );
}
