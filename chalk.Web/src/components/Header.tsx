import { Button } from "@/components/ui/button";

export const Header = () => {
  return (
    <header className="w-screen fixed top-0">
      <nav className="flex justify-between p-4">
        <Button>Chalk</Button>
        <div className="flex gap-x-2">
          <Button>Login</Button>
          <Button>Register</Button>
        </div>
      </nav>
    </header>
  );
};
