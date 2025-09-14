const navigationService = {
    async getNavigationLinks(userRole = 'admin'){
        // simulate api delay
        await new Promise(resolve => setTimeout(resolve, 500));
        const allLinks = [
            {id:'projects', label: 'Projects', icon:'📂', path:'/projects'},
            {id:'tasks', label: 'Tasks', icon:'📃', path:'/tasks'},
            {id:'members', label: 'Members', icon:'👥', path:'/Members'},
            {id:'reports', label: 'Reports', icon:'💹', path:'/reports'},
            {id:'settings', label: 'Settings', icon:'⚙️', path:'/settings'},
        ]

        // Filter links and returns
        return allLinks.filter(links => links);
    }



};

export default navigationService;