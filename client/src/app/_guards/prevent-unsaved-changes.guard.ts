import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {

  //this guard is to prevent the user from leaving the page to a another page in the same project without saving changes 
  canDeactivate(
    component: MemberEditComponent): boolean {
      if (component.editForm.dirty) {
        return confirm("Are u sure you wanna contiue ? , any unsaved changes will be lost !!")
        
      }
    return true;
  }
  
}
