import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public isAuthorized = new BehaviorSubject<boolean>(false);

  storageItemId = 'login';
  routes = {
    default: '/projects',
    login: '/login',
  };

  constructor(private router: Router) {
    this.autoSignIn();
  }

  autoSignIn() {
    const itemInStorage = localStorage.getItem(this.storageItemId);
    if (itemInStorage) {
      this.isAuthorized.next(true);
      this.router.navigate([this.routes.default]);
    }
  }

  signIn(login: string) {
    localStorage.setItem(this.storageItemId, login);
    this.isAuthorized.next(true);
    this.router.navigate([this.routes.default]);
  }

  signOut() {
    localStorage.removeItem(this.storageItemId);
    this.isAuthorized.next(false);
    this.router.navigate([this.routes.login]);
  }
}
