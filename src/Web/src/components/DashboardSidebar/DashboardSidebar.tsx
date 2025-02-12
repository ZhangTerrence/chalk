import React from "react";

import { Sidebar, SidebarContent, SidebarRail, SidebarSeparator } from "@/components/ui/sidebar.tsx";

import { DashboardSidebarFooter } from "@/components/DashboardSidebar/DashboardSidebarFooter.tsx";
import { DashboardSidebarHeader } from "@/components/DashboardSidebar/DashboardSidebarHeader.tsx";
import { CoursesSection } from "@/components/DashboardSidebar/Sections/CoursesSection.tsx";
import { DirectMessagesSection } from "@/components/DashboardSidebar/Sections/DirectMessagesSection.tsx";

import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

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
  const [context, setContext] = React.useState(user.displayName);

  const changeContext = (section: string) => {
    return setContext(section);
  };

  return (
    <Sidebar>
      <DashboardSidebarHeader contextList={contextList} context={context} changeContext={changeContext} />
      <SidebarContent>
        {context === user.displayName ? (
          <>
            <SidebarSeparator />
            <DirectMessagesSection />
            <SidebarSeparator />
            <CoursesSection />
          </>
        ) : null}
      </SidebarContent>
      <DashboardSidebarFooter />
      <SidebarRail />
    </Sidebar>
  );
};
