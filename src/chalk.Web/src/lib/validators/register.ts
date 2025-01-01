import { z } from "zod";

export const RegisterSchema = z.object({
  firstName: z
    .string({
      message: "First name is required."
    })
    .min(1, {
      message: "First name must have between 1 and 31 characters."
    })
    .max(31, {
      message: "First name must have between 1 and 31 characters."
    }),
  lastName: z
    .string({
      message: "Last name is required."
    })
    .min(1, {
      message: "Last name must have between 1 and 31 characters."
    })
    .max(31, {
      message: "Last name must have between 1 and 31 characters."
    }),
  displayName: z
    .string({
      message: "Display name is required."
    })
    .min(3, {
      message: "Display name must have between 1 and 31 characters."
    })
    .max(31, {
      message: "Display name must have between 1 and 31 characters."
    }),
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

export type RegisterSchemaType = z.infer<typeof RegisterSchema>;
