import { z } from "zod";

export const LoginSchema = z.object({
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
    })
    .transform((e) => e ?? null),
});

export type LoginSchemaType = z.infer<typeof LoginSchema>;
