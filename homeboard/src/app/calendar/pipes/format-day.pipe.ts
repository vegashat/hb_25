import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'formatDay',
})
export class FormatDayPipe implements PipeTransform {


  transform(value: any): string {
    const date = new Date(value);
    if(this.isTodayOrTomorrow(date))
    {
      return this.getFriendlyDay(date)
    }
    else
    {
      return moment(value, "YYYY-MM-DD hh:mm").format('dddd MM/DD');
    }
  }

  private isTodayOrTomorrow(date : Date) : boolean {
     return  this.isToday(date) || this.isTomorrow(date);
  }

  private isToday(date : Date) : boolean{
    return  new Date(date).toDateString() === new Date().toDateString();
  }

  private isTomorrow(date : Date) : boolean{
    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    return  new Date(date).toDateString() === tomorrow.toDateString();
  }

  private getFriendlyDay(date : Date): string{
    return this.isToday(date) ? 'Today' : 'Tomorrow';
  }
}
