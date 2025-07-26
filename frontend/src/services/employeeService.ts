import { Employee } from '@/types/employee';

const API_URL = 'https://localhost:5001/api/employees';

export const getEmployees = async (): Promise<Employee[]> => {
  const res = await fetch(API_URL);
  return res.json();
};

export const createEmployee = async (employee: Employee) => {
  return await fetch(API_URL, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(employee),
  });
};

export const updateEmployee = async (employee: Employee) => {
  return await fetch(`${API_URL}/${employee.id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(employee),
  });
};

export const deleteEmployee = async (id: number) => {
  return await fetch(`${API_URL}/${id}`, {
    method: 'DELETE',
  });
};
