import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Validator } from 'fluentvalidation-ts';
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
    private _userService: UserService,
    private router: Router) { }

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
    }, { validators: createUserValidator })

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

      //console.log(result)
      this._snackBar.open(`Successfully added ${form.name}`, 'Ok', {
        duration: 3000
      });


      this.router.navigateByUrl('admin/users');

    },
      err => {
        this.showSpinner = false;
      });
  }

}

// export const passwordMatchingValidatior: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
//   const password = control.get('password');
//   const confirmPassword = control.get('confirmPassword');

//   return password?.value === confirmPassword?.value ? null : { notmatched: true };
// };




export class FormValidator extends Validator<CreateUser> {
  constructor() {
    super();

    this.ruleFor('name')
      .notEmpty()
      .withMessage("name is required");

    this.ruleFor('email')
      .emailAddress()
      .withMessage('please enter a valid email address');

    this.ruleFor('password')
      .matches(new RegExp('(?=[A-Za-z0-9@#$%^&+!=]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+!=])(?=.{8,}).*$'))
      .withMessage('Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character');

    this.ruleFor('confirmPassword')
      .must((value, model) => model.password == value)
      .withMessage('please enter the same password value');

    this.ruleFor('role')
      .greaterThanOrEqualTo(1)
      .withMessage('please choose a valid role')
      .lessThanOrEqualTo(2)
      .withMessage('please choose a valid role');
  }
}


export const createUserValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {

  const name = control.get('name');
  const email = control.get('email');
  const password = control.get('password');
  const confirmPassword = control.get('confirmPassword');
  const role = control.get('role');

  let fluentValidator = new FormValidator();

  let createUserForm: CreateUser = {
    name: name?.value,
    email: email?.value,
    password: password?.value,
    confirmPassword: confirmPassword?.value,
    role: parseInt(role?.value),
    isActive: false
  };

  let isValid = fluentValidator.validate(createUserForm);

  if (Object.keys(isValid).length !== 0) {
    return {
      nameError: isValid.name,
      emailError: isValid.email,
      passwordError: isValid.password,
      confirmPasswordError: isValid.confirmPassword,
      roleError: isValid.role,
      notmatched: true
    }
  }

  return null;

};


