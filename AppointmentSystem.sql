create database appointmentdb


create table PatientDetails(
PId int primary key identity not null,
FirstName varchar(20),
LastName varchar(20),
PhoneNumber varchar(10),
DOB date,
Gender char(1),
EmailId varchar(20),
Pwd varchar(20)
)

create table DoctorDetails(
DId int primary key identity not null,
FirstName varchar(20),
LastName varchar(20),
PhoneNumber varchar(10),
DOB date,
Gender char(1),
EmailId varchar(20),
Pwd varchar(20),
Specialization varchar(30),
ProfileImage varchar(50)
)

create table Appointments(
AppointmentId int primary key identity not null,
PatientId int,
DoctorId int,
AppointmentDate date,
StartTime time,
CreatedOn datetime,
Details varchar(50)
)

alter table Appointments
add foreign key (PatientId) references PatientDetails(PId)

alter table Appointments
add foreign key (DoctorId) references DoctorDetails(DId)

insert into PatientDetails values('Mary','Moos','9081112222','10/7/1990','F','mary.m@gmail.com','mary')
select * from PatientDetails

insert into DoctorDetails values('Hannah','Lee','9081112222','12/9/1990','F','hanna@hospital.com','hannah','Family Medicine','~/Images/Doctor1.jpg')
insert into DoctorDetails values('Mark','Height','9081113333','09/10/1980','F','mark@hospital.com','mark','Pediatrician','~/Images/Doctor1.jpg')
select * from DoctorDetails

create or alter procedure spValidateUser @UserId varchar(20), @Password varchar(20), @Result int output
as
if exists(select * from PatientDetails where EmailId=@UserId and Pwd=@Password)
select @Result=PId from PatientDetails where EmailId=@UserId and Pwd=@Password
--if (right(@UserId,8)='@hospital.com')
--begin
--	if exists(select * from DoctorDetails where EmailId=@UserId and Pwd=@Password)
--	select @Result = 1
--	else
--	select @Result = -1
--end
--else
--begin
--	if exists(select * from PatientDetails where EmailId=@UserId and Pwd=@Password)
--	select @Result = 2
--	else
--	select @Result = -1
--end

select * from PatientDetails where EmailId='mary.m@gmail.com' and Pwd='mary'

declare @Result int
exec spValidateUser @UserId='mary.m@gmail.com',@Password='mary',@Result=@Result output
select @Result

create or alter procedure spAddPatient @FirstName varchar(20),@LastName varchar(20),@PhoneNumber varchar(10),@DOB date,@Gender char(1),@EmailId varchar(20),@Pwd varchar(20)
as
insert into PatientDetails values(@FirstName,@LastName,@PhoneNumber,@DOB,@Gender,@EmailId,@Pwd)

create or alter procedure spNewAppointment @PatientId int,@DoctorId int,@AppointmentDate date,@StartTime time,@Details varchar(50) int,@DoctorId int,@AppointmentDate date,@StartTime time,@Details varchar(50)
as
insert into Appointments values(@PatientId,@DoctorId,@AppointmentDate,@StartTime,GETDATE(),@Details) 

declare @PatientId int,@DoctorId int,@AppointmentDate date,@StartTime time,@Details varchar(50)
exec spNewAppointment  @PatientId=1,@DoctorId =2,@AppointmentDate='10/10/2022',@StartTime='19:53' ,@Details='general'

select * from Appointments where PatientId=@
update DoctorDetails set ProfileImage='~/Images/Doctor1.jpg' where AppointmentId=10

create or alter procedure spEditDetails @PId int, @FirstName varchar(20),@LastName varchar(20),@PhoneNumber varchar(10),@DOB date,@Gender char(1),@EmailId varchar(20)
as
update PatientDetails
set FirstName=@FirstName,LastName=@LastName,PhoneNumber=@PhoneNumber,DOB=@DOB,Gender=@Gender,EmailId=@EmailId
where PId=@PId

create or alter procedure spRescheduleAppointment @AppointmentId int, @AppointmentDate date,@StartTime time
as 
update Appointments
set AppointmentDate=@AppointmentDate,StartTime=@StartTime
where AppointmentId=@AppointmentId

create or alter procedure spDeleteAppointment @AppointmentId int
as
delete Appointments where AppointmentId=@AppointmentId