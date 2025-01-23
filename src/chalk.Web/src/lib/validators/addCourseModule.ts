import { z } from "zod";

import { inRange } from "@/lib/utils.ts";

export const AddCourseModuleSchema = z.object({
  name: z
    .string({
      message: "The course's name is required.",
    })
    .refine((e) => inRange(e.length, 3, 31), {
      message: "The course's name must have between 3 and 31 characters.",
    }),
  description: z
    .string()
    .max(255, {
      message: "The course's description must have at most 255 characters.",
    })
    .optional(),
});

export type AddCourseModuleType = z.infer<typeof AddCourseModuleSchema>;
