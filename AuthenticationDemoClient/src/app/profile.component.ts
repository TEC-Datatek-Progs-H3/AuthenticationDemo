import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from './_services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from './_models/user';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  template: `
    <p>
      profile works!
    </p>
    <h1>{{user.username}}</h1>
  `,
  styles: [
  ]
})
export class ProfileComponent implements OnInit {
  user: User = { id: 0, email: '', username: '' };


  constructor(
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute

  ) {

  }

  ngOnInit() {
    // redirect to home if user is not logged in, or userid not matching userid in url
    this.activatedRoute.paramMap.subscribe(params => {
      if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0 || this.authService.currentUserValue.id != Number(params.get("id"))) {
        this.router.navigate(['/']);
      }
      // store user in variable, instaed of having to type authService.currentUserValue all the time
      this.user = this.authService.currentUserValue;
    })
  }
}
