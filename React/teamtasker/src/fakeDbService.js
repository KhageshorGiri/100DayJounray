
let projects = [];
let tasks = [];

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
