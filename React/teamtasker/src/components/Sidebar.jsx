import { useState, useEffect } from "react";
import { Link, useLocation } from "react-router-dom";
import navigationService from "../navigationService";

const Sidebar = ({ isOpen, setIsOpen }) => {
  const [menuItems, setMenuItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const location = useLocation();

  useEffect(() => {
    const fetchNavigation = async () => {
      try {
        const links = await navigationService.getNavigationLinks('admin');
        setMenuItems(links);
      } catch (error) {
        console.error("Failed to fetch navigation links: ", error);
      } finally {
        setLoading(false);
      }
    };

    fetchNavigation();
  }, []);

  return (
    <>
      {/* Mobile overlay */}
      {isOpen && (
        <div
          className="fixed inset-0 bg-black bg-opacity-500 z-40 lg:hidden"
          onClick={() => setIsOpen(false)}
        ></div>
      )}

      {/* Sidebar */}
      <div
        className={`
            fixed lg:static inset-y-0 z-50 w-64 bg-gray-800 dark:bg-gray-900 text-white h-screen
            p-4 transform transition-transform duration-300 ease-in-out
            ${isOpen ? "translate-x-0" : "-translate-x-full lg:translate-x-0"}
        `}
      >
        <div className="flex items-center justify-between mb-8">
          <h1 className="text-lg font-bold">Menu</h1>
          <button
            className="lg:hidden text-white hover:text-gray-308"
            onClick={() => setIsOpen(false)}
          >
            ✖️
          </button>
        </div>

        <nav>
          {loading ? (
            <div className="text-center text-gray-400">Loading...</div>
          ) : (
            menuItems.map((item) => (
              <Link
                key={item.id}
                to={item.path}
                onClick={() => setIsOpen(false)}
                className={`w-full text-lef p-3 rounded mb-2 flex items-center gap-3 transition-colors block ${
                    location.pathname === item.path
                    ? 'bg-blue-600 dark:bg-blue-700'
                    : 'hover:bg-gray-700 dark:hover:bg-gray-800'
                }`}
              >
                <span>{item.icon}</span>
                {item.label}
              </Link>
            ))
          )}
        </nav>
      </div>
    </>
  );
};

export default Sidebar;
