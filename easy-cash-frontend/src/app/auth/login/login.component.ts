import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  fieldTextType: boolean = false;

  loginFormGroup!: FormGroup;

  showSpinner: boolean = false;

  constructor(private fb: FormBuilder, private service: AuthService, private router: Router,
    private tokenStorage: TokenStorageService) {
    this.navigateByRole();
  }

  ngOnInit(): void {
    this.loginFormGroup = this.fb.group({

      email: new FormControl('', Validators.compose([
        Validators.required,
        Validators.email
      ])),

      password: new FormControl('', Validators.compose([
        Validators.required
      ]))
    });
  }


  navigateByRole() {

    let role = this.tokenStorage.getUser().role;
    console.log('role here ', role);
    if (role == undefined) {
      return;
    }

    if (role == 1) {

      this.router.navigateByUrl('admin');

    } else if (role == 2) {

      this.router.navigateByUrl('user');

    }
  }

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }

  login(form: any) {

    this.showSpinner = true;

    this.service.login(form.email, form.password).subscribe(result => {

      this.showSpinner = false;
      console.log(result)

      this.tokenStorage.saveToken(result.responseObject.token);
      this.tokenStorage.saveUser(result.responseObject);

      this.navigateByRole();
    },
      err => {
        this.showSpinner = false;
      });
  }


}
