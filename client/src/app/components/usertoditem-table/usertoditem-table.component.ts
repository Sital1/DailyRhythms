import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { UserCategoryDto, UserToDoItemDto } from 'src/app/interfaces/userToDoItems';
import { CategoryService } from 'src/app/services/category.service';
import { TaskDeleteModal } from '../modals/task-delete.component';

@Component({
  selector: 'app-usertoditem-table',
  templateUrl: './usertoditem-table.component.html',
  styleUrls: ['./usertoditem-table.component.scss']
})
export class UsertoditemTableComponent {

  @Input() userToDoItems?: UserToDoItemDto[]
  bsModalRef?: BsModalRef;
  constructor(private modalService: BsModalService, private categoryService: CategoryService) { }

  openModalWithComponent(id: number, categoryId: number, userId: number) {
    const initialState: ModalOptions = {
      initialState: {
        title: 'This task won\'t show up on Daily Logs from tomorrow onwards. Continue?',
        value: { id, categoryId, userId }
      }
    };
    this.bsModalRef = this.modalService.show(TaskDeleteModal, initialState);
    this.bsModalRef.content.closeBtnName = 'Close';
    this.bsModalRef.content.taskDeleted.subscribe(() => {
      this.reloadUserToDoItems(categoryId);
    });
  }

  reloadUserToDoItems(categoryId: number) {
    this.categoryService.getUserToDoItemsByCategory(categoryId).subscribe({
      next: data=>{this.userToDoItems=data}
    })
  }
}


export { TaskDeleteModal };
/* This is a component which we pass in modal*/

