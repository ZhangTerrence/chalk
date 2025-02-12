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
  code: z
    .string()
    .max(31, {
      message: "The course's code must have at most 31 characters.",
    })
    .optional(),
  description: z
    .string()
    .max(255, {
      message: "The course's description must have at most 255 characters.",
    })
    .optional(),
  isPublic: z.boolean({
    message: "Must specify whether the course is public.",
  }),
});
export const UpdateCourseSchema = CreateCourseSchema;

export const CreateAssignmentGroupSchema = z.object({
  name: z
    .string({
      message: "The assignment group's name is required.",
    })
    .refine((e) => inRange(e.length, 3, 31), {
      message: "The assignment group's name must have between 3 and 31 characters.",
    }),
  description: z
    .string()
    .max(255, {
      message: "The assignment group's description must have at most 255 characters.",
    })
    .optional(),
  weight: z
    .number({
      message: "The assignment group's weight is required.",
    })
    .gte(0, {
      message: "The assignment group's weight must be greater than or equal to 0%.",
    })
    .lte(100, {
      message: "The assignment group's weight must be less than 100%.",
    }),
});
export const UpdateAssignmentGroupSchema = CreateAssignmentGroupSchema;

export const CreateAssignmentSchema = z.object({
  name: z
    .string({
      message: "The assignment's name is required.",
    })
    .refine((e) => inRange(e.length, 3, 31), {
      message: "The assignment's name must have between 3 and 31 characters.",
    }),
  description: z
    .string()
    .max(255, {
      message: "The assignment's description must have at most 255 characters.",
    })
    .optional(),
  isOpen: z.boolean({
    message: "Must specify whether the assignment is open for submission.",
  }),
  dueDate: z
    .date()
    .min(new Date(), {
      message: "The assignment's due date must be after the current date.",
    })
    .optional()
    .or(z.literal(undefined)),
  allowedAttempts: z
    .string()
    .refine(
      (e) => {
        if (!e) {
          return true;
        }
        const value = Number.parseInt(e);
        return !Number.isNaN(value) && inRange(value, 1);
      },
      {
        message: "The assignment's maximum allowed attempts must be greater than or equal to 1.",
      },
    )
    .optional()
    .or(z.literal(undefined)),
});
export const UpdateAssignmentSchema = CreateAssignmentSchema;

export type CreateCourseType = z.infer<typeof CreateCourseSchema>;
export type UpdateCourseType = z.infer<typeof UpdateCourseSchema>;
export type CreateAssignmentGroupType = z.infer<typeof CreateAssignmentGroupSchema>;
export type UpdateAssignmentGroupType = z.infer<typeof UpdateAssignmentGroupSchema>;
export type CreateAssignmentType = z.infer<typeof CreateAssignmentSchema>;
export type UpdateAssignmentType = z.infer<typeof UpdateAssignmentSchema>;
