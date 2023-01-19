import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CreateUser } from 'src/app/core/interfaces/User';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-add-new-user',
  templateUrl: './add-new-user.component.html',
  styleUrls: ['./add-new-user.component.css']
})
export class AddNewUserComponent implements OnInit {

  newUserFormGroup!: FormGroup;
  showSpinner: boolean = false;

  constructor(private fb: FormBuilder,
    private _snackBar: MatSnackBar,
    private _userService: UserService) { }

  ngOnInit(): void {

    this.newUserFormGroup = this.fb.group({

      name: new FormControl('', Validators.compose([
        Validators.required
      ])),

      email: new FormControl('', Validators.compose([
        Validators.required, Validators.email
      ])),

      password: new FormControl('', Validators.compose([
        Validators.required, Validators.minLength(8)
      ])),

      confirmPassword: new FormControl('', Validators.compose([
        Validators.required
      ])),

      role: new FormControl('', Validators.compose([
        Validators.required
      ])),

      isActive: new FormControl(false, Validators.compose([
        Validators.required
      ]))
    }, { validators: passwordMatchingValidatior })

  }

  addNewUser(form: any) {

    this.showSpinner = true;

    let user: CreateUser = {
      name: form.name,
      email: form.email,
      isActive: false,
      password: form.password,
      role: form.role,
      confirmPassword: form.confirmPassword
    }
    
    this._userService.addNewUser(user).subscribe(result => {

      this.showSpinner = false;

      this.newUserFormGroup.reset();

      console.log(result)
      this._snackBar.open(`Successfully added ${form.name}`, 'Ok', {
        duration: 3000
      })
    },
      err => {
        this.showSpinner = false;
      });
  }

}

export const passwordMatchingValidatior: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const password = control.get('password');
  const confirmPassword = control.get('confirmPassword');

  return password?.value === confirmPassword?.value ? null : { notmatched: true };
};

