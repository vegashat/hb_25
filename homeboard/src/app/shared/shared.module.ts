import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MomentModule } from 'ngx-moment'
import { AngularFontAwesomeModule} from 'angular-font-awesome'
import { MatRippleModule} from '@angular/material'

@NgModule({
  imports: [
    CommonModule,
    MomentModule.forRoot(),
    AngularFontAwesomeModule,
    MatRippleModule
  ],
  exports:[
    CommonModule,
    MomentModule,
    AngularFontAwesomeModule,
    MatRippleModule
  ],
  declarations: []
})
export class SharedModule { }
