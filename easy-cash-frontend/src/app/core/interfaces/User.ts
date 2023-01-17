

export interface User {
    name: string;
    email: string;
    id: number;
    isDeleted: boolean;
    createdOn: string;
    modifiedOn: string;
    role: number;
}


export interface CreateUser {
    name: string;
    email: string;
    password: string;
    confirmPassword: string;
    role: number;
    isActive: boolean;
}



export interface AuthResponse {
    id: string;
    token: string;
    name: string;
    email: string;
    role: number;
}

export interface ActivateOrDeActivateUser {
    action: string;
    userId: number;
}


