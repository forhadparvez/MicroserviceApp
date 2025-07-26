'use client';
import { Employee } from '@/types/employee';
import { deleteEmployee } from '@/services/employeeService';

type Props = {
  employees: Employee[];
  onEdit: (emp: Employee) => void;
  onDeleted: () => void;
};

export default function EmployeeList({ employees, onEdit, onDeleted }: Props) {
  const handleDelete = async (id: number) => {
    if (confirm('Delete employee?')) {
      const res = await deleteEmployee(id);
      if (res.ok) onDeleted();
      else alert('Delete failed');
    }
  };

  return (
    <div className="mt-6">
      <h2 className="text-xl font-bold mb-2">Employee List</h2>
      <table className="w-full border-collapse">
        <thead>
          <tr className="bg-gray-100">
            <th className="border p-2">Name</th>
            <th className="border p-2">Designation</th>
            <th className="border p-2">Department</th>
            <th className="border p-2">Joining Date</th>
            <th className="border p-2">Actions</th>
          </tr>
        </thead>
        <tbody>
          {employees.map((emp) => (
            <tr key={emp.id}>
              <td className="border p-2">{emp.fullName}</td>
              <td className="border p-2">{emp.designation}</td>
              <td className="border p-2">{emp.department}</td>
              <td className="border p-2">{emp.joiningDate}</td>
              <td className="border p-2 space-x-2">
                <button onClick={() => onEdit(emp)} className="text-blue-600">Edit</button>
                <button onClick={() => handleDelete(emp.id)} className="text-red-600">Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
