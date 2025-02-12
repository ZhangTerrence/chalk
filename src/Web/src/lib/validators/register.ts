import { z } from "zod";

import { inRange } from "@/lib/utils.ts";

export const RegisterSchema = z.object({
  firstName: z
    .string({
      message: "The user's first name is required.",
    })
    .refine((e) => inRange(e.length, 1, 31), {
      message: "The user's first name must have between 1 and 31 characters.",
    }),
  lastName: z
    .string({
      message: "The user's last name is required.",
    })
    .refine((e) => inRange(e.length, 1, 31), {
      message: "The user's last name must have between 1 and 31 characters.",
    }),
  displayName: z
    .string({
      message: "The user's display name is required.",
    })
    .refine((e) => inRange(e.length, 3, 31), {
      message: "The user's display name must have between 3 and 31 characters.",
    }),
  email: z
    .string({
      message: "The user's email is required.",
    })
    .email({
      message: "The user's is invalid.",
    }),
  password: z
    .string({
      message: "The user's password is required.",
    })
    .regex(/^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*.{8,}$/, {
      message:
        "The user's password must have at least 8 characters with least one number, one lowercase letter, one upper case letter, one special character.",
    }),
});

export type RegisterSchemaType = z.infer<typeof RegisterSchema>;
