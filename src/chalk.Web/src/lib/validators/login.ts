import { z } from "zod";

export const LoginSchema = z.object({
  email: z
    .string({
      message: "Email is required."
    })
    .email({
      message: "Email is invalid."
    }),
  password: z
    .string({
      message: "Password is required."
    })
    .regex(/^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*.{8,}$/, {
      message: "Password must have at least 8 characters with least one number, one lowercase letter, one upper case letter, one special character."
    })
});

export type LoginSchemaType = z.infer<typeof LoginSchema>;
