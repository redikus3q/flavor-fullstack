import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './menu/menu.component';
import { MatButtonModule } from '@angular/material/button';



@NgModule({
  declarations: [
    MenuComponent
  ],
  imports: [
    CommonModule,
    MatButtonModule
  ],
  exports: [
    MenuComponent
  ]
})
export class SharedModule { }
