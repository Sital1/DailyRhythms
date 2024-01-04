import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Observable } from 'rxjs';
import { DailyLog, DailyLogToDoItemDto, ToDoItem } from 'src/interfaces/dailylog';

@Injectable({
  providedIn: 'root'
})
export class DailyLogService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getDailyLog(userId:string,date:string):Observable<DailyLog>{
     return this.http.get<DailyLog>(this.baseUrl+`/DailyLogs/${userId}/${date}`)
  }

  startDay(userId:string):Observable<any>{
     return this.http.post(this.baseUrl+`/DailyLogs`,{userId})
  }

  toggleComplete(dailyLogId: number, toDoItemId: number): Observable<any> {
    
    return this.http.put(this.baseUrl+`/DailyLogs/todoitem/toggle`, {dailyLogId,  toDoItemId});
  }

  getToDoItem(dailyLogId: number, toDoItemId: number):Observable<ToDoItem>{
    return this.http.get<ToDoItem>(this.baseUrl+`/DailyLogs/toditem/${dailyLogId}/${toDoItemId}`)
  }
}
