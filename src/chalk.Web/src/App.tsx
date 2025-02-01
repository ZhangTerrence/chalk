import { HelmetProvider } from "react-helmet-async";
import { Provider } from "react-redux";
import { BrowserRouter, Route, Routes } from "react-router";
import { Toaster } from "sonner";

import CourseLayout from "@/pages/layouts/CourseLayout.tsx";
import DashboardLayout from "@/pages/layouts/DashboardLayout.tsx";
import PrivateLayout from "@/pages/layouts/PrivateLayout.tsx";
import RootLayout from "@/pages/layouts/RootLayout.tsx";

import CourseModulesPage from "@/pages/CourseModulesPage.tsx";
import CoursePage from "@/pages/CoursePage.tsx";
import CoursesPage from "@/pages/CoursesPage.tsx";
import DashboardLandingPage from "@/pages/DashboardLandingPage.tsx";
import FilePage from "@/pages/FilePage.tsx";
import LandingPage from "@/pages/LandingPage.tsx";
import LoginPage from "@/pages/LoginPage.tsx";
import NotFoundPage from "@/pages/NotFoundPage.tsx";
import OrganizationsPage from "@/pages/OrganizationsPage.tsx";
import RegisterPage from "@/pages/RegisterPage.tsx";

import { store } from "@/redux/store.ts";
import CourseAssignmentPage from "@/pages/CourseAssignmentPage.tsx";

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
                <Route path="/dashboard" element={<DashboardLandingPage />} />
                <Route element={<CourseLayout />}>
                  <Route path="/courses/:courseId/file/:fileId" element={<FilePage />} />
                  <Route path="/courses/:courseId/assignments" element={<CourseAssignmentPage />} />
                  <Route path="/courses/:courseId/modules" element={<CourseModulesPage />} />
                  <Route path="/courses/:courseId/channels" element={<CourseModulesPage />} />
                  <Route path="/courses/:courseId" element={<CoursePage />} />
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
