import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { ChevronDownIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from "@/components/ui/collapsible.tsx";
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu.tsx";
import {
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarMenuSub,
  SidebarMenuSubItem,
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
    <Collapsible defaultOpen className="group/collapsible [&[data-state=open]>div>button>svg:first-child]:rotate-180">
      <SidebarGroup className="gap-y-2">
        <SidebarGroupLabel className="underline" asChild>
          <CollapsibleTrigger className="flex justify-between">
            Courses
            <ChevronDownIcon className="ml-auto transition-transform" />
          </CollapsibleTrigger>
        </SidebarGroupLabel>
        <CollapsibleContent>
          <SidebarGroupContent>
            <SidebarMenu className="gap-y-2">
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
              {courses.map((course) => (
                <Collapsible
                  key={course.name}
                  defaultOpen={false}
                  className={`group/collapsible [&[data-state=open]>li>button>svg:last-child]:rotate-180`}
                >
                  <SidebarMenuItem>
                    <SidebarMenuButton className="px-2" asChild>
                      <CollapsibleTrigger>
                        <span className="text-nowrap text-ellipsis w-[90%] overflow-x-clip">
                          {course.code && course.code + " - "}
                          {course.name}
                        </span>
                        <ChevronDownIcon className={`ml-auto transition-transform`} />
                      </CollapsibleTrigger>
                    </SidebarMenuButton>
                    <CollapsibleContent>
                      <SidebarMenuSub>
                        <SidebarMenuSubItem className="p-2">Home</SidebarMenuSubItem>
                      </SidebarMenuSub>
                    </CollapsibleContent>
                  </SidebarMenuItem>
                </Collapsible>
              ))}
            </SidebarMenu>
          </SidebarGroupContent>
        </CollapsibleContent>
      </SidebarGroup>
    </Collapsible>
  );
};
