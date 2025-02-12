import { z } from "zod";

import { inRange } from "@/lib/utils.ts";

export const UpdateUserSchema = z.object({
  firstName: z.string().refine((e) => inRange(e.length, 1, 31), {
    message: "The user's first name must have between 1 and 31 characters.",
  }),
  lastName: z.string().refine((e) => inRange(e.length, 1, 31), {
    message: "The user's last name must have between 1 and 31 characters.",
  }),
  displayName: z.string().refine((e) => inRange(e.length, 3, 31), {
    message: "The user's display name must have between 3 and 31 characters.",
  }),
  description: z
    .string()
    .max(255, {
      message: "The user's description must have at most 255 characters.",
    })
    .optional(),
});

export type UpdateUserType = z.infer<typeof UpdateUserSchema>;
