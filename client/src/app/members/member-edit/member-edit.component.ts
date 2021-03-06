import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm :NgForm // to access the form by #editForm
  member: Member;
  user: User;

  @HostListener('window:beforeunload',['$event']) unloadNotification($event : any){
    //this is to prevent the user from cancelling the tab or the browser or even leaving the site to a another site like 'google.com'  without saving changes 
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private accountService: AccountService, private memberService: MembersService,
    private toastr :ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.LoadMember();
  }

  LoadMember() {

    this.memberService.getMemeber(this.user.username).subscribe(member=>this.member=member);

  }

  updateMember(){
    this.memberService.updateMemeber(this.member).subscribe(c=>{
      this.toastr.success("profile updated successfully")
      this.editForm.reset(this.member) //give it the member after being updated  
    })
   
  }

}
