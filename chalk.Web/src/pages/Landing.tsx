import { Header } from "@/components/Header.tsx";

export default function Landing() {
  return (
    <div className="min-h-screen w-screen flex items-center justify-center">
      <Header />
      <div className="flex flex-col gap-y-2 items-center">
        <h1 className="text-4xl">
          <strong>Chalk</strong>
        </h1>
        <p>A learning management system.</p>
      </div>
    </div>
  );
}
