import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

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
 
  
  constructor(private http :HttpClient) {
    this.getUsers();
  }
  getUsers(){
    this.http.get("https://localhost:5001/api/users").subscribe(
      response=>{this.users=response;},
      error=>{console.log(error);}
    )
  }
 
}
