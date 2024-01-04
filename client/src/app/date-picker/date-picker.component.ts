import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.scss']
})
export class DatePickerComponent {
  bsInlineValue! :Date ;
  selectedDate?: Date;

  constructor(private router:Router){
    
  }
  ngOnInit() {
    this.bsInlineValue = new Date();

  }

  onValueChange(value: Date): void {
    this.selectedDate = value;
    const newDate = this.selectedDate;
    const formattedDate = `${newDate.getFullYear()}-${(newDate.getMonth() + 1).toString().padStart(2, '0')}-${newDate.getDate().toString().padStart(2, '0')}`;

    console.log('/DailyLog/'+formattedDate+"/6");
    this.router.navigate(['/DailyLog', formattedDate, 2]);
  }
}
