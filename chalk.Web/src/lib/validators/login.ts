import { z } from "zod";

export const LoginSchema = z.object({
  email: z.string().email(),
  password: z.string().regex(/^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*.{8,}$/),
});

export type LoginSchemaType = z.infer<typeof LoginSchema>;
