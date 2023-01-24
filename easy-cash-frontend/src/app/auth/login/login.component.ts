import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { Validator } from 'fluentvalidation-ts';

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
    }, { validators: loginFormValidatior });
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

//Fluent Validator
export interface LoginFormModel {
  emailError : string;
  passwordError : string;
}

export class FormValidator extends Validator<LoginFormModel> {
  constructor() {
    super();

    this.ruleFor('emailError')
      .notEmpty()
      .withMessage("email is required");

    this.ruleFor('emailError')
      .emailAddress()
      .withMessage("please enter a valid email address");

    this.ruleFor('passwordError')
      .notEmpty()
      .withMessage("password is required");

    this.ruleFor('passwordError')
      .minLength(8)
      .matches(new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$'))      
      .withMessage('Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character');
  }
}


export const loginFormValidatior : ValidatorFn = (control: AbstractControl): ValidationErrors | null => {

  const formEmail = control.get('email');
  const formPassword = control.get('password');

  let fluentValidator = new FormValidator();

  let loginForm : LoginFormModel = {
    emailError : formEmail?.value,
    passwordError : formPassword?.value
  };

  let isValid = fluentValidator.validate(loginForm);

  console.log(isValid);

  if(Object.keys(isValid).length !== 0)
  {
    return {
      emailError : isValid.emailError,
      passwordError : isValid.passwordError,
      notmatched : true
    }
  }

  return null;

};
