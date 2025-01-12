import { useState } from "react";

import { PlusIcon } from "lucide-react";
import { NavLink } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import {
  Sidebar,
  SidebarContent,
  SidebarGroup,
  SidebarGroupAction,
  SidebarGroupLabel,
  SidebarRail,
} from "@/components/ui/sidebar.tsx";

import { DashboardSidebarFooter } from "@/components/DashboardSidebar/DashboardSidebarFooter.tsx";
import { DashboardSidebarHeader } from "@/components/DashboardSidebar/DashboardSidebarHeader.tsx";
import { CoursesSection } from "@/components/DashboardSidebar/Sections/CoursesSection.tsx";
import { OrganizationsSection } from "@/components/DashboardSidebar/Sections/OrganizationsSection.tsx";

export type Section = "direct-messages" | "courses" | "organizations";

export const DashboardSidebar = () => {
  const [section, setSection] = useState<Section>((localStorage.getItem("section") as Section) ?? "direct-messages");

  const changeSection = (section: Section) => {
    localStorage.setItem("section", section);
    return setSection(section);
  };

  const renderSection = () => {
    switch (section) {
      case "direct-messages":
        return (
          <SidebarGroup>
            <SidebarGroupLabel>Direct Messages</SidebarGroupLabel>
            <SidebarGroupAction>
              <Button variant="ghost" size="icon" asChild>
                <NavLink className="h-fit w-fit" to="/dashboard">
                  <PlusIcon />
                </NavLink>
              </Button>
            </SidebarGroupAction>
          </SidebarGroup>
        );
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
