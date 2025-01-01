import { useMemo } from "react";

import { selectAuthenticatedUser } from "@/redux/slices/auth.ts";
import { useSelector } from "react-redux";

export const useAuth = () => {
  const user = useSelector(selectAuthenticatedUser);

  return useMemo(() => ({ user }), [user]);
};
