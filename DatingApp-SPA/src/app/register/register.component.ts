import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() ValuesFromHome = 'values';
  @Output() cancelRegister = new EventEmitter();

  model: any = {};
  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }
  submit() {
    this.authService.register(this.model).subscribe(() => {
      this.alertify.success('Registration successfull');
    },
    error => {
      this.alertify.error(error);
    });
  }
cancel() {
  this.cancelRegister.emit(false);
  console.log('Cancelled');
}
}
