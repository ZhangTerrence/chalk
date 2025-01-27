import React from "react";

import { Dialog, DialogContent } from "@/components/ui/dialog.tsx";
import { Sidebar, SidebarContent, SidebarRail, SidebarSeparator } from "@/components/ui/sidebar.tsx";

import { DashboardSidebarFooter } from "@/components/DashboardSidebar/DashboardSidebarFooter.tsx";
import { DashboardSidebarHeader } from "@/components/DashboardSidebar/DashboardSidebarHeader.tsx";
import { CoursesSection } from "@/components/DashboardSidebar/Sections/CoursesSection.tsx";
import { DirectMessagesSection } from "@/components/DashboardSidebar/Sections/DirectMessagesSection.tsx";
import { CreateCourseDialog } from "@/components/Dialogs/CreateCourseDialog.tsx";
import { CreateOrganizationDialog } from "@/components/Dialogs/CreateOrganizationDialog.tsx";
import { UpdateAccountDialog } from "@/components/Dialogs/UpdateAccountDialog.tsx";
import { UpdateAppearanceDialog } from "@/components/Dialogs/UpdateAppearanceDialog.tsx";
import { UpdateProfileDialog } from "@/components/Dialogs/UpdateProfileDialog.tsx";

import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

export type DashboardDialogs = {
  open: boolean;
  type: "create-course" | "create-organization" | "update-account" | "update-profile" | "update-appearance" | null;
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

  const [dialog, setDialog] = React.useState<DashboardDialogs>({
    open: false,
    type: null,
  });
  const [context, setContext] = React.useState(user.displayName);

  const changeDialog = (type: Pick<DashboardDialogs, "type">["type"]) => {
    setDialog({
      open: !!type,
      type: type,
    });
  };

  const changeContext = (section: string) => {
    return setContext(section);
  };

  const renderDialogContent = () => {
    switch (dialog.type) {
      case "create-course":
        return <CreateCourseDialog changeDialog={changeDialog} />;
      case "create-organization":
        return <CreateOrganizationDialog />;
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
          type: dialog.type,
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
      <DialogContent>{renderDialogContent()}</DialogContent>
    </Dialog>
  );
};
