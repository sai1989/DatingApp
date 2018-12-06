import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};


  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }
  login() {
    this.authService.login(this.model).subscribe(next => {this.alertify.success('Logged in sucessfully'); } ,
    // error => {console.log(error); } );
    error => {this.alertify.error(error) ; });
  }
  loggedIn() {
    // const token = localStorage.getItem('token'); // we are not checking the token content
    // return !!token; // this means if there is any content it returns true else it returns false
    return this.authService.loggedIn();
  }
  loggedOut() {
    localStorage.removeItem('token');
    this.alertify.message('Logged out sucessfully ');
    // can use alertify directly because it is already imported globally in angular.json
    // alertify.success('Double check');
  }
  onHidden(): void {
    console.log('Dropdown is hidden');
  }
  onShown(): void {
    console.log('Dropdown is shown');
  }
  isOpenChange(): void {
    console.log('Dropdown state is changed');
  }
}
