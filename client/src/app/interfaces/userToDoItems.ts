export interface UserToDoItems {
  id: number;
  morning: UserCategoryDto;
  afternoon: UserCategoryDto;
  evening: UserCategoryDto;
  anytime: UserCategoryDto;
}

export interface UserCategoryDto {
  userToDoItems: UserToDoItemDto[];
}

export interface UserToDoItemDto {
  id: number;
  title: string;
  createdAt: string; // Dates are typically represented as strings in JSON and can be parsed to Date objects if necessary
  categoryId: number;
  userId:number;
}
