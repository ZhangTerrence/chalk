import React from "react";

import { Sidebar, SidebarContent, SidebarRail } from "@/components/ui/sidebar.tsx";

import { DashboardSidebarFooter } from "@/components/DashboardSidebar/DashboardSidebarFooter.tsx";
import { DashboardSidebarHeader } from "@/components/DashboardSidebar/DashboardSidebarHeader.tsx";
import { CoursesSection } from "@/components/DashboardSidebar/Sections/CoursesSection.tsx";
import { DirectMessagesSection } from "@/components/DashboardSidebar/Sections/DirectMessagesSection.tsx";
import { OrganizationsSection } from "@/components/DashboardSidebar/Sections/OrganizationsSection.tsx";

export type DashboardSection = "direct-messages" | "courses" | "organizations";

export const DashboardSidebar = () => {
  const [section, setSection] = React.useState<DashboardSection>(
    (localStorage.getItem("section") as DashboardSection) ?? "direct-messages",
  );

  const changeSection = (section: DashboardSection) => {
    localStorage.setItem("section", section);
    return setSection(section);
  };

  const renderSection = () => {
    switch (section) {
      case "direct-messages":
        return <DirectMessagesSection />;
      case "courses":
        return <CoursesSection />;
      case "organizations":
        return <OrganizationsSection />;
      default:
        return null;
    }
  };

  return (
    <Sidebar>
      <DashboardSidebarHeader section={section} changeSection={changeSection} />
      <SidebarContent>{renderSection()}</SidebarContent>
      <DashboardSidebarFooter />
      <SidebarRail />
    </Sidebar>
  );
};
