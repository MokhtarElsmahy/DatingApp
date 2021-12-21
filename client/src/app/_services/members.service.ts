import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
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
  members:Member[]=[];
  constructor(private http :HttpClient) { }

  getMemebers() {
    if (this.members.length > 0) return of(this.members);
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(m => { this.members = m; return this.members })
    );
    //  return this.http.get<Member[]>(this.baseUrl+'users',httpOptions);
  }

  getMemeber(username: string) {
    const member = this.members.find(c => c.userName === username);
    if (member !== undefined) {
      return of(member);
    }
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
    // return this.http.get<Member>(this.baseUrl+'users/'+username,httpOptions);
  }

   updateMemeber(member :Member){
    return this.http.put(this.baseUrl+'users',member).pipe(
      map(
        ()=>{
          const index =this.members.indexOf(member);
          this.members[index] =member
        }
      )
    );
   }
}
