import { z } from "zod";

import { IsInvalid, IsInvalidPassword, IsRequired } from "@/lib/errors.ts";

export const LoginSchema = z.object({
  email: z.string({ message: IsRequired("Email") }).email({ message: IsInvalid("Email") }),
  password: z
    .string({ message: IsRequired("Email") })
    .regex(/^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*.{8,}$/, { message: IsInvalidPassword }),
});

export type LoginSchemaType = z.infer<typeof LoginSchema>;
