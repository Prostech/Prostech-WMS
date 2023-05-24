import { SideBar, Header } from "../components";

const PublicLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <div className="flex w-full h-full">
      <SideBar />
      <div className="w-full h-full text-center">
        <Header />
        {children}
      </div>
    </div>
  );
};

export default PublicLayout;
