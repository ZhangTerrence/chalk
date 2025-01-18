import React from "react";

import { Dialog, DialogContent } from "@/components/ui/dialog.tsx";
import { Sidebar, SidebarContent, SidebarRail } from "@/components/ui/sidebar.tsx";

import { DashboardSidebarFooter } from "@/components/DashboardSidebar/DashboardSidebarFooter.tsx";
import { DashboardSidebarHeader } from "@/components/DashboardSidebar/DashboardSidebarHeader.tsx";
import { CreateCourseDialog } from "@/components/DashboardSidebar/Dialogs/CreateCourseDialog.tsx";
import { CreateOrganizationDialog } from "@/components/DashboardSidebar/Dialogs/CreateOrganizationDialog.tsx";
import { UpdateAccountDialog } from "@/components/DashboardSidebar/Dialogs/UpdateAccountDialog.tsx";
import { UpdateAppearanceDialog } from "@/components/DashboardSidebar/Dialogs/UpdateAppearanceDialog.tsx";
import { UpdateProfileDialog } from "@/components/DashboardSidebar/Dialogs/UpdateProfileDialog.tsx";
import { CoursesSection } from "@/components/DashboardSidebar/Sections/CoursesSection.tsx";
import { DirectMessagesSection } from "@/components/DashboardSidebar/Sections/DirectMessagesSection.tsx";
import { OrganizationsSection } from "@/components/DashboardSidebar/Sections/OrganizationsSection.tsx";

export type DashboardDialog = {
  open: boolean;
  section: "create-course" | "create-organization" | "update-account" | "update-profile" | "update-appearance" | null;
};
export type DashboardSection = "direct-messages" | "courses" | "organizations";

export const DashboardSidebar = () => {
  const [dialog, setDialog] = React.useState<DashboardDialog>({
    open: false,
    section: null,
  });
  const [section, setSection] = React.useState<DashboardSection>(
    (localStorage.getItem("section") as DashboardSection) ?? "direct-messages",
  );

  const changeDialog = (section: Pick<DashboardDialog, "section">["section"]) => {
    if (section) {
      setDialog({
        open: true,
        section: section,
      });
    } else {
      setDialog({
        open: false,
        section: null,
      });
    }
  };

  const changeSection = (section: DashboardSection) => {
    localStorage.setItem("section", section);
    return setSection(section);
  };

  const renderDialog = () => {
    switch (dialog.section) {
      case "create-course":
        return <CreateCourseDialog changeDialog={changeDialog} />;
      case "create-organization":
        return <CreateOrganizationDialog changeDialog={changeDialog} />;
      case "update-account":
        return <UpdateAccountDialog />;
      case "update-profile":
        return <UpdateProfileDialog changeDialog={changeDialog} />;
      case "update-appearance":
        return <UpdateAppearanceDialog />;
      default:
        return null;
    }
  };

  const renderSection = () => {
    switch (section) {
      case "direct-messages":
        return <DirectMessagesSection />;
      case "courses":
        return <CoursesSection changeDialog={changeDialog} />;
      case "organizations":
        return <OrganizationsSection changeDialog={changeDialog} />;
      default:
        return null;
    }
  };

  return (
    <Dialog
      open={dialog.open}
      onOpenChange={(open) => {
        setDialog({
          open: open,
          section: dialog.section,
        });
      }}
    >
      <Sidebar>
        <DashboardSidebarHeader section={section} changeSection={changeSection} />
        <SidebarContent>{renderSection()}</SidebarContent>
        <DashboardSidebarFooter changeDialog={changeDialog} />
        <SidebarRail />
      </Sidebar>
      <DialogContent>{renderDialog()}</DialogContent>
    </Dialog>
  );
};
