import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-addtaskmodal',
  templateUrl: './addtaskmodal.component.html',
  styleUrls: ['./addtaskmodal.component.scss']
})
export class AddtaskmodalComponent implements OnInit {

  title?: string;
  closeBtnName?: string;

  userId?: number
  errors: string[] | null = null;
  addToDoItemForm = this.fb.group({
    title: ['', Validators.required],
    category: ['', [Validators.required]],
  })

  constructor(private fb: FormBuilder, public bsModalRef: BsModalRef, private categoryService: CategoryService) { }
  @Output() taskAdded = new EventEmitter<void>();
  ngOnInit(): void {

  }

  onSubmit() {
    const formValue = this.addToDoItemForm.value;
    const value = {
      categoryId: formValue.category,
      title: formValue.title,
      userId: this.userId
    }
    this.categoryService.addToDoItem(value).subscribe({
      next: () => {
        this.bsModalRef.hide();
        this.taskAdded.emit();
      }
    })
  }
}
