import { Component } from '@angular/core';
import { UserService } from '../../users/services/user.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MenuModule } from 'primeng/menu';
import { ButtonModule } from 'primeng/button';
import { MenuItem } from 'primeng/api';
import { NavigationConst } from '../constants/navigation.const';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, MenuModule, ButtonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  items: MenuItem[];

  constructor(
    public userService: UserService,
    private router: Router
  ) {
    this.items = [
      {
          label: 'Actions',
          items: [
              {
                  label: 'Home',
                  icon: 'pi pi-sparkles',
                  command: () => this.home(),
              },
              {
                  label: 'Publish story',
                  icon: 'pi pi-upload',
                  command: () => this.navigateToPublishStory(),
              },
              {
                label: 'Sign out',
                icon: 'pi pi-sign-out',
                command: () => this.logOut(),
              }
          ]
      }
  ];
  }

  home(): void {
    this.router.navigate([NavigationConst.Home]);
  }

  logOut(): void {
    this.userService.logOut();
    this.router.navigate([NavigationConst.Login]);
  }

  navigateToPublishStory(): void {
    this.router.navigate([NavigationConst.PublishStory]);
  }
}
