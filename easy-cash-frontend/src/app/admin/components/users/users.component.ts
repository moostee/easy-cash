import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { User } from "../../../core/interfaces/User";
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AddNewUserComponent } from '../add-new-user/add-new-user.component';
import { Role } from 'src/app/core/interfaces/Role';
import { UserService } from 'src/app/core/services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  listData!: MatTableDataSource<User>;

  displayColumn: String[] = ["name", "email", 'isActive', 'createdOn', 'modifiedOn', "actions"];

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  users: User[] = [];

  searchKey!: string;


  constructor(private _liveAnnouncer: LiveAnnouncer,
    private dialog: MatDialog,
    private _service: UserService,
    private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this._service.getAllUser().subscribe((result) => {
      this.users = result.responseObject;

      this.listData = new MatTableDataSource<User>(this.users);


      this.listData.paginator = this.paginator;
      this.listData.sort = this.sort;
    },
      err => {
        console.log(err);
      });
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

  onCreate() {
    const dialigConfig = new MatDialogConfig();
    dialigConfig.autoFocus = true;
    dialigConfig.width = "60%";
    let dialogRef = this.dialog.open(AddNewUserComponent, dialigConfig);


    dialogRef.afterClosed().subscribe(() => {
      //console.log('closed');
      this._service.getAllUser().subscribe((result) => {
        this.users = result.responseObject;
        this.listData.data = this.users;
      },
        err => {
          console.log(err);
        });
    });

  }


  activateOrDeactivateUser(action: string, id: number) {
    this._service.activateOrDeactivateUser({
      'action': action,
      'userId': id
    }).subscribe(result => {
      this._snackBar.open(`Successful`, 'Ok', {
        duration: 3000
      })
    }, err => console.log(err));
  }

}
