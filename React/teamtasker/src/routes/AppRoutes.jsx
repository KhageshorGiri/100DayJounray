import { Routes, Route, Navigate } from "react-router-dom";
import ProjectPage from "../pages/ProjectPage";
import MainLayout from "../components/MainLayout";
import Tasks from "../pages/Tasks";
import Reports from "../pages/Reports";

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<MainLayout />}>
        <Route index element={<Navigate to="/projects" replace />} />
        <Route path="projects" element={<ProjectPage />} />
        <Route path="tasks" element={<Tasks />} />
        <Route path="reports" element={<Reports />} />
      </Route>
    </Routes>
  );
};

export default AppRoutes;
