CREATE TABLE Customer
(
CustomerId int primary key identity,
[Name] varchar(30) not null,
[Address] varchar(225) not null,
PropertyType varchar(50) not null,
NumberOfBedrooms int not null,
Evc varchar(8) null
)


CREATE TABLE Users
(
UserId int identity,
CustomerId int FOREIGN KEY REFERENCES Customer(CustomerId),
EmailId varchar(50) not null unique,
[Password] varchar(30) not null,
SaltedPassword varchar(40) not null, 
[role] varchar(20) not null,
CreatedDate datetime not null,
IsActive bit not null
)

CREATE TABLE Evc
(
EvcId int primary key,
EvcVoucher varchar(8) not null unique,
IsUsed bit not null
)

CREATE TABLE Bill
(
BillId int primary key,
CustomerId int FOREIGN KEY REFERENCES Customer(CustomerId),
DayElectricityReading int not null,
NightElectricityReading int not null,
GasReading int not null,
BillMonth varchar(20) not null,
IsPaid bit not null,
Amount int not null,
IsVoucherUsed bit not null,
EvcId int FOREIGN KEY REFERENCES Evc(EvcId),
DueDate datetime not null
)

CREATE TABLE Payments
(
CustomerId int FOREIGN KEY REFERENCES Customer(CustomerId),
BillId int FOREIGN KEY REFERENCES Bill(BillId),
PaymentStatus bit not null,
PaymentDate datetime not null
)

CREATE TABLE SetPrice
(
	SetId int identity not null,
	SetDate datetime not null,
	SetType varchar(100) not null,
	ElectricityPriceNight int not null,
	ElectricityPriceDay int not null,
	GasPrice int not null

)
