import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Loan, Loans } from "../../../core/interfaces/Loan";
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { LoanDetailsComponent } from '../loan-details/loan-details.component';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { Role } from 'src/app/core/interfaces/Role';
import { LoanService } from 'src/app/core/services/loan.service';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-loans',
  templateUrl: './loans.component.html',
  styleUrls: ['./loans.component.css']
})

export class LoansComponent implements OnInit {


  listData!: MatTableDataSource<Loans>;

  loans: Loans[] = [];

  displayColumn: String[] = ["loanAmount", "status", 'startDate', 'endDate', "actions"];

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;


  searchKey!: string;

  userId: number = 0;


  constructor(private _liveAnnouncer: LiveAnnouncer,
    private dialog: MatDialog,
    private _tokenService: TokenStorageService,
    private _loanService: LoanService,
    private _snackBar: MatSnackBar) { }

  ngOnInit(): void {

    let userRole = this._tokenService.getUser().role;
    this.userId = this._tokenService.getUser().id;

    if (userRole == Role.LoanUser) {
      this._loanService.getLoanByUserId(this.userId).subscribe(result => {
        this.loans = result.responseObject;

        this.listData = new MatTableDataSource(this.loans);


        this.listData.paginator = this.paginator;
        this.listData.sort = this.sort;

      }, err => {
        console.log(err);
      });
    } else {
      this._loanService.getAllLoans().subscribe(result => {
        this.loans = result.responseObject;
        this.listData = new MatTableDataSource(this.loans);

        this.listData.paginator = this.paginator;
        this.listData.sort = this.sort;
      }, err => {
        console.log(err);
      });
    }

  }



  onSearchClear(event: Event) {
    this.searchKey = "";
    this.applyFilter(event);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.listData.filter = filterValue.trim().toLowerCase();
  }

  announceSortChange(sortState: Sort) {

    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }


  viewDetails(loan: any) {
    const dialigConfig = new MatDialogConfig();
    dialigConfig.autoFocus = true;
    dialigConfig.width = "60%";
    let dialogRef = this.dialog.open(LoanDetailsComponent, dialigConfig);
    dialogRef.componentInstance.loan = loan;

    dialogRef.afterClosed().subscribe(() => {
    });
  }


  repayLoan() {
    this._loanService.repayLoan(this.userId).subscribe(result => {
      this._snackBar.open(`Successful`, 'Ok', {
        duration: 3000
      })
    }, err => {
      console.log(err);
    })
  }


}

