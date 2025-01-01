import { z } from "zod";

import { IsBetween, IsInvalid, IsInvalidPassword, IsRequired } from "@/lib/errors.ts";

export const RegisterSchema = z.object({
  firstName: z
    .string({ message: IsRequired("FirstName") })
    .min(1, {
      message: IsBetween("FirstName", 1, 31),
    })
    .max(31, {
      message: IsBetween("FirstName", 1, 31),
    }),
  lastName: z
    .string({ message: IsRequired("LastName") })
    .min(1, {
      message: IsBetween("LastName", 1, 31),
    })
    .max(31, {
      message: IsBetween("LastName", 1, 31),
    }),
  displayName: z
    .string({ message: IsRequired("DisplayName") })
    .min(3, {
      message: IsBetween("DisplayName", 3, 31),
    })
    .max(31, {
      message: IsBetween("DisplayName", 3, 31),
    }),
  email: z.string({ message: IsRequired("Email") }).email({ message: IsInvalid("Email") }),
  password: z
    .string({ message: IsRequired("Email") })
    .regex(/^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*.{8,}$/, { message: IsInvalidPassword }),
});

export type RegisterSchemaType = z.infer<typeof RegisterSchema>;
