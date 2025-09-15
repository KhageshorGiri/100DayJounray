import { Routes, Route, Outlet } from "react-router-dom";
import NavBar from "./NavBar"
import { useState } from "react";
import Sidebar from "./Sidebar";


export default function MainLayout() {
  const [sidebarOpen, setSidebarOpen] = useState(false);
    return (
     <div className="flex h-screen bg-gray-100 dark:bg-gray-900">
      <Sidebar
      isOpen={sidebarOpen}
      setIsOpen={setSidebarOpen}
      />

      <div className="flex-1 flex flex-col overflow-hidden">
        <div className="flex items-center lg:hidden p-4 bg-white dark:bg-gray-800 border-b dark:border-gray-700">
          <button
            onClick={() => setSidebarOpen(true)}
            className="text-gray-600 dark:text-gray-300 hover:text-gray-900 dark:hover:text-white">
            <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16"/>
            </svg>
          </button>
            <h1 className="ml-4 text-xl font-bold text-gray-900 dark:test-white">Team Tasker</h1>
        </div>

        <NavBar/>
        
        <div className="flex-1 overflow-auto px-4 py-3">
          <Outlet/>
        </div>
      </div>
     </div>
    );
  }