import { Outlet } from "react-router-dom";

const AuthLayout = () => {
  return (
    <div className="min-h-screen item-center bg-gray-100 dark:bg-gray-900">
      <Outlet />
    </div>
  );
};

export default AuthLayout;
