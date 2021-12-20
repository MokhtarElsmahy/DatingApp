import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';


// var user = localStorage.getItem('user');
// var token = JSON.parse(user)?.token;
// var headers = new HttpHeaders({Authorization:'Bearer '+token});

// const httpOptions = {
//   headers: headers
// };

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl =environment.apiUrl;
  constructor(private http :HttpClient) { }

  getMemebers(){
   
   return this.http.get<Member[]>(this.baseUrl+'users');
  //  return this.http.get<Member[]>(this.baseUrl+'users',httpOptions);
  }

  getMemeber(username :string){
   
    return this.http.get<Member>(this.baseUrl+'users/'+username);
    // return this.http.get<Member>(this.baseUrl+'users/'+username,httpOptions);
   }
}
