import { Component, EventEmitter, Output } from '@angular/core';
import { UserService } from '../../users/services/user.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { NavigationConst } from '../constants/navigation.const';

export interface MenuItem {
  Label: string;
  Command: () => void
}

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, ButtonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  items: MenuItem[];

  @Output() commandExecuted = new EventEmitter<void>();

  constructor(
    public userService: UserService,
    private router: Router
  ) {
    this.items = [
      {
        Label: 'Home',
        Command: () => this.home(),
      },
      {
        Label: 'Publish story',
        Command: () => this.navigateToPublishStory(),
      },
      {
        Label: 'Sign out',
        Command: () => this.logOut(),
      }
    ];
  }

  callItemCommand(item: MenuItem): void {
    item?.Command();
    this.commandExecuted?.emit();
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
