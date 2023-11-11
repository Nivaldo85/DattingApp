import { HttpClient } from '@angular/common/http';
import { Component,OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{
  regiterMode=false;
  users:any;
  constructor(private http: HttpClient){

  }

  ngOnInit():void{
    this.getUsers();
  }

  registerToggle(){
    this.regiterMode=!this.regiterMode;
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe({
      next:response =>this.users=response,
      error:error=>console.log(error)
      ,
      complete:()=>console.log('Request inchis')
      
    });
  }
  cancelRegisterMode(event:boolean){
    this.regiterMode=event;

  }

}
