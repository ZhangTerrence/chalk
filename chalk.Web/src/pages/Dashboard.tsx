import { useEffect } from "react";

import { useNavigate } from "react-router-dom";

import { useAuth } from "@/hooks/useAuth.tsx";

export default function Dashboard() {
  const user = useAuth().user;
  const navigate = useNavigate();

  useEffect(() => {
    if (!user) {
      navigate("/login");
    }
  }, [user]);

  return <div>Hello.</div>;
}
