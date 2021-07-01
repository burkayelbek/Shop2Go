
export interface IProducts{
    Id:number,
    Title:string,
    Price:number,
    Description:string,
    PublishedDate:any,
    CategoryId:number,
    UserID:string,
    isApproved:number,
    Image:string[],
}

export interface ICategory{
    Id:number,
    Name:string
}

export interface IUserPorducts{
    Id:number,
    UserID:String,
    Title:string,
    Price:number,
    Description:string,
    PublishedDate:any,
    CategoryId:number
}

export interface IProductId{
    ProductId:number
}

export interface IFavoritedProduct{
    Id:number,
    UserID:String,
    ProductId:number,
    Products:{
        Id:number,
        Title:string,
        Price:number,
        Description:string,
        PublishedDate:any
    }
}

export interface IGetUser{
    Id:string,
    FirstName:string,
    LastName:string,
    UserName:string,
    Email:string
}

export interface IForgotPasswordMail{
    Email:string
}

export interface IResetPasswordModel{
    UserID:string,
    Email:string,
    Password:string
}

export interface IGetAllTickets {
    Id: string,
    UserID: string,
    UserFullName:string,
    Title:string,
    Message: string,
    Priority: number,
    Status:number,
    CreatedOn:any,
    FixedDate:any
}

export interface IDeleteTicketModel{
    Id:string[]
}

export interface IGetAllCategories{
    Id:number,
    Name:string,
    MatIconName:string
}