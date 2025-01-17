import React from "react";

import { LoaderIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";

import { useLazyRefreshQuery } from "@/redux/services/account.ts";
import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

export const AuthenticationGuard = ({ children }: { children: React.ReactNode }) => {
  const user = useTypedSelector(selectUser);
  const [refresh, { isFetching, isError }] = useLazyRefreshQuery();
  const navigate = useNavigate();

  React.useEffect(() => {
    const tryRefresh = async () => {
      await refresh(null).unwrap();
    };

    if (!user) {
      tryRefresh();
    }
  }, [user]);

  React.useEffect(() => {
    if (!isFetching && isError) {
      navigate("/login");
    }
  }, [isFetching, isError]);

  if (isFetching) {
    return <LoaderIcon className="absolute animate-spin" />;
  }

  return user && children;
};
