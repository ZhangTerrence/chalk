import { useEffect } from "react";

import { useRegisterMutation } from "@/redux/services/auth.ts";
import { zodResolver } from "@hookform/resolvers/zod";
import { Helmet } from "react-helmet-async";
import { useForm } from "react-hook-form";
import { NavLink, useNavigate } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";

import { Header } from "@/components/Header.tsx";

import { RegisterSchema, type RegisterSchemaType } from "@/lib/validators/register.ts";

import { useAuth } from "@/hooks/useAuth.tsx";

export default function Register() {
  const user = useAuth().user;
  const navigate = useNavigate();

  useEffect(() => {
    if (user) {
      navigate("/dashboard");
    }
  }, [user]);

  const form = useForm<RegisterSchemaType>({
    resolver: zodResolver(RegisterSchema),
    defaultValues: {
      firstName: "",
      lastName: "",
      displayName: "",
      email: "",
      password: "",
    },
  });

  const [register, { isLoading }] = useRegisterMutation();

  const onSubmit = async (data: RegisterSchemaType) => {
    await register(data).unwrap();
  };

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="min-h-screen w-screen flex items-center justify-center">
      <Helmet>
        <title>Chalk - Register</title>
      </Helmet>
      <Header />
      <main className="flex flex-col gap-y-4 w-1/3">
        <h1 className="text-2xl underline">
          <strong>Register</strong>
        </h1>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="flex flex-col gap-y-4">
            <div className="flex gap-x-4">
              <FormField
                control={form.control}
                name="firstName"
                render={({ field }) => (
                  <FormItem className="grow">
                    <FormLabel>First Name</FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="lastName"
                render={({ field }) => (
                  <FormItem className="grow">
                    <FormLabel>Last Name</FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <FormField
              control={form.control}
              name="displayName"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Display Name</FormLabel>
                  <FormControl>
                    <Input {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
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
          Already have an account? Login{" "}
          <NavLink className="hover:underline" to="/login">
            here
          </NavLink>
          .
        </p>
      </main>
    </div>
  );
}
