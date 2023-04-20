CREATE DATABASE QL_HeThongThiTracNghiem
GO 
USE QL_HeThongThiTracNghiem
GO	
CREATE TABLE Khoá_Học
(
	MaKhoa NVARCHAR(10) PRIMARY KEY,
	TenKhoa NVARCHAR(100)
);
GO	
CREATE TABLE Lớp
(
	MaLop NVARCHAR(10) PRIMARY KEY,
	TenLop NVARCHAR(100),
	MaKhoa NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Khoá_Học(MaKhoa)
);
GO
CREATE TABLE Phân_Quyền
(
	MaPhanQuyen NVARCHAR(10) PRIMARY KEY,
	TenPhanQuyen NVARCHAR(100)
);
GO
CREATE TABLE Môn_Học
(
	MaMon NVARCHAR(10) PRIMARY KEY,
	TenMon NVARCHAR(100),
	MaKhoa NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Khoá_Học(MaKhoa)
); 
GO
CREATE TABLE Tài_Khoản
(
	MaTaiKhoan NVARCHAR(10) PRIMARY KEY,
	TenDN NVARCHAR(10),
	MatKhau NVARCHAR(10),
	MaPhanQuyen NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Phân_Quyền(MaPhanQuyen),
	TrangThai NVARCHAR(10),
	MaLop NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Lớp(MaLop)
);
GO
CREATE TABLE Học_Sinh
(
	MaHS NVARCHAR(10) PRIMARY KEY,
	HoTen NVARCHAR(100),
	NgaySinh DATETIME,
	GioiTinh NVARCHAR(10),
	SDT NVARCHAR(10),
	DiaChi NVARCHAR(100),
	MaTaiKhoan NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Tài_Khoản(MaTaiKhoan)
); 
GO
CREATE TABLE Giáo_Viên
(
	MaGV NVARCHAR(10) PRIMARY KEY,
	HoTen NVARCHAR(100),
	NgaySinh DATETIME,
	GioiTinh NVARCHAR(10),
	SDT NVARCHAR(10),
	DiaChi NVARCHAR(100),
	MaTaiKhoan NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Tài_Khoản(MaTaiKhoan)
);
GO
CREATE TABLE Loại_Đề
(
	MaLoaiDe NVARCHAR(10) PRIMARY KEY,
	TenLoaiDe NVARCHAR(100)
);
GO
CREATE TABLE Nhóm_Câu_Hỏi
(
	MaNhom NVARCHAR(10) PRIMARY KEY,
	TenNhom NVARCHAR(100),
	MaMon NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Môn_Học(MaMon),
	SoCauHoi INT
);
GO 
CREATE TABLE Câu_Hỏi
(
	MaCauHoi NVARCHAR(10) PRIMARY KEY,
	PhanCauHoi NVARCHAR(100),
	PhanCauTraLoi NVARCHAR(100),
	DapAn NVARCHAR(100),
	MaNhom NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Nhóm_Câu_Hỏi(MaNhom)
);
GO 
CREATE TABLE Đoạn_Văn
(
	MaDoanVan NVARCHAR(10) PRIMARY KEY,
	TenDoanVan NVARCHAR(100),
	MaNhom NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Nhóm_Câu_Hỏi(MaNhom),
	TrangThai NVARCHAR(10)
);
GO 
CREATE TABLE Đề_Thi
(
	MaDe NVARCHAR(10) PRIMARY KEY,
	TenDe NVARCHAR(100),
	ThoiGianLamBai INT,
	ThoiGianBatDau DATETIME,
	ThoiGianKetThuc DATETIME,
	MaMon NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Môn_Học(MaMon),
	MaNhom NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Nhóm_Câu_Hỏi(MaNhom),
	MaGV NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Giáo_Viên(MaGV),
	MaLoaiDe NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Loại_Đề(MaLoaiDe),
	SoDeThiDuocTao INT,
	DaoDapAn BIT,
	TrangThai NVARCHAR(10)
);
GO 
CREATE TABLE Bài_Làm
(
	MaBaiLam NVARCHAR(10) PRIMARY KEY,
	MaHS NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Học_Sinh(MaHS),
	MaDe NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Đề_Thi(MaDe),
	Diem INT
);	
GO
CREATE TABLE Kết_Quả
(
	MaHS NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Học_Sinh(MaHS),
	MaMon NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Môn_Học(MaMon),
	MaBaiLam NVARCHAR(10) FOREIGN KEY REFERENCES dbo.Bài_Làm(MaBaiLam),
	Diem INT
);
Go
INSERT INTO dbo.Khoá_Học
(
    MaKhoa,
    TenKhoa
)
VALUES
	('K26',N'Khoá 26'),
	('K25',N'Khoá 25'),
	('K27',N'Khoá 27'),
	('K28',N'Khoá 28'),
	('K29',N'Khoá 29')
Go
	INSERT INTO dbo.Lớp
	(
	    MaLop,
	    TenLop,
	    MaKhoa
	)
	VALUES
	(   'TH2002',   -- MaLop - varchar(10)
	    'CNTT', -- TenLop - varchar(100)
	    'K26'  -- MaKhoa - varchar(10)
	    ),
		('AV2002','NNA','K26'),
		('QT2002','QTKD','K26'),
		('TH2003','CNTT','K27'),
		('AV2003','NNA','K27'),
		('QT2003','QTKD','K27'),
		('TH2004','CNTT','K28'),
		('AV2004','NNA','K28'),
		('QT2004','QTKD','K28'),
		('TH2005','CNTT','K28'),
		('AV2005','NNA','K28'),
		('QT2005','QTKD','K28'),
		('TH2001','CNTT','K25'),
		('AV2001','NNA','K25'),
		('QT2001','QTKD','K25')
GO
INSERT INTO dbo.Phân_Quyền
(
    MaPhanQuyen,
    TenPhanQuyen
)
VALUES
(   'HS',  -- MaPhanQuyen - varchar(10)
    N'Học Sinh' -- TenPhanQuyen - varchar(100)
),
('GV',N'Giáo Viên'),
('AD',N'Quản Trị Viên')

GO
INSERT INTO dbo.Tài_Khoản
(
    MaTaiKhoan,
    TenDN,
    MatKhau,
    MaPhanQuyen,
    TrangThai,
    MaLop
)
VALUES
(   'TK01',   -- MaTaiKhoan - varchar(10)
    '20DH110715', -- TenDN - varchar(10)
    '12345678', -- MatKhau - varchar(10)
    'HS', -- MaPhanQuyen - varchar(10)
    'ON', -- TrangThai - varchar(10)
    'TH2002'  -- MaLop - varchar(10)
),
('TK02','20DH111540','12345678','HS','ON','AV2002'),
('TK03','20DH112176','12345678','HS','ON','QT2002'),
('TK04','20DH112177','12345678','HS','ON','TH2001'),
('TK05','20DH110161','12345678','HS','ON','AV2001'),
('TK06','20DH112004','12345678','HS','ON','QT2001'),
('TK07','20DH111916','12345678','HS','ON','TH2003'),
('TK08','20DH110423','12345678','HS','ON','AV2003'),
('TK09','20DH111177','12345678','HS','ON','TH2002')

GO
INSERT INTO dbo.Học_Sinh
(
    MaHS,
    HoTen,
    NgaySinh,
    GioiTinh,
    SDT,
    DiaChi,
    MaTaiKhoan
)
VALUES
('20DH110715',N'Nguyễn Phạm Hữu Tài','2002/07/10',N'Nam','0896329531',N'558 Bình Quới Bình Thạnh TPHCM','TK01'),
('20DH111540',N'Nguyễn Lê Uyên','2002/09/20',N'Nữ','0860111540',N'828 Sư Vạn Hạnh Q10 TPHCM','TK02'),
('20DH112176',N'Trần Trọng Tuấn','2002/10/31',N'Nam','0860112176',N'828 Sư Vạn Hạnh Q10 TPHCM','TK03'),
('20DH112177',N'Trịnh Gia Tuấn','2002/06/01',N'Nam','0860112177',N'828 Sư Vạn Hạnh Q10 TPHCM','TK04'),
('20DH110161',N'Lê Công Tuấn Kiệt','2002/11/25',N'Nam','0860110161',N'828 Sư Vạn Hạnh Q10 TPHCM','TK05'),
('20DH112004',N'Lê Quốc Huy','2002/06/01',N'Nam','0860112004',N'828 Sư Vạn Hạnh Q10 TPHCM','TK06'),
('20DH111916',N'Nguyễn Ngọc Anh','2002/11/06',N'Nữ','0860111916',N'828 Sư Vạn Hạnh Q10 TPHCM','TK07'),
('20DH110423',N'Phạm Thanh Trúc','2002/02/14',N'Nữ','0860110423',N'828 Sư Vạn Hạnh Q10 TPHCM','TK08'),
('20DH111177',N'Lê Hoài Thanh Huy','2002/03/16',N'Nam','0860111177',N'828 Sư Vạn Hạnh Q10 TPHCM','TK09')
GO
INSERT INTO dbo.Giáo_Viên
(
    MaGV,
    HoTen,
    NgaySinh,
    GioiTinh,
    SDT,
    DiaChi,
    MaTaiKhoan
)
VALUES
(   'GV01',   -- MaGV - varchar(10)
    N'Nguyễn Đức Cường' , -- HoTen - varchar(100)
    '1978/01/01', -- NgaySinh - date
    N'Nam', -- GioiTinh - varchar(10)
    '0918087000', -- SDT - varchar(10)
    NULL, -- DiaChi - varchar(10)
      NULL-- MaTaiKhoan - varchar(10)
    ),
	('GV02',N'Tôn Quang Toại','1978/1/1',N'Nam','0947774847',NULL,NULL),
	('GV03',N'Dinh Hùng','1978/1/1',N'Nam','0903789108',NULL,NULL),
	('GV04',N'Trần Minh Thái','1978/1/1',N'Nam','0918674717',NULL,NULL)
	GO
    INSERT INTO dbo.Môn_Học
    (
        MaMon,
        TenMon,
        MaKhoa
    )
    VALUES
    (   'NMLT',   -- MaMon - varchar(10)
        N'Nhập Môn Lâp Trình', -- TenMon - varchar(10)
        'K29'  -- MaKhoa - varchar(10)
        ),
		('CNPM',N'Công Nghệ Phần Mềm','K27')

go
CREATE PROCEDURE tao_moi_hs @mahs nvarchar(10),@tenhs nvarchar(100),@ngaysinh datetime,@gioitinh nvarchar(10),@sdt nvarchar(10),@diachi nvarchar(100)
AS	begin
 if exists (select hs.MaHS from dbo.Học_Sinh hs
			where hs.MaHS = @mahs)
begin
	raiserror ('Đã tồn tại học sinh có mã này ',16,1)
	return
end
else BEGIN
 INSERT INTO dbo.Học_Sinh
 (
     MaHS,
     HoTen,
     NgaySinh,
     GioiTinh,
     SDT,
     DiaChi,
     MaTaiKhoan
 )
 VALUES
 (   @mahs,   -- MaHS - varchar(10)
    @tenhs, -- HoTen - varchar(100)
     @ngaysinh, -- NgaySinh - date
     @gioitinh, -- GioiTinh - varchar(10)
     @sdt, -- SDT - varchar(10)
     @diachi, -- DiaChi - varchar(10)
     NULL  -- MaTaiKhoan - varchar(10)
     )
	 END
END
EXEC tao_moi_hs @mahs='20DH110715',@tenhs=NULL,@ngaysinh=NULL,@gioitinh=NULL,@sdt=NULL,@diachi=NULL


USE QL_HeThongThiTracNghiem
ALTER TABLE dbo.Bài_Làm
--ADD TraLoi NVARCHAR(255)
ADD	ThoiGian Datetime