import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-today',
  templateUrl: './today.component.html',
  styleUrls: ['./today.component.css']
})
export class TodayComponent implements OnInit {

  currentDate : Date;

  constructor() { }

  ngOnInit() {
    this.currentDate = new Date();
  }
}
