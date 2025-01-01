import { useEffect } from "react";

import { useLogoutMutation } from "@/redux/services/auth.ts";
import { useNavigate } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";

import { useAuth } from "@/hooks/useAuth.tsx";

export default function Dashboard() {
  const user = useAuth().user;
  const navigate = useNavigate();

  useEffect(() => {
    if (!user) {
      navigate("/login");
    }
  }, [user]);

  const [logout] = useLogoutMutation();

  if (!user) {
    return <div>Forbidden.</div>;
  }

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
