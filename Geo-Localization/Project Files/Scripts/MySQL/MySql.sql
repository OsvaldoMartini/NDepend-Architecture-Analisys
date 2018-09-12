
USE databaseName;
/*
CREATE USER IF NOT EXISTS 'admindb'@'localhost' IDENTIFIED BY 'admindb123';
GRANT SELECT,INSERT,UPDATE,DELETE,CREATE,DROP ON databaseName.* TO 'admindb'@'localhost';
GRANT ALTER ROUTINE, CREATE ROUTINE, EXECUTE ON *.* TO 'admindb'@'localhost' ;
FLUSH PRIVILEGES;
*/

		


SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS `databaseName`.`Employee`;
DROP TABLE IF EXISTS `databaseName`.`Company`;
DROP TABLE IF EXISTS `databaseName`.`GeoLocalization`;
DROP procedure IF EXISTS `PROC_DELETE_GEOLOCALIZATION`;
DROP procedure IF EXISTS `PROC_DELETE_EMPLOYEE`;
DROP procedure IF EXISTS `PROC_INSERT_GEOLOCALIZATION`;
DROP procedure IF EXISTS `PROC_INSERT_EMPLOYEE`;
DROP procedure IF EXISTS `PROC_UPDATE_EMPLOYEE`;
DROP procedure IF EXISTS `PROC_UPDATE_GEOLOCALIZATION`;
SET FOREIGN_KEY_CHECKS = 1; 




/****** Object:  Table Company    Script Date: 08/07/2017 09:07:02 ******/
CREATE TABLE Company(
	CompanyID int AUTO_INCREMENT NOT NULL,
	Name varchar(100) NOT NULL,
	Address varchar(250) NOT NULL,
	PostCode varchar(10) NOT NULL,
	State  varchar(50) NOT NULL,
	Country  varchar(50) NOT NULL,
	Email varchar(255) NOT NULL,
	WebSite varchar(255) NOT NULL,
	Phone varchar(20) NOT NULL,
	DateCreated datetime NULL,
CONSTRAINT PK_Employee PRIMARY KEY CLUSTERED 
(
	CompanyID ASC
)
)
;


/****** Object:  Table Employee Script Date: 08/07/2017 09:07:02 ******/
CREATE TABLE Employee(
	EmployeeID int AUTO_INCREMENT NOT NULL,
	CompanyID int NOT NULL,
	LastName varchar(30) NOT NULL,
	FirstName varchar(20) NOT NULL,
	UserName varchar(20) NOT NULL,
	Email varchar(255) NOT NULL,
	Password varchar(20) NOT NULL,
	RoleID int NOT NULL,
	DateCreated datetime NULL,
CONSTRAINT PK_Employee PRIMARY KEY CLUSTERED 
(
	EmployeeID ASC
)
)
;


/****** Object:  Table GeoLocalization    Script Date: 08/07/2017 09:07:02 ******/
CREATE TABLE GeoLocalization(
	GeoLocalizationID int AUTO_INCREMENT NOT NULL,
	EmployeeID int NOT NULL,
	LocalName varchar(100) NOT NULL,
	Lat varchar(30) NOT NULL,
	Lng varchar(30) NOT NULL,
	DateCreated datetime NULL,
 CONSTRAINT PK_GeoLocalization PRIMARY KEY CLUSTERED 
(
	GeoLocalizationID ASC
));


/*
# ---------------------------------------------------------------------- #
# Foreign keys                                                           #
# ---------------------------------------------------------------------- #
*/


ALTER TABLE Employee ADD CONSTRAINT FK_CompanyToEmployee 
    FOREIGN KEY (CompanyID) REFERENCES Company(CompanyID);


ALTER TABLE GeoLocalization ADD CONSTRAINT FK_GeoLocalizationToEmployee
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID);

/*
# ---------------------------------------------------------------------- #
# Foreign key Others                                                     #
# ---------------------------------------------------------------------- #
*/


INSERT INTO `Company` (`Name`,`Address`,`PostCode`,`State`,`Country`,`Email`,`WebSite`,`Phone`,`DateCreated`) 
VALUES('W. Services','W. Services Global Headquarters Receptionist','W1W','Croydon','United Kingdom','osvaldo.martini@gmail.com  ','osvaldo.martini@gmail.com  ', '+44 (0)7951 144 116', CURRENT_TIMESTAMP());

INSERT INTO `Employee` (`CompanyID`,`LastName`,`FirstName`,`UserName`,`Email`,`Password`,`RoleID`,`DateCreated`) VALUES(1, 'admin','Adm','Admin','admin@gmail.com','admin', 1, CURRENT_TIMESTAMP());
INSERT INTO `Employee` (`CompanyID`,`LastName`,`FirstName`,`UserName`,`Email`,`Password`,`RoleID`,`DateCreated`) VALUES(1, 'Martini','Osvaldo','Omartini','osvaldo.martini@gmail.com','martini', 1, CURRENT_TIMESTAMP());

INSERT INTO `GeoLocalization` (`EmployeeID`,`LocalName`,`Lat`,`Lng`,`DateCreated`) VALUES(1,'Liverpool Museum','-2.979919','51.521437', CURRENT_TIMESTAMP());
INSERT INTO `GeoLocalization` (`EmployeeID`,`LocalName`,`Lat`,`Lng`,`DateCreated`) VALUES(1,'Merseyside Maritime Museum','-0.081437','51.518952', CURRENT_TIMESTAMP());
INSERT INTO `GeoLocalization` (`EmployeeID`,`LocalName`,`Lat`,`Lng`,`DateCreated`) VALUES(1,'Walker Art Gallery','-0.143975','51.521831', CURRENT_TIMESTAMP());
INSERT INTO `GeoLocalization` (`EmployeeID`,`LocalName`,`Lat`,`Lng`,`DateCreated`) VALUES(1,'National Conservation Centre','-2.984683','53.407511', CURRENT_TIMESTAMP());

/****** Object:  StoredProcedure PROC_UPDATE_EMPLOYEE    Script Date: 08/22/2017 07:02:44 ******/
DELIMITER $$
CREATE DEFINER=`databaseName`@`%` PROCEDURE `PROC_UPDATE_EMPLOYEE`(
in P_CompanyID int,
in P_EmployeeID int,
in P_LastName varchar(30),
in P_FirstName varchar(20),
in P_UserName varchar(20),
in P_Email varchar(255),
in P_Password varchar(20),
in P_RoleID int,
out P_Return_Message varchar(500) 
)
BEGIN

declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
        set P_Return_Message = 'Operation Not Allowed!';
    end;
  
start transaction;
    
         UPDATE Employee
                        SET LastName = P_LastName
                         ,FirstName = P_FirstName
                         ,UserName = P_UserName
                         ,Email = P_Email
                         ,Password = P_Password
                         ,RoleID = P_RoleID
						 Where 
						 CompanyID = P_CompanyID and
		    			 EmployeeID = P_EmployeeID;
                         
         
    commit;
	SET P_Return_Message = '';
END$$

DELIMITER ;



/****** Object:  StoredProcedure PROC_INSERT_EMPLOYEE    Script Date: 08/22/2017 07:02:32 ******/
DELIMITER $$
CREATE DEFINER=`databaseName`@`%` PROCEDURE `PROC_INSERT_EMPLOYEE`(
in P_CompanyID int,
in P_LastName varchar(20),
in P_FirstName varchar(30),
in P_UserName varchar(20),
in P_Email varchar(255),
in P_Password varchar(20),
in P_RoleID int,
out P_int_Identity int,
out P_Return_Message varchar(500)
)
BEGIN
declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
	set P_Return_Message = 'Operation Not Allowed!';
    end;  
  
start transaction;
    
INSERT INTO Employee 
			(CompanyID
			,FirstName 
           ,LastName
           ,UserName 
           ,Email 
           ,Password
           ,RoleID
           ,DateCreated)
			VALUES (
			 P_CompanyID
			,P_FirstName 
			,P_LastName 
			,P_UserName 
			,P_Email
			,P_Password 
			,P_RoleID 
			,CURRENT_TIMESTAMP()
			);
 
         SET P_int_Identity = LAST_INSERT_ID();
   
    commit;
	SET P_Return_Message = '';

END$$

DELIMITER ;




/****** Object:  StoredProcedure PROC_DELETE_EMPLOYEE    Script Date: 09/18/2017 04:44:44 ******/
DELIMITER $$
CREATE DEFINER=`databaseName`@`%` PROCEDURE `PROC_DELETE_EMPLOYEE`(
in P_CompanyID int,
in P_EmployeeID int,
out P_Return_Message varchar(500) 
)
BEGIN

declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
set P_Return_Message = 'Operation Not Allowed!';
    end;

start transaction;


	DELETE FROM Employee 
WHERE
    CompanyID = P_CompanyID and
    EmployeeID = P_EmployeeID;

commit;
SET P_Return_Message = '';

END$$

DELIMITER ;


/****** Object:  StoredProcedure PROC_UPDATE_GEOLOCALIZATION    Script Date: 08/22/2017 07:02:35 ******/
DELIMITER $$
CREATE DEFINER=`databaseName`@`%` PROCEDURE `PROC_UPDATE_GEOLOCALIZATION` (
in P_GeoLocalizationID int,
in P_EmployeeID int ,
in P_LocalName varchar(100) ,
in P_Latitude varchar(30) ,
in P_Longitude varchar(30) ,
out P_Return_Message varchar(500) 
)
BEGIN
declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
        set P_Return_Message = 'Operation Not Allowed!';
    end;
  
start transaction;

    
		UPDATE GeoLocalization 
			SET LocalName = ifnull(P_LocalName, LocalName )
            ,Lat = ifnull(P_Latitude, Lat )
		    ,Lng = ifnull(P_Longitude, Lng )
		   where
				GeoLocalizationID = P_GeoLocalizationID
			and EmployeeID = P_EmployeeID;
           
 
    commit;
	SET P_Return_Message = '';

END$$

DELIMITER ;


/****** Object:  StoredProcedure PROC_INSERT_GEOLOCALIZATION    Script Date: 08/22/2017 07:02:22 ******/
DELIMITER $$
CREATE DEFINER=`databaseName`@`%` PROCEDURE `PROC_INSERT_GEOLOCALIZATION` (
in P_EmployeeID int,
in P_LocalName varchar(100),
in P_Longitude varchar(30) ,
in P_Latitude varchar(30) ,
out P_int_Identity int ,
out P_Return_Message varchar(500)
)
BEGIN
declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
        set P_Return_Message = 'Operation Not Allowed!';
    end;

start transaction;

	INSERT INTO GeoLocalization 
			(
			EmployeeID
			,LocalName
            ,Lat
            ,Lng
		    ,DateCreated
		    )
			VALUES (
			P_EmployeeID
			,P_LocalName
			,P_Latitude
            ,P_Longitude
		    ,CURRENT_TIMESTAMP()
			);
			

   SET P_int_Identity = LAST_INSERT_ID();
commit;
SET P_Return_Message = '';

END$$

DELIMITER ;





/****** Object:  StoredProcedure PROC_DELETE_GEOLOCALIZATION    Script Date: 09/18/2017 04:46:27 ******/
DELIMITER $$
CREATE DEFINER=`databaseName`@`%` PROCEDURE `PROC_DELETE_GEOLOCALIZATION`(
in P_EmployeeID int,
in P_GeoLocalizationID int,
out P_Return_Message varchar(500)
)
BEGIN


declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
		set P_Return_Message = 'Operation Not Allowed!';
    end;

start transaction;


		
		DELETE FROM GeoLocalization 
		WHERE
			EmployeeID = P_EmployeeID and
			GeoLocalizationID = P_GeoLocalizationID;

commit;
SET P_Return_Message = '';


END$$

DELIMITER ;




