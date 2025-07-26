'use client';
import { useEffect, useState } from 'react';
import { Employee } from '@/types/employee';
import { createEmployee, updateEmployee } from '@/services/employeeService';

type Props = {
  selectedEmployee: Employee | null;
  onSaved: () => void;
};

export default function EmployeeForm({ selectedEmployee, onSaved }: Props) {
  const [employee, setEmployee] = useState<Employee>({
    id: 0,
    fullName: '',
    designation: '',
    department: '',
    joiningDate: '',
  });

  useEffect(() => {
    if (selectedEmployee) setEmployee(selectedEmployee);
    else setEmployee({ id: 0, fullName: '', designation: '', department: '', joiningDate: '' });
  }, [selectedEmployee]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setEmployee((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const res = employee.id ? await updateEmployee(employee) : await createEmployee(employee);
    if (res.ok) {
      alert(employee.id ? 'Updated' : 'Created');
      onSaved();
      setEmployee({ id: 0, fullName: '', designation: '', department: '', joiningDate: '' });
    } else {
      alert('Error saving employee');
    }
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4 border p-4 rounded shadow">
      <h2 className="text-xl font-bold">{employee.id ? 'Update' : 'Add'} Employee</h2>
      <input name="fullName" value={employee.fullName} onChange={handleChange} placeholder="Full Name" className="border p-2 w-full" required />
      <input name="designation" value={employee.designation} onChange={handleChange} placeholder="Designation" className="border p-2 w-full" required />
      <input name="department" value={employee.department} onChange={handleChange} placeholder="Department" className="border p-2 w-full" required />
      <input type="date" name="joiningDate" value={employee.joiningDate} onChange={handleChange} className="border p-2 w-full" required />
      <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded">
        {employee.id ? 'Update' : 'Create'}
      </button>
    </form>
  );
}
