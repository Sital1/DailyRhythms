import { Component, Input } from '@angular/core';
import { ToDoItem } from 'src/app/interfaces/dailylog';
import { DailyLogService } from '../services/daily-log.service';

@Component({
  selector: 'app-todoitems',
  templateUrl: './todoitems.component.html',
  styleUrls: ['./todoitems.component.scss']
})
export class TodoitemsComponent {

  @Input() dailyLogId? : number
  @Input() item? : ToDoItem;
  @Input() passedDateIsToday?: boolean;
  constructor(private dailyLogService: DailyLogService) {}

  submitChange(id:number){
      this.dailyLogService.toggleComplete(this.dailyLogId!, this.item?.id!).subscribe({
        next: data => this.reloadToDoItem(this.dailyLogId!,this.item?.id!),
        error: error => console.log(error)
        
      }
    );
  }

   reloadToDoItem(dailyLogId:number,toDoItemId:number) {
    this.dailyLogService.getToDoItem(dailyLogId,toDoItemId).subscribe({
      next: data => this.item =  data,
      error: (error)=>console.log(error)
      
      
    })
  }
  
  
}

