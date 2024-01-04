export interface DailyLog {
    id:number
    date: string;
    morning: Categories;
    afternoon: Categories;
    evening: Categories;
    anytime: Categories;
}

export interface Categories {
    toDoItems: ToDoItem[];
}

export interface ToDoItem {
    id:number
    title: string;
    isCompleted: boolean;
}

export interface DailyLogToDoItemDto{
    dailyLogId : number
    toDoItemId: number
}
