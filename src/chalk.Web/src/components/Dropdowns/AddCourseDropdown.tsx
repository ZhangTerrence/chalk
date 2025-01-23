import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { useNavigate } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu.tsx";
import { SidebarMenuItem, useSidebar } from "@/components/ui/sidebar.tsx";

import type { DashboardDialogs } from "@/components/DashboardSidebar/DashboardSidebar.tsx";

type AddCourseDropdownProps = {
  changeDialog: (type: Pick<DashboardDialogs, "type">["type"]) => void;
};

export const AddCourseDropdown = (props: AddCourseDropdownProps) => {
  const { isMobile } = useSidebar();
  const navigate = useNavigate();

  return (
    <DropdownMenu>
      <DropdownMenuTrigger className="focus:outline-none" asChild>
        <SidebarMenuItem>
          <Button variant="outline" className="w-full">
            Add
          </Button>
        </SidebarMenuItem>
      </DropdownMenuTrigger>
      <DropdownMenuContent
        side={isMobile ? "bottom" : "right"}
        align="start"
        sideOffset={isMobile ? 5 : 20}
        className="w-[--radix-dropdown-menu-trigger-width] min-w-56 rounded-lg"
      >
        <DropdownMenuItem onClick={() => navigate("/courses")} className="py-3">
          <span>Find Courses</span>
        </DropdownMenuItem>
        <DropdownMenuItem onClick={() => props.changeDialog("create-course")} className="py-3">
          <span>Create New Course</span>
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
};
