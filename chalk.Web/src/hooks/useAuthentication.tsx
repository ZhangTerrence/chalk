import { useMemo } from "react";

import { selectAuthenticatedUser } from "@/redux/slices/authentication.ts";
import { useSelector } from "react-redux";

export const useAuthentication = () => {
  const authenticatedUser = useSelector(selectAuthenticatedUser);

  return useMemo(() => ({ authenticatedUser }), [authenticatedUser]);
};
