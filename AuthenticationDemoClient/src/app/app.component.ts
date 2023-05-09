import { Component } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { UserService } from './_services/user.service';
import { User } from './_models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AuthenticationDemoClient';
  currentUser: User = { id: 0, username: '', email: '' };


  constructor(
    private router: Router,
    private authService: AuthService,
    private userService: UserService) {
    // get the current user from auth service
    this.authService.currentUser.subscribe(x => this.currentUser = x);
  }

  logout() {
    if (confirm('Er du sikker på du vil logge ud')) {
      // ask auth service to perform logout
      this.authService.logout();

      // subscribe to the changes in currentUser, and load Home component
      this.authService.currentUser.subscribe(x => {
        this.currentUser = x
        this.router.navigate(['/']);
      });
    }
  }

}
