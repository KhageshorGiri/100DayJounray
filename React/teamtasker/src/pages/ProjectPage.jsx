import { useEffect, useState } from "react";
import { fakeDbService } from "../fakeDbService";
import Table from "../components/Table";

const ProjectPage = () => {
  const [projects, setProjects] = useState([]);

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
      <Table columns={columns} data={projects}></Table>
    </div>
  );
};

export default ProjectPage;
