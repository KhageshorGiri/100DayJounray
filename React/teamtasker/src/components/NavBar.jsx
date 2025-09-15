const NavBar = () => {
  return (
    <nav className="bg-white dark:bg-gray-800 border-b border-gray-200 dark:border-gray-700 px-4 py-3">
      <div className="flex justify-between items-center">
        <div className="flex items-center">
          <h1 className="text-xl font-bold text-gray-900 dark:text-white">
            Team Tasker
          </h1>
        </div>

        <div className="flex items-center space-x-4">
          {/* Dark Mode Toggle*/}
          <button className="p-2 rounded-lg bg-gray-100 dark:bg-gray-700 text-gray-600 dark:text-gray-300 hover:bg-gray-200 dark:hover:bg-gray-600">
            ☀️
          </button>

          {/* Notification*/}
          <button className="p-2 rounded-lg bg-gray-100 dark:bg-gray-700 text-gray-600 dark:text-gray-300 hover:bg-gray-200 dark:hover:bg-gray-600 relative">
            🔔
            <span className="absolute -top-1 -right-1 bg-red-500 text-white text-white text-xs rounded-full h-5 w-5 flex items-center justify-center">
              9
            </span>
          </button>

          {/* User Menu */}
          <div className="relative">
            <button className="flex items-center space-x-2 p-2 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-700">
              <div className="w-8 h-8 bg-blue-600 rounded-full flex item-center justify-center text-white text-sm font-medium">
                🪴
              </div>
              <span className="hidden md:block text-gray-900 dark:text-white">
                Khageshor Giri
              </span>
            </button>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default NavBar;
