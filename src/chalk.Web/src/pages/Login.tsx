import { useEffect } from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { LoaderIcon } from "lucide-react";
import { Helmet } from "react-helmet-async";
import { useForm } from "react-hook-form";
import { NavLink, useNavigate } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";

import { useLoginMutation } from "@/redux/services/auth.ts";

import { LoginSchema, type LoginSchemaType } from "@/lib/validators/login.ts";

import { useAuth } from "@/hooks/useAuth.tsx";

export default function Login() {
  const user = useAuth().user;
  const navigate = useNavigate();

  useEffect(() => {
    if (user) {
      navigate("/dashboard");
    }
  }, [user]);

  const form = useForm<LoginSchemaType>({
    resolver: zodResolver(LoginSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  });

  const [login, { isLoading }] = useLoginMutation();

  const onSubmit = async (data: LoginSchemaType) => {
    await login(data).unwrap();
  };

  return (
    <>
      <Helmet>
        <title>Chalk - Login</title>
      </Helmet>
      {isLoading && <LoaderIcon className="absolute" />}
      <main className="flex flex-col gap-y-4 w-1/2 max-xl:w-3/4 max-sm:w-full max-sm:p-4">
        <h1 className="text-2xl underline">
          <strong>Login</strong>
        </h1>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="flex flex-col gap-y-4 min-w-80">
            <FormField
              control={form.control}
              name="email"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Email</FormLabel>
                  <FormControl>
                    <Input {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="password"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Password</FormLabel>
                  <FormControl>
                    <Input {...field} type={"password"} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <Button type="submit">Submit</Button>
          </form>
        </Form>
        <p className="self-center">
          Don't have an account? Register{" "}
          <NavLink className="hover:underline" to="/register">
            here
          </NavLink>
          .
        </p>
      </main>
    </>
  );
}
