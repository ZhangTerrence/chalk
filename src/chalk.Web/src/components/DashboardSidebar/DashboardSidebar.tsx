import { useState } from "react";

import { PlusIcon } from "lucide-react";
import { NavLink } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog.tsx";
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
import { SettingsDialog } from "@/components/DashboardSidebar/Dialogs/SettingsDialog.tsx";
import { OrganizationsSection } from "@/components/DashboardSidebar/Sections/OrganizationsSection.tsx";

export type Dialog = null | "profile" | "settings";
export type Section = "direct-messages" | "courses" | "organizations";

export const DashboardSidebar = () => {
  const [section, setSection] = useState<Section>((localStorage.getItem("section") as Section) ?? "direct-messages");
  const [dialog, setDialog] = useState<Dialog>(null);

  const changeSection = (section: Section) => {
    localStorage.setItem("section", section);
    return setSection(section);
  };

  const changeDialog = (dialog: Dialog) => {
    return setDialog(dialog);
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
      case "organizations":
        return <OrganizationsSection />;
      case "courses":
        return (
          <SidebarGroup>
            <SidebarGroupLabel>Courses</SidebarGroupLabel>
            <SidebarGroupAction>
              <Button variant="ghost" size="icon" asChild>
                <NavLink className="h-fit w-fit" to="/dashboard">
                  <PlusIcon />
                </NavLink>
              </Button>
            </SidebarGroupAction>
          </SidebarGroup>
        );
      default:
        return null;
    }
  };

  const renderDialog = () => {
    switch (dialog) {
      case "profile":
        return (
          <DialogContent>
            <DialogHeader>
              <DialogTitle>Profile</DialogTitle>
              <DialogDescription>
                This action cannot be undone. Are you sure you want to permanently delete this file from our servers?
              </DialogDescription>
            </DialogHeader>
            <DialogFooter>
              <Button onClick={() => setDialog(null)}>Confirm</Button>
            </DialogFooter>
          </DialogContent>
        );
      case "settings":
        return <SettingsDialog setDialog={setDialog} />;
      default:
        return null;
    }
  };

  return (
    <Dialog open={!!dialog} onOpenChange={() => setDialog(dialog)}>
      <Sidebar>
        <DashboardSidebarHeader section={section} changeSection={changeSection} />
        <SidebarContent>{renderSection()}</SidebarContent>
        <DashboardSidebarFooter changeDialog={changeDialog} />
        <SidebarRail />
      </Sidebar>
      {renderDialog()}
    </Dialog>
  );
};
