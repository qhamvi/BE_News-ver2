--DESKTOP-2BQ80VB\SQLEXPRESS   
--admin_webapi
--358
create database webapi
use webapi
create table [dbo].[Position](
	[PositionId] [int] IDENTITY(1,1),
	[PositionName] [nvarchar](20)
)
insert into [dbo].[Position]([PositionName]) values ('Admin')
insert into [dbo].[Position]([PositionName]) values ('Editor')
insert into [dbo].[Position]([PositionName]) values ('Author')
select *from [dbo].[Position]

create table [dbo].[User](
	[UserId] [int] IDENTITY(1,1),
	[UserName] [nvarchar](100),
	[Account] [nvarchar](100),
	[Password] [nvarchar](100),
	[Phone] [nvarchar] (10),
	[Email] [nvarchar](100),
	[PhotoFileName] [nvarchar](100),
	[Position] [nvarchar](20)--PositionName
)
insert into [dbo].[User] values (N'Tường Vi','vivi','358','0968687687','vi@gmail.com','cp.jpg','Admin');
select *from [dbo].[User]


create table [dbo].[New](
	[NewId] [int] IDENTITY(1,1),
	[NewTitle] [nvarchar](120),
	[NewSummary] [nvarchar](600),
	[NewContent] [nvarchar](max),--536,870,912 char
	[NewStatus] [nvarchar](5),--true false,
	[User] [nvarchar] (100),--UserName
	[Topic][nvarchar] (100),--TopicName
	[createDate][datetime],--ngay tao new
	[publishDate][datetime],--ngay dang
	[ImageFileName][nvarchar](2000),
	[Reason][nvarchar](2000)

)
insert into [dbo].[New] values(N'Báo số 1',N'Tóm tắt Báo 1',N'Nội dung báo 1','false',N'Tường Vi',N'Xã hội',GETDATE(),null,'cp.jpg',null);
insert into [dbo].[New] values(N'Báo số 2',N'Tóm tắt Báo 2',N'Nội dung báo 2','false',N'Tường Vi',N'Xã hội',GETDATE(),null,'cp.jpg',null);
select *from [dbo].[New] 
update [dbo].[New] set [NewStatus]='true',[publishDate]=getdate()  where NewId='1';

create table [dbo].[Topic](
	[TopicId][int] IDENTITY(1,1),
	[TopicName][nvarchar] (100),
)
insert into [dbo].[Topic] values(N'Kinh tế');
insert into [dbo].[Topic] values(N'Văn hóa');
insert into [dbo].[Topic] values(N'Sức khỏe');
insert into [dbo].[Topic] values(N'Thể thao');
insert into [dbo].[Topic] values(N'Giải trí');
insert into [dbo].[Topic] values(N'Giáo dục');
insert into [dbo].[Topic] values(N'Xã hội');
select *from [dbo].[Topic] 

