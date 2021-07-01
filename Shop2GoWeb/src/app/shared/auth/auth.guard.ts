import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private _router: Router,
    private _userService:UserService
    ) { }


  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    
    var tokenControl = localStorage.getItem('token');
    if(tokenControl != null){
      let roles = route.data['permittedRoles'] as Array<string>;
      if(roles){
        if(this._userService.roleMatch(roles))
          return true;
        else{
          this._router.navigate(['/forbidden']);
          return false;
        }
      }
      return true;
      
    }
    else{
      this._router.navigate(['/Login']);
      return false;
    }
  }
}