import { useState } from "react";

import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { PlusIcon, UsersIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";

import { Dialog, DialogTrigger } from "@/components/ui/dialog.tsx";
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

import { CreateCourseDialog } from "@/components/DashboardSidebar/Dialogs/CreateCourseDialog.tsx";

import { selectUserCourses } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

export const CoursesSection = () => {
  const [open, setOpen] = useState(false);
  const courses = useTypedSelector(selectUserCourses) ?? [];
  const { isMobile } = useSidebar();
  const navigate = useNavigate();

  const close = () => {
    setOpen(false);
  };

  return (
    <SidebarGroup>
      <Dialog open={open} onOpenChange={setOpen}>
        <DropdownMenu>
          <DropdownMenuTrigger>
            <SidebarGroupLabel>Courses</SidebarGroupLabel>
            <SidebarGroupAction asChild>
              <PlusIcon />
            </SidebarGroupAction>
          </DropdownMenuTrigger>
          <DropdownMenuContent
            className="w-[--radix-dropdown-menu-trigger-width] min-w-56 rounded-lg"
            side={isMobile ? "bottom" : "right"}
            align="start"
            sideOffset={isMobile ? 5 : 20}
          >
            <DropdownMenuItem className="py-3" onClick={() => navigate("/courses")}>
              <UsersIcon />
              <span>Find Courses</span>
            </DropdownMenuItem>
            <DropdownMenuItem className="py-3" asChild>
              <DialogTrigger className="flex w-full">
                <PlusIcon />
                <span>Create New Course</span>
              </DialogTrigger>
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
        <CreateCourseDialog close={close} />
      </Dialog>
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
