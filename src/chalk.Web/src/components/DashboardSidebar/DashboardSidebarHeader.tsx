import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select.tsx";
import { SidebarHeader, SidebarMenu, SidebarMenuButton, SidebarMenuItem } from "@/components/ui/sidebar.tsx";

import type { DashboardSection } from "@/components/DashboardSidebar/DashboardSidebar.tsx";

type DashboardSidebarHeaderProps = {
  section: DashboardSection;
  changeSection: (section: DashboardSection) => void;
};

export const DashboardSidebarHeader = (props: DashboardSidebarHeaderProps) => {
  return (
    <SidebarHeader className="mt-2">
      <SidebarMenu>
        <SidebarMenuItem>
          <Select
            defaultValue={props.section}
            onValueChange={(section) => props.changeSection(section as DashboardSection)}
          >
            <SidebarMenuButton asChild>
              <SelectTrigger>
                <SelectValue />
              </SelectTrigger>
            </SidebarMenuButton>
            <SelectContent className="w-[--radix-popper-anchor-width]">
              <SelectItem value="direct-messages">Direct Messages</SelectItem>
              <SelectItem value="courses">Courses</SelectItem>
              <SelectItem value="organizations">Organizations</SelectItem>
            </SelectContent>
          </Select>
        </SidebarMenuItem>
      </SidebarMenu>
    </SidebarHeader>
  );
};
