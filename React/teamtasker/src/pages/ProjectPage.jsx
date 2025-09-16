import { useEffect, useState } from "react";
import { fakeDbService } from "../fakeDbService";
import Table from "../components/Table";
import { useNavigate } from "react-router-dom";

const ProjectPage = () => {
  const [projects, setProjects] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const allProjects = fakeDbService.getProjects();
    setProjects(allProjects);
  }, []);

  const columns = [
    { key: "id", header: "ID" },
    { key: "name", header: "Name" },
    { key: "description", header: "Description" },
    { key: "ownerId", header: "Owner" },
    {
      key: "action",
      header: "Actions",
      render: (_, row) => (
        <button
          className="px-3 py-1 bg-blue-600 text-white text-xs rounded hover:bg-blue-700"
          onClick={() => alert(`Edit project ${row.name}`)}
        >
          Edit
        </button>
      ),
    },
  ];

  return (
    <div>
      <h1 className="text-xl fond-bold mb-4">Projects</h1>
      <div className="flex justify-end pb-4 pe-5">
        <button
          onClick={() => navigate("/create-project")}
          className="bg-blue-600 text-white px-4 py-2 rounded-lg shadow hover:bg-blue-700 transition"
        >
          Add New Project
        </button>
      </div>
      <div>
        <Table columns={columns} data={projects}></Table>
      </div>
    </div>
  );
};

export default ProjectPage;
