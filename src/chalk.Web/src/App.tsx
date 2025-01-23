import { HelmetProvider } from "react-helmet-async";
import { Provider } from "react-redux";
import { BrowserRouter, Route, Routes } from "react-router";
import { Toaster } from "sonner";

import CourseLayout from "@/pages/(private)/(dashboard)/courses/[id]/layout.tsx";
import CourseModulesPage from "@/pages/(private)/(dashboard)/courses/[id]/modules/page.tsx";
import CoursePage from "@/pages/(private)/(dashboard)/courses/[id]/page.tsx";
import DashboardPage from "@/pages/(private)/(dashboard)/dashboard/page.tsx";
import DashboardLayout from "@/pages/(private)/(dashboard)/layout.tsx";
import CoursesPage from "@/pages/(private)/courses/page.tsx";
import PrivateLayout from "@/pages/(private)/layout.tsx";
import OrganizationsPage from "@/pages/(private)/organizations/page.tsx";
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
              <Route path="/" element={<LandingPage />} />
              <Route path="/login" element={<LoginPage />} />
              <Route path="/register" element={<RegisterPage />} />
            </Route>
            <Route element={<PrivateLayout />}>
              <Route element={<DashboardLayout />}>
                <Route path="/dashboard" element={<DashboardPage />} />
                <Route element={<CourseLayout />}>
                  <Route path="/courses/:id/modules" element={<CourseModulesPage />} />
                  <Route path="/courses/:id" element={<CoursePage />} />
                </Route>
              </Route>
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
