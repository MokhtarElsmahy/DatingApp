import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  // @Input() usersFromHomeComponent;
  @Output() cancelRegister = new EventEmitter();
  model :any ={
    // username  : "eee",
    // password  : "123" 

  };

  constructor(private accountservice : AccountService , private toastr : ToastrService) { }

  ngOnInit(): void {
  
  }

  register(){
    this.accountservice.register(this.model).subscribe(r=>{console.log(r);this.cancel()},err=>{this.toastr.error(err)});
  }

  cancel(){
    this.cancelRegister.emit(false);
  }

}
