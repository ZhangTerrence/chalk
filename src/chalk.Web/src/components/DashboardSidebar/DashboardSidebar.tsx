import React from "react";

import { Dialog, DialogContent } from "@/components/ui/dialog.tsx";
import { Sidebar, SidebarContent, SidebarRail, SidebarSeparator } from "@/components/ui/sidebar.tsx";

import { DashboardSidebarFooter } from "@/components/DashboardSidebar/DashboardSidebarFooter.tsx";
import { DashboardSidebarHeader } from "@/components/DashboardSidebar/DashboardSidebarHeader.tsx";
import { CreateCourseDialog } from "@/components/DashboardSidebar/Dialogs/CreateCourseDialog.tsx";
import { CreateOrganizationDialog } from "@/components/DashboardSidebar/Dialogs/CreateOrganizationDialog.tsx";
import { UpdateAccountDialog } from "@/components/DashboardSidebar/Dialogs/UpdateAccountDialog.tsx";
import { UpdateAppearanceDialog } from "@/components/DashboardSidebar/Dialogs/UpdateAppearanceDialog.tsx";
import { UpdateProfileDialog } from "@/components/DashboardSidebar/Dialogs/UpdateProfileDialog.tsx";
import { CoursesSection } from "@/components/DashboardSidebar/Sections/CoursesSection.tsx";
import { DirectMessagesSection } from "@/components/DashboardSidebar/Sections/DirectMessagesSection.tsx";

import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

export type DashboardDialog = {
  open: boolean;
  section: "create-course" | "create-organization" | "update-account" | "update-profile" | "update-appearance" | null;
};

export const DashboardSidebar = () => {
  const user = useTypedSelector(selectUser)!;

  const contextList: { [key: string]: { id: number } } = {};
  contextList[user.displayName] = {
    id: user.id,
  };
  user.organizations.forEach((organization) => {
    contextList[organization.name] = {
      id: organization.id,
    };
  });

  const [dialog, setDialog] = React.useState<DashboardDialog>({
    open: false,
    section: null,
  });
  const [context, setContext] = React.useState<string>(user.displayName);

  const changeDialog = (section: Pick<DashboardDialog, "section">["section"]) => {
    setDialog({
      open: !!section,
      section: section,
    });
  };

  const changeContext = (section: string) => {
    return setContext(section);
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
        <DashboardSidebarHeader contextList={contextList} context={context} changeContext={changeContext} />
        <SidebarContent>
          {context === user.displayName ? (
            <>
              <SidebarSeparator />
              <DirectMessagesSection />
              <SidebarSeparator />
              <CoursesSection changeDialog={changeDialog} />
            </>
          ) : null}
        </SidebarContent>
        <DashboardSidebarFooter changeDialog={changeDialog} />
        <SidebarRail />
      </Sidebar>
      <DialogContent>{renderDialog()}</DialogContent>
    </Dialog>
  );
};
