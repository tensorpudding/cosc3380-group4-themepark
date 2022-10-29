-- WARNING
-- THIS SCRIPT DELETES AND THEN RECREATES THE SCHEMA, FKs, PKs, and CHECK CONSTRAINTS
-- IT DOES NOT DO ANYTHING ELSE

while(exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where (CONSTRAINT_TYPE='FOREIGN KEY' OR CONSTRAINT_TYPE='CHECK')))
begin
 declare @sql nvarchar(2000)
 SELECT TOP 1 @sql=('ALTER TABLE ' + TABLE_SCHEMA + '.[' + TABLE_NAME
 + '] DROP CONSTRAINT [' + CONSTRAINT_NAME + ']')
 FROM information_schema.table_constraints
 WHERE (CONSTRAINT_TYPE = 'FOREIGN KEY' OR CONSTRAINT_TYPE = 'CHECK')
 exec (@sql)
 PRINT @sql
end

while(exists(select 1 from INFORMATION_SCHEMA.TABLES 
             where TABLE_NAME != '__EFMigrationsHistory' 
             AND TABLE_TYPE = 'BASE TABLE'))
begin
 --declare @sql nvarchar(2000)
 SELECT TOP 1 @sql=('DROP TABLE ' + TABLE_SCHEMA + '.[' + TABLE_NAME
 + ']')
 FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME != '__EFMigrationsHistory' AND TABLE_TYPE = 'BASE TABLE'
exec (@sql)
 /* you dont need this line, it just shows what was executed */
 PRINT @sql
end
go
--exec ('DROP TABLE dbo.__EFMigrationsHistory')

--
-- THIS IS COPY-PASTE FROM THE DUMPFILE
--

-- I'm not dropping the schema since it is still being used by stored procedures!

--DROP SCHEMA [Theme_Park]
--go
--CREATE SCHEMA [Theme_Park]
--    AUTHORIZATION [dbo];


GO
PRINT N'Creating SqlTable [Theme_Park].[Employee]...';


GO
CREATE TABLE [Theme_Park].[Employee] (
    [fname]          VARCHAR (20)   NOT NULL,
    [lname]          VARCHAR (20)   NOT NULL,
    [ssn]            NUMERIC (9)    NOT NULL,
    [gender]         CHAR (1)       NOT NULL,
    [address]        VARCHAR (50)   NOT NULL,
    [phone]          NUMERIC (10)   NOT NULL,
    [date_joined]    DATE           NOT NULL,
    [dept_id]        INT            NOT NULL,
    [role]           VARCHAR (20)   NULL,
    [supervisor_ssn] NUMERIC (9)    NULL,
    [salaried]       BIT            NOT NULL,
    [payrate]        MONEY          NULL,
    [vacation_days]  DECIMAL (4, 2) NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([ssn] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Adverse_Event_Calendar]...';


GO
CREATE TABLE [Theme_Park].[Adverse_Event_Calendar] (
    [date]        DATETIME      NULL,
    [park_closed] BIT           NULL,
    [reason]      VARCHAR (100) NULL
);


GO
PRINT N'Creating SqlTable [Theme_Park].[EmployeeSchedule]...';


GO
CREATE TABLE [Theme_Park].[EmployeeSchedule] (
    [employee_SSN] NUMERIC (9) NOT NULL,
    [shift_start]  DATETIME    NOT NULL,
    [shift_end]    DATETIME    NOT NULL,
    CONSTRAINT [PK_EmployeeSchedule] PRIMARY KEY CLUSTERED ([employee_SSN] ASC, [shift_start] ASC, [shift_end] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Merchandise]...';


GO
CREATE TABLE [Theme_Park].[Merchandise] (
    [item_id] INT          NOT NULL IDENTITY(1,1),
    [name]    VARCHAR (30) NOT NULL,
    [price]   SMALLMONEY   NOT NULL,
    CONSTRAINT [PK_item_id] PRIMARY KEY CLUSTERED ([item_id] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Ticket_Reservation]...';


GO
CREATE TABLE [Theme_Park].[Ticket_Reservation] (
    [Reservation_ID]     INT            NOT NULL IDENTITY(1,1),
    [Customer_ID]        INT            NULL,
    [FirstName]          VARCHAR (25)   NOT NULL,
    [LastName]           VARCHAR (25)   NOT NULL,
    [Date_Placed]        DATETIME       NOT NULL,
    [Date_of_Visit]      DATETIME       NOT NULL,
    [Credit_Card_Number] INT            NOT NULL,
    [Ticket_Class]       VARCHAR (10)   NULL,
    [Expired]            BIT            NULL,
    [Price]              DECIMAL (5, 2) NOT NULL,
    [Ticket_ID]          INT            NULL,
    PRIMARY KEY CLUSTERED ([Reservation_ID] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Merchandise_Sale]...';


GO
CREATE TABLE [Theme_Park].[Merchandise_Sale] (
    [transaction_id] INT        NOT NULL IDENTITY(1,1),
    [item_id]        INT        NOT NULL,
    [quantity]       INT        NOT NULL,
    [sale_price]     SMALLMONEY NOT NULL,
    [attraction_id]  INT        NULL,
    CONSTRAINT [PK_merch_sale] PRIMARY KEY CLUSTERED ([transaction_id] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Ticket]...';


GO
CREATE TABLE [Theme_Park].[Ticket] (
    [Ticket_ID]    INT            NOT NULL IDENTITY(1,1),
    [Date]         DATETIME       NOT NULL,
    [Ticket_Class] VARCHAR (10)   NULL,
    [Price]        DECIMAL (5, 2) NOT NULL,
    [Reservation]  INT            NULL,
    PRIMARY KEY CLUSTERED ([Ticket_ID] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Department]...';


GO
CREATE TABLE [Theme_Park].[Department] (
    [dept_id]        INT          NOT NULL IDENTITY(1,1),
    [dept_name]      VARCHAR (20) NOT NULL,
    [supervisor_ssn] NUMERIC (9)  NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED ([dept_id] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Customer]...';


GO
CREATE TABLE [Theme_Park].[Customer] (
    [customer_id]        INT          NOT NULL IDENTITY(1,1),
    [name]               VARCHAR (50) NOT NULL,
    [cc_num]             VARCHAR (16) NULL,
    [email]              VARCHAR (50) NULL,
    [visit_count]        INT          NOT NULL,
    [season_pass_holder] BIT          NULL,
    CONSTRAINT [Customer_PK] PRIMARY KEY CLUSTERED ([customer_id] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Leave_Calendar]...';


GO
CREATE TABLE [Theme_Park].[Leave_Calendar] (
    [ssn]           NUMERIC (9)  NOT NULL,
    [leave_start]   DATE         NOT NULL,
    [leave_end]     DATE         NULL,
    [type_of_leave] VARCHAR (20) NULL,
    CONSTRAINT [PK_Leave_Calendar] PRIMARY KEY CLUSTERED ([ssn] ASC, [leave_start] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Incident]...';


GO
CREATE TABLE [Theme_Park].[Incident] (
    [incident_id]          INT           NOT NULL IDENTITY(1,1),
    [incident_severity]    VARCHAR (8)   NOT NULL,
    [dept_id]              INT           NULL,
    [incident_time]        DATETIME      NOT NULL,
    [incident_description] VARCHAR (400) NOT NULL,
    CONSTRAINT [PK_Incident] PRIMARY KEY CLUSTERED ([incident_id] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Attractions]...';


GO
CREATE TABLE [Theme_Park].[Attractions] (
    [attraction_id] INT          NOT NULL IDENTITY(1,1),
    [name]          VARCHAR (20) NULL,
    [dept_id]       INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([attraction_id] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Park_Calandar]...';


GO
CREATE TABLE [Theme_Park].[Park_Calandar] (
    [Event_name]  VARCHAR (76) NULL,
    [Event_Start] DATETIME     NOT NULL,
    [Event_End]   DATETIME     NOT NULL
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Attractions_Visit]...';


GO
CREATE TABLE [Theme_Park].[Attractions_Visit] (
    [visit_time]     DATETIME NOT NULL,
    [attractions_id] INT      NOT NULL
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Users]...';


GO
CREATE TABLE [Theme_Park].[Users] (
    [db_user]     VARCHAR (20) NOT NULL,
    [db_password] VARCHAR (20) NOT NULL,
    [db_role]     VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([db_user] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Maintenance_Calendar]...';


GO
CREATE TABLE [Theme_Park].[Maintenance_Calendar] (
    [Maint_ID]           INT	         NOT NULL IDENTITY(1,1),
    [Occurence_datetime] DATETIME        NOT NULL,
    [Vendor_ID]          INT             NULL,
    [Attraction_ID]      INT             NULL,
    [Priority]           VARCHAR (20)    NOT NULL,
    [Maint_Start]        DATETIME        NOT NULL,
    [Maint_Completion]   DATETIME        NOT NULL,
    [Billed_Hours]       INT             NULL,
    [Invoice_amount]     SMALLMONEY      NULL,
    [Scanned_invoice]    VARBINARY (MAX) NULL,
    CONSTRAINT [Maint_PK] PRIMARY KEY CLUSTERED ([Maint_ID] ASC)
);


GO
PRINT N'Creating SqlTable [Theme_Park].[Vendor]...';


GO
CREATE TABLE [Theme_Park].[Vendor] (
    [name]                         VARCHAR (30)    NOT NULL,
    [vendor_point_of_contact_name] VARCHAR (30)    NOT NULL,
    [business_phone]               NUMERIC (10)    NOT NULL,
    [business_address]             VARCHAR (50)    NOT NULL,
    [service_type]                 VARCHAR (20)    NOT NULL,
    [contract_start_date]          DATE            NOT NULL,
    [scanned_contract]             VARBINARY (MAX) NULL,
    [vendor_id]                    INT             NOT NULL IDENTITY(1,1),
    CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED ([vendor_id] ASC)
);


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_Employee_Department]...';


GO
ALTER TABLE [Theme_Park].[Employee]
    ADD CONSTRAINT [FK_Employee_Department] FOREIGN KEY ([dept_id]) REFERENCES [Theme_Park].[Department] ([dept_id]) ON DELETE CASCADE;


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_Employee_Supervisor]...';


GO
ALTER TABLE [Theme_Park].[Employee]
    ADD CONSTRAINT [FK_Employee_Supervisor] FOREIGN KEY ([supervisor_ssn]) REFERENCES [Theme_Park].[Employee] ([ssn]);


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_Scheduled_Employee]...';


GO
ALTER TABLE [Theme_Park].[EmployeeSchedule]
    ADD CONSTRAINT [FK_Scheduled_Employee] FOREIGN KEY ([employee_SSN]) REFERENCES [Theme_Park].[Employee] ([ssn]);


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_Ticket_Of_Reservation]...';


GO
ALTER TABLE [Theme_Park].[Ticket_Reservation]
    ADD CONSTRAINT [FK_Ticket_Of_Reservation] FOREIGN KEY ([Ticket_ID]) REFERENCES [Theme_Park].[Ticket] ([Ticket_ID]);


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_Customer_Of_Reservation]...';


GO
ALTER TABLE [Theme_Park].[Ticket_Reservation]
    ADD CONSTRAINT [FK_Customer_Of_Reservation] FOREIGN KEY ([Customer_ID]) REFERENCES [Theme_Park].[Customer] ([customer_id]);


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_merch_sale_merch]...';


GO
ALTER TABLE [Theme_Park].[Merchandise_Sale]
    ADD CONSTRAINT [FK_merch_sale_merch] FOREIGN KEY ([item_id]) REFERENCES [Theme_Park].[Merchandise] ([item_id]) ON DELETE CASCADE;


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_merch_sale_location]...';


GO
ALTER TABLE [Theme_Park].[Merchandise_Sale]
    ADD CONSTRAINT [FK_merch_sale_location] FOREIGN KEY ([attraction_id]) REFERENCES [Theme_Park].[Attractions] ([attraction_id]) ON DELETE SET NULL;


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_Reservation_Of_Ticket]...';


GO
ALTER TABLE [Theme_Park].[Ticket]
    ADD CONSTRAINT [FK_Reservation_Of_Ticket] FOREIGN KEY ([Reservation]) REFERENCES [Theme_Park].[Ticket_Reservation] ([Reservation_ID]);


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_Department_Supervisor]...';


GO
ALTER TABLE [Theme_Park].[Department]
    ADD CONSTRAINT [FK_Department_Supervisor] FOREIGN KEY ([supervisor_ssn]) REFERENCES [Theme_Park].[Employee] ([ssn]);


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_Employee_On_Leave]...';


GO
ALTER TABLE [Theme_Park].[Leave_Calendar]
    ADD CONSTRAINT [FK_Employee_On_Leave] FOREIGN KEY ([ssn]) REFERENCES [Theme_Park].[Employee] ([ssn]);


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_Department_Of_Incident]...';


GO
ALTER TABLE [Theme_Park].[Incident]
    ADD CONSTRAINT [FK_Department_Of_Incident] FOREIGN KEY ([dept_id]) REFERENCES [Theme_Park].[Department] ([dept_id]);


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_attraction_department]...';


GO
ALTER TABLE [Theme_Park].[Attractions]
    ADD CONSTRAINT [FK_attraction_department] FOREIGN KEY ([dept_id]) REFERENCES [Theme_Park].[Department] ([dept_id]) ON DELETE CASCADE;


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_Visited_At]...';


GO
ALTER TABLE [Theme_Park].[Attractions_Visit]
    ADD CONSTRAINT [FK_Visited_At] FOREIGN KEY ([attractions_id]) REFERENCES [Theme_Park].[Attractions] ([attraction_id]);


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_Task_Performed_On_Attraction]...';


GO
ALTER TABLE [Theme_Park].[Maintenance_Calendar]
    ADD CONSTRAINT [FK_Task_Performed_On_Attraction] FOREIGN KEY ([Attraction_ID]) REFERENCES [Theme_Park].[Attractions] ([attraction_id]);


GO
PRINT N'Creating SqlForeignKeyConstraint [Theme_Park].[FK_Task_Assigned_To_Vendor]...';


GO
ALTER TABLE [Theme_Park].[Maintenance_Calendar]
    ADD CONSTRAINT [FK_Task_Assigned_To_Vendor] FOREIGN KEY ([Vendor_ID]) REFERENCES [Theme_Park].[Vendor] ([vendor_id]);


GO
PRINT N'Creating SqlCheckConstraint [Theme_Park].[CK_Employee_Gender]...';


GO
ALTER TABLE [Theme_Park].[Employee]
    ADD CONSTRAINT [CK_Employee_Gender] CHECK ([gender]='F' OR [gender]='M');


GO
PRINT N'Creating SqlCheckConstraint [Theme_Park].[CK_Employee_Valid_SSN]...';


GO
ALTER TABLE [Theme_Park].[Employee]
    ADD CONSTRAINT [CK_Employee_Valid_SSN] CHECK ([ssn]>(100000000) AND [ssn]<(999999999));


GO
PRINT N'Creating SqlCheckConstraint [Theme_Park].[CK_Merchandise_Price]...';


GO
ALTER TABLE [Theme_Park].[Merchandise]
    ADD CONSTRAINT [CK_Merchandise_Price] CHECK ([price]>=(0));


GO
PRINT N'Creating SqlCheckConstraint unnamed constraint on [Theme_Park].[Ticket_Reservation]...';


GO
ALTER TABLE [Theme_Park].[Ticket_Reservation]
    ADD CHECK ([Ticket_Class]='Poor' OR [Ticket_Class]='Normal' OR [Ticket_Class]='Premium');


GO
PRINT N'Creating SqlCheckConstraint [Theme_Park].[CK_merch_sale_item_count]...';


GO
ALTER TABLE [Theme_Park].[Merchandise_Sale]
    ADD CONSTRAINT [CK_merch_sale_item_count] CHECK ([quantity]>(0));


GO
PRINT N'Creating SqlCheckConstraint unnamed constraint on [Theme_Park].[Ticket]...';


GO
ALTER TABLE [Theme_Park].[Ticket]
    ADD CHECK ([Ticket_Class]='Poor' OR [Ticket_Class]='Normal' OR [Ticket_Class]='Premium');


GO
PRINT N'Creating SqlCheckConstraint [Theme_Park].[CK_Leave_Date]...';


GO
ALTER TABLE [Theme_Park].[Leave_Calendar]
    ADD CONSTRAINT [CK_Leave_Date] CHECK ([leave_start]<[leave_end] OR [leave_end] IS NULL);


GO
PRINT N'Creating SqlCheckConstraint [Theme_Park].[CK_Incident_Severity]...';


GO
ALTER TABLE [Theme_Park].[Incident]
    ADD CONSTRAINT [CK_Incident_Severity] CHECK ([incident_severity]='Critical' OR [incident_severity]='Minor');


GO
PRINT N'Creating SqlCheckConstraint [Theme_Park].[checkPriority]...';


GO
ALTER TABLE [Theme_Park].[Maintenance_Calendar]
    ADD CONSTRAINT [checkPriority] CHECK ([Priority]='ASAP' OR [Priority]='Urgent' OR [Priority]='Not Urgent');


GO
PRINT N'Creating SqlCheckConstraint [Theme_Park].[CK_Calendar_Maintenance]...';


GO
ALTER TABLE [Theme_Park].[Maintenance_Calendar]
    ADD CONSTRAINT [CK_Calendar_Maintenance] CHECK ([Maint_Start]<=[Maint_Completion]);


GO
PRINT N'Creating SqlCheckConstraint [Theme_Park].[ServiceTypeEnum]...';


GO
ALTER TABLE [Theme_Park].[Vendor]
    ADD CONSTRAINT [ServiceTypeEnum] CHECK ([service_type]='Merchandise' OR [service_type]='Attraction' OR [service_type]='Food');


GO