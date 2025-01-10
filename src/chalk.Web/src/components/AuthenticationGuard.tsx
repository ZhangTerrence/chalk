import * as React from "react";

import { Navigate, useNavigate } from "react-router-dom";

import { useRefreshMutation } from "@/redux/services/base.ts";
import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

export const AuthenticationGuard = ({ children }: { children: React.ReactNode }) => {
  const user = useTypedSelector(selectUser);
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
