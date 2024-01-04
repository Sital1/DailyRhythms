import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DailyLog } from 'src/interfaces/dailylog';
import { DailyLogService } from '../services/daily-log.service';

@Component({
  selector: 'app-daily-log',
  templateUrl: './daily-log.component.html',
  styleUrls: ['./daily-log.component.scss']
})
export class DailyLogComponent implements OnInit {
  userId?: string | null
  date?: string | null
  dailyLog: DailyLog | null = null
  canStartDay: boolean = false;

  constructor(private activatedRoute: ActivatedRoute, private dailyLogService: DailyLogService) { }
  ngOnInit(): void {
    this.activatedRoute.params.subscribe(queryParams => {
      console.log("mf being caled");
      this.userId = queryParams['userId']
     this.date = queryParams['date']
     this.loadDailyLog()
    });
   
  }

  loadDailyLog() {

    if (this.userId && this.date) {
      this.dailyLogService.getDailyLog(this.userId, this.date).subscribe({
        next: data => {
          this.dailyLog = data;
          this.canStartDay = false;
        },
        error: error => {
          if (error.status === 404) {
            this.dailyLog = null;
            this.canStartDay = true;
          } else {
            console.error("Some error occured: ", error);
          }
        }

      });
    }

  }

  onStartDayClick() {
    const id = this.userId ?? "";
    this.dailyLogService.startDay(id).subscribe({
      next: data => {
        this.loadDailyLog();
      },
      error: error => {
        console.error("Some error occured: ", error);

      }

    });
  }

}
