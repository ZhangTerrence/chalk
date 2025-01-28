import { z } from "zod";

import { inRange } from "@/lib/utils.ts";

export const CreateCourseSchema = z.object({
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
  code: z
    .string()
    .max(31, {
      message: "The course's code must have at most 31 characters.",
    })
    .optional(),
  isPublic: z.boolean({
    message: "Must specify whether the course is public.",
  }),
});

export const UpdateCourseSchema = CreateCourseSchema;

export type CreateCourseType = z.infer<typeof CreateCourseSchema>;
export type UpdateCourseType = z.infer<typeof UpdateCourseSchema>;
