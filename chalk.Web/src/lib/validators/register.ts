import { z } from "zod";

export const RegisterSchema = z.object({
  firstName: z.string().min(1).max(31),
  lastName: z.string().min(1).max(31),
  displayName: z.string().min(3).max(31),
  email: z.string().email(),
  password: z.string().regex(/^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*.{8,}$/),
});

export type RegisterSchemaType = z.infer<typeof RegisterSchema>;
