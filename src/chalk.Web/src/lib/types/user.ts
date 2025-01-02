import type { ChannelDTO } from "@/lib/types/channel.ts";
import type { CourseDTO } from "@/lib/types/course.ts";
import type { OrganizationDTO } from "@/lib/types/organization.ts";

export type UserResponse = {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  displayName: string;
  profilePicture?: string;
  createdDate: string;
  updatedDate: string;
  organizations: OrganizationDTO[];
  courses: CourseDTO[];
  channels: ChannelDTO[];
};
