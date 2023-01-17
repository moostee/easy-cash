import { User } from "./User";


export interface Loan {
    loanAmount: number;
    amountCurrency: String;
    startDate: Date;
    endDate: Date;
}


export interface CreateLoan {
    loanAmount: number;
    startDate: string;
    endDate: string;
    userId: number;
}


export interface Loans {
    userId: number;
    user: User;
    loanAmount: number;
    startDate: Date;
    endDate: Date;
    repaymentCount: number;
    repaymentCycle: number;
    isCompleted: boolean;
    status: string;
    id: number;
    isDeleted: boolean;
    createdBy?: any;
    createdOn: Date;
    modifiedBy?: any;
    modifiedOn: Date;
}


export interface LoanRepayment {
    loanId: number;
    loans: Loans;
    status: string;
    startDate: Date;
    endDate: Date;
    dueDate: Date;
    id: number;
    isDeleted: boolean;
    createdBy?: any;
    createdOn: Date;
    modifiedBy?: any;
    modifiedOn: Date;
}

export interface Wallet {
    userId: number;
    user: User;
    availableBalance: number;
    id: number;
    isDeleted: boolean;
    createdBy?: any;
    createdOn: Date;
    modifiedBy?: any;
    modifiedOn: Date;
}



