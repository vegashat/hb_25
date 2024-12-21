export interface IEntry {
    startTime: Date;
    endTime: Date;
    description: string;
    isAllDay: boolean;
}


export interface ICalendar {
    date: Date;
    entries: IEntry[];
}
