import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { map } from 'rxjs';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);
 
  return accountService.currentUser$.pipe(
    map(auth => {
      if (auth) return true;
      else {
        router.navigate(['/login'], {queryParams: {returnUrl: router.url}});
        return false
      }
    })
  );
};
