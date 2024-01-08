import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import { CategoryService } from "src/app/services/category.service";

@Component({
    // eslint-disable-next-line @angular-eslint/component-selector
    selector: 'modal-content',
    template: `
      <div class="modal-header">
        <h6 class="modal-title pull-left text-danger">{{ title }}</h6>
        <button type="button" class="btn-close close pull-right" aria-label="Close" (click)="bsModalRef.hide()">
          <span aria-hidden="true" class="visually-hidden">&times;</span>
        </button>
      </div>
    
      <div class="d-flex justify-content-center p-3">
      <button type="button" class="btn btn-danger me-3" (click)="softDeleteTask()">Delete</button>
      <button type="button" class="btn btn-primary" (click)="bsModalRef.hide()">{{ closeBtnName }}</button>
      </div>
    `
  })
  export class TaskDeleteModal implements OnInit {
    title?: string;
    closeBtnName?: string;
    value?: object
    constructor(public bsModalRef: BsModalRef, private categoryService: CategoryService) { }
    @Output() taskDeleted = new EventEmitter<void>();
    softDeleteTask() {
      this.categoryService.softDeleteToDoItem(this.value).subscribe({
        next: () => {
          this.bsModalRef.hide();
          this.taskDeleted.emit();
        }
      })
    }
  
    ngOnInit() {
  
    }
  }