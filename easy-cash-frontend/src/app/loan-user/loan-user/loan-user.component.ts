import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Wallet } from 'src/app/core/interfaces/Loan';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { WalletService } from 'src/app/core/services/wallet.service';
import { LoanApplicationComponent } from '../loan-application/loan-application.component';

@Component({
  selector: 'app-loan-user',
  templateUrl: './loan-user.component.html',
  styleUrls: ['./loan-user.component.css']
})
export class LoanUserComponent implements OnInit {

  userWallet! : Wallet;
  

  constructor(private dialog: MatDialog,
    private _walletService: WalletService,
    private _tokenService : TokenStorageService) { }

  ngOnInit(): void {
    this.getUserWallet();
  }

  onCreate() {
    const dialigConfig = new MatDialogConfig();
    dialigConfig.autoFocus = true;
    dialigConfig.width = "60%";
    let dialogRef = this.dialog.open(LoanApplicationComponent, dialigConfig);


    dialogRef.afterClosed().subscribe(() => {
      console.log('closed');
    });

  }

  getUserWallet()
  {
    var userid = this._tokenService.getUser().id;
    this._walletService.getWalletByUserId(userid).subscribe(r => {
      this.userWallet = r.responseObject;
    })
  }

}
