import { z } from "zod";

import { inRange } from "@/lib/utils.ts";

export const CreateModuleSchema = z.object({
  name: z
    .string({
      message: "The module's name is required.",
    })
    .refine((e) => inRange(e.length, 3, 31), {
      message: "The module's name must have between 3 and 31 characters.",
    }),
  description: z
    .string()
    .max(255, {
      message: "The module's description must have at most 255 characters.",
    })
    .optional(),
});
export const UpdateModuleSchema = CreateModuleSchema;

export type CreateModuleType = z.infer<typeof CreateModuleSchema>;
export type UpdateModuleType = z.infer<typeof UpdateModuleSchema>;
