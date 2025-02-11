import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { SnackbarService } from '../services/snackbar.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);
  const snakckBar = inject(SnackbarService);


  if(accountService.isAdmin()) {
    return true;
  } else {
    snakckBar.error('You are not authorized to access this page');
    router.navigateByUrl('/account/login');
    return false;
  }
};
