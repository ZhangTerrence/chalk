import React from "react";

import { Navigate, useNavigate } from "react-router-dom";

import { useLazyRefreshQuery } from "@/redux/services/account.ts";
import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

export const AuthenticationGuard = ({ children }: { children: React.ReactNode }) => {
  const user = useTypedSelector(selectUser);
  const [refresh] = useLazyRefreshQuery();
  const navigate = useNavigate();

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
