import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { User } from '../interfaces/user';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.scss']
})
export class DatePickerComponent {
  bsInlineValue! :Date ;
  selectedDate?: Date;
  user?:User

  constructor(private router:Router, private accountService:AccountService){
    
  }
  ngOnInit() {
    this.bsInlineValue = new Date();
    this.accountService.currentUser$.subscribe({
      next: data => {
        if(data)this.user=data
        console.log(this.user);
        
      }
      
    })
  }

  loadCurrentUser(){
    const token = localStorage.getItem('token');
    this.accountService.loadCurrentUser(token).subscribe();
  }

  onValueChange(value: Date): void {
    this.selectedDate = value;
    const newDate = this.selectedDate;
    const formattedDate = `${newDate.getFullYear()}-${(newDate.getMonth() + 1).toString().padStart(2, '0')}-${newDate.getDate().toString().padStart(2, '0')}`;
    if(this.user){
    const id = this.user.userId;
    this.router.navigate(['/DailyLog', formattedDate, id]);
    }

  }
}
