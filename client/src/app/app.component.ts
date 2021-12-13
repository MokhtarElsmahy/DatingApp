import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

//decorator
@Component({
  selector: 'app-root',  //we call the component by this name , we can change it to any name we want 
  templateUrl: './app.component.html', // this referes to the html page 
  styleUrls: ['./app.component.css']  // this referes to the css page for this page 
})
export class AppComponent implements OnInit {
  title = 'Dating'; 
  users : any;
  ngOnInit(): void {
    this.getUsers();
  }
 
  
  constructor(private http :HttpClient ,private accountService :AccountService) {
    this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser(){
    const user : User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }
  getUsers(){
    this.http.get("https://localhost:5001/api/users").subscribe(
      response=>{this.users=response;},
      error=>{console.log(error);}
    )
  }
 
}
