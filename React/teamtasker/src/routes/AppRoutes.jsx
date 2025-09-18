import { Routes, Route, Navigate } from "react-router-dom";
import ProjectPage from "../pages/ProjectPage";
import MainLayout from "../components/MainLayout";
import Tasks from "../pages/Tasks";
import Reports from "../pages/Reports";
import CreateProject from "../pages/CreateProjectPage";
import LoginPage from "../pages/LoginPage";
import Register from "../pages/Register";
import AuthLayout from "../components/AuthLayout";

const AppRoutes = () => {
  return (
    <Routes>
      {/* Public routes */}
      <Route element={<AuthLayout/>}>
        <Route path="login" element={<LoginPage />} />
        <Route path="register" element={<Register />} />
      </Route>

      {/* Prive routes | Proteced routes */}
      <Route path="/" element={<MainLayout />}>
        <Route path="projects" element={<ProjectPage />} />
        <Route path="tasks" element={<Tasks />} />
        <Route path="reports" element={<Reports />} />
        <Route path="create-project" element={<CreateProject />} />
      </Route>
    </Routes>
  );
};

export default AppRoutes;
