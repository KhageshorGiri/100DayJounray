const CreateProject = () => {
  return (
    <div className="max-w-3xl mx-auto mt-10 bg-white shadow-lg p-6">
      <h2 className="text-2xl font-semibold mb-6 text-gray-800">
        Create Project
      </h2>
      <form className="space-y-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Project Name
          </label>
          <input
            type="text"
            placeholder="Enter project name"
            className="w-full px-3 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Description
          </label>
          <textarea
            placeholder="Enter project description"
            rows="3"
            className="w-full px-3 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
          ></textarea>
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Project Owner
          </label>
          <select
            placeholder="Enter owner ID"
            className="w-full px-3 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
            <option value="1">Ram</option>
            <option value="2">Hari</option>
            <option value="3">Krishna</option>
          </select>
          <input type="number" />
        </div>

        <div className="flex justify-end">
          <button
            type="submit"
            className="bg-blue-600 text-white px-4 py-2 rounded-lg shadow hover:bg-blue-700 transition"
          >
            Save Project
          </button>
        </div>
      </form>
    </div>
  );
};

export default CreateProject;
