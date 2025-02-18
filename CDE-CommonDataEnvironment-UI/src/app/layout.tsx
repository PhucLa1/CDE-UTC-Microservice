/* eslint-disable @typescript-eslint/no-unused-vars */
import type { Metadata } from "next";
import "./globals.css";
import AppLayout from "@/app/system/ui/app-layout";
import AppContext from "@/app/system/ui/app-context";
import { EdgeStoreProvider } from "@/lib/edgestore";

export const metadata: Metadata = {
  title: ".NET ",
  description: "Created by .NET Developers",
};



export default function RootLayout({ children, }: Readonly<{ children: React.ReactNode; }>) {
  return (
    <html lang="en" suppressHydrationWarning={true} >
      <body className={``}>
        <EdgeStoreProvider>
          <AppContext>
            <AppLayout>
              {children}
            </AppLayout>
          </AppContext>
        </EdgeStoreProvider>
      </body>
    </html>
  );
}
