CREATE PROC CreateAccount
@IdAccount char(10),
@Password nvarchar(50),
@Type int,
@IdStaff char(10),
@Name nvarchar(50),
@Birth date,
@Address nvarchar(50),
@Phone char(10)
AS BEGIN
	INSERT INTO dbo.TaiKhoan(
		TaiKhoan,
		MatKhau,
		LoaiTK
	) VALUES (
		@IdAccount,
		@Password,
		@Type
	)

	INSERT INTO dbo.Nguoi(
		MaNguoi,
		HoTen,
		NgaySinh,
		DiaChi,
		Sdt
	) VALUES (
		@IdStaff,
		@Name,
		@Birth,
		@Address,
		@Phone
	)

	INSERT INTO dbo.NhanVien(
		MaNV,
		MaNguoi,
		TaiKhoan
	) VALUES (
		@IdStaff,
		@IdStaff,
		@IdAccount
	)

END