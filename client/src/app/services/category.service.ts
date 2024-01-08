import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AccountService } from './account.service';
import { UserToDoItemDto, UserToDoItems } from '../interfaces/userToDoItems';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private baseUrl: string = environment.apiUrl;
  userId?: string;
  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.subscribe({
      next: data => {
        if (data) this.userId = data.userId
      }
    })
  }
  getUserToDoItems(): Observable<UserToDoItems> {
    return this.http.get<UserToDoItems>(`${this.baseUrl}/Category/todoitems/${this.userId}`);
  }

  softDeleteToDoItem(value: any) {
    return this.http.put(`${this.baseUrl}/Category/todoitems/softdelete`,value);
  }

  getUserToDoItemsByCategory(categoryId:number){
    return this.http.get<UserToDoItemDto[]>(`${this.baseUrl}/Category/todoitems/${this.userId}/${categoryId}`);
  }

  addToDoItem(value:any){
    return this.http.post(`${this.baseUrl}/Category/todoitems/`,value);
  }

}
