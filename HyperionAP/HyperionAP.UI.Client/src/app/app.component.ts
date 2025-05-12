import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { SidebarModule } from 'primeng/sidebar';
import { PanelMenuModule } from 'primeng/panelmenu';

@Component({
  selector: 'app-root',
  imports: [
    RouterModule,
    RouterOutlet,
    MenubarModule,
    ButtonModule,
    SidebarModule,
    PanelMenuModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  sidebarVisible = false;
  topMenuItems: MenuItem[] = [
    { label: 'Projects', icon: 'pi pi-book', routerLink: ['/projects'] },
    { label: 'Pipeline Editor', icon: 'pi pi-microchip', routerLink: ['/pipeline'] },
  ];
}
