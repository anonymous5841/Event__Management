create database db_EventManagement

create table tb_User (
    [User_Id] int IDENTITY(100,1) Not null Primary key,
    Firstname varchar(20) Not null 
	CHECK (Firstname NOT LIKE '%[^a-zA-Z]%'),  --won't allow anthing other than alphabets
    Lastname varchar(20)
	CHECK (Lastname NOT LIKE '%[^a-zA-Z]%'),
    Email varchar(40) UNIQUE Not null,
    User_Password varchar(20) Not null,
    User_Role varchar(15) Not null
	CHECK(User_Role  IN ('Employee', 'Administrator')),
    Contact varchar(11) Not null 
	CHECK (Contact NOT LIKE '%[^0-9]%' and len(Contact) = 11), --only numeric data with legth 11 allowed
    User_Address varchar(30) not null);


-- Insert 1: Employee with all valid data
INSERT INTO tb_User (Firstname, Lastname, Email, User_Password, User_Role, Contact, User_Address)
VALUES ('Azeema', 'Sabir', 'azeema@example.com', 'azeema123', 'Administrator', '12345678901', '123 Elm Street');

-- Insert 2: Administrator with all valid data
INSERT INTO tb_User (Firstname, Lastname, Email, User_Password, User_Role, Contact, User_Address)
VALUES ('Jane', 'Smith', 'jane.smith@example.com', 'securepass', 'Administrator', '98765432101', '456 Maple Avenue');


-- Insert 2: Administrator with all valid data
INSERT INTO tb_User (Firstname, Lastname, Email, User_Password, User_Role, Contact, User_Address)
VALUES ('Areeba', 'Noor', 'areeba@example.com', 'a123', 'Employee', '01234567890', '456 Maple Avenue');


INSERT INTO tb_User (Firstname, Lastname, Email, User_Password, User_Role, Contact, User_Address)
VALUES ('Alice', 'Smith', 'alice.smith@example.com', 'password123', 'Employee', '03001234567', '123 Elm Street');

INSERT INTO tb_User (Firstname, Lastname, Email, User_Password, User_Role, Contact, User_Address)
VALUES ('Bob', 'Johnson', 'bob.johnson@example.com', 'securePass', 'Administrator', '03009876543', '456 Maple Avenue');

INSERT INTO tb_User (Firstname, Lastname, Email, User_Password, User_Role, Contact, User_Address)
VALUES ('Charlie', 'Brown', 'charlie.brown@example.com', 'charlie2024', 'Employee', '03005555555', '789 Oak Lane');

INSERT INTO tb_User (Firstname, Lastname, Email, User_Password, User_Role, Contact, User_Address)
VALUES ('Diana', 'Jones', 'diana.jones@example.com', 'DianaPwd#1', 'Employee', '03007777777', '321 Pine Road');

INSERT INTO tb_User (Firstname, Lastname, Email, User_Password, User_Role, Contact, User_Address)
VALUES ('Evan', 'Davis', 'evan.davis@example.com', 'Evan123!', 'Administrator', '03003333333', '654 Cedar Boulevard');

INSERT INTO tb_User (Firstname, Lastname, Email, User_Password, User_Role, Contact, User_Address)
VALUES ('Fiona', 'Clark', 'fiona.clark@example.com', 'FionaPass9', 'Employee', '03009999999', '987 Willow Street');

INSERT INTO tb_User (Firstname, Lastname, Email, User_Password, User_Role, Contact, User_Address)
VALUES ('George', 'Miller', 'george.miller@example.com', 'George#45', 'Administrator', '03004444444', '159 Birch Crescent');


create table tb_Vendor (
    Vendor_Id int not null PRIMARY KEY IDENTITY(400,1),
    Company_Name varchar(25) not null
	CHECK (Company_Name NOT LIKE '%[^a-zA-Z ]%'),
    Resource_Person_Contact varchar(11) Not null
	CHECK (Resource_Person_Contact NOT LIKE '%[^0-9]%' and len(Resource_Person_Contact) = 11),
    Contact_Number varchar(11) Not null
	CHECK (Contact_Number NOT LIKE '%[^0-9]%' and len(Contact_Number) = 11),
    Email varchar(50) not null UNIQUE,
    V_Address varchar(30),
    User_Id_FK int not null 
	FOREIGN KEY references tb_User([User_Id]),
);


INSERT INTO tb_Vendor (Company_Name, Resource_Person_Contact, Contact_Number, Email, V_Address, User_Id_FK)
VALUES 
 ('Gamma Innovations', '22233344455', '88899900011', 'sales@gammainnovations.com', '789 Tech Park', 102),
 ('Beta Solutions', '11122233344', '55566677788', 'contact@betasolutions.com', '456 Market Street', 101),
 ('Alpha Tech', '12345678901', '98765432101', 'info@alphatech.com', '123 Business Ave', 100),
('Alpha Tech', '12345678901', '98765432101', 'info@alphatech.com', '123 Business Ave', 100),
('Beta Solutions', '11122233344', '55566677788', 'contact@betasolutions.com', '456 Market Street', 101),
('Gamma Innovations', '22233344455', '88899900011', 'sales@gammainnovations.com', '789 Tech Park', 102),
('bismashah', '87268927637', '67394782980', 'yup@gmail.com', '876 model town', 108),
('dew', '62773647283', '29882672672', 'dew@gmail.com', 'homes', 108),
('Delta Corp', '33344455566', '77788899900', 'support@deltacorp.com', '123 Innovation Lane', 103),
('Epsilon Ltd', '44455566677', '99988877700', 'info@epsilonltd.com', '456 Tech Street', 104),
('Zeta Enterprises', '55566677788', '88877766655', 'contact@zetaenterprises.com', '789 Corporate Blvd', 105),
('Omega Systems', '66677788899', '99900011122', 'help@omegasystems.com', '567 Tech Valley', 106),
('Theta Services', '77788899900', '00011122233', 'sales@thetaservices.com', '890 Business Park', 108);


create table tb_Attendee (
    Attendee_Id int not null PRIMARY KEY IDENTITY(500,1),
    Admin_User_Id_FK int not null
	FOREIGN KEY references tb_User([User_Id]),
	User_Id_FK int not null
	FOREIGN KEY references tb_User([User_Id]),
    Feedback varchar(100),
);


INSERT INTO tb_Attendee (Admin_User_Id_FK, User_Id_FK, Feedback)
VALUES 
(100, 102, 'Great event management and support.'),
(100, 102, null),
(100, 101, 'Great event, well-organized!'),
(102, 103, 'Had a wonderful experience.'),
(101, 104, 'The event was informative and engaging.'),
(103, 105, 'Logistics could be improved.'),
(104, 106, 'Excellent speakers and content.'),
(105, 108, 'Venue was a bit crowded.'),
(100, 109, 'Loved the interactive sessions.'),
(102, 110, 'Good networking opportunities.')

create table tb_Events (
    Event_Id int not null PRIMARY KEY IDENTITY(600,1),
    Event_Name varchar(20) Not null 
	CHECK (Event_Name NOT LIKE '%[^a-zA-Z ]%'), 
    E_Date DATE Not null ,
	CHECK (E_Date > GETDATE()),
    Start_Time TIME Not null,
    End_Time TIME Not null,
    E_Type varchar(10) Not null 
	CHECK (E_Type NOT LIKE '%[^a-zA-Z]%'),
    E_Description varchar(100),
	Vendor_Price int
	CHECK(Vendor_Price > 0),
    Profit_Percent int not null
	CHECK(Profit_Percent between 0 and 100),
    Profit int not null CHECK (Profit >= 0),
	Revenue int not null CHECK (Revenue >= 0),
    Attendee_Id_FK int not null
	FOREIGN KEY references tb_Attendee(Attendee_Id),
	User_Id_FK int not null
	FOREIGN KEY references tb_User([User_Id]),
);

INSERT INTO tb_Events (Event_Name, E_Date, Start_Time, End_Time, E_Type, E_Description, Vendor_Price, Profit_Percent, Profit, Revenue, Attendee_Id_FK, User_Id_FK)
VALUES
('TechConference', '2024-12-20', '09:00:00', '17:00:00', 'Tech', 'A conference for tech enthusiasts.', 2000, 20, 400, 2160, 502, 102),
('hellyes', '2024-12-26', '04:00:00', '07:00:00', 'music', 'is going to be held in hell itself.', 60000, 20, 14000, 58800, 502, 102),
('gloom', '2024-12-18', '02:00:00', '05:00:00', 'musical', 'its going to be gloomy!', 89000, 10, 14800, 97680, 502, 108),
('ArtExpo', '2024-12-22', '10:00:00', '16:00:00', 'Art', 'An exhibition for local artists.', 15000, 30, 4500, 19500, 502, 101),
('CodeFest', '2024-12-28', '09:00:00', '18:00:00', 'Tech', 'A festival for coding enthusiasts.', 12000, 25, 3000, 15000, 505, 102),
('FoodCarnival', '2024-12-25', '11:00:00', '20:00:00', 'Food', 'A carnival for food lovers.', 25000, 35, 8750, 33750, 507, 103),
('MusicMania', '2024-12-30', '17:00:00', '23:00:00', 'music', 'An electrifying musical evening.', 40000, 40, 16000, 56000, 506, 108),
('BizCon', '2024-12-21', '08:00:00', '15:00:00', 'Business', 'A convention for business leaders.', 5000, 15, 750, 5750, 507, 101),
('CharityRun', '2024-12-27', '06:00:00', '12:00:00', 'Sports', 'A charity run for a noble cause.', 3000, 50, 1500, 4500, 508, 102),
('LitFest', '2024-12-29', '14:00:00', '20:00:00', 'Literature', 'A festival celebrating literature.', 8000, 20, 1600, 9600, 510, 104);

create table tb_Vendor_Registration (
    Vendor_Id_FK int not null
	FOREIGN KEY references tb_Vendor(Vendor_Id),
    Event_Id_FK int not null
	FOREIGN KEY references tb_Events(Event_Id),
    Admin_User_Id_FK int FOREIGN KEY
	references tb_User([User_Id]),
	Event_Status varchar(11)
	default 'Pending'
	CHECK(Event_Status IN ('Approved', 'Rejected','Pending')),
    PRIMARY KEY (Vendor_Id_FK, Event_Id_FK),
);

INSERT INTO tb_Vendor_Registration (Vendor_Id_FK, Event_Id_FK, Admin_User_Id_FK, Event_Status)
VALUES
(400, 617, NULL, 'Approved'),
(403, 649, NULL, 'Rejected'),
(403, 651, NULL, 'Approved'),
(401, 653, 101, 'Pending'), 
(402, 654, 102, 'Approved'), 
(410, 655, 103, 'Rejected'), 
(411, 656, 104, 'Pending'), 
(412, 657, 105, 'Approved'), 
(413, 658, 106, 'Pending'),
(414, 659, 109, 'Approved'); 


create table tb_Tickets (
    Ticket_Id int not null identity(700,1),
    Event_Id_FK int not null
	FOREIGN KEY references tb_Events(Event_Id),
    Ticket_Type varchar(6) default 'Bronze' CHECK(Ticket_Type IN ('Bronze', 'Gold')),
    Price int not null
	CHECK (Price > 0),
	Quantity int not null
	CHECK (Quantity > 0),
    PRIMARY KEY (Ticket_Id, Event_Id_FK),
);

INSERT INTO tb_Tickets (Event_Id_FK, Ticket_Type, Price, Quantity)
VALUES
(617, 'Gold', 150, 200), 
(653, 'Bronze', 80, 300),
(654, 'Gold', 200, 100), 
(655, 'Bronze', 60, 350),
(657, 'Gold', 180, 250),
(656, 'Gold', 250, 120),
(649, 'Bronze', 50, 500), 
(651, 'Gold', 120, 150),
(651, 'Bronze',	700, 60),
(649, 'Gold',	100, 50);

create table tb_Venue (
    Venue_Id int primary key IDENTITY(300,1),
    Venue_Name varchar(20) Not null 
	CHECK (Venue_Name NOT LIKE '%[^a-zA-Z ]%'), 
    Venue_Location varchar(40) not null,
    Capacity int not null
	CHECK(Capacity > 0),
    Contact_Number varchar(11) Not null
	CHECK (Contact_Number NOT LIKE '%[^0-9]%' and len(Contact_Number) = 11),
	User_Id_FK int not null
	FOREIGN KEY references tb_User([User_Id]),
);

INSERT INTO tb_Venue (Venue_Name, Venue_Location, Capacity, Contact_Number, User_Id_FK)
VALUES
('Event Plaza', '567 Celebration Blvd, Midtown', 700, '03101234567', 103),
('Elite Banquet', '890 Luxury Lane, Uptown', 400, '03204567890', 104),
('Community Center', '234 Unity Street, Old Town', 300, '03307894561', 105),
('Skyline Terrace', '654 Skyline Drive, Heights', 150, '03009876543', 106),
('Ocean View', '123 Seaside Road, Waterfront', 1200, '03112345678', 101);
('Grand Hall', '123 Main Street, City Center', 500, '03001234567', 102),
('Conference Room A', '456 Park Avenue, Downtown', 200, '03211234567', 102),
('Sports Arena', '789 Stadium Road, Suburb', 10000, '03321234567', 101),
('glamourpark', '987 ferozpur road', 1000, '03556782453', 108),
('few', 'home', 2000, '58445686543', 108);

create table tb_Venue_Registration (
    Venue_Id_FK int not null
	references tb_Venue(Venue_Id),
    Event_Id_FK int not null 
	references tb_Events(Event_Id),
    PRIMARY KEY (Venue_Id_FK, Event_Id_FK),
    Venue_Price int not null
	CHECK(Venue_Price > 0 ),
);

INSERT INTO tb_Venue_Registration (Venue_Id_FK, Event_Id_FK, Venue_Price)
VALUES
(300, 617, 1000),
(301, 649, 700),
(302, 651, 1500), 
(303, 653, 800), 
(304, 654, 1200), 
(305, 655, 1000), 
(306, 656, 1800), 
(307, 657, 600), 
(308, 658, 900), 
(309, 659, 1100); 

create table tb_Sponsors (
    Sponsor_Id int PRIMARY KEY IDENTITY(200,1),
    Sponsor_Name varchar(20) Not null 
	CHECK (Sponsor_Name NOT LIKE '%[^a-zA-Z ]%'), 
    Contact_Number varchar(11) Not null
	CHECK (Contact_Number NOT LIKE '%[^0-9]%' and len(Contact_Number) = 11),
    Email varchar(40)not null UNIQUE,
    Sponsor_Address varchar(35),
    User_Id_FK int not null
    references tb_User([User_Id]),
);

INSERT INTO tb_Sponsors (Sponsor_Name, Contact_Number, Email, Sponsor_Address, User_Id_FK)
VALUES
('BrightFuture', '03011223344', 'future@bright.com', '12 Sunrise Lane, Downtown', 103),
('GreenFields', '03456789012', 'contact@greenfields.com', '45 Eco Road, Suburb', 102),
('UrbanTech', '03119876543', 'info@urbantech.com', '78 Metropolitan Blvd, Uptown', 101),
('Visionary Corp', '03214567890', 'hello@visionary.com', '34 Dream Avenue, Green Zone', 108),
('Elite Group', '03098765432', 'elite@group.com', '90 Prestige Street, City Core', 102);
('TechCorp', '03123456789', 'techcorp@example.com', '123 Tech Road, City Center', 101),
('EduSmart', '03345678901', 'edusmart@example.com', '789 Education Ave, Suburb', 101),
('EduSmart', '03345678902', 'smart@example.com', '789 Education Ave, Suburb', 102),
('hadi', '74889305678', 'sponsor@gmail.com', 'home shahi guzar gah', 108),
('hadishah', '53667489564', 'sponsors@gmail.com', 'home shahi guzar gah', 108);

create table tb_Sponsors_Registration (
    Event_Id_FK int not null
	references tb_Events(Event_Id),
    Sponsor_Id_FK int not null
	references tb_Sponsors(Sponsor_Id)
    PRIMARY KEY (Event_Id_FK, Sponsor_Id_FK),
	Sponsor_Percent int not null
	CHECK(Sponsor_Percent between 0 and 100),
);

INSERT INTO tb_Sponsors_Registration (Event_Id_FK, Sponsor_Id_FK, Sponsor_Percent)
VALUES
(617, 200, 10),  
(651, 203, 15),  
(653, 205, 25),  
(654, 208, 30),  
(655, 209, 20), 
(656, 210, 18),
(657, 211, 12),
(617,203,10),
(649,202,30),
(651,205,40);

CREATE INDEX IDX_Email ON tb_User (Email);
CREATE INDEX IDX_VEmail ON tb_Vendor (Email);
CREATE INDEX IDX_Event_Date ON tb_Events (E_Date);
CREATE INDEX IDX_SEmail ON tb_Sponsors (Email);

--QUERIES

--sign up validation
CREATE PROCEDURE InsertUser(
    @u_Firstname varchar(20),
    @u_Lastname varchar(20),
    @u_Email varchar(30),
    @u_User_Password varchar(20),
    @u_User_Role varchar(15),
    @u_Contact varchar(11),
    @u_User_Address varchar(30)
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
    -- Check if email already exists
	
    IF exists (select -1 from tb_User where Email = @u_Email) 
	BEGIN
		ROLLBACK TRANSACTION;
		THROW 60000, 'User already exists!',1
		
		RETURN;
	END
    ELSE
	BEGIN
        -- Insert the new user
        INSERT INTO tb_User (Firstname, Lastname, Email, User_Password, User_Role, Contact, User_Address)
        VALUES (@u_Firstname, @u_Lastname, @u_Email, @u_User_Password, @u_User_Role, @u_Contact, @u_User_Address);
		
		COMMIT TRANSACTION;
		END
	  END TRY
	  BEGIN CATCH 
		ROLLBACK TRANSACTION;
		--THROW 50000, 'Transaction Rolled back',2
		throw;
	  END CATCH
END;

EXEC InsertUser
    @u_Firstname = 'John',
    @u_Lastname = 'Doe',
    @u_Email = 'johoe@example.com',
    @u_User_Password = 'securepassword',
    @u_User_Role = 'Administrator',
    @u_Contact = '12845678901',
    @u_User_Address = '123 Main St';


--Login validation
CREATE PROCEDURE ValidateUser
(
    @u_Email varchar(30),
    @u_User_Password varchar(20),
    @User_Id int OUTPUT,
	@status varchar(20) OUTPUT,
	@IsAttendee int OUTPUT
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

    -- Check if the user exists with the given email and password
    IF exists (select 1 from tb_User
	where Email = @u_Email AND User_Password = @u_User_Password)
   
   BEGIN
        -- Retrieve the User_Id
        select  @User_Id = [User_Id], @status = User_Role from tb_User
        WHERE Email = @u_Email AND User_Password = @u_User_Password;

		if exists (select 1 from tb_Attendee where User_Id_FK = @User_Id)
		Begin 
		SET @IsAttendee = 1
		End
		Else
		SET @IsAttendee = -1
		COMMIT TRANSACTION;
    END
    ELSE
    BEGIN
        -- If the email or password is incorrect, set the User_Id to -1 to indicate failure
         set @User_Id = -1;
		 set @IsAttendee = -1;
		 set @status = null;
		 ROLLBACK TRANSACTION;
		 RETURN;
    END
	 END TRY
	 BEGIN CATCH 

		ROLLBACK TRANSACTION;
		THROW 50000, 'Transaction Rolled back',2
	 
	 END CATCH
END;


-- for changing password
	CREATE PROCEDURE ForgotPassword
(
    @u_Email varchar(30),
    @u_Contact varchar(11),
    @u_NewPassword varchar(20)
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

		-- Check if the email and contact number match in the database
		IF exists (select 1 from tb_User where Email = @u_Email AND Contact = @u_Contact)
		BEGIN

        -- Update the password if a match is found
        UPDATE tb_User
        set User_Password = @u_NewPassword
        WHERE Email = @u_Email AND Contact = @u_Contact;
	
		COMMIT TRANSACTION;
    END
    ELSE
    BEGIN
		 ROLLBACK TRANSACTION;
		 THROW 60000, 'Invalid Email or Contact number!',1
		 RETURN;
    END
	 END TRY
	 BEGIN CATCH 

		ROLLBACK TRANSACTION;
		--THROW 50000, 'Transaction Rolled back  2222',2
	  THROW 50000, 'Transaction Rolled back', 2;
	 END CATCH
END;
	

CREATE PROCEDURE RegisterEvent
(
    @Event_Name varchar(20),
    @Event_Date Date,
    @Event_Type varchar(10),
    @Start_Time time,
    @End_Time time,
    @Attendee_id_FK int,
    @Event_Description varchar(100),
    @Vendor_Price int,
    @Profit_Percent int,
    @User_id int,
    @Sponsor_id_FK int,
    @Sponsor_Percent int,
    @Venue_id_FK int = 0,     
    @Vendor_id_FK int,
    @Venue_Price int = 0,    
    @Ticket_quantity_bronze int = 0,
    @Ticket_quantity_gold int = 0,
    @per_ticket_price int = 0,
	@Price float OUTPUT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Check if event with the same name and date exists
        IF EXISTS (SELECT -1 FROM tb_Events WHERE Event_Name = @Event_Name AND E_Date = @Event_Date)
        BEGIN
		 THROW 60000, 'Event already exists!', 1;
		END;

		DECLARE @total FLOAT = 0.0; 
		DECLARE @Profit FLOAT = 0.0;
        DECLARE @ticket_p INT = 0;
		DECLARE @ticket_g INT = 0;
		DECLARE @ticket_b INT = 0;
		DECLARE @venue_p INT = 0;
		DECLARE @vendor_p INT = @Vendor_Price;
		DECLARE @profit_p FLOAT = @Profit_Percent;
		Declare @sponsor_p float = @Sponsor_Percent;

        -- Insert the event into the table
        INSERT INTO tb_Events (Event_Name, E_Date, Start_Time, End_Time, E_Type, E_Description, Vendor_Price, Profit_Percent, Attendee_Id_FK, User_Id_FK, Profit, Revenue) 
        VALUES (@Event_Name, @Event_Date, @Start_Time, @End_Time, @Event_Type, @Event_Description, @Vendor_Price, @Profit_Percent, @Attendee_id_FK, @User_id, 0, 0);

        DECLARE @NewEventId INT;
        SET @NewEventId = SCOPE_IDENTITY();

        -- Insert into vendor registration
        INSERT INTO tb_Vendor_Registration (Vendor_Id_FK, Event_Id_FK, Admin_User_Id_FK, Event_Status)
        VALUES (@Vendor_id_FK, @NewEventId, NULL, 'Pending');

        -- Insert into sponsor registration
        INSERT INTO tb_Sponsors_Registration (Event_Id_FK, Sponsor_Id_FK, Sponsor_Percent)
        VALUES (@NewEventId, @Sponsor_id_FK, @Sponsor_Percent);


        -- Check and insert venue details only if both Venue_id_FK and Venue_Price are not NULL
        IF @Venue_id_FK > 0  AND @Venue_Price > 0
        BEGIN
            INSERT INTO tb_Venue_Registration (Venue_Id_FK, Event_Id_FK, Venue_Price)
            VALUES (@Venue_id_FK, @NewEventId, @Venue_Price);
			SET @venue_p = @Venue_Price;  -- Store venue price
        END;

        -- Insert bronze tickets
        IF @per_ticket_price > 0 AND @Ticket_quantity_bronze > 0
        BEGIN
            INSERT INTO tb_Tickets (Event_Id_FK, Ticket_Type, Price, Quantity)
            VALUES (@NewEventId, 'Bronze', @per_ticket_price, @Ticket_quantity_bronze);
			SET  @ticket_p = @per_ticket_price ;
			SET  @ticket_b = @Ticket_quantity_bronze;
        END;

        -- Insert gold tickets
        IF @per_ticket_price > 0 AND @Ticket_quantity_gold > 0
        BEGIN
            INSERT INTO tb_Tickets (Event_Id_FK, Ticket_Type, Price, Quantity)
            VALUES (@NewEventId, 'Gold', @per_ticket_price, @Ticket_quantity_gold);
			SET  @ticket_p = @per_ticket_price ;
			SET  @ticket_g = @Ticket_quantity_gold;
        END;

  EXEC Profit
	@Event_Id = @NewEventId,
	@ticket_price = @ticket_p,           
    @Ticket_quantity_gold = @ticket_g,     
    @Ticket_quantity_bronze = @ticket_b,   
    @vendor_Price = @vendor_p,          
    @venue_price = @venue_p,            
    @Profit_Percent = @profit_p;


	SELECT @Profit = Profit
	FROM tb_Events
	WHERE Event_Id = @NewEventId;

	SET @total =  (@ticket_p * @ticket_g) + (@ticket_p * @ticket_b) + @vendor_p + @venue_p + @Profit 
	
	EXEC Price 
    @total = @total, 
    @sponsor_percent = @sponsor_p, 
    @Price = @Price OUTPUT;

	UPDATE tb_Events
	SET Revenue = @Price
	WHERE Event_Id = @NewEventId;

        -- Commit the transaction if everything is successful
     COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback only if a transaction was initiated
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        -- Re-throw the error
        THROW;
    END CATCH;
END;


CREATE PROCEDURE DeleteEvent
(
    @Event_id INT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Check if the event exists
        IF EXISTS (SELECT 1 FROM tb_Events WHERE Event_Id = @Event_id)
        BEGIN
            -- Step 1: Delete related records in tb_Venue_Registration (foreign key reference)
            DELETE FROM tb_Venue_Registration WHERE Event_Id_FK = @Event_id;

            -- Step 2: Delete related records in tb_Vendor_Registration (foreign key reference)
            DELETE FROM tb_Vendor_Registration WHERE Event_Id_FK = @Event_id;

            -- Step 3: Delete related records in tb_Sponsors_Registration (foreign key reference)
            DELETE FROM tb_Sponsors_Registration WHERE Event_Id_FK = @Event_id;

            -- Step 4: Delete related records in tb_Tickets (foreign key reference)
            DELETE FROM tb_Tickets WHERE Event_Id_FK = @Event_id;

            -- Step 5: Now delete the event from tb_Events
            DELETE FROM tb_Events WHERE Event_Id = @Event_id;

            COMMIT TRANSACTION;
        END
        ELSE
        BEGIN
            ROLLBACK TRANSACTION;
            THROW 60000, 'Event does not exist!', 1;
        END
    END TRY
    BEGIN CATCH
        -- Rollback the transaction if an error occurs
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;


CREATE PROCEDURE UpdateEvent
(
	@Event_id int,
    @Event_Name varchar(20),
    @Event_Date Date,
    @Event_Type varchar(10),
    @Start_Time time,
    @End_Time time,
    @Attendee_id_FK int,
    @Event_Description varchar(100),
    @Vendor_Price int,
    @Profit_Percent int,
    @User_id int,
    @Sponsor_id_FK int,
    @Sponsor_Percent int,
    @Venue_id_FK int = 0,     
    @Vendor_id_FK int,
    @Venue_Price int = 0,    
    @Ticket_quantity_bronze int = 0,
    @Ticket_quantity_gold int = 0,
    @per_ticket_price int = 0,
	@Price float OUTPUT
 )
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

		IF exists ( select 1 from tb_Events
		WHERE @Event_id = Event_Id) 
	 Begin

		 IF ((@Venue_id_FK > 0 AND  @Venue_Price <= 0) OR (@Venue_id_FK <= 0 AND  @Venue_Price > 0))
            BEGIN
                ROLLBACK TRANSACTION;
                THROW 6001, 'Venue_Price and Venue_id both should be filled in pairs.', 1;
         END

		 IF (@per_ticket_price > 0 AND 
            (@Ticket_quantity_bronze <= 0 AND @Ticket_quantity_gold <= 0))
            BEGIN
                ROLLBACK TRANSACTION;
                THROW 6002, 'Ticket quantities must be provided for at least one ticket type with per_ticket_price.', 1;
         END

		 IF (((@Ticket_quantity_bronze > 0 OR @Ticket_quantity_gold > 0) 
			  AND @per_ticket_price <= 0) OR
			  ((@Ticket_quantity_bronze > 0  AND @Ticket_quantity_gold > 0)
			  AND @per_ticket_price <= 0))
            BEGIN
                ROLLBACK TRANSACTION;
                THROW 6003, 'Ticket price must be provided when ticket quantities are specified.', 1;
         END

		DECLARE @total FLOAT = 0.0; 
		DECLARE @Profit FLOAT = 0.0; 
		DECLARE @ticket_p INT = 0;
		DECLARE @ticket_g INT = 0;
		DECLARE @ticket_b INT = 0;
		DECLARE @venue_p INT = 0;
		DECLARE @vendor_p INT = @Vendor_Price;
		DECLARE @profit_p FLOAT = @Profit_Percent;
		Declare @sponsor_p float = @Sponsor_Percent;

		 update tb_Events
		 SET 
		 Event_Name = @Event_Name,
		 E_Date =  @Event_Date,
		 Start_Time = @Start_Time,
		 End_Time = @End_Time,
		 E_Type = @Event_Type,
		 E_Description = @Event_Description,
		 Vendor_Price = @Vendor_Price,
		 Profit_Percent = @Profit_Percent,
		 Attendee_Id_FK = @Attendee_id_FK,
		 @vendor_p = @Vendor_Price
		 Where Event_Id = @Event_id;
		
		UPDATE tb_Vendor_Registration
        SET
			Vendor_Id_FK = @Vendor_id_FK
           WHERE Event_Id_FK = @Event_id;

            -- Update tb_Sponsors_Registration
            UPDATE tb_Sponsors_Registration
            SET 
                Sponsor_Id_FK = @Sponsor_id_FK,
                Sponsor_Percent = @Sponsor_Percent
            WHERE Event_Id_FK = @Event_id;

            -- Update tb_Venue_Registration
				IF @Venue_id_FK > 0  AND @Venue_Price > 0  
				BEGIN
                UPDATE tb_Venue_Registration
                SET 
                    Venue_Id_FK = @Venue_id_FK,
                    Venue_Price = @Venue_Price
                WHERE Event_Id_FK = @Event_id;
            END

            -- Update tb_Tickets
             IF @per_ticket_price > 0
            BEGIN
                IF @Ticket_quantity_bronze > 0
                BEGIN
                    UPDATE tb_Tickets
                    SET 
                        Price = @per_ticket_price,
                        Quantity = @Ticket_quantity_bronze,
						@ticket_p = @per_ticket_price,
					    @ticket_b =@Ticket_quantity_bronze
                    WHERE Event_Id_FK = @Event_id AND Ticket_Type = 'Bronze';
                END

                IF @Ticket_quantity_gold > 0
                BEGIN
                    UPDATE tb_Tickets
                    SET 
                        Price = @per_ticket_price,
                        Quantity = @Ticket_quantity_gold,
						@ticket_p = @per_ticket_price,
					    @ticket_g =@Ticket_quantity_bronze
                    WHERE Event_Id_FK = @Event_id AND Ticket_Type = 'Gold';
                END
            END

			EXEC Profit
	@Event_Id = @Event_id,
	@ticket_price = @ticket_p,           
    @Ticket_quantity_gold = @ticket_g,     
    @Ticket_quantity_bronze = @ticket_b,   
    @vendor_Price = @vendor_p,          
    @venue_price = @venue_p,            
    @Profit_Percent = @profit_p;


	SELECT @Profit = Profit
	FROM tb_Events
	WHERE Event_Id = @Event_Id ;

	SET @total =  (@ticket_p * @ticket_g) + (@ticket_p * @ticket_b) + @vendor_p + @venue_p + @Profit 
	
	EXEC Price 
    @total = @total, 
    @sponsor_percent = @sponsor_p, 
     @Price = @Price OUTPUT;

	UPDATE tb_Events
	SET Revenue = @Price
	WHERE Event_Id = @Event_Id;
		 COMMIT TRANSACTION;
	 End
     ELSE
	    BEGIN

		ROLLBACK TRANSACTION;
		THROW 60000, 'Event does not exist!',1

	   END

	END TRY
	 BEGIN CATCH 

		ROLLBACK TRANSACTION;
		THROW 50000, 'Transaction Rolled back',2
	 
	 END CATCH
END;


CREATE PROCEDURE RegisterSponsor
(
 @Sponsor_Name varchar(20),
 @Sponsor_Email varchar(20),
 @Sponsor_Contact varchar(11),
 @Sponsor_Address varchar(30),
 @User_id inT
) 
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		IF exists (select -1 from tb_Sponsors WHERE Email = @Sponsor_Email 
		AND Contact_Number = @Sponsor_Contact)
		BEGIN

        ROLLBACK TRANSACTION;
		THROW 60000, 'Sponsor already exists!',1
		RETURN;

		END 
	ELSE 
		BEGIN

		Insert into tb_Sponsors (Sponsor_Name, Contact_Number, Email,  Sponsor_Address, User_Id_FK) 
		VALUES (@Sponsor_Name,  @Sponsor_Contact, @Sponsor_Email, @Sponsor_Address, @User_id);
		
		COMMIT TRANSACTION;
		END
	 END TRY
	 BEGIN CATCH 

		ROLLBACK TRANSACTION;
		THROW 50000, 'Transaction Rolled back',2
	 
	 END CATCH
END;

EXEC RegisterSponsor
    @Sponsor_Name = 'Example',
    @Sponsor_Email = 'exaei@email.com',
    @Sponsor_Contact = '23450945671',
    @Sponsor_Address = '123 Example St.',
    @User_id = 101;


 CREATE PROCEDURE DeleteSponsor
(
@Sponsor_id int
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

	 IF exists ( select 1 from tb_Sponsors WHERE Sponsor_Id = @Sponsor_id)
	 Begin
		delete from tb_Sponsors WHERE Sponsor_Id = @Sponsor_id;
	    
		COMMIT TRANSACTION;

	 End
     ELSE
	    BEGIN

        ROLLBACK TRANSACTION;
		THROW 60000, 'Sponsor does not exist!',1

	   END
	END TRY
	 BEGIN CATCH 

		ROLLBACK TRANSACTION;
		THROW;
	 
	 END CATCH
END;


CREATE PROCEDURE UpdateSponsor
(
 @Sponsor_id int,
 @Sponsor_Name varchar(20),
 @Sponsor_Email varchar(20),
 @Sponsor_Contact varchar(11),
 @Sponsor_Address varchar(30)
) 
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
		IF exists ( select 1 from tb_Sponsors WHERE Sponsor_Id = @Sponsor_id)
		BEGIN

		Update tb_Sponsors
		SET 
		Sponsor_Name = @Sponsor_Name,
        Contact_Number = @Sponsor_Contact,
		Email = @Sponsor_Email,
		Sponsor_Address = @Sponsor_Address
		Where @Sponsor_id = Sponsor_Id
		
		 COMMIT TRANSACTION;
	 End
     ELSE
	    BEGIN

		ROLLBACK TRANSACTION;
		THROW 60000, 'Sponsor does not exist!',1

	   END

	END TRY
	 BEGIN CATCH 

		ROLLBACK TRANSACTION;
		THROW 50000, 'Transaction Rolled back',2
	 
	 END CATCH
END;


CREATE PROCEDURE RegisterVenue(
    @Venue_Name varchar(20),
    @Venue_Location varchar(30),
    @Venue_Capacity int, 
    @Venue_Contact varchar(11),
	@User_id int
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

		IF exists (select -1 from tb_Venue WHERE Contact_Number = @Venue_Contact )
	  BEGIN
        
		ROLLBACK TRANSACTION;
		THROW 60000, 'Venue already exists!', 1;
        RETURN;

	  END 
	ELSE 
	   BEGIN
       
	   Insert into tb_Venue (Venue_Name, Venue_Location, Capacity, Contact_Number, User_Id_FK )
       values (@Venue_Name, @Venue_Location, @Venue_Capacity, @Venue_Contact, @User_id);
	 
	   COMMIT TRANSACTION;
	 END
    END TRY
    BEGIN CATCH
        -- Rollback transaction in case of an error
        ROLLBACK TRANSACTION;
        THROW 50000, 'Transaction Rolled back', 2;
    END CATCH
END;

EXEC RegisterVenue 
    @Venue_Name = 'Grand Hall',
    @Venue_Location = 'Downtown City',
    @Venue_Capacity = 500,
    @Venue_Contact = '12345678901',
    @User_id = 101;
	

CREATE PROCEDURE DeleteVenue
(
@Venue_id int
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
	   IF exists ( select 1 from tb_Venue WHERE Venue_Id = @Venue_id)
		Begin

	     delete from tb_Venue WHERE Venue_Id = @Venue_id;
	 
	    COMMIT TRANSACTION;
		END
     ELSE
        BEGIN
            -- Raise an error if the venue does not exist
			ROLLBACK TRANSACTION;
            THROW 60000, 'Venue does not exist!', 1;
        END
    END TRY
    BEGIN CATCH
        -- Rollback transaction in case of an error
        ROLLBACK TRANSACTION;
        THROW 50000, 'Transaction Rolled back', 2;
    END CATCH
END;



CREATE PROCEDURE UpdateVenue
(
    @Venue_id INT,
    @Venue_Name VARCHAR(20),
    @Venue_Location VARCHAR(30),
    @Venue_Capacity INT, 
    @Venue_Contact VARCHAR(11)
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Check if the venue exists
        if exists (select 1 from tb_Venue WHERE Venue_Id = @Venue_id)
        BEGIN
            -- Update the venue details
            UPDATE tb_Venue
            SET
                Venue_Name = @Venue_Name,
                Venue_Location = @Venue_Location,
                Capacity = @Venue_Capacity,
                Contact_Number = @Venue_Contact
            WHERE Venue_Id = @Venue_id;

            -- Commit the transaction if update is successful
            COMMIT TRANSACTION;
        END
        ELSE
        BEGIN
            -- Rollback the transaction if the venue does not exist
            ROLLBACK TRANSACTION;
            THROW 60000, 'Venue does not exist!', 1;
        END

    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of any error
        ROLLBACK TRANSACTION;
        THROW 50000, 'Transaction Rolled back', 2;
    END CATCH
END;

EXEC UpdateVenue 
    @Venue_id = 316, 
    @Venue_Name = 'diyanoor', 
    @Venue_Location = 'CityCenter', 
    @Venue_Capacity = 300, 
    @Venue_Contact = '09876543211';



CREATE PROCEDURE RegisterVendor(
   @Company_Name varchar(15), 
   @R_P_Contact varchar(11),
   @Contact_Number varchar(11), 
   @Vendor_Email varchar(20), 
   @Vendor_Address varchar(30),
   @User_id int
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

		IF exists (select -1 from tb_Vendor WHERE Email = @Vendor_Email AND Contact_Number = @Contact_Number )
	    BEGIN
        
		ROLLBACK TRANSACTION;
		THROW 60000, 'Vendor already exists!', 1;
        RETURN;

	  END 
	ELSE 
	   BEGIN 
		Insert into tb_Vendor (Company_Name, Resource_Person_Contact , Contact_Number , Email, V_Address, User_Id_FK)
		VALUES (@Company_Name, @R_P_Contact, @Contact_Number, @Vendor_Email, @Vendor_Address, @User_id);
		
		COMMIT TRANSACTION;
	    END
		END TRY
    BEGIN CATCH
        -- Rollback transaction in case of an error
        ROLLBACK TRANSACTION;
        THROW 50000, 'Transaction Rolled back', 2;
    END CATCH
END;


CREATE PROCEDURE DeleteVendor
(
@Vendor_id int 
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
		IF exists ( select 1 from tb_Vendor
		 WHERE @Vendor_id = Vendor_Id) 
	     Begin

		 delete from tb_Vendor WHERE Vendor_Id = @Vendor_id;
		 
		 COMMIT TRANSACTION;
		END
     ELSE
        BEGIN
            -- Raise an error if the vendor does not exist
			ROLLBACK TRANSACTION;
            THROW 60000, 'Vendor does not exist!', 1;
        END
    END TRY
    BEGIN CATCH
        -- Rollback transaction in case of an error
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;



EXEC UpdateVendor 
   @Vendor_id= 400, 
   @Company_Name = 'urooj', 
   @Vendor_Address = 'model town', 
   @R_P_Contact = '12345678901',
   @Contact_Number = '12345678901',
   @Vendor_Email = 'yes3@gmail.com';

CREATE PROCEDURE UpdateVendor
(
@Vendor_id int,
@Company_Name varchar(15), 
@R_P_Contact varchar(11),
@Contact_Number varchar(11), 
@Vendor_Email varchar(20), 
@Vendor_Address varchar(30)
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
		IF exists ( select 1 from tb_Vendor
		WHERE @Vendor_id = Vendor_Id) 
	    Begin

		Update tb_Vendor
		SET  
		Company_Name = @Company_Name,
		Resource_Person_Contact = @R_P_Contact,
		Contact_Number = @Contact_Number,
		Email = @Vendor_Email,
		V_Address = @Vendor_Address
		WHERE Vendor_Id = @Vendor_id;

        COMMIT TRANSACTION;
	 End
     ELSE
	    BEGIN

		ROLLBACK TRANSACTION;
		THROW 60000, 'Vendor does not exist!',1

	   END

	 END TRY
	 BEGIN CATCH 

		ROLLBACK TRANSACTION;
		THROW 50000, 'Transaction Rolled back',2
	 
	 END CATCH
END;


--GET Tables for editing

CREATE PROCEDURE GetVendorsByUserId
    @UserId INT
AS
BEGIN
    SELECT 
        Vendor_Id, 
        Company_Name, 
        Resource_Person_Contact, 
        Contact_Number, 
        Email, 
        V_Address
    FROM tb_Vendor
    WHERE User_Id_FK = @UserId;
END;

CREATE PROCEDURE GetSponsorsByUserId
    @UserId int
AS
BEGIN
    SELECT 
        Sponsor_Id, 
        Sponsor_Name, 
        Contact_Number, 
        Email, 
        Sponsor_Address
	FROM tb_Sponsors
    WHERE User_Id_FK = @UserId;
END;

CREATE PROCEDURE GetVenuesByUserId
(
    @UserId int
)
AS
BEGIN
    SELECT 
        Venue_Id, 
        Venue_Name, 
        Venue_Location, 
        Capacity, 
        Contact_Number
    FROM tb_Venue
    WHERE User_Id_FK = @UserId;
END;

--checking phase
CREATE PROCEDURE GetDescription
(
@Event_id int,
@Feedback varchar(100) OUTPUT
)
AS
BEGIN
if exists (select 1 from tb_Events where Event_Id = @Event_id)
Begin
select @Feedback = Feedback from tb_Attendee
where Attendee_Id = 
(select Attendee_Id_FK from tb_Events where Event_Id = @Event_id)
End
Else
Begin
		THROW 60000, 'Event does not exist!',1
End
END;

Create Procedure EditDescription
(
@Event_id int,
@Feedback varchar(100)
)
AS
BEGIN
if exists (select 1 from tb_Events where Event_Id = @Event_id)
Begin
Update tb_Attendee
SET
 Feedback = @Feedback 
where Attendee_Id = 
(select Attendee_Id_FK from tb_Events where Event_Id = @Event_id)
End
Else
Begin
		THROW 60000, 'Event does not exist!',1
End
END;

Create Procedure AddDescription
(
    @Event_id INT,
    @Feedback VARCHAR(100)
)
AS
BEGIN
    -- Check if the Event_Id exists
    IF EXISTS (SELECT 1 FROM tb_Events WHERE Event_Id = @Event_id)
    BEGIN
        -- Check if Attendee_Id_FK is not null
        IF EXISTS (SELECT 1 FROM tb_Events WHERE Event_Id = @Event_id AND Attendee_Id_FK IS NOT NULL)
        BEGIN
            -- Update Feedback in tb_Attendee
            UPDATE tb_Attendee
            SET Feedback = @Feedback
            WHERE Attendee_Id = 
            (
                SELECT Attendee_Id_FK
                FROM tb_Events
                WHERE Event_Id = @Event_id
            );

            PRINT 'Feedback successfully updated.';
        END
        ELSE
        BEGIN
            THROW 60001, 'Attendee ID is missing for the specified Event ID.', 1;
        END
    END
    ELSE
    BEGIN
        THROW 60000, 'Event does not exist!', 1;
    END
END;

create Procedure GetUserSpecifiedEvent
(
@User_id int
)
AS
Begin
SELECT 
    E.Event_Id,
    E.Event_Name,
    A.Feedback
FROM 
    tb_Events E
INNER JOIN 
    tb_Attendee A
ON 
    E.Attendee_Id_FK = A.Attendee_Id
WHERE 
    A.User_Id_FK = @User_Id;
End;




--fetching data
CREATE PROCEDURE GetEventDetails
(
    @Event_Id INT 
)
AS
BEGIN
    BEGIN TRY
        
		IF exists ( select 1 from tb_Events WHERE Event_Id = @Event_id)
     Begin
        SELECT 
            e.Event_Name,
            e.E_Date,
            e.E_Type,
            e.Start_Time,
            e.End_Time,
            e.Attendee_Id_FK,
            e.E_Description,
            e.Vendor_Price,
            e.Profit_Percent,
            v.Vendor_Id_FK,
            s.Sponsor_Id_FK,
            s.Sponsor_Percent,
            vr.Venue_Id_FK,
            vr.Venue_Price,
            tbBronze.Quantity AS Ticket_quantity_bronze,
            tbGold.Quantity AS Ticket_quantity_gold,
            tbBronze.Price AS per_ticket_price -- Assuming bronze and gold prices are the same
        FROM tb_Events e
        LEFT JOIN tb_Vendor_Registration v ON e.Event_Id = v.Event_Id_FK
        LEFT JOIN tb_Sponsors_Registration s ON e.Event_Id = s.Event_Id_FK
        LEFT JOIN tb_Venue_Registration vr ON e.Event_Id = vr.Event_Id_FK
        LEFT JOIN tb_Tickets tbBronze 
            ON e.Event_Id = tbBronze.Event_Id_FK AND tbBronze.Ticket_Type = 'Bronze'
        LEFT JOIN tb_Tickets tbGold 
            ON e.Event_Id = tbGold.Event_Id_FK AND tbGold.Ticket_Type = 'Gold'
        WHERE e.Event_Id = @Event_Id;
		END
	END TRY
    BEGIN CATCH
        -- Handle any errors
		THROW 70000, 'Event does not exists!',1
    END CATCH
END;


CREATE PROCEDURE GetEvents
(
    @User_id INT 
)
AS
BEGIN
    BEGIN TRY
        
		IF exists ( select 1 from tb_Events WHERE User_Id_FK = @User_id)
     Begin
	 SELECT 
    e.Event_Id,
    e.Event_Name,
    e.E_Type,
    e.E_Date,
    e.Attendee_Id_FK,
    e.Profit_Percent,
    e.Profit,
    s.Sponsor_Id_FK,
    s.Sponsor_Percent
FROM 
    tb_Events e
LEFT JOIN 
    tb_Sponsors_Registration s 
ON 
    e.Event_Id = s.Event_Id_FK
WHERE 
    e.Attendee_Id_FK = (
        SELECT Attendee_Id 
        FROM tb_Attendees 
        WHERE User_Id_FK = @User_id
    );

       END
	END TRY
    BEGIN CATCH
        -- Handle any errors
		THROW 70000, 'Event does not exists!',1
    END CATCH
END;


CREATE PROCEDURE GetEvent
(
    @User_id INT 
)
AS
BEGIN
        IF EXISTS (SELECT 1 FROM tb_Events WHERE User_Id_FK = @User_id)
      
            SELECT 
                e.Event_Id,
                e.Event_Name,
                e.E_Type,
                e.E_Date,
                e.Attendee_Id_FK,
                e.Profit_Percent,
                e.Profit,
                s.Sponsor_Id_FK,
                s.Sponsor_Percent
            FROM 
                tb_Events e
            LEFT JOIN 
                tb_Sponsors_Registration s 
            ON 
                e.Event_Id = s.Event_Id_FK
            WHERE 
                e.User_Id_FK = @User_id    
END;





-- Stored Procedure to fetch vendor details based on parameters
CREATE PROCEDURE GetVendorDetails
(
    @Vendor_id int,
	@Name varchar(15) OUTPUT,
	@RP varchar(11) OUTPUT,
	@Contact varchar(11) OUTPUT,
	@Email varchar(20) OUTPUT,
	@Address varchar(30) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        -- Query to fetch vendor details from the backend
        SELECT 
            @Name = v.Company_Name,
            @RP = v.Resource_Person_Contact,
            @Contact = v.Contact_Number,
            @Email = v.Email,
            @Address = v.V_Address
        FROM tb_Vendor v
        WHERE Vendor_Id = @Vendor_id
      END TRY
    BEGIN CATCH
        -- Handle error, e.g., Venue does not exist
        THROW 70003, 'Vendor does not exist!', 1;
    END CATCH
END;



CREATE PROCEDURE GetVenueDetails
(
	@Venue_Id INT,
    @Name varchar(20) OUTPUT,
    @Location varchar(30) OUTPUT,
    @Capacity int OUTPUT, 
    @Contact varchar(11) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        -- Query to fetch venue details from the backend
        SELECT 
            @Name = v.Venue_Name,
            @Location = v.Venue_Location,
            @Capacity = v.Capacity,
            @Contact = v.Contact_Number
        FROM tb_Venue v
        WHERE v.Venue_Id = @Venue_Id;
    END TRY
    BEGIN CATCH
        -- Handle error, e.g., Venue does not exist
        THROW 70003, 'Venue does not exist!', 1;
    END CATCH
END;


-- Stored Procedure to fetch sponsor details based on Sponsor_Id
CREATE PROCEDURE GetSponsorDetails
(
    @Sponsor_Id INT,
	@Name varchar(20) OUTPUT,
	@Contact varchar(11) OUTPUT,
	@Email varchar(20) OUTPUT,
	@Address varchar(30) OUTPUT)
AS
BEGIN
    BEGIN TRY
        -- Query to fetch sponsor details from the backend
        SELECT 
           @Name = s.Sponsor_Name,
           @Contact = s.Contact_Number,
           @Email = s.Email,
           @Address = s.Sponsor_Address
        FROM tb_Sponsors s
        WHERE s.Sponsor_Id = @Sponsor_Id;
    END TRY
    BEGIN CATCH
        -- Handle error, e.g., Sponsor does not exist
        THROW 70004, 'Sponsor does not exist!', 1;
    END CATCH
END;



--Utils
CREATE PROCEDURE Profit
(
    @Event_Id INT,              
    @ticket_price INT,         
    @Ticket_quantity_gold INT, 
    @Ticket_quantity_bronze INT, 
    @vendor_Price INT,          
    @venue_price INT,           
    @Profit_Percent FLOAT      
)
AS
BEGIN
    DECLARE @profit INT = 0;

    -- Start with the Vendor Price
    SET @profit = @vendor_Price;

    -- Add Ticket Price if available (Gold Tickets)
    IF @ticket_price IS NOT NULL AND @ticket_price > 0
    BEGIN
        IF @Ticket_quantity_bronze IS NOT NULL AND @Ticket_quantity_bronze > 0
        BEGIN
            SET @profit = @profit + (@ticket_price * @Ticket_quantity_bronze);  -- Add Bronze Ticket revenue
        END

        IF @Ticket_quantity_gold IS NOT NULL AND @Ticket_quantity_gold > 0
        BEGIN
            SET @profit = @profit + (@ticket_price * @Ticket_quantity_gold);    -- Add Gold Ticket revenue
        END
    END

    -- Add Venue Price if available
    IF @venue_price IS NOT NULL AND @venue_price <> 0
    BEGIN
        SET @profit = @profit + @venue_price;  -- Add venue revenue
    END

    -- Apply profit percentage
    SET @profit = @profit * (@Profit_Percent / 100.0);  -- Apply profit percent

    -- Update the Profit field in the tb_Events table with the calculated profit
    UPDATE tb_Events
    SET Profit = @profit
    WHERE Event_Id = @Event_Id;
END;


Create Proc Price 
(
	@total float,
	@sponsor_percent float,
	@Price float Output
)
AS
BEGIN
   SET @Price = @total - (@total * @sponsor_percent / 100);
END;


--functional procs
CREATE PROCEDURE GetLastWeekEvents
AS
BEGIN
    SELECT  
        Event_Name, 
        E_Date, 
        E_Type, 
        Profit
    FROM 
        tb_Events
    WHERE 
        E_Date BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE()
    ORDER BY 
        E_Date, Start_Time;
END;


CREATE PROCEDURE GetUpcomingEvents
AS
BEGIN
    SELECT 
        Event_Name, 
        E_Date, 
        E_Type, 
        Profit
    FROM 
        tb_Events
    WHERE 
        E_Date > GETDATE()
    ORDER BY 
        E_Date, Start_Time;
END;


CREATE PROCEDURE GetLastMonthProfitSum
AS
BEGIN
    SELECT  
        SUM(Profit) AS LastMonth_Profit
    FROM 
        tb_Events
    WHERE 
        E_Date BETWEEN DATEADD(DAY, -30, GETDATE()) AND GETDATE();
END;


CREATE PROCEDURE GetLastWeekProfitSum
AS
BEGIN
    SELECT  
        SUM(Profit) AS LastWeek_Profit
    FROM 
        tb_Events
    WHERE 
        E_Date BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE();
END;


CREATE PROCEDURE GetTotalProfitUptoNow
AS
BEGIN
    SELECT 
        SUM(Profit) AS Total_Profit
    FROM 
        tb_Events
    WHERE 
        E_Date <= GETDATE();
END;


CREATE PROCEDURE GetTotalRevenue
AS
BEGIN
    SELECT SUM(Revenue) AS TotalRevenue
    FROM tb_Events
    WHERE E_Date <= GETDATE(); 
END;


CREATE PROCEDURE GetLastMonthRevenue
AS
BEGIN
    SELECT SUM(Revenue) AS LastMonthRevenue
    FROM tb_Events
    WHERE E_Date BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE();  -- Revenue for the last month
END;


CREATE PROCEDURE GetTotalEventsBeforeToday
AS
BEGIN
    SELECT COUNT(*) AS TotalEventsBeforeToday
    FROM tb_Events
    WHERE E_Date < GETDATE();
END;


CREATE PROCEDURE GetTotalEventsBeforeTodayPercentage
AS
BEGIN
    DECLARE @totalEvents INT;
    DECLARE @totalEventsBeforeToday INT;
    DECLARE @percentage int;

    -- Get the total number of events
    SELECT @totalEvents = COUNT(*) FROM tb_Events;

    -- Get the total number of events before today
    SELECT @totalEventsBeforeToday = COUNT(*) FROM tb_Events WHERE E_Date < GETDATE();

    -- Calculate the percentage
    IF @totalEvents > 0
    BEGIN
        SET @percentage = (@totalEventsBeforeToday * 100.0) / @totalEvents;
    END
    ELSE
    BEGIN
        SET @percentage = 0;
    END


    -- Return the percentage
    SELECT @percentage AS EventsBeforeTodayPercentage;
END;


CREATE PROCEDURE GetLastWeekEventsPercentage
AS
BEGIN
    DECLARE @totalEvents INT;
    DECLARE @lastWeekEvents INT;
    DECLARE @percentage FLOAT;

    -- Get the total number of events
    SELECT @totalEvents = COUNT(*) FROM tb_Events;

    -- Get the number of events in the last week
    SELECT @lastWeekEvents = COUNT(*) 
    FROM tb_Events 
    WHERE E_Date BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE();

    -- Calculate the percentage
    IF @totalEvents > 0
    BEGIN
        SET @percentage = (@lastWeekEvents * 100.0) / @totalEvents;
    END
    ELSE
    BEGIN
        SET @percentage = 0;
    END

    -- Return the percentage
    SELECT @percentage AS LastWeekEventsPercentage;
END;


CREATE PROCEDURE GetVendorPercentageCount
AS
BEGIN
    -- Total vendors
    DECLARE @TotalVendors INT;
    SELECT @TotalVendors = COUNT(*) 
    FROM tb_Vendor_Registration;

    -- Approved vendors
    DECLARE @ApprovedVendors INT;
    SELECT @ApprovedVendors = COUNT(*) 
    FROM tb_Vendor_Registration
    WHERE Event_Status = 'Approved';

    -- Disapproved vendors
    DECLARE @RejectedVendors INT;
    SELECT @RejectedVendors = COUNT(*) 
    FROM tb_Vendor_Registration
    WHERE Event_Status = 'Disapproved';

    -- Pending vendors
    DECLARE @PendingVendors INT;
    SELECT @PendingVendors = COUNT(*) 
    FROM tb_Vendor_Registration
    WHERE Event_Status = 'Pending';

    -- Calculate percentages (return as integers)
    DECLARE @ApprovedPercentage INT;
    DECLARE @RejectedPercentage INT;
    DECLARE @PendingPercentage INT;

    -- Avoid division by zero if total vendors are 0
    IF @TotalVendors > 0
    BEGIN
        SET @ApprovedPercentage = CAST((@ApprovedVendors * 100) / @TotalVendors AS INT);
        SET @RejectedPercentage = CAST((@RejectedVendors * 100) / @TotalVendors AS INT);
        SET @PendingPercentage = CAST((@PendingVendors * 100) / @TotalVendors AS INT);
    END
    ELSE
    BEGIN
        SET @ApprovedPercentage = 0;
        SET @RejectedPercentage = 0;
        SET @PendingPercentage = 0;
    END

    -- Return only the percentages as a single row
    SELECT 
        @ApprovedPercentage AS Approved_Percentage,
        @RejectedPercentage AS Rejected_Percentage,
        @PendingPercentage AS Pending_Percentage;
END;



-- checking if present at back or not
--event
CREATE PROCEDURE ValidateEventID
    @EventID INT,
    @IsPresent BIT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM tb_Events WHERE event_id = @EventID)
        SET @IsPresent = 1;
    ELSE
        SET @IsPresent = 0;
END;

CREATE PROCEDURE ValidateSponsorID
    @SponsorID INT,
    @IsPresent BIT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM tb_Sponsors WHERE Sponsor_Id = @SponsorID)
        SET @IsPresent = 1;
    ELSE
        SET @IsPresent = 0;
END;

CREATE PROCEDURE ValidateVenueID
    @VenueID INT,
    @IsPresent BIT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM tb_Venue WHERE Venue_Id = @VenueID)
        SET @IsPresent = 1;
    ELSE
        SET @IsPresent = 0;
END;

CREATE PROCEDURE ValidateVendorID
    @VendorID INT,
    @IsPresent BIT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM tb_Vendor WHERE Vendor_Id = @VendorID)
        SET @IsPresent = 1;
    ELSE
        SET @IsPresent = 0;
END;

--azeema

-- Create the View for eventsApproval form
CREATE VIEW vw_Events AS
SELECT 
    Event_Id, 
    Event_Name, 
    E_Date, 
    Start_Time, 
    End_Time, 
    E_Type, 
    User_Id_FK
FROM tb_Events;

CREATE TRIGGER trg_vw_Events_All
ON vw_Events
INSTEAD OF INSERT, UPDATE, DELETE
AS
BEGIN
    RAISERROR ('INSERT, UPDATE, and DELETE operations are not allowed on vw_Events.', 16, 1);
END;

--view for eventsviewmore 
CREATE VIEW vw_Eventsss AS
SELECT 
    Event_Id, 
    Event_Name, 
    E_Date, 
    Start_Time, 
    End_Time, 
    E_Type, 
    E_Description, 
    Vendor_Price, 
    Profit_Percent, 
    Attendee_Id_FK, 
    User_Id_FK
FROM tb_Events;

-- Restrict Modifications on vw_Events
CREATE TRIGGER trg_RestrictModifications_Events 
ON vw_Eventsss
INSTEAD OF INSERT, UPDATE, DELETE
AS
BEGIN
    RAISERROR ('Modifications (INSERT, UPDATE, DELETE) are not allowed on Events Table.', 16, 1);
END;

CREATE VIEW vw_Approved_Events AS
SELECT 
    e.Event_Id,
    e.Event_Name,
    e.E_Date,
    e.Start_Time,
    e.End_Time,
    e.E_Type,
    e.E_Description,
    e.Vendor_Price,
    e.Profit_Percent,
    e.Attendee_Id_FK,
    e.User_Id_FK
FROM 
    tb_Events e
INNER JOIN 
    tb_Vendor_Registration vr
    ON e.Event_Id = vr.Event_Id_FK
WHERE 
    vr.Event_Status = 'Approved';


CREATE VIEW vw_Rejected_Events AS
SELECT 
    e.Event_Id,
    e.Event_Name,
    e.E_Date,
    e.Start_Time,
    e.End_Time,
    e.E_Type,
    e.E_Description,
    e.Vendor_Price,
    e.Profit_Percent,
    e.Attendee_Id_FK,
    e.User_Id_FK,
    vr.Vendor_Id_FK,
    vr.Admin_User_Id_FK,
    vr.Event_Status
FROM 
    tb_Events e
JOIN 
    tb_Vendor_Registration vr ON e.Event_Id = vr.Event_Id_FK
WHERE 
    vr.Event_Status = 'Rejected';


CREATE VIEW vw_SponsorDetails AS
SELECT 
    Sponsor_Id,
    Sponsor_Name,
    Contact_Number,
    Email,
    Sponsor_Address,
    User_Id_FK AS User_Id
FROM 
    tb_Sponsors;

CREATE TRIGGER trg_RestrictModifications 
ON vw_SponsorDetails
INSTEAD OF INSERT, UPDATE, DELETE
AS
BEGIN
    RAISERROR ('Modifications (INSERT, UPDATE, DELETE) are not allowed on Sponsors Table.', 16, 1);
END;
-- Create a view for tb_Attendee
CREATE VIEW vw_Attendee AS
SELECT 
    Attendee_Id,
    Admin_User_Id_FK AS Admin_User_ID,
    User_Id_FK AS User_ID,
    Feedback
FROM tb_Attendee;


-- Restrict Modifications on vw_Attendee
CREATE TRIGGER trg_RestrictModifications_Attendee 
ON vw_Attendee
INSTEAD OF INSERT, UPDATE, DELETE
AS
BEGIN
    RAISERROR ('Modifications (INSERT, UPDATE, DELETE) are not allowed on Attendee Table.', 16, 1);
END

CREATE VIEW vw_VenueDetails AS
SELECT 
    v.Venue_Id,
    v.Venue_Name,
    v.Venue_Location,
    v.Capacity,
    v.Contact_Number,
    v.User_Id_FK,
FROM 
    tb_Venue v
JOIN 
    tb_User u ON v.User_Id_FK = u.User_Id;
	--for event history
	drop view vw_ApprovedOrRejectedEvents
	execute vw_ApprovedOrRejectedEvents
CREATE VIEW vw_ApprovedOrRejectedEvents AS
SELECT 
    e.Event_Id,
    e.Event_Name,
    e.E_Date,
    e.Start_Time,
    e.End_Time,
    e.E_Type,
	e.Attendee_Id_FK,
    e.Vendor_Price,
    e.Profit_Percent,
    e.User_Id_FK,
	vr.Event_Status-- Assuming you want to include User_Id_FK as the user who created the event
 -- Assuming you want to include Admin_User_Id_FK from Vendor Registration
FROM 
    tb_Events e
JOIN 
    tb_Vendor_Registration vr ON e.Event_Id = vr.Event_Id_FK
WHERE 
    vr.Event_Status IN ('Approved', 'Rejected');
	--event approval button
	UPDATE tb_Vendor_Registration
SET Event_Status = 'Approved'
WHERE Event_Id_FK = <eventId>;

--decline event button
	UPDATE tb_Vendor_Registration
SET Event_Status = 'Rejected'
WHERE Event_Id_FK = <eventId>;

--all vendors view

CREATE VIEW vw_tb_Vendor AS
SELECT 
    Vendor_Id,
    Company_Name,
    Resource_Person_Contact,
    Contact_Number,
    Email,
    V_Address,
    User_Id_FK
FROM tb_Vendor;


CREATE PROCEDURE DeleteAttendee
(
     @Attendee_Id int,
    @approval int OUTPUT
)
AS
BEGIN
    -- Check if the attendee exists
    IF EXISTS (SELECT 1 FROM tb_Attendee WHERE Attendee_Id = @Attendee_Id)
    BEGIN
        -- If exists, delete the record
        DELETE FROM tb_Attendee WHERE Attendee_Id = @Attendee_Id;
        SET @approval = 1; -- Operation successful
    END
    ELSE
    BEGIN
        -- If not exists, set approval to -1
        SET @approval = -1; -- No such attendee
    END
END;

CREATE PROCEDURE RegisterAttendee
(
    @Admin_User_Id_FK int,
    @User_Id_FK int,
	@Feedback varchar(100))
AS
BEGIN
    -- Check if the attendee already exists based on the Admin_User_Id_FK and User_Id_FK
    IF EXISTS (
        SELECT 1 
        FROM tb_Attendee
        WHERE Admin_User_Id_FK = @Admin_User_Id_FK
          AND User_Id_FK = @User_Id_FK
    )
    BEGIN
        -- Set approval to -1 if the attendee is already registered
           RAISERROR ('Attendee alreay exists!',16,1);
    END
    ELSE
    BEGIN
        -- Insert a new attendee record
        INSERT INTO tb_Attendee (Admin_User_Id_FK, User_Id_FK, Feedback)
        VALUES (@Admin_User_Id_FK, @User_Id_FK, @Feedback);

      
    END
END;


DECLARE @approval INT;

EXEC RegisterAttendee
    @Admin_User_Id_FK = 100, -- Replace with the actual Admin_User_Id_FK
    @User_Id_FK = 106,       -- Replace with the actual User_Id_FK
    @approval = @approval OUTPUT;

SELECT @approval AS ApprovalStatus;

DECLARE @approval INT;

EXEC DeleteAttendee
    @Attendee_Id = 502,       -- Replace with the actual Attendee_Id to delete
    @approval = @approval OUTPUT; -- Capture the output

-- Display the result of the operation
SELECT @approval AS ApprovalStatus;







