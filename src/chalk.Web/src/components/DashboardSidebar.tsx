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
  SidebarHeader,
  SidebarRail,
} from "@/components/ui/sidebar.tsx";

import { DashboardSidebarFooter } from "@/components/DashboardSidebarFooter.tsx";
import { SettingsDialog } from "@/components/SettingsDialog.tsx";

export const DashboardSidebar = () => {
  const [dialog, setDialog] = useState<null | "profile" | "settings">(null);

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
    <Dialog open={!!dialog}>
      <Sidebar>
        <SidebarHeader />
        <SidebarContent>
          <SidebarGroup>
            <SidebarGroupLabel>Direct Messages</SidebarGroupLabel>
            <SidebarGroupAction>
              <Button variant="ghost" size="icon" asChild>
                <NavLink className="h-fit w-fit" to="/organization">
                  <PlusIcon />
                </NavLink>
              </Button>
            </SidebarGroupAction>
          </SidebarGroup>
          <SidebarGroup>
            <SidebarGroupLabel>Organizations</SidebarGroupLabel>
            <SidebarGroupAction>
              <Button variant="ghost" size="icon" asChild>
                <NavLink className="h-fit w-fit" to="/organization">
                  <PlusIcon />
                </NavLink>
              </Button>
            </SidebarGroupAction>
          </SidebarGroup>
          <SidebarGroup>
            <SidebarGroupLabel>Courses</SidebarGroupLabel>
          </SidebarGroup>
        </SidebarContent>
        <DashboardSidebarFooter setDialog={setDialog} />
        <SidebarRail />
      </Sidebar>
      {renderDialog()}
    </Dialog>
  );
};
