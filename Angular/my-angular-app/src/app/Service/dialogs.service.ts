import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { SimplestDialogComponent } from '../dialogs/simplest-dialog.component';
@Injectable({
  providedIn: 'root'
})
export class DialogsService {

  constructor(private dialog: MatDialog) { }

  showMessage(message: string, title?: string): void {
    this.dialog.open(SimplestDialogComponent, {
      data: { message, title },
      width: '500px'
      
    });
  }
}


