import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../account/account.service';
import { IUser } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
      return this.accountService.currentUser$.pipe(
        map((auth: any) => {
          if(auth && auth.token) {
            return true;
          }
          this.router.navigate(['account/login'], {queryParams: {returnUrl: state.url}});
          //added return false to avoid warning message; though after 
          return false;
        })
      );
  }
  
}
