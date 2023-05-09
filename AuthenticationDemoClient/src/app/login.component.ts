import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from './_services/auth.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <h1>Login</h1>
    <div class="mb-3">
      <label for="exampleInputEmail1" class="form-label">Email</label>
      <input type="email" class="form-control" [(ngModel)]="email">
    </div>
    <div class="mb-3">
      <label for="exampleInputPassword1" class="form-label">Password</label>
      <input type="password" class="form-control" [(ngModel)]="password">
    </div>
    <!-- <div class="mb-3 form-check">
      <input type="checkbox" class="form-check-input" id="exampleCheck1">
      <label class="form-check-label" for="exampleCheck1">Check me out</label>
    </div> -->
    <button type="submit" class="btn btn-primary" (click)="login()">Login</button>

    {{ error }}
  `,
  styles: [
  ]
})
export class LoginComponent implements OnInit {
  email: string = '';
  password: string = '';
  error = '';

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    // redirect to home if already logged in
    if (this.authService.currentUserValue != null && this.authService.currentUserValue.id > 0) {
      this.router.navigate(['/']);
    }
  }

  login(): void {
    this.error = '';
    this.authService.login(this.email, this.password)
      .subscribe({
        next: () => {
          // get return url from activatedRoute parameters or default to '/'
          let returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigate([returnUrl]);
        },
        error: err => {
          if (err.error?.status == 400 || err.error?.status == 401 || err.error?.status == 500) {
            this.error = 'Forkert brugernavn eller kodeord';
          }
          else {
            this.error = err.error.title;
          }
        }
      });
  }
}
