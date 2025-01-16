import React from "react";

import { Helmet } from "react-helmet-async";
import { NavLink } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import { Separator } from "@/components/ui/separator.tsx";

import { ProfileSection } from "@/components/Settings/ProfileSection.tsx";
import { ThemeSection } from "@/components/Settings/ThemeSection.tsx";

export type SettingsSection = "Profile" | "Theme";

export default function SettingsPage() {
  const [section, setSection] = React.useState<SettingsSection>("Profile");

  const renderSection = () => {
    switch (section) {
      case "Profile":
        return <ProfileSection />;
      case "Theme":
        return <ThemeSection />;
      default:
        return null;
    }
  };

  return (
    <>
      <Helmet>
        <title>Chalk - Settings</title>
      </Helmet>
      <main className="relative w-screen h-screen flex">
        <Button variant="link" className="text-xl px-2" asChild>
          <NavLink to="/dashboard" className="fixed top-0 left-0 m-4">
            Back
          </NavLink>
        </Button>
        <ul className="flex flex-col gap-y-4 text-xl min-w-80 pr-4 items-end pt-20">
          <li onClick={() => setSection("Profile")} className="hover:cursor-pointer">
            Profile
          </li>
          <li onClick={() => setSection("Theme")} className="hover:cursor-pointer">
            Theme
          </li>
        </ul>
        <Separator orientation="vertical" />
        {renderSection()}
      </main>
    </>
  );
}
