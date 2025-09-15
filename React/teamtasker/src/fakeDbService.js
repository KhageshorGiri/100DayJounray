
let projects = [
  { id: 1, name: "Project Alpha", description: "E-commerce platform build", ownerId: 101, tasks: [] },
  { id: 2, name: "Project Beta", description: "Internal HR system", ownerId: 102, tasks: [] },
  { id: 3, name: "Project Gamma", description: "Mobile banking app", ownerId: 103, tasks: [] },
  { id: 4, name: "Project Delta", description: "Social media analytics tool", ownerId: 104, tasks: [] },
  { id: 5, name: "Project Omega", description: "Healthcare appointment scheduler", ownerId: 105, tasks: [] },
];

let tasks = [
  { id: 1, title: "Design UI mockups", description: "Create wireframes for main pages", assignedTo: 201, projectId: 1 },
  { id: 2, title: "Setup database schema", description: "Define ERD and create migrations", assignedTo: 202, projectId: 1 },
  { id: 3, title: "Implement login API", description: "JWT authentication with refresh tokens", assignedTo: 203, projectId: 2 },
  { id: 4, title: "Payment gateway integration", description: "Integrate with Stripe and PayPal", assignedTo: 204, projectId: 3 },
  { id: 5, title: "Analytics dashboard", description: "Build charts and metrics page", assignedTo: 205, projectId: 4 },
];

export const fakeDbService = {
    crateProject : (name, description, ownerId) => {
        const project = {id: projects.length+1, name, description, ownerId, tasks:[]}
        project.push(project);
        return project;
    },

    getProjects: () => projects,
    getProjectById: (projectId) => projects.find((p) => p.projectId == projectId),

    createTask: (projectId, title, description, assignedTo) => {
        const task = {id: task.length+1, title, description, assignedTo, projectId};
        tasks.push(task)

        const project = getProjectById(projectId);
        if(project){
            project.tasks.push(task.id);
        }
        return task;
    },
    getTaskByProjects: (projectId) => tasks.filter( (t) => t.projectId == projectId),
    getTaskByAssigne: (assignedTo) => tasks.filter((t) => t.assignedTo == assignedTo)
};
