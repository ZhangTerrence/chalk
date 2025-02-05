import React from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { LoaderIcon } from "lucide-react";
import { Helmet } from "react-helmet-async";
import { useForm } from "react-hook-form";
import { NavLink, useNavigate } from "react-router-dom";
import { toast } from "sonner";

import { Button } from "@/components/ui/button.tsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";

import { useRegisterMutation } from "@/redux/services/account.ts";
import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

import { RegisterSchema, type RegisterSchemaType } from "@/lib/validators/register.ts";

export default function RegisterPage() {
  const user = useTypedSelector(selectUser);

  const [register, { isLoading, isSuccess }] = useRegisterMutation();
  const navigate = useNavigate();

  React.useEffect(() => {
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

  React.useEffect(() => {
    if (!isLoading && isSuccess) {
      form.reset();
      toast.success("Successfully registered account.");
    }
  }, [isLoading, isSuccess]);

  return (
    <>
      <Helmet>
        <title>Register</title>
      </Helmet>
      {isLoading && <LoaderIcon className="absolute animate-spin" />}
      <main className="flex w-1/2 flex-col gap-y-4 max-xl:w-3/4 max-sm:w-full max-sm:p-4">
        <h1 className="text-2xl underline">
          <strong>Register</strong>
        </h1>
        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(async (data) => await register(data).unwrap())}
            className="flex flex-col gap-y-4"
          >
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
          <NavLink to="/login" className="hover:underline">
            here
          </NavLink>
          .
        </p>
      </main>
    </>
  );
}
