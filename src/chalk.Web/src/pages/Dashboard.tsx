import { Helmet } from "react-helmet-async";

import { SidebarInset, SidebarProvider, SidebarTrigger } from "@/components/ui/sidebar.tsx";

import { DashboardSidebar } from "@/components/DashboardSidebar.tsx";

export default function Dashboard() {
  return (
    <div className="min-h-screen w-screen">
      <Helmet>
        <title>Chalk - Dashboard</title>
      </Helmet>
      <SidebarProvider>
        <DashboardSidebar />
        <SidebarInset className="p-4">
          <SidebarTrigger />
        </SidebarInset>
      </SidebarProvider>
    </div>
  );
}
