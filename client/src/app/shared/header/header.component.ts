import { Component } from '@angular/core';
import { UserService } from '../../users/services/user.service';
import { Router } from '@angular/router'; // Import Router
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  constructor(
    public userService: UserService,
    private router: Router
  ) {}

  home(): void {
    this.router.navigate(['/home']);
  }

  logOut(): void {
    this.userService.logOut();
    window.location.reload();
  }

  navigateToPublishStory(): void {
    this.router.navigate(['/publish-story']);
  }
}
