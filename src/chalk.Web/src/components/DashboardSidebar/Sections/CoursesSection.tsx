import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { PlusIcon, UsersIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";

import { DropdownMenu, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu.tsx";
import {
  SidebarGroup,
  SidebarGroupAction,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  useSidebar,
} from "@/components/ui/sidebar.tsx";

import type { DashboardDialog } from "@/components/DashboardSidebar/DashboardSidebar.tsx";

import { selectUserCourses } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

type CoursesSectionProps = {
  changeDialog: (section: Pick<DashboardDialog, "section">["section"]) => void;
};

export const CoursesSection = (props: CoursesSectionProps) => {
  const courses = useTypedSelector(selectUserCourses) ?? [];
  const { isMobile } = useSidebar();
  const navigate = useNavigate();

  return (
    <SidebarGroup>
      <DropdownMenu>
        <DropdownMenuTrigger>
          <SidebarGroupLabel>Courses</SidebarGroupLabel>
          <SidebarGroupAction asChild>
            <PlusIcon />
          </SidebarGroupAction>
        </DropdownMenuTrigger>
        <DropdownMenuContent
          side={isMobile ? "bottom" : "right"}
          align="start"
          sideOffset={isMobile ? 5 : 20}
          className="w-[--radix-dropdown-menu-trigger-width] min-w-56 rounded-lg"
        >
          <DropdownMenuItem onClick={() => navigate("/courses")} className="py-3">
            <div className="flex space-x-2 items-center">
              <UsersIcon />
              <span>Find Organizations</span>
            </div>
          </DropdownMenuItem>
          <DropdownMenuItem onClick={() => props.changeDialog("create-course")} className="py-3">
            <div className="flex space-x-2 items-center">
              <PlusIcon />
              <span>Create New Organization</span>
            </div>
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
      <SidebarGroupContent>
        <SidebarMenu>
          {courses.map((course) => (
            <SidebarMenuItem key={course.name}>
              <SidebarMenuButton asChild>
                <span>{course.name}</span>
              </SidebarMenuButton>
            </SidebarMenuItem>
          ))}
        </SidebarMenu>
      </SidebarGroupContent>
    </SidebarGroup>
  );
};
