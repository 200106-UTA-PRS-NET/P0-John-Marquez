create database PizzaDb;
go;-- this ensure the that sql will execute the statement/query above and then jump to nect statement/ query after
use PizzaDb;
go;
create schema PizzaStore; --- Schema can be an extra layer for all your DB objects
go;
create table PizzaStore.Employee(
-- name of the col, datatype, constraints (Identity will give auto generated value(seed, incremental))
Id int Identity(1,1),-- primary key,
fname varchar(max) not null, -- not null is constrainmt which enforces to have a value 
lname varchar(max) not null,
uname varchar(max) not null, 
pword varchar(max) not null,
Primary key (Id)
)

insert into PizzaStore.Employee(fname,lname,uname,pword) values('Jerry','Smith','A1234','password')
select * from PizzaStore.Employee

create table PizzaStore.Customer(
-- name of the col, datatype, constraints (Identity will give auto generated value(seed, incremental))
Id int Identity(1,1),-- primary key,
fname varchar(max) not null, -- not null is constrainmt which enforces to have a value 
lname varchar(max) not null,
uname varchar(max) not null, 
pword varchar(max) not null,
Primary key (Id)
)

insert into PizzaStore.Customer(fname,lname,uname,pword) values('John','Marquez','jcm2321','password')
insert into PizzaStore.Customer(fname,lname,uname,pword) values('Jake','Long','jlk2020','pass')
select * from PizzaStore.Customer

--UPDATE PizzaStore.Customer
--SET fname = 'Jake', lname= 'Long'
--WHERE Id = 2;

--TRUNCATE Table PizzaStore.Customer



create table PizzaStore.Records(
-- name of the col, datatype, constraints (Identity will give auto generated value(seed, incremental))
Id int Identity(1,1),-- primary key,
locatId int,
userId int,
total money,
dateT DateTime not null,
amountP int, 
pizzaType varchar(max) not null,
size varchar(max) not null,
crust varchar(max) not null,
Primary key (Id),
foreign key (userId) references PizzaStore.Customer(Id),
foreign key (locatId) references PizzaStore.Locations(Id), 
)

select * from PizzaStore.Records
--DROP TABLE PizzaStore.Records
--DELETE FROM PizzaStore.Records
--TRUNCATE TABLE PizzaStore.Records


create table PizzaStore.Pizza(
-- name of the col, datatype, constraints (Identity will give auto generated value(seed, incremental))
Id int Identity(1,1),-- primary key,
pizzaType varchar(max) not null,
large money,
med money,
small money,
Primary key (Id)
)

insert into PizzaStore.Pizza(pizzaType, large, med, small) values('Hawaiian', 12.99, 8.99, 5.99)
insert into PizzaStore.Pizza(pizzaType, large, med, small) values('Meat Lovers', 12.99, 8.99, 5.99)
insert into PizzaStore.Pizza(pizzaType, large, med, small) values('Supreme', 12.99, 8.99, 5.99)
insert into PizzaStore.Pizza(pizzaType, large, med, small) values('Custom', 8.99, 5.99, 3.99)
select * from PizzaStore.Pizza

--DELETE FROM PizzaStore.Pizza
--WHERE small = 5.99;

create Table PizzaStore.Locations(
Id int Identity(1,1),-- primary key,
locat varchar(max),
Primary key (Id)
)
select * from PizzaStore.Locations
insert into PizzaStore.Locations(locat) values ('1001 S Center St, Arlington')
insert into PizzaStore.Locations(locat) values ('1234 Candy Lane, Arlington')
insert into PizzaStore.Locations(locat) values ('2020 East Rd, Arlington')

--UPDATE PizzaStore.Locations
--SET locat = '2020 East Rd, Arlington, TX'
--WHERE Id = 3;

