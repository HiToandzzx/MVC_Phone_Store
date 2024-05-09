CREATE DATABASE CNPM_SHOPDT;

USE CNPM_SHOPDT;

CREATE TABLE UserAccount(
	ID VARCHAR(10) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,
	Email varchar(50) NOT NULL,
	Admin bit,
	Password nvarchar(50),
	AddressUser NVARCHAR(50) NOT NULL
)
drop table UserAccount

INSERT INTO UserAccount VALUES('A01', N'Admin', 'admin@gmail.com', 1,'Admin@123', 'Admin')
GO
INSERT INTO UserAccount VALUES('A02', N'Staff', 'staff@gmail.com', 0,'Staff@123', 'Staff')
GO
INSERT INTO UserAccount VALUES('A03', N'User', 'user@gmail.com', 0,'User@123', 'User')
GO

-- LOẠI ĐIỆN THOẠI 
CREATE TABLE KindOfPhone (
    KindID NVARCHAR(10) PRIMARY KEY,
    NameOfKind NVARCHAR(50) NOT NULL
);

CREATE TABLE Phones (
    PhonesID VARCHAR(10) PRIMARY KEY,
	PicPhone NVARCHAR(MAX), 
    PhonesName NVARCHAR(50) NOT NULL,
	Chip NVARCHAR(200) NOT NULL,
	Ram NVARCHAR(200) NOT NULL,
	DL NVARCHAR(200) NOT NULL,
	CameraSau NVARCHAR(200) NOT NULL,
	CameraTruoc NVARCHAR(200) NOT NULL,
	Pin NVARCHAR(200) NOT NULL,
    OldPrice INT NOT NULL, 
    Per TINYINT NOT NULL, 
    NewPrice INT NOT NULL, 
	KindID NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES KindOfPhone(KindID)
	ON UPDATE CASCADE
	ON DELETE CASCADE
);
drop table Phones

INSERT INTO KindOfPhone (KindID, NameOfKind) 
VALUES 
    ('IP', N'Iphone'),
	('SS', N'SamSung'),
	('OP', N'Oppo'),
	('XI', N'Xiaomi')

INSERT INTO Phones 
VALUES 
    ('P01', N'15prm_1TB.jpg', N'Iphone 15 Pro Max 1TB', N'Apple A17 Pro 6 nhân', N'8GB', N'1TB', N'Chính 48MP & phụ 12MP, 12MP', N'12MP', N'4422 mAh, Sạc 20W', 46990000, 6, 43990000, 'IP'),
    ('P02', N'15prm_512GB.jpg', N'Iphone 15 Pro Max 512GB', N'Apple A17 Pro 6 nhân', N'8GB', N'512GB', N'Chính 48MP & phụ 12MP, 12MP', N'12MP', N'4422 mAh, Sạc 20W', 40990000, 9, 36990000, 'IP'),
    ('P03', N'S24_Ultra_5G_1TB.jpg', N'SS S24 Ultra 5G 1TB', N'Snapdragon 8 Gen 3', N'12GB', N'1TB', N'Chính 200MP & phụ 50MP, 12MP, 10MP', N'12MP', N'5000 mAh, Sạc 25W', 44490000, 15, 37590000, 'SS'),
	('P04', N'S24_Ultra_5G_512GB.jpg', N'SS S24  Ultra 5G 512GB', N'Snapdragon 8 Gen 3', N'12GB', N'512GB', N'Chính 200MP & phụ 50MP, 12MP, 10MP', N'12MP', N'5000 mAh, Sạc 25W', 37490000, 18, 30690000, 'SS'),
    ('P05', N'15plus_128GB.jpg', N'Iphone 15 Plus 128GB', N'Apple A16 Bionic', N'8GB', N'128GB', N'Chính 48MP & phụ 12MP', N'12MP', N'4383 mAh, Sạc 20W', 25990000, 9, 22790000, 'IP'),
    ('P06', N'15_256GB.jpg', N'Iphone 15 256GB', N'Apple A16 Bionic', N'6GB', N'256GB', N'Chính 48MP & phụ 12MP', N'12MP', N'3349 mAh, Sạc 20W', 25990000, 8, 23690000, 'IP'),
	('P07', N'14prm_1TB.jpg', N'Iphone 14 Pro Max 1TB', N'Apple A16 Bionic', N'6GB', N'1TB', N'Chính 48MP & phụ 12MP', N'12MP', N'4323 mAh, Sạc 20W', 43990000, 4, 41990000, 'IP'),
	('P08', N'14prm_512GB.jpg', N'Iphone 14 Pro Max 512GB', N'Apple A16 Bionic', N'6GB', N'521GB', N'Chính 48MP & phụ 12MP', N'12MP', N'4323 mAh, Sạc 20W', 37990000, 5, 35990000, 'IP'),
	('P09', N'12_64GB.jpg', N'Iphone 12 64GB', N'Apple A14 Bionic', N'4GB', N'64GB', N'Chính 48MP & phụ 12MP, 12MP', N'12MP', N'2815 mAh, Sạc 20W', 14890000, 18, 120900000, 'IP'),
	('P10', N'S24plus_5G_256GB.png', N'SamSung S24+ 5G 256GB', N'Exynos 2400', N'12GB', N'256GB', N'Chính 50MP & phụ 12MP, 10MP', N'12MP', N'4900 mAh, Sạc 25W', 26990000, 26, 19790000, 'SS'),
	('P11', N'S24_5G_512GB.png', N'SamSung S24 5G 512', N'Exynos 2400', N'8GB', N'512GB', N'Chính 50MP & phụ 12MP, 10MP', N'12MP', N'4000 mAh, Sạc 25W', 26490000, 22, 20490000, 'SS'),
	('P12', N'S24_5G_256GB.png', N'SamSung S24 5G 256GB', N'Exynos 2400', N'8GB', N'256GB', N'CChính 50MP & phụ 12MP, 10MP', N'12MP', N'4000 mAh, Sạc 25W', 22990000, 25, 17090000, 'SS'),
	('P13', N'13_256GB.jpg', N'Iphone 13 256GB', N'Apple A15 Bionic', N'4GB', N'256GB', N'2 camera 12MP', N'12MP', N'3240 mAh, Sạc 20W', 20990000, 18, 170900000, 'IP'),
	('P14', N'13_128GB.jpg', N'Iphone 13 128GB', N'Apple A15 Bionic', N'4GB', N'128GB', N'2 camera 12MP', N'12MP', N'3240 mAh, Sạc 20W', 17790000, 22, 137900000, 'IP'),
	('P15', N'12_256GB.jpg', N'Iphone 12 256GB', N'Apple A14 Bionic', N'4GB', N'256GB', N'2 camera 12MP', N'12MP', N'2815 mAh, Sạc 20W', 17990000, 8, 163900000, 'IP'),
	('P16', N'12_128GB.jpg', N'Iphone 12 128GB', N'Apple A14 Bionic', N'4GB', N'128GB', N'2 camera 12MP', N'12MP', N'2815 mAh, Sạc 20W', 16490000, 18, 134900000, 'IP'),
	('P17', N'14pro_256GB.jpg', N'Iphone 14 Pro 256GB', N'Apple A16 Bionic', N'6GB', N'256GB', N'Chính 48MP', N'12MP', N'3200 mAh, Sạc 20W', 29490000, 6, 27490000, 'IP'),
	('P18', N'14plus_128GB.jpg', N'Iphone 14 Plus 128GB', N'Apple A15 Bionic', N'6GB', N'128GB', N'2 camera 12MP', N'12MP', N'4325 mAh, Sạc 20W', 22990000, 13, 19990000, 'IP'),
	('P19', N'14_256GB.jpg', N'Iphone 14 256GB', N'Apple A15 Bionic', N'6GB', N'256GB', N'2 camera 12MP', N'12MP', N'3279 mAh, Sạc 20W', 23990000, 16, 20090000, 'IP'),
	('P20', N'14_128GB.jpg', N'Iphone 14 128GB', N'Apple A15 Bionic', N'6GB', N'128GB', N'2 camera 12MP', N'12MP', N'3279 mAh, Sạc 20W', 20490000, 15, 17390000, 'IP'),
	('P21', N'15pro_512GB.jpg', N'Iphone 15 Pro 512GB', N'Apple A17 Pro 6 nhân', N'8GB', N'512GB', N'Chính 48MP & phụ 12MP, 12MP', N'12MP', N'3274 mAh, Sạc 20W', 37990000, 7, 35090000, 'IP'),
    ('P22', N'15pro_256GB.jpg', N'Iphone 15 Pro 256GB', N'Apple A17 Pro 6 nhân', N'8GB', N'256GB', N'Chính 48MP & phụ 12MP, 12MP', N'12MP', N'3274 mAh, Sạc 20W', 31990000, 13, 27690000, 'IP'),
	('P23', N'S24plus_5G_512GB.png', N'SamSung S24+ 5G 512GB', N'Exynos 2400', N'12GB', N'512GB', N'Chính 50MP & phụ 12MP, 10MP', N'12MP', N'4900 mAh, Sạc 25W', 30490000, 24, 23090000, 'SS');
	
		-- SS_A

CREATE PROCEDURE Find_Phone (
    @PhonesName NVARCHAR(50) = NULL,
    @KindID NVARCHAR(10) = NULL,
    @MinNewPrice INT = NULL,
    @MaxNewPrice INT = NULL,
    @RAM NVARCHAR(200) = NULL,
    @DL NVARCHAR(200) = NULL,
    @Pin NVARCHAR(200) = NULL
)
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000),
            @ParamList NVARCHAR(2000);

    -- Khởi tạo câu lệnh truy vấn
    SET @SqlStr = '
        SELECT *
        FROM Phones
        WHERE 1 = 1';
    
    -- Thêm điều kiện tìm kiếm theo tên điện thoại
    IF @PhonesName IS NOT NULL
        SET @SqlStr = @SqlStr + '
            AND (PhonesName LIKE ''%' + @PhonesName + '%'')';
    
    -- Thêm điều kiện tìm kiếm theo loại điện thoại (KindID)
    IF @KindID IS NOT NULL
        SET @SqlStr = @SqlStr + '
            AND (KindID LIKE ''%' + @KindID + '%'')';

    -- Thêm điều kiện tìm kiếm theo giá mới
    IF @MinNewPrice IS NOT NULL AND @MaxNewPrice IS NOT NULL
        SET @SqlStr = @SqlStr + '
            AND (NewPrice BETWEEN @MinNewPrice AND @MaxNewPrice)';
    ELSE IF @MinNewPrice IS NOT NULL
        SET @SqlStr = @SqlStr + '
            AND (NewPrice >= @MinNewPrice)';
    ELSE IF @MaxNewPrice IS NOT NULL
        SET @SqlStr = @SqlStr + '
            AND (NewPrice <= @MaxNewPrice)';

    -- Thêm điều kiện tìm kiếm theo RAM
    IF @RAM IS NOT NULL
        SET @SqlStr = @SqlStr + '
            AND (RAM LIKE ''%' + @RAM + '%'')';

    -- Thêm điều kiện tìm kiếm theo DL (dung lượng lưu trữ)
    IF @DL IS NOT NULL
        SET @SqlStr = @SqlStr + '
            AND (DL LIKE ''%' + @DL + '%'')';

    -- Thêm điều kiện tìm kiếm theo Pin
    IF @Pin IS NOT NULL
        SET @SqlStr = @SqlStr + '
            AND (Pin LIKE ''%' + @Pin + '%'')';

    -- Thực thi truy vấn với các tham số tìm kiếm
    EXEC sp_executesql @SqlStr, N'@MinNewPrice INT, @MaxNewPrice INT, @RAM NVARCHAR(200), @DL NVARCHAR(200), @Pin NVARCHAR(200)', 
                      @MinNewPrice, @MaxNewPrice, @RAM, @DL, @Pin;
END;



EXEC Find_Phone
    @PhonesName = NULL, -- No filter on phone names
    @KindID = NULL, -- No filter on kind ID
    @MinNewPrice = 10000000, -- Minimum price of $300
    @MaxNewPrice = 20000000; -- Maximum price of $700


drop PROCEDURE Find_Phone;