
USE db_a418f0_martini;
/*
CREATE USER IF NOT EXISTS 'admindb'@'localhost' IDENTIFIED BY 'admindb123';
GRANT SELECT,INSERT,UPDATE,DELETE,CREATE,DROP ON db_a418f0_martini.* TO 'admindb'@'localhost';
GRANT ALTER ROUTINE, CREATE ROUTINE, EXECUTE ON *.* TO 'admindb'@'localhost' ;
FLUSH PRIVILEGES;
*/

		


SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS `db_a418f0_martini`.`GeoEmployee`;
DROP TABLE IF EXISTS `db_a418f0_martini`.`GeoCompanySales`;
DROP TABLE IF EXISTS `db_a418f0_martini`.`GeoCompany`;
DROP TABLE IF EXISTS `db_a418f0_martini`.`GeoLocalization`;
DROP procedure IF EXISTS `PROC_DELETE_Geolocalization`;
DROP procedure IF EXISTS `PROC_DELETE_GeoEmployee`;
DROP procedure IF EXISTS `PROC_INSERT_Geolocalization`;
DROP procedure IF EXISTS `PROC_INSERT_GeoEmployee`;
DROP procedure IF EXISTS `PROC_UPDATE_GeoEmployee`;
DROP procedure IF EXISTS `PROC_UPDATE_Geolocalization`;
SET FOREIGN_KEY_CHECKS = 1; 




/****** Object:  Table GeoCompany    Script Date: 08/07/2017 09:07:02 ******/
CREATE TABLE GeoCompany(
	CompanyID int AUTO_INCREMENT NOT NULL,
	CompanyType varchar(100) NOT NULL,
	Name varchar(100) NOT NULL,
	Address varchar(250) NOT NULL,
	PostCode varchar(10) NOT NULL,
	State  varchar(50) NOT NULL,
	Country  varchar(50) NOT NULL,
	Email varchar(255) NOT NULL,
	WebSite varchar(255) NOT NULL,
	Phone varchar(20) NOT NULL,
	DateCreated datetime NULL,
CONSTRAINT PK_GeoCompany PRIMARY KEY CLUSTERED 
(
	CompanyID ASC
)
)
;


/****** Object:  Table GeoCompanySales    Script Date: 08/07/2017 09:07:02 ******/
CREATE TABLE GeoCompanySales(
	CompanySalesID int AUTO_INCREMENT NOT NULL,
	CompanyID int NOT NULL,
	SalesYear int NOT NULL,
	SalesMonth int NOT NULL,
	SalesTotal Decimal(10,2) NOT NULL,
	DateCreated datetime NULL,
CONSTRAINT PK_CompanySales PRIMARY KEY CLUSTERED 
(
	CompanySalesID ASC
)
)
;


/****** Object:  Table GeoEmployee Script Date: 08/07/2017 09:07:02 ******/
CREATE TABLE GeoEmployee(
	EmployeeID int AUTO_INCREMENT NOT NULL,
	CompanyID int NOT NULL,
	LastName varchar(30) NOT NULL,
	FirstName varchar(20) NOT NULL,
	UserName varchar(20) NOT NULL,
	Email varchar(255) NOT NULL,
	Password varchar(20) NOT NULL,
	RoleID int NOT NULL,
	DateCreated datetime NULL,
CONSTRAINT PK_GeoEmployee PRIMARY KEY CLUSTERED 
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


ALTER TABLE GeoEmployee ADD CONSTRAINT FK_GeoCompanyToGeoEmployee 
    FOREIGN KEY (CompanyID) REFERENCES GeoCompany(CompanyID);


ALTER TABLE GeoLocalization ADD CONSTRAINT FK_GeoLocalizationToGeoEmployee
    FOREIGN KEY (EmployeeID) REFERENCES GeoEmployee(EmployeeID);

/*
# ---------------------------------------------------------------------- #
# Foreign key Others                                                     #
# ---------------------------------------------------------------------- #
*/


INSERT INTO `GeoCompany` (`CompanyType`,`Name`,`Address`,`PostCode`,`State`,`Country`,`Email`,`WebSite`,`Phone`,`DateCreated`) 
VALUES('IT Services','WServices','W. Services Global Headquarters Receptionist','W1W','London','United Kingdom','osvaldo.martini@gmail.com  ','osvaldo.martini@gmail.com  ', '+44 (0)7951 144 116', CURRENT_TIMESTAMP());

INSERT INTO `GeoEmployee` (`CompanyID`,`LastName`,`FirstName`,`UserName`,`Email`,`Password`,`RoleID`,`DateCreated`) VALUES(1, 'admin','Adm','Admin','admin@gmail.com','admin', 1, CURRENT_TIMESTAMP());
INSERT INTO `GeoEmployee` (`CompanyID`,`LastName`,`FirstName`,`UserName`,`Email`,`Password`,`RoleID`,`DateCreated`) VALUES(1, 'Martini','Osvaldo','Omartini','osvaldo.martini@gmail.com','martini', 1, CURRENT_TIMESTAMP());

INSERT INTO `GeoLocalization` (`EmployeeID`,`LocalName`,`Lat`,`Lng`,`DateCreated`) VALUES(1,'Liverpool Museum','-2.979919','51.521437', CURRENT_TIMESTAMP());
INSERT INTO `GeoLocalization` (`EmployeeID`,`LocalName`,`Lat`,`Lng`,`DateCreated`) VALUES(1,'Merseyside Maritime Museum','-0.081437','51.518952', CURRENT_TIMESTAMP());
INSERT INTO `GeoLocalization` (`EmployeeID`,`LocalName`,`Lat`,`Lng`,`DateCreated`) VALUES(1,'Walker Art Gallery','-0.143975','51.521831', CURRENT_TIMESTAMP());
INSERT INTO `GeoLocalization` (`EmployeeID`,`LocalName`,`Lat`,`Lng`,`DateCreated`) VALUES(1,'National Conservation Centre','-2.984683','53.407511', CURRENT_TIMESTAMP());

/****** Object:  StoredProcedure PROC_UPDATE_GeoEmployee    Script Date: 08/22/2017 07:02:44 ******/
DELIMITER $$
CREATE  PROCEDURE `PROC_UPDATE_GeoEmployee`(
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
    
         UPDATE GeoEmployee
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



/****** Object:  StoredProcedure PROC_INSERT_GeoEmployee    Script Date: 08/22/2017 07:02:32 ******/
DELIMITER $$
CREATE  PROCEDURE `PROC_INSERT_GeoEmployee`(
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
    
INSERT INTO GeoEmployee 
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




/****** Object:  StoredProcedure PROC_DELETE_GeoEmployee    Script Date: 09/18/2017 04:44:44 ******/
DELIMITER $$
CREATE  PROCEDURE `PROC_DELETE_GeoEmployee`(
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


	DELETE FROM GeoEmployee 
WHERE
    CompanyID = P_CompanyID and
    EmployeeID = P_EmployeeID;

commit;
SET P_Return_Message = '';

END$$

DELIMITER ;


/****** Object:  StoredProcedure PROC_UPDATE_Geolocalization    Script Date: 08/22/2017 07:02:35 ******/
DELIMITER $$
CREATE  PROCEDURE `PROC_UPDATE_Geolocalization` (
in P_GeoLocalizationID int,
in P_EmployeeID int ,
in P_LocalName varchar(100) ,
in P_Lat varchar(30) ,
in P_Long varchar(30) ,
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
            ,Lat = ifnull(P_Lat, Lat )
		    ,Lng = ifnull(P_Long, Lng )
		   where
				GeoLocalizationID = P_GeoLocalizationID
			and EmployeeID = P_EmployeeID;
           
 
    commit;
	SET P_Return_Message = '';

END$$

DELIMITER ;


/****** Object:  StoredProcedure PROC_INSERT_Geolocalization    Script Date: 08/22/2017 07:02:22 ******/
DELIMITER $$
CREATE  PROCEDURE `PROC_INSERT_Geolocalization` (
in P_EmployeeID int,
in P_LocalName varchar(100),
in P_Long varchar(30) ,
in P_Lat varchar(30) ,
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
			,P_Lat
            ,P_Long
		    ,CURRENT_TIMESTAMP()
			);
			

   SET P_int_Identity = LAST_INSERT_ID();
commit;
SET P_Return_Message = '';

END$$

DELIMITER ;





/****** Object:  StoredProcedure PROC_DELETE_Geolocalization    Script Date: 09/18/2017 04:46:27 ******/
DELIMITER $$
CREATE  PROCEDURE `PROC_DELETE_Geolocalization`(
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




