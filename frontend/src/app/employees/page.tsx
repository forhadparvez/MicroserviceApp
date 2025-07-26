'use client';
import { useEffect, useState } from 'react';
import { Employee } from '@/types/employee';
import { getEmployees } from '@/services/employeeService';
import EmployeeForm from '@/components/EmployeeForm';
import EmployeeList from '@/components/EmployeeList';

export default function HomePage() {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [selectedEmployee, setSelectedEmployee] = useState<Employee | null>(null);

  const loadEmployees = async () => {
    const data = await getEmployees();
    setEmployees(data);
    setSelectedEmployee(null);
  };

  useEffect(() => {
    loadEmployees();
  }, []);

  return (
    <div className="p-8">
      <EmployeeForm selectedEmployee={selectedEmployee} onSaved={loadEmployees} />
      <EmployeeList employees={employees} onEdit={setSelectedEmployee} onDeleted={loadEmployees} />
    </div>
  );
}
