import React from "react";

const Table = ({ columns, data }) => {
  return (
    <div className="overflow-x-auto">
      <table className="min-w-full border border-gray-200 deark:border-gray-700 rounded-lg">
        <thead className="bg-gray-100 dark:bg-gray-800">
          <tr>
            {columns.map((col) => (
              <th
                key={col.key}
                className="px-4 py-2 text-left text-sm font-semibold text-gray-700 dark:text-gray-300"
              >
                {col.key}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {data.lenght === 0 ? (
            <td
              colSpan={columns.lenght}
              className="px-4 py-3 text-center text-gray-500 dark:text-gray-400"
            >
              No data available
            </td>
          ) : (
            data.map((row, index) => (
              <tr
                key={index}
                className="border-t border-gray-200 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-800"
              >
                {columns.map((col) => (
                  <td
                    key={col.key}
                    className="px-4 py-2 text-sm text-gray-900 dark:text-gray-200"
                  >
                    {col.render ? col.render(row[col.key], row) : row[col.key]}
                  </td>
                ))}
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  );
};

export default Table;
