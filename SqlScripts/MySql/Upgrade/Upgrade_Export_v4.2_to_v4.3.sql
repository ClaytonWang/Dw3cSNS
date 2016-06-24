SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS `spb_VoteThreads`;
CREATE TABLE `spb_VoteThreads` (
    Id BigInt NOT NULL AUTO_INCREMENT,
    TenantTypeId Varchar(6) NOT NULL,
	Title Varchar(255) NOT NULL,
	Body Varchar(512) NOT NULL,
	VoteType TinyInt NOT NULL,
	OptionType TinyInt NOT NULL,
	EndDate DateTime NOT NULL,
	VoteResult TinyInt NOT NULL,
	HiddenText Varchar(256) NOT NULL,
	MemberCount SmallInt NOT NULL,
	DateCreated  DateTime NOT NULL,
	UserId BigInt NOT NULL,
	AuditStatus SmallInt NOT NULL,
    PropertyNames mediumtext DEFAULT NULL,
    PropertyValues mediumtext DEFAULT NULL,
PRIMARY KEY (`Id`)
)ENGINE=innodb DEFAULT CHARSET=utf8;

SET FOREIGN_KEY_CHECKS = 0;


DROP TABLE IF EXISTS `spb_VoteOptions`;

CREATE TABLE `spb_VoteOptions` (
    Id BigInt NOT NULL AUTO_INCREMENT,
	VoteId BigInt NOT NULL,
	FeaturedImage Varchar(256) NOT NULL,
	LinkPath Varchar(256) NULL,
	OptionName Varchar(150) NOT NULL,
	VoteCount SmallInt NOT NULL,
    PropertyNames mediumtext DEFAULT NULL,
    PropertyValues mediumtext DEFAULT NULL,
PRIMARY KEY (`Id`)
)ENGINE=innodb DEFAULT CHARSET=utf8;

SET FOREIGN_KEY_CHECKS = 0;


DROP TABLE IF EXISTS `spb_VoteRecords`;
CREATE TABLE `spb_VoteRecords` (
    Id BigInt NOT NULL AUTO_INCREMENT,
	VoteId BigInt NOT NULL,
	OptionId BigInt NOT NULL,
	UserId BigInt NOT NULL,
	DateCreated DateTime NOT NULL,
	IP Varchar(64) NOT NULL,
	IsAnoymity tinyint NOT NULL,
    PropertyNames mediumtext DEFAULT NULL,
    PropertyValues mediumtext DEFAULT NULL,
PRIMARY KEY (`Id`)
)ENGINE=innodb DEFAULT CHARSET=utf8;

--用户--
ALTER TABLE tn_Users ADD COLUMN AuditStatus smallint NOT NULL;
ALTER TABLE tn_Users ADD COLUMN DateAvatar bigint NOT NULL;
--群组--  
ALTER TABLE spb_Groups ADD COLUMN IsDynamicPermission Tinyint NULL;
-- 2015-08-13 zhufj 修改数据库版本号
update tn_SystemData set DecimalValue = 4.3 where Datakey = 'SPBVersion';