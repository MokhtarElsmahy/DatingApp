import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  users :any;
  RegisterMode : boolean = false;
  constructor() { }

  ngOnInit(): void {
    // this.getUsers();
  }

  RegisterToggle(){
    this.RegisterMode = !this.RegisterMode;
  }

  // getUsers(){
  //   this.http.get("https://localhost:5001/api/users").subscribe(
  //     response=>{this.users=response;},
  //     error=>{console.log(error);}
  //   )}

    cancelRegisterMode(event : boolean){
     this.RegisterMode=event;
      }

}
