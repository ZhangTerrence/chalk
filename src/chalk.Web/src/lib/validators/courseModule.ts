import { z } from "zod";

import { inRange } from "@/lib/utils.ts";

export const CreateCourseModuleSchema = z.object({
  name: z
    .string({
      message: "The course module's name is required.",
    })
    .refine((e) => inRange(e.length, 3, 31), {
      message: "The course module's name must have between 3 and 31 characters.",
    }),
  description: z
    .string()
    .max(255, {
      message: "The course module's description must have at most 255 characters.",
    })
    .optional(),
});
export const UpdateCourseModuleSchema = CreateCourseModuleSchema;

export type CreateCourseModuleType = z.infer<typeof CreateCourseModuleSchema>;
export type UpdateCourseModuleType = z.infer<typeof UpdateCourseModuleSchema>;
