import { useMemo } from "react";

import { selectAuthenticatedUser } from "@/redux/slices/auth.ts";

import { useStore } from "@/hooks/useStore.tsx";

export const useAuth = () => {
  const [useTypedSelector] = useStore();
  const user = useTypedSelector(selectAuthenticatedUser);

  return useMemo(() => ({ user }), [user]);
};
