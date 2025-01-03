import * as React from "react";

import { Navigate, useNavigate } from "react-router-dom";

import { useRefreshMutation } from "@/redux/services/auth.ts";

import { useAuth } from "@/hooks/useAuth.tsx";

export const AuthenticationGuard = ({ children }: { children: React.ReactNode }) => {
  const user = useAuth().user;
  const navigate = useNavigate();
  const [refresh] = useRefreshMutation();

  const tryRefresh = async () => {
    try {
      await refresh(null).unwrap();
    } catch (error) {
      navigate("/login");
    }
  };

  React.useEffect(() => {
    if (!user) {
      tryRefresh().then();
    }
  }, [user]);

  if (!user) {
    return <Navigate to="/login" />;
  }

  return children;
};
