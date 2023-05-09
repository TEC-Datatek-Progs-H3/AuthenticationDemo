import { Role } from "./role";

export interface User {
  id: number;
  username: string;
  email: string;
  role?: Role;
  token?: string;
}

export function resetUser() {
  return { id: 0, username: '', email: '' };
}

