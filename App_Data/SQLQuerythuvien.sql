
CREATE TABLE Nguoi(
	MaNguoi				varchar(10)					Primary Key,
	HoTen 				nvarchar(50)				not NULL,
	NgaySinh			date						not NULL,
	DiaChi				nvarchar(100)				not NULL,
	Sdt					char(10)					NULL,
)

CREATE TABLE TaiKhoan (
	TaiKhoan	varchar (50)	Primary Key,
	MatKhau		varchar(50)		not NULL,
	LoaiTK		int				Default((0))
)

CREATE TABLE Admin (
	MaNguoi				varchar(10)					,
	TaiKhoan			varchar (50)				PRIMARY KEY,
	Constraint MaNguoi_FK Foreign Key(MaNguoi) References Nguoi(MaNguoi),
	Constraint TaiKhoan_FK Foreign Key(TaiKhoan) References TaiKhoan(TaiKhoan)
)

CREATE TABLE NhanVien (
	MaNV		varchar(10)				Primary Key,
	MaNguoi		varchar(10)				,
	TaiKhoan	varchar(50)				,
	Constraint MaNguoi1_FK Foreign Key(MaNguoi) References Nguoi(MaNguoi),
	Constraint TaiKhoan1_FK Foreign Key(TaiKhoan) References TaiKhoan(TaiKhoan)
)


CREATE TABLE DocGia (
	MaDG			 varchar(10)				Primary Key,
	NgheNghiep		 nvarchar(10)			not NULL,
	LoaiDG			 nvarchar(50)			not NULL,
	MaNguoi			 varchar(10)				,
	Constraint MaNguoi_FK2 Foreign Key(MaNguoi) References Nguoi(MaNguoi)
)

CREATE TABLE TheLoai(
	MaTL		varchar(10)			Primary Key,
	TenTL		nvarchar(50)		not NULL,
)

CREATE TABLE Sach (
	MaSach		varchar(10)		Primary Key,
	TenSach		nvarchar(50)	not NULL,
	TenTG		nvarchar(50)	not NULL,
	SoLuong		int				not NULL,
	SoLuongTT	int				not NULL,
	MaTL		char(10)		not NULL,
	Constraint MaTL_FK Foreign Key(MaTL) References TheLoai(MaTL)
)


CREATE TABLE TheThuVien (
	MaDG		varchar(10)			,
	ThoiHan		date				not NULL,
	Constraint MaDG_PK Primary Key (MaDG),
	Constraint MaDG_FK Foreign Key(MaDG) References DocGia(MaDG)
)

CREATE TABLE PhieuMuon(
	MaPM		varchar(10)		Primary Key,
	MaDG		varchar(10)		,
	MaNV		varchar(10)		,
	NgayMuon	date			not NULL,
	NgayTra		date			not NULL,
	LuaChon		nvarchar(50)    not NULL,
	TrangThai	nvarchar(20)	not NULL,
	Constraint MaDG_FK1 Foreign Key(MaDG) References TheThuVien(MaDG),
	Constraint MaNV_FK Foreign Key(MaNV) References NhanVien(MaNV)
)

CREATE TABLE ChiTietPhieuMuon (
	MaCT		varchar(10)		PRIMARY KEY,
	MaPM		varchar(10)		,
	SoLuong		int				not NULL,
	MaSach		varchar(10)		,
	Constraint MaPM_FK Foreign Key(MaPM) References PhieuMuon(MaPM),
	Constraint MaSach_FK Foreign Key(MaSach) References Sach(MaSach)
)

CREATE TABLE PhieuPhat(
	MaPP		varchar(10)				Primary Key,
	MaPM		varchar(10)				,
	LyDo		nvarchar(150)			not NULL,
	Constraint MaPM_FK1 Foreign Key(MaPM) References PhieuMuon(MaPM)
)



