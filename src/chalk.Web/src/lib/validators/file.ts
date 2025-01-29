import { z } from "zod";

import { inRange } from "@/lib/utils.ts";

export const CreateFileSchema = z.object({
  name: z
    .string({
      message: "The file's name is required.",
    })
    .refine((e) => inRange(e.length, 3, 31), {
      message: "The file's name must have between 3 and 31 characters.",
    }),
  description: z
    .string()
    .max(255, {
      message: "The file's description must have at most 255 characters.",
    })
    .optional(),
});

export type CreateFileType = z.infer<typeof CreateFileSchema>;
