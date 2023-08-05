import { Component,Inject  } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
@Component({
  selector: 'app-simplest-dialog',
  templateUrl: './simplest-dialog.component.html',
})
export class SimplestDialogComponent {
  title: string;
  message: string;

  constructor(
    private dialogRef: MatDialogRef<SimplestDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any
  ) {
    this.title = data.title || 'Message';
    this.message = data.message || 'Unknown error occurred.';
  }
}
