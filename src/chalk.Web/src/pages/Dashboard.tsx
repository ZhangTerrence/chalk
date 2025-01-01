import { useLogoutMutation } from "@/redux/services/auth.ts";

import { Button } from "@/components/ui/button.tsx";

import { useAuth } from "@/hooks/useAuth.tsx";

export default function Dashboard() {
  const user = useAuth().user!;
  const [logout] = useLogoutMutation();

  return (
    <div className="min-h-screen w-screen flex p-4 items-center flex-col">
      <h1>
        <strong>
          {user.firstName} {user.lastName}
        </strong>
      </h1>
      <h2>{user.displayName}</h2>
      <Button onClick={() => logout(null)}>Logout</Button>
    </div>
  );
}
