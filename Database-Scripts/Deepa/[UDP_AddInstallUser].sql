USE [jgrove_JGP]
GO

/****** Object:  StoredProcedure [dbo].[UDP_AddInstallUser]    Script Date: 6/15/2016 12:51:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  Proc [dbo].[UDP_AddInstallUser]  
@FristName varchar(50),  
@LastName varchar(50),  
@Email varchar(100),  
@phone varchar(20),  
@phonetype char(15),
@address varchar(100),  
@Zip varchar(10),  
@State varchar(30),  
@City varchar(30),  
@password varchar(50),  
@designation varchar(50),  
@status varchar(20),  
@Picture varchar(max),  
@Attachements varchar(max),
@bussinessname varchar(100),
@ssn varchar(20),
@ssn1 varchar(20),
@ssn2 varchar(20),
@signature varchar(25),
@dob varchar(20),
@citizenship varchar(50),
@ein1 varchar(20),
@ein2 varchar(20), 
@a varchar(20),
@b varchar(20),
@c varchar(20),
@d varchar(20),
@e varchar(20),
@f varchar(20),
@g varchar(20),
@h varchar(20),
@i varchar(20),
@j varchar(20),
@k varchar(20),
@maritalstatus varchar(20),
@PrimeryTradeId int = 0,
@SecondoryTradeId int = 0,
@Source	varchar(MAX)='',
@Notes	varchar(MAX)='',
@StatusReason varchar(MAX) = '',
@GeneralLiability	varchar(MAX) = '',
@PCLiscense	varchar(MAX) = '',
@WorkerComp	varchar(MAX) = '',
@HireDate varchar(50) = '',
@TerminitionDate varchar(50) = '',
@WorkersCompCode varchar(20) = '',
@NextReviewDate	varchar(50) = '',
@EmpType varchar(50) = '',
@LastReviewDate	varchar(50) = '',
@PayRates varchar(50) = '',
@ExtraEarning varchar(max) = '',
@ExtraEarningAmt varchar(max) = 0,
@PayMethod varchar(50) = '',
@Deduction VARCHAR(MAX) = '',
@DeductionType varchar(50) = '',
@AbaAccountNo varchar(50) = '',
@AccountNo varchar(50) = '',
@AccountType varchar(50) = '',
@InstallId VARCHAR(MAX) = '',
@PTradeOthers varchar(100) = '',
@STradeOthers varchar(100) = '',
@DeductionReason varchar(MAX) = '',
@SuiteAptRoom varchar(10) = '',
@FullTimePosition int = 0,
@ContractorsBuilderOwner VARCHAR(500) = '',
@MajorTools VARCHAR(250) = '',
@DrugTest bit = null,
@ValidLicense bit = null,
@TruckTools bit = null,
@PrevApply bit = null,
@LicenseStatus bit = null,
@CrimeStatus bit = null,
@StartDate VARCHAR(50) = '',
@SalaryReq VARCHAR(50) = '',
@Avialability VARCHAR(50) = '',
@ResumePath VARCHAR(MAX) = '',
@skillassessmentstatus bit = null,
@assessmentPath VARCHAR(MAX) = '',
@WarrentyPolicy  VARCHAR(50) = '',
@CirtificationTraining VARCHAR(MAX) = '',
@businessYrs decimal = 0,
@underPresentComp decimal = 0,
@websiteaddress VARCHAR(MAX) = '',
@PersonName VARCHAR(MAX) = '',
@PersonType VARCHAR(MAX) = '',
@CompanyPrinciple VARCHAR(MAX) = '',
@UserType VARCHAR(25) = '',
@Email2	varchar(70)	= '',
@Phone2	varchar(70)	= '',
@CompanyName	varchar(100) = '',
@SourceUser	varchar(10)	= '',
@DateSourced	varchar(50)	= '',
@InstallerType varchar(20) = '',
@BusinessType varchar(50) = '',
@CEO varchar(100) = '',
@LegalOfficer	varchar(100) = '',
@President	varchar(100) = '',
@Owner	varchar(100) = '',
@AllParteners	varchar(MAX) = '',
@MailingAddress	varchar(100) = '',
@Warrantyguarantee	bit = null,
@WarrantyYrs	int = 0,
@MinorityBussiness	bit = null,
@WomensEnterprise	bit = null,
@InterviewTime varchar(20) ='',
@ActivationDate	varchar(50)	= '',
@UserActivated	varchar(100) = '',
@LIBC VARCHAR(5) = '',


@CruntEmployement bit = null,
@CurrentEmoPlace varchar(100) = '',
@LeavingReason varchar(MAX) = '',
@CompLit bit = null,
@FELONY	bit = null,
@shortterm	varchar(250) = '',
@LongTerm	varchar(250) = '',
@BestCandidate	varchar(MAX) = '',
@TalentVenue	varchar(MAX) = '',
@Boardsites	varchar(300) = '',
@NonTraditional	varchar(MAX) = '',
@ConSalTraning	varchar(100) = '',
@BestTradeOne	varchar(50) = '',
@BestTradeTwo	varchar(50) = '',
@BestTradeThree	varchar(50) = '',

@aOne	varchar(50)	= '',
@aOneTwo	varchar(50)	= '',
@bOne	varchar(50)	= '',
@cOne	varchar(50)	= '',
@aTwo	varchar(50)	= '',
@aTwoTwo	varchar(50)	= '',
@bTwo	varchar(50)	= '',
@cTwo	varchar(50)	= '',
@aThree	varchar(50)	= '',
@aThreeTwo	varchar(50)	= '',
@bThree	varchar(50)	= '',
@cThree	varchar(50)	= '',
@RejectionDate	varchar(50)	='',
@RejectionTime	varchar(50)	='',
@RejectedUserId  int = 0,
@TC bit = null,
@ExtraIncomeType varchar(MAX) = '',
@AddedBy int = 0,

@result bit output  
as begin  
DECLARE @MaxId int = 0
INSERT INTO tblInstallUsers   
           (  
				FristName,LastName,Email,Phone,phonetype,[Address],Zip,[State],[City],[Password],Designation,[Status],Picture,Attachements,Bussinessname,SSN,SSN1,SSN2,[Signature]
				,DOB,Citizenship,EIN1,EIN2,A,B,C,D,E,F,G,H,[5],[6],[7],maritalstatus,PrimeryTradeId
				,SecondoryTradeId,Source,Notes,StatusReason,GeneralLiability,PCLiscense,WorkerComp,HireDate,TerminitionDate,WorkersCompCode,NextReviewDate,EmpType,LastReviewDate
				,PayRates,ExtraEarning,ExtraEarningAmt,PayMethod,Deduction,DeductionType,AbaAccountNo,AccountNo,AccountType
				,InstallId,PTradeOthers,STradeOthers,DeductionReason,SuiteAptRoom,FullTimePosition,ContractorsBuilderOwner,MajorTools,DrugTest,ValidLicense,TruckTools
				,PrevApply,LicenseStatus,CrimeStatus,StartDate,SalaryReq,Avialability,ResumePath,skillassessmentstatus,assessmentPath,WarrentyPolicy,CirtificationTraining
				,businessYrs,underPresentComp,websiteaddress,PersonName,PersonType,CompanyPrinciple,UserType,Email2,Phone2,CompanyName,SourceUser,DateSourced,InstallerType
				,BusinessType,CEO,LegalOfficer,President,Owner,AllParteners,MailingAddress,Warrantyguarantee,WarrantyYrs,MinorityBussiness,WomensEnterprise,InterviewTime
				,ActivationDate,UserActivated,LIBC,CruntEmployement,CurrentEmoPlace,LeavingReason,CompLit,FELONY,shortterm,LongTerm,BestCandidate,TalentVenue,Boardsites
				,NonTraditional,ConSalTraning,BestTradeOne,BestTradeTwo,BestTradeThree,aOne,aOneTwo,bOne,cOne,aTwo,aTwoTwo,bTwo,cTwo,aThree,aThreeTwo,bThree,cThree
				,RejectionDate,RejectionTime,RejectedUserId,TC,ExtraIncomeType
           )  
     VALUES  
           (  
				@FristName,@LastName,@Email,@phone,@phonetype,@address,@Zip,@State,@City,@password,@designation,@status,@Picture,@Attachements,@bussinessname,@ssn,@ssn1,@ssn2,@signature
				,@dob,@citizenship,@ein1,@ein2,@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@maritalstatus,@PrimeryTradeId,@SecondoryTradeId,@Source,@Notes,@StatusReason,@GeneralLiability
				,@PCLiscense,@WorkerComp,@HireDate,@TerminitionDate,@WorkersCompCode,@NextReviewDate,@EmpType,@LastReviewDate
				,@PayRates,@ExtraEarning,@ExtraEarningAmt,@PayMethod,@Deduction,@DeductionType,@AbaAccountNo,@AccountNo,@AccountType,@InstallId,@PTradeOthers,@STradeOthers
				,@DeductionReason,@SuiteAptRoom,@FullTimePosition,@ContractorsBuilderOwner,@MajorTools,@DrugTest,@ValidLicense,@TruckTools,@PrevApply,@LicenseStatus
				,@CrimeStatus,@StartDate,@SalaryReq,@Avialability,@ResumePath,@skillassessmentstatus,@assessmentPath,@WarrentyPolicy,@CirtificationTraining,@businessYrs
				,@underPresentComp,@websiteaddress,@PersonName,@PersonType,@CompanyPrinciple,@UserType,@Email2,@Phone2,@CompanyName,@SourceUser,@DateSourced,@InstallerType
				,@BusinessType,@CEO,@LegalOfficer,@President,@Owner,@AllParteners,@MailingAddress,@Warrantyguarantee,@WarrantyYrs,@MinorityBussiness,@WomensEnterprise,@InterviewTime
				,@ActivationDate,@UserActivated,@LIBC,@CruntEmployement,@CurrentEmoPlace,@LeavingReason,@CompLit,@FELONY,@shortterm,@LongTerm,@BestCandidate,@TalentVenue
				,@Boardsites,@NonTraditional,@ConSalTraning,@BestTradeOne,@BestTradeTwo,@BestTradeThree,@aOne,@aOneTwo,@bOne,@cOne,@aTwo,@aTwoTwo,@bTwo,@cTwo,@aThree,@aThreeTwo
				,@bThree,@cThree,@RejectionDate,@RejectionTime,@RejectedUserId,@TC,@ExtraIncomeType
           )  

		   SELECT @MaxId = MAX(Id) FROM tblInstallUsers

		   INSERT INTO [jgrov_User].[tblInstalledReport]
                      ([SourceId],[InstallerId],[Status])
				VALUES(Cast(@SourceUser as int),@MaxId,@status)

				IF @status = 'InterviewDate' OR @status = 'Interview Date'
				BEGIN
				INSERT INTO tbl_AnnualEvents(EventName,EventDate,EventAddedBy,ApplicantId)
				                      VALUES('InterViewDetails',@StatusReason,@AddedBy,@MaxId)--CAST(@SourceUser as int)(Added by Sandeep...)

				END


           Set @result ='1'  
  
        return @result  
  
 end
--modified/created by Other Party


GO


