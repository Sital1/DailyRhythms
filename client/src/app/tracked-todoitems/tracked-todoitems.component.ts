import { Component } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { UserToDoItems } from '../interfaces/userToDoItems';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { AddtaskmodalComponent } from '../components/modals/addtaskmodal/addtaskmodal.component';

@Component({
  selector: 'app-tracked-todoitems',
  templateUrl: './tracked-todoitems.component.html',
  styleUrls: ['./tracked-todoitems.component.scss']
})
export class TrackedTodoitemsComponent {
  userToDoItems?: UserToDoItems;

  constructor(private categoryToDoItemsService: CategoryService,private modalService: BsModalService) { }

  ngOnInit() {
    this.fetchUserToDoItems();
  }

  
  fetchUserToDoItems() {
    this.categoryToDoItemsService.getUserToDoItems().subscribe({
      next: data => this.userToDoItems = data,
      error: err => console.log(err)
    });
  }


  bsModalRef?: BsModalRef;

  openModalWithComponent() {
    const initialState: ModalOptions = {
      initialState: {
        title: 'Add Task',
        userId:this.userToDoItems?.id
      }
    };
    this.bsModalRef = this.modalService.show(AddtaskmodalComponent, initialState);
    this.bsModalRef.content.closeBtnName = 'Close';

    this.bsModalRef.content.taskAdded.subscribe(() => {
      this.fetchUserToDoItems();
    });
  }
}
