Use master;
go
CREATE DATABASE QuanLyThuVien;
go
Use QuanLyThuVien;

-- -------------------------------
-- Table 1: structure for Reader
-- -------------------------------
create table Reader (
	[Id] varchar(10) not null primary key,
	[LName] nvarchar(100) not null,
	[FName] nvarchar(20) not null,
	[boF] DateTime not null,
	[ReaderType] bit not null,
	[Status] bit not null default(1),
	[CreatedAt] DateTime not null,
	[ModifiedAt] DateTime not null,
)

-- ----------------------------
-- Records of Reader
-- ----------------------------
insert into [Reader] ([Id], [LName], [FName], [boF], [ReaderType], [CreatedAt], [ModifiedAt])
values
('R01', N'Trần Văn' , N'A', '1970-12-11', 1, '2023-09-12', '2023-09-12'),
('R02', N'Nguyễn Thị', N'Hà', '1970-1-11', 1, '2023-09-10', '2023-09-10'),
('R03', N'Nguyễn Văn', N'Bưởi', '1970-2-11', 1, '2022-09-12', '2022-09-12'),
('R04', N'Nguyễn Thúc Hà', N'Tiên', '2010-4-11', 0, '2023-09-14', '2023-09-14'),
('R05', N'Lê Văn', N'Lực', '2010-07-11', 0, '2023-09-12', '2023-09-12'),
('R06', N'Lê Văn', N'Bảy', '2010-07-11', 0, '2023-09-12', '2023-09-12'),
('R07', N'Lê Văn', N'Tám', '2010-11-11', 0, '2023-09-12', '2023-09-12')

-- -------------------------------
-- Table 2: structure for Adult
-- -------------------------------
create table Adult (
	[IdReader] varchar(10) not null primary key,
	[Identify] varchar(12),
	[Address] nvarchar(100) not null,
	[City] nvarchar(100) not null,
	[Phone] varchar(12) not null,
	[ExpireDate] DateTime not null,
	[Status] bit not null default(1),
	[CreatedAt] DateTime not null,
	[ModifiedAt] DateTime not null,
	
	foreign key ([IdReader]) references Reader([Id])
)

-- ----------------------------
-- Records of Adult
-- ----------------------------
insert into [Adult] ([IdReader], [Identify], [Address], [City], [Phone], [ExpireDate], [CreatedAt], [ModifiedAt])
values
('R01', '123456789012', N'Số 2, Lê Thắng Tôn', N'Khánh Hòa', '0935768249', '2025-09-12', '2024-09-12', '2024-09-12'),
('R02', '234567890123', N'Số 10 Trần Quý Cáp', N'Khánh Hòa', '0123222111', '2025-09-10', '2024-09-10', '2024-09-10'),
('R03', '345678901234', N'Số 3 Trần Nhân Tông', N'Khánh Hòa','0145333111', '2023-09-12', '2022-09-12', '2022-09-12')

-- -------------------------------
-- Table 3: structure for Child
-- -------------------------------
create table Child (
	[IdReader] varchar(10) not null primary key,
	[IdAdult] varchar(10) not null,
	[Status] bit not null default(1),
	[CreatedAt] DateTime not null,
	[ModifiedAt] DateTime not null,
	
	foreign key ([IdReader]) references Reader([ID]),
	foreign key ([IdAdult]) references Adult([IdReader])
)

-- ----------------------------
-- Records of Child
-- ----------------------------
insert into [Child] ([IdReader], [IdAdult], [CreatedAt], [ModifiedAt])
values
	('R04', 'R01', '2023-09-14', '2023-09-14'),
	('R05', 'R01', '2023-09-12', '2023-09-12'),
	('R06', 'R02', '2023-09-12', '2023-09-12'),
	('R07', 'R03', '2023-09-12', '2023-09-12')

-- -------------------------------
-- Table 4: structure for Publisher
-- -------------------------------
create table Publisher (
	[Id] varchar(10) not null primary key,
	[Name] nvarchar(100) not null,
	[Phone] varchar(12) not null,
	[Address] nvarchar(100) not null,
)

-- ----------------------------
-- Records of Publisher
-- ----------------------------
insert into [Publisher]
values
('P01', N'NXB Kim Đồng', '0901931765', N'79 Hoàng Hoa Thám'),
('P02', N'NXB Kim Lân', '0901000765', N'79 Hoàng Hoa Thụ'),
('P03', N'NXB Hải Quân', '0101000765', N'79 Hoàng Hoa Hái')

-- -------------------------------
-- Table 5: structure for Category
-- -------------------------------
create table Category (
	[Id] varchar(10) not null primary key,
	[Name] nvarchar(50) not null,
	
	[Status] bit not null default(1),
	[CreatedAt] DateTime not null,
	[ModifiedAt] DateTime not null,
)

-- ----------------------------
-- Records of Category
-- ----------------------------
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C01', N'Truyện tranh', 1, CAST(0x0000B07D00000000 AS DateTime), CAST(0x0000B07D00000000 AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C02', N'Tiểu thuyết', 1, CAST(0x0000B07E00000000 AS DateTime), CAST(0x0000B07E00000000 AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C03', N'Luật Pháp', 1, CAST(0x0000B07F00000000 AS DateTime), CAST(0x0000B07F00000000 AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C04', N'Kỹ năng sống và phát triển bản thân', 1, CAST(0x0000B0E400A3BC37 AS DateTime), CAST(0x0000B0E400A3BC37 AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C05', N'Tiểu thuyết huyền bí và triết học', 1, CAST(0x0000B0E400A3D77B AS DateTime), CAST(0x0000B0E400A3D77B AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C06', N'Tiểu thuyết và tâm lý và xã hội', 1, CAST(0x0000B0E400A41D1E AS DateTime), CAST(0x0000B0E400A41D1E AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C07', N'Tiểu thuyết và lịch sử và chiến tranh', 1, CAST(0x0000B0E400A43A80 AS DateTime), CAST(0x0000B0E400A43A80 AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C08', N'Hài hước và trào phúng và xã hội', 1, CAST(0x0000B0E400A44D8E AS DateTime), CAST(0x0000B0E400A44D8E AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C09', N'Tiểu thuyết và thiếu nhi và tình cảm', 1, CAST(0x0000B0E400A4635B AS DateTime), CAST(0x0000B0E400A4635B AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C10', N'Tiểu thuyết và lịch sử và tình cảm', 1, CAST(0x0000B0E400A8C263 AS DateTime), CAST(0x0000B0E400A8C263 AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C11', N'Tiểu thuyết và chiến tranh và tình cảm', 1, CAST(0x0000B0E400AAB59D AS DateTime), CAST(0x0000B0E400AAB59D AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C12', N'Tiểu thuyết và lịch sử và văn hóa', 1, CAST(0x0000B0E400AB6062 AS DateTime), CAST(0x0000B0E400AB6062 AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'C13', N'Tiểu thuyết và đời sống và tình cảm', 1, CAST(0x0000B0E400ABD66D AS DateTime), CAST(0x0000B0E400ABD66D AS DateTime))

-- -------------------------------
-- Table 6: structure for Author
-- -------------------------------
create table Author (
	[Id] varchar(10) not null primary key,
	[Name] nvarchar(50) not null,
	[Description] ntext not null,
	[boF] DateTime not null,
	[Summary] ntext not null,
	
	[Status] bit not null default(1),
	[CreatedAt] DateTime not null,
	[ModifiedAt] DateTime not null,
)

-- ----------------------------
-- Records of Author
-- ----------------------------
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A01', N'Nguyễn Nhật Ánh', N'Tác giả của nhiều tác phẩm văn học nổi tiếng như "Cho tôi xin một vé đi tuổi thơ", "Kính vạn hoa", "Tôi thấy hoa vàng trên cỏ xanh"', CAST(0x0000735800000000 AS DateTime), N'Tác giả nổi tiếng Việt Nam', 1, CAST(0x0000AB5500000000 AS DateTime), CAST(0x0000AB5500000000 AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A02', N'Ngô Thị Thúy Vân', N'Tác giả của cuốn sách "Bí mật của tôi" được đánh giá cao về nội dung và phong cách viết', CAST(0x00008DF500000000 AS DateTime), N'Tác giả triển vọng', 1, CAST(0x0000AB3500000000 AS DateTime), CAST(0x0000AB3500000000 AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A03', N'Lê Minh Tú', N'Tác giả của cuốn sách "Sống thật lòng" đã gây được tiếng vang lớn trong giới trẻ', CAST(0x0000874E00000000 AS DateTime), N'Tác giả trẻ có tầm ảnh hưởng', 1, CAST(0x0000ACA300000000 AS DateTime), CAST(0x0000ACA300000000 AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A04', N'Fujiko Fujio', N'Tác giả của bộ truyện tranh nổi tiếng "Doraemon" đã trở thành một biểu tượng văn hóa của Nhật Bản', CAST(0x0000306300000000 AS DateTime), N'Tác giả truyện tranh nổi tiếng', 1, CAST(0x0000AD1F00000000 AS DateTime), CAST(0x0000AD1F00000000 AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A05', N'Nguyễn Thị Hà', N'Tác giả của cuốn sách "Đi tìm giấc mơ" được đánh giá cao về tính nhân văn và sự tinh tế trong cách diễn đạt', CAST(0x0000901B00000000 AS DateTime), N'Tác giả trẻ triển vọng', 1, CAST(0x0000AF2100000000 AS DateTime), CAST(0x0000AF2100000000 AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A06', N'Dale Carnegie', N'Dale Carnegie là một nhà văn, nhà giáo dục, nhà ngoại giao và nhà tâm lý học nổi tiếng của Mỹ. Ông là tác giả của nhiều cuốn sách bán chạy nhất thế giới về kỹ năng giao tiếp, lãnh đạo và tự giúp mình.', CAST(0xFFFFF02900000000 AS DateTime), N'Dale Carnegie là một người có tầm nhìn xa xôi và sáng tạo. Ông đã tạo ra một phương pháp đào tạo dựa trên kinh nghiệm thực tế và những bài học từ cuộc sống. Ông đã giúp hàng triệu người trên thế giới cải thiện kỹ năng giao tiếp, tự tin và thành công hơn trong công việc và cuộc sống.', 1, CAST(0x0000B0E400A4812A AS DateTime), CAST(0x0000B0E400A4812A AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A07', N'Paulo Coelho', N'Paulo Coelho là một nhà văn, nhà thơ, nhà báo và nhà hoạt động xã hội người Brazil. Ông là một trong những tác giả được yêu thích nhất thế giới, với hơn 200 triệu bản sách được bán ra ở hơn 150 quốc gia và được dịch ra hơn 80 ngôn ngữ.', CAST(0x000043F900000000 AS DateTime), N'Paulo Coelho là một người theo đuổi ước mơ và tinh thần phiêu lưu. Ông đã từng làm nhiều nghề khác nhau, từ nhạc sĩ, nhà sản xuất, đạo diễn đến diễn viên. Ông đã viết nhiều cuốn sách nổi tiếng về tâm linh, triết học và nhân sinh quan, như Nhà Giả Kim, Veronika Quyết Định Chết, Người Tình, Điệu Valse Của Những Người Cô Đơn, vv.', 1, CAST(0x0000B0E400A4B506 AS DateTime), CAST(0x0000B0E400A4B506 AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A08', N'Fyodor Dostoyevsky', N'Fyodor Dostoyevsky là một nhà văn, nhà triết học và nhà phê bình xã hội nổi tiếng của Nga. Ông là một trong những nhà văn vĩ đại nhất thế giới, được coi là cha đẻ của tiểu thuyết hiện đại. Ông đã viết nhiều tác phẩm kinh điển, phản ánh những vấn đề sâu sắc về con người, tôn giáo, đạo đức, tội ác, xã hội và lịch sử, như Tội Ác Và Hình Phạt, Những Kẻ Bị Hủy Hoại, Con Trai Lý Tưởng, Nhà Tù Siberia, Nhân Vật Bé Nhỏ, vv.', CAST(0xFFFF908400000000 AS DateTime), N'Fyodor Dostoyevsky là một người có cuộc đời đầy biến cố và thử thách. Ông đã từng bị kết án tử hình vì tham gia một nhóm chống chính quyền, nhưng sau đó được ân xá vào phút chót. Ông đã phải sống trong nghèo khổ, bệnh tật, nợ nần và bị đánh bại bởi những kẻ thù. Ông đã trải qua nhiều mối tình đau khổ, mất mát và phản bội. Ông đã chiến đấu với những nỗi ám ảnh, nghi ngờ và tuyệt vọng. Nhưng qua tất cả, ông đã không ngừng sáng tạo và viết nên những tác phẩm bất hủ, làm rung động lòng người.', 1, CAST(0x0000B0E400A4DD6F AS DateTime), CAST(0x0000B0E400A4DD6F AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A09', N'Tố Tâm', N'Tố Tâm là một nhà văn, nhà báo và nhà hoạt động chính trị người Việt Nam. Ông là một trong những nhà văn đầu tiên viết về cuộc chiến tranh Đông Dương từ góc nhìn của người lính Pháp. Ông cũng là một nhà cách mạng, tham gia vào phong trào đấu tranh giải phóng dân tộc và đoàn kết quốc tế. Ông đã bị bắt và xử tử bởi chính quyền Ngô Đình Diệm vào năm 1958.', CAST(0x00001F6300000000 AS DateTime), N'Tố Tâm là một người có tài năng và tâm hồn cao đẹp. Ông đã viết nên những tác phẩm văn học có giá trị nghệ thuật và lịch sử, phản ánh những mặt trái và khốc liệt của chiến tranh, cũng như những tình cảm và tư tưởng của người lính Pháp đối với đất nước và dân tộc Việt Nam. Ông cũng là một người anh hùng, hy sinh vì sự nghiệp giải phóng dân tộc và hòa bình thế giới.', 1, CAST(0x0000B0E400A6E10E AS DateTime), CAST(0x0000B0E400A6E10E AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A10', N'Trịnh Công Sơn', N'Trịnh Công Sơn là một nhà thơ, nhạc sĩ và họa sĩ nổi tiếng của Việt Nam. Ông được coi là một trong những nhà văn hóa lớn nhất của Việt Nam trong thế kỷ 20. Ông đã sáng tác hơn 600 bài hát, trong đó nhiều bài hát đã trở thành những bản tình ca bất hủ, như Diễm Xưa, Ru Tình, Như Cánh Vạc Bay, vv. Ông cũng là một nhà thơ và họa sĩ có tài, viết nhiều bài thơ và tranh vẽ đẹp và ý nghĩa.', CAST(0x000037DE00000000 AS DateTime), N'Trịnh Công Sơn là một người nghệ sĩ đa tài và đa màu sắc. Ông đã góp phần làm giàu cho văn hóa và nghệ thuật của Việt Nam bằng những tác phẩm sáng tạo và độc đáo. Ông cũng là một người yêu chuộng hòa bình và nhân ái, phản đối chiến tranh và bạo lực. Ông đã để lại một dấu ấn sâu đậm trong lòng người Việt Nam và bạn bè quốc tế.', 1, CAST(0x0000B0E400A74529 AS DateTime), CAST(0x0000B0E400A74529 AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A11', N'Nguyễn Nhật Ánh', N'Nguyễn Nhật Ánh là một nhà văn, nhà thơ và nhà báo nổi tiếng của Việt Nam. Ông là một trong những tác giả được yêu thích nhất của thế hệ đọc sách 8x, 9x và 10x. Ông đã viết hơn 100 cuốn sách, trong đó nhiều cuốn sách đã được dịch ra nhiều ngôn ngữ và được chuyển thể thành phim, như Thấy Hoa Vàng Trên Cỏ Xanh, Tôi Thấy Hoa Vàng Trên Cỏ Xanh, Mắt Biếc, Chúc Một Ngày Tốt Lành, vv.', CAST(0x00004EF600000000 AS DateTime), N'Nguyễn Nhật Ánh là một người có tình yêu và niềm đam mê với văn chương. Ông đã viết nên những tác phẩm văn học mang đậm màu sắc quê hương, phản ánh những nét đẹp và đáng yêu của cuộc sống, những tình cảm và tư tưởng của tuổi trẻ. Ông cũng là một người hài hước và duyên dáng, luôn mang đến cho người đọc những tiếng cười và những bài học ý nghĩ', 1, CAST(0x0000B0E400A7D3D9 AS DateTime), CAST(0x0000B0E400A7D3D9 AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A12', N'Dương Hướng', N' Dương Hướng là một nhà văn, nhà báo và nhà hoạt động xã hội nổi tiếng của Việt Nam. Ông là một trong những nhà văn đầu tiên viết về cuộc sống và tâm lý của phụ nữ Việt Nam trong thời kỳ kháng chiến. Ông cũng là một nhà cách mạng, tham gia vào phong trào đấu tranh giải phóng dân tộc và đoàn kết quốc tế. Ông đã bị bắt và bị tù giam nhiều lần bởi chính quyền Pháp và Ngô Đình Diệm.', CAST(0x000023AB00000000 AS DateTime), N' Dương Hướng là một người có tài năng và tâm hồn cao đẹp. Ông đã viết nên những tác phẩm văn học có giá trị nghệ thuật và lịch sử, phản ánh những mặt trái và khốc liệt của chiến tranh, cũng như những tình cảm và tư tưởng của phụ nữ Việt Nam trong thời đại biến động. Ông cũng là một người anh hùng, hy sinh vì sự nghiệp giải phóng dân tộc và hòa bình thế giới.', 1, CAST(0x0000B0E400A90031 AS DateTime), CAST(0x0000B0E400A90031 AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A13', N'Đoàn Giỏi', N'Đoàn Giỏi là một nhà văn, nhà báo và nhà cách mạng nổi tiếng của Việt Nam. Ông là một trong những nhà văn đầu tiên viết về cuộc chiến tranh Đông Dương từ góc nhìn của người lính Việt Nam. Ông cũng là một người lính thực thụ, đã tham gia vào nhiều trận đánh lịch sử, như Điện Biên Phủ, Chiến dịch Hồ Chí Minh, vv. Ông đã hy sinh anh dũng trong một trận đánh ở miền Nam năm 1972.', CAST(0x000023AB00000000 AS DateTime), N'Đoàn Giỏi là một người có tài năng và tâm hồn cao thượng. Ông đã viết nên những tác phẩm văn học có giá trị nghệ thuật và lịch sử, phản ánh những khổ cực và vinh quang của người lính Việt Nam trong cuộc chiến tranh chống ngoại xâm. Ông cũng là một người anh hùng, hy sinh vì sự nghiệp giải phóng dân tộc và thống nhất đất nước.', 1, CAST(0x0000B0E400A91C8D AS DateTime), CAST(0x0000B0E400A91C8D AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A14', N'Lê Lựu', N'Lê Lựu là một nhà văn, nhà báo và nhà nghiên cứu văn hóa nổi tiếng của Việt Nam. Ông là một trong những nhà văn đầu tiên viết về cuộc sống và tâm lý của người Việt Nam trong thời kỳ đô hộ Pháp. Ông cũng là một nhà văn hóa, đã có nhiều công trình nghiên cứu về lịch sử, văn hóa, tôn giáo và dân tộc học của Việt Nam.', CAST(0x00001C8800000000 AS DateTime), N'Lê Lựu là một người có tài năng và tâm hồn cao thượng. Ông đã viết nên những tác phẩm văn học có giá trị nghệ thuật và lịch sử, phản ánh những khổ cực và vinh quang của người Việt Nam trong cuộc đấu tranh chống ngoại xâm. Ông cũng là một người học giả, đã góp phần làm sáng tỏ những nét đẹp và đặc sắc của văn hóa và dân tộc Việt Nam.', 1, CAST(0x0000B0E400AB1E5B AS DateTime), CAST(0x0000B0E400AB1E5B AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [Description], [boF], [Summary], [Status], [CreatedAt], [ModifiedAt]) VALUES (N'A15', N'Nguyễn Ngọc Tư', N'Nguyễn Ngọc Tư là một nhà văn, nhà thơ và nhà báo nổi tiếng của Việt Nam. Ông là một trong những tác giả trẻ được yêu thích nhất của thế hệ đọc sách 10x và 11x. Ông đã viết nhiều cuốn sách, trong đó nhiều cuốn sách đã được dịch ra nhiều ngôn ngữ và được chuyển thể thành phim, như Cánh Đồng Bất Tận, Cô Gái Mở Đường, Cánh Đồng Mặn, vv.', CAST(0x00006C6E00000000 AS DateTime), N'Nguyễn Ngọc Tư là một người có tình yêu và niềm đam mê với văn chương. Ông đã viết nên những tác phẩm văn học mang đậm màu sắc miền Tây, phản ánh những nét đẹp và đáng yêu của cuộc sống, những tình cảm và tư tưởng của người dân miền quê. Ông cũng là một người hài hước và duyên dáng, luôn mang đến cho người đọc những tiếng cười và những bài học ý nghĩa.', 1, CAST(0x0000B0E400AB3CCC AS DateTime), CAST(0x0000B0E400AB3CCC AS DateTime))

-- -------------------------------
-- Table 7: structure for Translator
-- -------------------------------
create table Translator (
	[Id] varchar(10) not null primary key,
	[Name] nvarchar(50) not null,
	[Description] ntext not null,
	[boF] DateTime not null,
	[Summary] ntext not null,
	
	[Status] bit not null default(1),
	[CreatedAt] DateTime not null,
	[ModifiedAt] DateTime not null,
)

-- ----------------------------
-- Records of Translator
-- ----------------------------
insert into [Translator] ([Id], [Name], [Description], [boF], [Summary], [CreatedAt], [ModifiedAt])
values
	('T01', N'Nguyễn Nhật Ánh', N'Tác giả của nhiều tác phẩm văn học nổi tiếng như "Cho tôi xin một vé đi tuổi thơ", "Kính vạn hoa", "Tôi thấy hoa vàng trên cỏ xanh"', '1980-11-05', N'Tác giả nổi tiếng Việt Nam', '2020-02-02', '2020-02-02'),
	('T02', N'Ngô Thị Thúy Vân', N'Tác giả của cuốn sách "Bí mật của tôi" được đánh giá cao về nội dung và phong cách viết', '1999-07-02', N'Tác giả triển vọng', '2020-01-01', '2020-01-01'),
	('T03', N'Lê Minh Tú', N'Tác giả của cuốn sách "Sống thật lòng" đã gây được tiếng vang lớn trong giới trẻ', '1994-11-02', N'Tác giả trẻ có tầm ảnh hưởng', '2021-01-01', '2021-01-01'),
	('T04', N'Fujiko Fujio', N'Tác giả của bộ truyện tranh nổi tiếng "Doraemon" đã trở thành một biểu tượng văn hóa của Nhật Bản', '1933-12-01', N'Tác giả truyện tranh nổi tiếng', '2021-05-05', '2021-05-05'),
	('T05', N'Nguyễn Thị Hà', N'Tác giả của cuốn sách "Đi tìm giấc mơ" được đánh giá cao về tính nhân văn và sự tinh tế trong cách diễn đạt', '2001-01-02', N'Tác giả trẻ triển vọng', '2022-10-01', '2022-10-01')


-- -------------------------------
-- Table 12: structure for BookStatus
-- -------------------------------
create table BookStatus (
	[Id] varchar(10) not null primary key,
	[Name] nvarchar(100) not null,
	[Description] ntext not null default(''),
	
	[Status] bit not null default(1),
	[CreatedAt] DateTime not null,
	[ModifiedAt] DateTime not null,
)

-- ----------------------------
-- Records of BookStatus
-- ----------------------------
insert into [BookStatus] ([Id], [Name], [Description], [Status], [CreatedAt], [ModifiedAt])
values
	('BS1', N'Normal', N'The book is normal', 1, '2023-09-01 15:00', '2023-09-01 15:00'),
	('BS2', N'Lost', N'The book is lost', 1, '2023-09-01 15:00', '2023-09-01 15:00'),
	('BS3', N'Spoil', N'The book is spoil', 1, '2023-09-01 15:00', '2023-09-01 15:00')
	
-- -------------------------------
-- Table 8: structure for BookTitle
-- -------------------------------
create table BookTitle (
	[Id] varchar(10) not null primary key,
	[IdCategory] varchar(10) not null,
	[Name] nvarchar(100) not null,
	[Note] ntext null default(''),
	[Summary] ntext not null,
	[UrlImage] varchar(100) not null,
	
	foreign key ([IdCategory]) references Category([ID])
)

-- ----------------------------
-- Records of BookTitle
-- ----------------------------
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT01', N'C03', N'Tư Duy Pháp Lý Của Luật Sư', N'', N'Nội dung hấp dẫn', '/Assets/BookImages/BT01.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT02', N'C01', N'Doraemon', N'', N'Cuốn sách rất thú vị và đáng đọc', '/Assets/BookImages/BT02.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT03', N'C02', N'Giết con chim nhại', N'', N'Một cuốn sách tuyệt vời cho những người yêu thích thể loại tiểu thuyết', '/Assets/BookImages/BT03.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT04', N'C01', N'Trò Chơi Thế Thân', N'', N'Tác phẩm đầy cảm hứng', '/Assets/BookImages/BT04.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT05', N'C02', N'Con chim xanh biếc bay về', N'', N'Tác giả đã tạo ra một câu chuyện đầy cảm hứng và ý nghĩa', '/Assets/BookImages/BT05.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT06', N'C04', N'Đắc Nhân Tâm', NULL, N'Sách giới thiệu những nguyên tắc vàng trong việc đối nhân xử thế, thu phục lòng người và tạo ảnh hưởng tích cực.', '/Assets/BookImages/BT06.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT07', N'C05', N'Nhà Giả Kim', NULL, N'Sách kể về hành trình phiêu lưu của Santiago, một chàng chăn cừu người Tây Ban Nha, đi tìm kho báu ẩn giấu ở Ai Cập. Trên đường đi, anh gặp nhiều người và trải qua nhiều thử thách, đồng thời khám phá ra ý nghĩa sâu sắc của cuộc sống và tình yêu.', '/Assets/BookImages/BT07.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT08', N'C06', N'Tội Ác Và Hình Phạt', NULL, N'Sách xoay quanh câu chuyện của Raskolnikov, một sinh viên nghèo khổ ở Sankt-Peterburg, quyết định giết chết một bà già cho vay nặng lãi để lấy tiền. Sau khi gây án, anh phải đối mặt với những cơn ác mộng, nỗi hối hận và sự truy lùng của cảnh sát. Anh cũng', '/Assets/BookImages/BT08.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT09', N'C07', N'Vàng Và Máu', NULL, N'Sách tái hiện lại cuộc chiến tranh Đông Dương lần thứ nhất giữa Pháp và Việt Nam, qua góc nhìn của một người lính Pháp, Tố Tâm, người yêu nước Việt Nam và ghét sự đàn áp của đế quốc. Sách là một tác phẩm độc đáo, mang âm hưởng của văn học hiện thực và bi kịch nhân sinh.', '/Assets/BookImages/BT09.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT10', N'C08', N'Bốn Mươi Năm Nói Láo', NULL, N'Sách là một tuyển tập những bài viết hài hước và sắc bén của Trịnh Công Sơn, một nhà thơ, nhạc sĩ và họa sĩ nổi tiếng của Việt Nam. Sách bao gồm những chuyện vui, những bình luận châm biếm và những suy ngẫm sâu sắc về cuộc sống, văn hóa, chính trị và tình yêu của người Việt Nam trong bốn mươi năm qua.', '/Assets/BookImages/BT10.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT11', N'C09', N'Thấy Hoa Vàng Trên Cỏ Xanh', NULL, N'Sách kể về tuổi thơ của hai anh em Thành và Tú ở một làng quê miền Tây Nam Bộ, nơi có những cánh đồng lúa bát ngát, những con sông uốn lượn và những bông hoa vàng rực rỡ. Sách là một tác phẩm đẹp và trong sáng, mang đến cho người đọc những cảm xúc ngọt ngào và nhớ nhung về một thời ấu thơ đơn giản và hạnh phúc.', '/Assets/BookImages/BT11.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT12', N'C10', N'Bến Không Chồng', NULL, N'Sách tái hiện lại cuộc sống của những người phụ nữ miền Nam trong thời kỳ kháng chiến chống Pháp, qua góc nhìn của chị Hà, một người phụ nữ trẻ, xinh đẹp, thông minh và quyết tâm. Sách là một tác phẩm độc đáo, mang âm hưởng của văn học hiện thực và nữ quyền.', '/Assets/BookImages/BT12.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT13', N'C07', N'Đất Rừng Phương Nam', NULL, N'Sách kể về cuộc chiến tranh Đông Dương lần thứ hai giữa Việt Nam và Pháp, qua góc nhìn của một người lính Việt Nam, Đoàn Giỏi, người yêu nước và anh dũng. Sách là một tác phẩm kinh điển, mang đến cho người đọc những cảnh tượng chiến tranh đẫm máu, những mối tình đẹp và những tình huynh đệ cao cả.', '/Assets/BookImages/BT13.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT14', N'C11', N'Bồ câu không đưa thư', NULL, N'Sách kể về cuộc tình đẹp nhưng đầy bi kịch của cô gái Pháp Lan và chàng trai Việt Nam Hùng trong bối cảnh chiến tranh Việt Nam. Sách là một tác phẩm cảm động, mang đến cho người đọc những cảnh tượng chiến tranh khốc liệt, những mất mát đau lòng và những hy sinh cao cả.', '/Assets/BookImages/BT14.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT15', N'C12', N'Thời Xa Vắng', NULL, N' Sách tái hiện lại cuộc sống của những người Việt Nam trong thời kỳ đầu thế kỷ XX, khi đất nước đang chịu sự đô hộ của thực dân Pháp. Sách là một tác phẩm sâu sắc, mang đến cho người đọc những góc nhìn đa chiều về những vấn đề về lịch sử, văn hóa, tôn giáo, tình yêu và gia đình của người Việt Nam đương thời.', '/Assets/BookImages/BT15.png')
INSERT [dbo].[BookTitle] ([Id], [IdCategory], [Name], [Note], [Summary], [UrlImage]) VALUES (N'BT16', N'C13', N'Cánh Đồng Bất Tận', NULL, N' Sách kể về cuộc sống của những người dân miền Tây Nam Bộ, nơi có những cánh đồng lúa bạt ngàn, những con sông mênh mông và những bông sen tinh khiết. Sách là một tác phẩm đẹp và trong sáng, mang đến cho người đọc những cảm xúc ngọt ngào và nhẹ nhàng về một miền quê yên bình và hạnh phúc.', '/Assets/BookImages/BT16.png')
	
-- -------------------------------
-- Table 9: structure for BookISBN (sách gốc)
-- -------------------------------
create table BookISBN (
	[ISBN] varchar(10) not null primary key,
	[IdBookTitle] varchar(10) not null,
	[IdAuthor] varchar(10) not null, -- 1 tác giả
	[OriginLanguage] nvarchar(50) not null, -- ngôn ngữ gốc

	[Description] ntext null default(''),
	[Status] bit not null default(1),

	foreign key ([IdBookTitle]) references BookTitle([ID]),
	foreign key ([IdAuthor]) references Author([ID]),
)

-- ----------------------------
-- Records of BookISBN
-- ----------------------------	
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN01', N'BT01', N'A03', N'Tiếng Việt', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN02', N'BT01', N'A01', N'Tiếng Nhật', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN03', N'BT01', N'A02', N'Tiếng Pháp', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN04', N'BT02', N'A02', N'Tiếng Việt', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN05', N'BT02', N'A05', N'Tiếng Pháp', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN06', N'BT06', N'A06', N'Vietnamese (Vietnam)', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN07', N'BT07', N'A07', N'Vietnamese (Vietnam)', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN08', N'BT08', N'A08', N'Vietnamese (Vietnam)', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN09', N'BT09', N'A09', N'Vietnamese (Vietnam)', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN10', N'BT10', N'A10', N'Vietnamese (Vietnam)', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN11', N'BT11', N'A11', N'Vietnamese (Vietnam)', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN12', N'BT12', N'A12', N'Vietnamese (Vietnam)', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN13', N'BT13', N'A13', N'Vietnamese (Vietnam)', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN14', N'BT14', N'A11', N'Vietnamese (Vietnam)', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN15', N'BT15', N'A14', N'Vietnamese (Vietnam)', NULL, 1)
INSERT [dbo].[BookISBN] ([ISBN], [IdBookTitle], [IdAuthor], [OriginLanguage], [Description], [Status]) VALUES (N'ISBN16', N'BT16', N'A15', N'Vietnamese (Vietnam)', NULL, 0)
	

-- -------------------------------
-- Table 10: structure for Book
-- -------------------------------
create table Book (
	[Id] int not null primary key,
	[ISBN] varchar(10) not null, -- mã sách gốc
	[IdPublisher] varchar(10) not null, -- mã nhà xuất bản
	[IdTranslator] varchar(10) not null, -- mã người dịch
	
	[Language] nvarchar(50) not null, -- ngôn ngữ khác
	[PublishDate] DateTime not null,
	[Price] decimal(12,3) not null,
	[PriceCurrent] decimal(12,3) not null,
	
	[Note] ntext null default(''),
	[IdBookStatus] varchar(10) not null default('BS1'),
	
	[Status] bit not null default(1),
	[CreatedAt] DateTime not null,
	[ModifiedAt] DateTime not null,
	
	foreign key ([ISBN]) references BookISBN([ISBN]),
	foreign key ([IdPublisher]) references Publisher([Id]),
	foreign key ([IdTranslator]) references Translator([Id]),
	foreign key ([IdBookStatus]) references BookStatus([Id])
)
-- ----------------------------
-- Records of Book
-- ----------------------------
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (1, N'ISBN01', N'P01', N'T01', N'Tiếng Việt', CAST(0x0000AFD800C5C100 AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), N'', N'BS1', 0, CAST(0x0000AFD800C5C100 AS DateTime), CAST(0x0000AFD800C5C100 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (2, N'ISBN01', N'P01', N'T01', N'Tiếng Việt', CAST(0x0000AFF600E6B680 AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), N'', N'BS1', 1, CAST(0x0000AFF600E6B680 AS DateTime), CAST(0x0000AFF600E6B680 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (3, N'ISBN01', N'P02', N'T02', N'Tiếng Việt', CAST(0x0000B01500FF6EA0 AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), N'', N'BS1', 1, CAST(0x0000B01500FF6EA0 AS DateTime), CAST(0x0000B01500FF6EA0 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (4, N'ISBN02', N'P02', N'T02', N'Tiếng Nhật', CAST(0x0000B03300DBBA00 AS DateTime), CAST(98.000 AS Decimal(12, 3)), CAST(98.000 AS Decimal(12, 3)), N'', N'BS1', 0, CAST(0x0000B03300DBBA00 AS DateTime), CAST(0x0000B03300DBBA00 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (5, N'ISBN02', N'P02', N'T02', N'Tiếng Nhật', CAST(0x0000B05200C88020 AS DateTime), CAST(98.000 AS Decimal(12, 3)), CAST(98.000 AS Decimal(12, 3)), N'', N'BS1', 1, CAST(0x0000B05200C88020 AS DateTime), CAST(0x0000B05200C88020 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (6, N'ISBN02', N'P02', N'T02', N'Tiếng Nhật', CAST(0x0000B07100F9F060 AS DateTime), CAST(98.000 AS Decimal(12, 3)), CAST(98.000 AS Decimal(12, 3)), N'', N'BS1', 1, CAST(0x0000B07100F9F060 AS DateTime), CAST(0x0000B07100F9F060 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (7, N'ISBN02', N'P03', N'T03', N'Tiếng Nhật', CAST(0x0000B08F005AA320 AS DateTime), CAST(98.000 AS Decimal(12, 3)), CAST(98.000 AS Decimal(12, 3)), N'', N'BS1', 1, CAST(0x0000B08F005AA320 AS DateTime), CAST(0x0000B08F005AA320 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (8, N'ISBN02', N'P03', N'T03', N'Tiếng Nhật', CAST(0x0000AF9C00685EC0 AS DateTime), CAST(98.000 AS Decimal(12, 3)), CAST(98.000 AS Decimal(12, 3)), N'', N'BS1', 1, CAST(0x0000AF9C00685EC0 AS DateTime), CAST(0x0000AF9C00685EC0 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (9, N'ISBN02', N'P03', N'T04', N'Tiếng Nhật', CAST(0x0000AFC1006B1DE0 AS DateTime), CAST(98.000 AS Decimal(12, 3)), CAST(98.000 AS Decimal(12, 3)), N'', N'BS1', 1, CAST(0x0000AFC1006B1DE0 AS DateTime), CAST(0x0000AFC1006B1DE0 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (10, N'ISBN02', N'P03', N'T04', N'Tiếng Nhật', CAST(0x0000AFA000EA8EE0 AS DateTime), CAST(98.000 AS Decimal(12, 3)), CAST(98.000 AS Decimal(12, 3)), N'', N'BS1', 1, CAST(0x0000AFA000EA8EE0 AS DateTime), CAST(0x0000AFA000EA8EE0 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (11, N'ISBN03', N'P03', N'T04', N'Tiếng Pháp', CAST(0x0000AFD800C5C100 AS DateTime), CAST(80.000 AS Decimal(12, 3)), CAST(80.000 AS Decimal(12, 3)), N'', N'BS1', 0, CAST(0x0000AFD800C5C100 AS DateTime), CAST(0x0000AFD800C5C100 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (12, N'ISBN03', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC5215 AS DateTime), CAST(0x0000B0E400AC5215 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (13, N'ISBN03', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC5217 AS DateTime), CAST(0x0000B0E400AC5217 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (14, N'ISBN03', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC5218 AS DateTime), CAST(0x0000B0E400AC5218 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (15, N'ISBN03', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC521A AS DateTime), CAST(0x0000B0E400AC521A AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (16, N'ISBN03', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC521B AS DateTime), CAST(0x0000B0E400AC521B AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (17, N'ISBN03', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC521D AS DateTime), CAST(0x0000B0E400AC521D AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (18, N'ISBN03', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC521E AS DateTime), CAST(0x0000B0E400AC521E AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (19, N'ISBN03', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC5220 AS DateTime), CAST(0x0000B0E400AC5220 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (20, N'ISBN03', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC5221 AS DateTime), CAST(0x0000B0E400AC5221 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (21, N'ISBN03', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC5222 AS DateTime), CAST(0x0000B0E400AC5222 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (22, N'ISBN04', N'P03', N'T04', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC541F AS DateTime), CAST(80.000 AS Decimal(12, 3)), CAST(80.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC6B6C AS DateTime), CAST(0x0000B0E400AC6B6C AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (23, N'ISBN04', N'P03', N'T04', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC541F AS DateTime), CAST(80.000 AS Decimal(12, 3)), CAST(80.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC6B6D AS DateTime), CAST(0x0000B0E400AC6B6D AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (24, N'ISBN04', N'P03', N'T04', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC541F AS DateTime), CAST(80.000 AS Decimal(12, 3)), CAST(80.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC6B6F AS DateTime), CAST(0x0000B0E400AC6B6F AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (25, N'ISBN04', N'P03', N'T04', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC541F AS DateTime), CAST(80.000 AS Decimal(12, 3)), CAST(80.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC6B71 AS DateTime), CAST(0x0000B0E400AC6B71 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (26, N'ISBN04', N'P03', N'T04', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC541F AS DateTime), CAST(80.000 AS Decimal(12, 3)), CAST(80.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC6B72 AS DateTime), CAST(0x0000B0E400AC6B72 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (27, N'ISBN05', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC6E3D AS DateTime), CAST(85.000 AS Decimal(12, 3)), CAST(85.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC7E35 AS DateTime), CAST(0x0000B0E400AC7E35 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (28, N'ISBN05', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC6E3D AS DateTime), CAST(85.000 AS Decimal(12, 3)), CAST(85.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC7E36 AS DateTime), CAST(0x0000B0E400AC7E36 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (29, N'ISBN05', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC6E3D AS DateTime), CAST(85.000 AS Decimal(12, 3)), CAST(85.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC7E37 AS DateTime), CAST(0x0000B0E400AC7E37 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (30, N'ISBN05', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC6E3D AS DateTime), CAST(85.000 AS Decimal(12, 3)), CAST(85.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC7E39 AS DateTime), CAST(0x0000B0E400AC7E39 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (31, N'ISBN05', N'P01', N'T05', N'Vietnamese (Vietnam)', CAST(0x0000B0E400AC6E3D AS DateTime), CAST(85.000 AS Decimal(12, 3)), CAST(85.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AC7E3A AS DateTime), CAST(0x0000B0E400AC7E3A AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (32, N'ISBN06', N'P02', N'T03', N'English', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACC769 AS DateTime), CAST(0x0000B0E400ACC769 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (33, N'ISBN06', N'P02', N'T03', N'English', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACC76A AS DateTime), CAST(0x0000B0E400ACC76A AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (34, N'ISBN06', N'P02', N'T03', N'English', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACC76E AS DateTime), CAST(0x0000B0E400ACC76E AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (35, N'ISBN06', N'P02', N'T03', N'English', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACC770 AS DateTime), CAST(0x0000B0E400ACC770 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (36, N'ISBN06', N'P02', N'T03', N'English', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACC771 AS DateTime), CAST(0x0000B0E400ACC771 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (37, N'ISBN06', N'P02', N'T03', N'English', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACC773 AS DateTime), CAST(0x0000B0E400ACC773 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (38, N'ISBN06', N'P02', N'T03', N'English', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACC777 AS DateTime), CAST(0x0000B0E400ACC777 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (39, N'ISBN06', N'P02', N'T03', N'English', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACC778 AS DateTime), CAST(0x0000B0E400ACC778 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (40, N'ISBN06', N'P02', N'T03', N'English', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACC77A AS DateTime), CAST(0x0000B0E400ACC77A AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (41, N'ISBN06', N'P02', N'T03', N'English', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACC77B AS DateTime), CAST(0x0000B0E400ACC77B AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (42, N'ISBN07', N'P01', N'T04', N'German', CAST(0x0000B0E400ACCA29 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACEDA1 AS DateTime), CAST(0x0000B0E400ACEDA1 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (43, N'ISBN07', N'P01', N'T04', N'German', CAST(0x0000B0E400ACCA29 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACEDA3 AS DateTime), CAST(0x0000B0E400ACEDA3 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (44, N'ISBN07', N'P01', N'T04', N'German', CAST(0x0000B0E400ACCA29 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACEDA5 AS DateTime), CAST(0x0000B0E400ACEDA5 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (45, N'ISBN07', N'P01', N'T04', N'German', CAST(0x0000B0E400ACCA29 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACEDA6 AS DateTime), CAST(0x0000B0E400ACEDA6 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (46, N'ISBN07', N'P01', N'T04', N'German', CAST(0x0000B0E400ACCA29 AS DateTime), CAST(150.000 AS Decimal(12, 3)), CAST(150.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ACEDA7 AS DateTime), CAST(0x0000B0E400ACEDA7 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (47, N'ISBN08', N'P01', N'T04', N'Breton', CAST(0x0000B0E400ACF12A AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD1000 AS DateTime), CAST(0x0000B0E400AD1000 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (48, N'ISBN08', N'P01', N'T04', N'Breton', CAST(0x0000B0E400ACF12A AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD1002 AS DateTime), CAST(0x0000B0E400AD1002 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (49, N'ISBN08', N'P01', N'T04', N'Breton', CAST(0x0000B0E400ACF12A AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD1003 AS DateTime), CAST(0x0000B0E400AD1003 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (50, N'ISBN08', N'P01', N'T04', N'Breton', CAST(0x0000B0E400ACF12A AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD1005 AS DateTime), CAST(0x0000B0E400AD1005 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (51, N'ISBN08', N'P01', N'T04', N'Breton', CAST(0x0000B0E400ACF12A AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD1006 AS DateTime), CAST(0x0000B0E400AD1006 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (52, N'ISBN09', N'P03', N'T05', N'Hausa', CAST(0x0000B0E400AD128D AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD3200 AS DateTime), CAST(0x0000B0E400AD3200 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (53, N'ISBN09', N'P03', N'T05', N'Hausa', CAST(0x0000B0E400AD128D AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD3201 AS DateTime), CAST(0x0000B0E400AD3201 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (54, N'ISBN09', N'P03', N'T05', N'Hausa', CAST(0x0000B0E400AD128D AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD3203 AS DateTime), CAST(0x0000B0E400AD3203 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (55, N'ISBN09', N'P03', N'T05', N'Hausa', CAST(0x0000B0E400AD128D AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD3204 AS DateTime), CAST(0x0000B0E400AD3204 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (56, N'ISBN09', N'P03', N'T05', N'Hausa', CAST(0x0000B0E400AD128D AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD3206 AS DateTime), CAST(0x0000B0E400AD3206 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (57, N'ISBN10', N'P02', N'T05', N'isiZulu', CAST(0x0000B0E400AD33F3 AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD4C1A AS DateTime), CAST(0x0000B0E400AD4C1A AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (58, N'ISBN10', N'P02', N'T05', N'isiZulu', CAST(0x0000B0E400AD33F3 AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD4C1B AS DateTime), CAST(0x0000B0E400AD4C1B AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (59, N'ISBN10', N'P02', N'T05', N'isiZulu', CAST(0x0000B0E400AD33F3 AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD4C1D AS DateTime), CAST(0x0000B0E400AD4C1D AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (60, N'ISBN10', N'P02', N'T05', N'isiZulu', CAST(0x0000B0E400AD33F3 AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD4C1E AS DateTime), CAST(0x0000B0E400AD4C1E AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (61, N'ISBN10', N'P02', N'T05', N'isiZulu', CAST(0x0000B0E400AD33F3 AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD4C22 AS DateTime), CAST(0x0000B0E400AD4C22 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (62, N'ISBN11', N'P03', N'T01', N'Spanish', CAST(0x0000B0E400AD4E8A AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD78B3 AS DateTime), CAST(0x0000B0E400AD78B3 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (63, N'ISBN11', N'P03', N'T01', N'Spanish', CAST(0x0000B0E400AD4E8A AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD78B6 AS DateTime), CAST(0x0000B0E400AD78B6 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (64, N'ISBN11', N'P03', N'T01', N'Spanish', CAST(0x0000B0E400AD4E8A AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD78B7 AS DateTime), CAST(0x0000B0E400AD78B7 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (65, N'ISBN11', N'P03', N'T01', N'Spanish', CAST(0x0000B0E400AD4E8A AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD78B8 AS DateTime), CAST(0x0000B0E400AD78B8 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (66, N'ISBN11', N'P03', N'T01', N'Spanish', CAST(0x0000B0E400AD4E8A AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD78BA AS DateTime), CAST(0x0000B0E400AD78BA AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (67, N'ISBN12', N'P02', N'T04', N'Langi', CAST(0x0000B0E400AD7B55 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD9183 AS DateTime), CAST(0x0000B0E400AD9183 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (68, N'ISBN12', N'P02', N'T04', N'Langi', CAST(0x0000B0E400AD7B55 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD9185 AS DateTime), CAST(0x0000B0E400AD9185 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (69, N'ISBN12', N'P02', N'T04', N'Langi', CAST(0x0000B0E400AD7B55 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD9186 AS DateTime), CAST(0x0000B0E400AD9186 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (70, N'ISBN12', N'P02', N'T04', N'Langi', CAST(0x0000B0E400AD7B55 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD9188 AS DateTime), CAST(0x0000B0E400AD9188 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (71, N'ISBN12', N'P02', N'T04', N'Langi', CAST(0x0000B0E400AD7B55 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400AD9189 AS DateTime), CAST(0x0000B0E400AD9189 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (72, N'ISBN13', N'P01', N'T03', N'Asu', CAST(0x0000B0E400AD93BE AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADB1F4 AS DateTime), CAST(0x0000B0E400ADB1F4 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (73, N'ISBN13', N'P01', N'T03', N'Asu', CAST(0x0000B0E400AD93BE AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADB1F7 AS DateTime), CAST(0x0000B0E400ADB1F7 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (74, N'ISBN13', N'P01', N'T03', N'Asu', CAST(0x0000B0E400AD93BE AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADB1F9 AS DateTime), CAST(0x0000B0E400ADB1F9 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (75, N'ISBN13', N'P01', N'T03', N'Asu', CAST(0x0000B0E400AD93BE AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADB1FA AS DateTime), CAST(0x0000B0E400ADB1FA AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (76, N'ISBN13', N'P01', N'T03', N'Asu', CAST(0x0000B0E400AD93BE AS DateTime), CAST(120.000 AS Decimal(12, 3)), CAST(120.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADB1FC AS DateTime), CAST(0x0000B0E400ADB1FC AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (77, N'ISBN14', N'P02', N'T05', N'Afar', CAST(0x0000B0E400ADB48B AS DateTime), CAST(90.000 AS Decimal(12, 3)), CAST(90.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADCE07 AS DateTime), CAST(0x0000B0E400ADCE07 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (78, N'ISBN14', N'P02', N'T05', N'Afar', CAST(0x0000B0E400ADB48B AS DateTime), CAST(90.000 AS Decimal(12, 3)), CAST(90.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADCE09 AS DateTime), CAST(0x0000B0E400ADCE09 AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (79, N'ISBN14', N'P02', N'T05', N'Afar', CAST(0x0000B0E400ADB48B AS DateTime), CAST(90.000 AS Decimal(12, 3)), CAST(90.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADCE0A AS DateTime), CAST(0x0000B0E400ADCE0A AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (80, N'ISBN14', N'P02', N'T05', N'Afar', CAST(0x0000B0E400ADB48B AS DateTime), CAST(90.000 AS Decimal(12, 3)), CAST(90.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADCE0C AS DateTime), CAST(0x0000B0E400ADCE0C AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (81, N'ISBN14', N'P02', N'T05', N'Afar', CAST(0x0000B0E400ADB48B AS DateTime), CAST(90.000 AS Decimal(12, 3)), CAST(90.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADCE0E AS DateTime), CAST(0x0000B0E400ADCE0E AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (82, N'ISBN15', N'P01', N'T04', N'Basaa', CAST(0x0000B0E400ADD028 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADE9EA AS DateTime), CAST(0x0000B0E400ADE9EA AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (83, N'ISBN15', N'P01', N'T04', N'Basaa', CAST(0x0000B0E400ADD028 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADE9EB AS DateTime), CAST(0x0000B0E400ADE9EB AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (84, N'ISBN15', N'P01', N'T04', N'Basaa', CAST(0x0000B0E400ADD028 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADE9ED AS DateTime), CAST(0x0000B0E400ADE9ED AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (85, N'ISBN15', N'P01', N'T04', N'Basaa', CAST(0x0000B0E400ADD028 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADE9EF AS DateTime), CAST(0x0000B0E400ADE9EF AS DateTime))
INSERT [dbo].[Book] ([Id], [ISBN], [IdPublisher], [IdTranslator], [Language], [PublishDate], [Price], [PriceCurrent], [Note], [IdBookStatus], [Status], [CreatedAt], [ModifiedAt]) VALUES (86, N'ISBN15', N'P01', N'T04', N'Basaa', CAST(0x0000B0E400ADD028 AS DateTime), CAST(100.000 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, N'BS1', 1, CAST(0x0000B0E400ADE9F3 AS DateTime), CAST(0x0000B0E400ADE9F3 AS DateTime))


-- -------------------------------
-- Table 11: structure for Parameter
-- -------------------------------
create table Parameter (
	[Id] varchar(10) not null primary key,
	[Name] nvarchar(100) not null,
	[Description] ntext not null default(''),
	[Value] varchar(100) not null,
	
	[Status] bit not null default(1),
	[CreatedAt] DateTime not null,
	[ModifiedAt] DateTime not null,
)

-- ----------------------------
-- Records of Parameter
-- ----------------------------
insert into [Parameter] ([Id], [Name], [Description], [Value], [Status], [CreatedAt], [ModifiedAt])
values
	('QD1', N'Trẻ em được người lớn bảo lãnh', N'Mỗi độc giả người lớn chỉ có thể bảo lãnh tối đa cho số trẻ em là', '2', 1, '2023-09-01 15:00', '2023-09-01 15:00'),
	('QD2', N'Số quyển sách cho phép mượn của người lớn', N'Một độc giả người lớn cùng 1 lúc chỉ được mượn tối đa ? quyển sách thuộc ? đầu sách khác nhau', '5', 1, '2023-09-01 15:00', '2023-09-01 15:00'),
	('QD3', N'Số quyển sách cho phép mượn của trẻ em', N'Một độc giả trẻ em cùng 1 lúc chỉ được mượn ? quyển sách', '1', 1, '2023-09-01 15:00', '2023-09-01 15:00'),
	('QD4', N'Số quyển sách đã mượn của người lớn', N'Nếu độc giả người lớn có bảo lãnh trẻ em thì số sách của trẻ em đang mượn sẽ được tính vào số lượng sách đang mượn của độc giả người lớn này', '', 1, '2023-09-01 15:00', '2023-09-01 15:00'),
	('QD5', N'Tình trạng quyển sách trong thư viện', N'Nếu độc giả mượn những đầu sách không còn trong thư viện thì hệ thống sẽ chuyển qua bảng dữ liệu đăng ký', '', 0, '2023-09-01 15:00', '2023-09-01 15:00'),
	('QD6', N'Quá trình mượn của 1 quyển sách', N'Nếu độc giả trả sách thì thông tin mượn sẽ chuyển sang quá trình mượn', '', 1, '2023-09-01 15:00', '2023-09-01 15:00'),
	('QD7', N'Quy định số tuổi tối thiểu của người lớn', N'', '18', 1, '2023-09-01 15:00', '2023-09-01 15:00'),
	('QD8', N'Thời hạn thẻ độc giả', N'', '12', 1, '2023-09-01 15:00', '2023-09-01 15:00'),
	('QD9', N'Số ngày mượn sách', N'', '7', 1, '2023-09-01 15:00', '2023-09-01 15:00'),
	('QD10', N'Số tiền phải trả khi mượn một cuốn sách', N'', '30.000', 1, '2023-09-01 15:00', '2023-09-01 15:00'),
	('QD11', N'Khi mượn sách, deposit được tính theo % giá hiện tại của cuốn sách', N'', '50%', 1, '2023-09-01 15:00', '2023-09-01 15:00'),
	('QD12', N'Phiếu mượn quá hạn', N'Nếu phiếu mượn quá hạn, thì không cho phép mượn sách', '', 1, '2023-12-18 20:20', '2023-12-18 20:20')
	
-- --------------------------------
-- Table 12: structure for Province
-- -------------------------------=
create table Province (
	[Id] int not null primary key,
	[Name] nvarchar(50) not null,
	
	[Status] bit not null,
)

-- ----------------------------
-- Records of Province
-- ----------------------------
INSERT INTO Province (Id, Name, Status)
VALUES 
	(1, N'Hà Nội', 1),
	(2, N'Hồ Chí Minh', 1),
	(3, N'Đà Nẵng', 1),
	(4, N'Hải Phòng', 1),
	(5, N'Cần Thơ', 1),
	(6, N'An Giang', 1),
	(7, N'Bà Rịa - Vũng Tàu', 1),
	(8, N'Bắc Giang', 1),
	(9, N'Bắc Kạn', 1),
	(10, N'Bạc Liêu', 1),
	(11, N'Bắc Ninh', 1),
	(12, N'Bến Tre', 1),
	(13, N'Bình Định', 1),
	(14, N'Bình Dương', 1),
	(15, N'Bình Phước', 1),
	(16, N'Bình Thuận', 1),
	(17, N'Cà Mau', 1),
	(18, N'Cao Bằng', 1),
	(19, N'Đắk Lắk', 1),
	(20, N'Đắk Nông', 1),
	(21, N'Điện Biên', 1),
	(22, N'Đồng Nai', 1),
	(23, N'Đồng Tháp', 1),
	(24, N'Gia Lai', 1),
	(25, N'Hà Giang', 1),
	(26, N'Hà Nam', 1),
	(27, N'Hà Tĩnh', 1),
	(28, N'Hải Dương', 1),
	(29, N'Hậu Giang', 1),
	(30, N'Hòa Bình', 1),
	(31, N'Hưng Yên', 1),
	(32, N'Khánh Hòa', 1),
	(33, N'Kiên Giang', 1),
	(34, N'Kon Tum', 1),
	(35, N'Lai Châu', 1),
	(36, N'Lâm Đồng', 1),
	(37, N'Lạng Sơn', 1),
	(38, N'Lào Cai', 1),
	(39, N'Long An', 1),
	(40, N'Nam Định', 1),
	(41, N'Nghệ An', 1),
	(42, N'Ninh Bình', 1),
	(43, N'Ninh Thuận', 1),
	(44, N'Phú Thọ', 1),
	(45, N'Quảng Bình', 1),
	(46, N'Quảng Nam', 1),
	(47, N'Quảng Ngãi', 1),
	(48, N'Quảng Ninh', 1),
	(49, N'Quảng Trị', 1),
	(50, N'Sóc Trăng', 1),
	(51, N'Sơn La', 1),
	(52, N'Tây Ninh', 1),
	(53, N'Thái Bình', 1),
	(54, N'Thái Nguyên', 1),
	(55, N'Thanh Hóa', 1),
	(56, N'Thừa Thiên Huế', 1),
	(57, N'Tiền Giang', 1),
	(58, N'Trà Vinh', 1),
	(59, N'Tuyên Quang', 1),
	(60, N'Vĩnh Long', 1),
	(61, N'Vĩnh Phúc', 1),
	(62, N'Yên Bái', 1),
	(63, N'Phú Yên', 1);
	
-- -------------------------------
-- Table 13: structure for User
-- -------------------------------
create table [User] (
	[Id] varchar(10) not null primary key,
	[Username] varchar(60) not null,
	[Password] varchar(60) not null,
	
	[Note] ntext null default(''),
	[Status] bit not null default(1),
	[CreatedAt] DateTime not null,
	[ModifiedAt] DateTime not null,
)

-- ----------------------------
-- Records of User
-- ----------------------------
insert into [User] ([Id], [Username], [Password], [Status], [CreatedAt], [ModifiedAt])
values
	('U1', 'nhatminh', 'nhatminh', 1, '2023-10-20 18:20', '2023-10-20 18:20'),
	('U2', 'hungvuong', 'hungvuong', 1, '2023-10-20 18:25', '2023-10-20 18:25'),
	('U3', 'anhquoc', 'anhquoc', 1, '2023-10-20 18:30', '2023-10-20 18:30')


-- -------------------------------
-- Table 14: structure for UserInfo
-- -------------------------------
create table [UserInfo] (
	[IdUser] varchar(10) not null primary key,
	[LName] nvarchar(60) not null,
	[FName] nvarchar(60) not null,
	[Phone] varchar(12) not null,
	[Address] nvarchar(100) not null,
	
	foreign key ([IdUser]) references [User]([Id]),
)

-- ----------------------------
-- Records of UserInfo
-- ----------------------------
insert into [UserInfo] ([IdUser], [LName], [FName], [Phone], [Address])
values
	('U1', N'Dương Nhật', N'Minh', '0935768249', N'Số 2, Lê Thắng Tôn'),
	('U2', N'Nghiêm Hùng', N'Vương', '0123222111', N'Số 10 Trần Quý Cáp'),
	('U3', N'Lê Anh', N'Quốc', '0901931765', N'Số 9 Lư Giang')

-- -------------------------------
-- Table 15: structure for Role
-- -------------------------------
create table [Role] (
	[Id] varchar(10) not null primary key,
	[Name] nvarchar(60) not null,
	[Group] nvarchar(60) not null,
	[Status] bit not null default(1),
)

-- ----------------------------
-- Records of Role
-- ----------------------------
insert into [Role] ([Id], [Name], [Group], [Status])
values
	('R1', N'admin', N'administration', 1),
	('R2', N'librarian 1', N'librarian', 1),
	('R3', N'librarian 2', N'librarian', 1)
	
-- -------------------------------
-- Table 16: structure for UserRole
-- -------------------------------
create table [UserRole] (
	[Id] varchar(10) not null primary key,
	[IdUser] varchar(10) not null,
	[IdRole] varchar(10) not null,
	
	foreign key ([IdUser]) references [User]([Id]),
	foreign key ([IdRole]) references [Role]([Id]),
)

-- ----------------------------
-- Records of UserRole
-- ----------------------------
insert into [UserRole] ([Id], [IdUser], [IdRole])
values
	('UR1', 'U1', 'R1'),
	('UR2', 'U2', 'R2'),
	('UR3', 'U3', 'R3')

-- -------------------------------
-- Table 17: structure for Function
-- -------------------------------
create table [Function] (
	[Id] varchar(10) not null primary key,
	[Name] nvarchar(60) not null,
	[Description] ntext not null,
	[IdParent] varchar(10),
	[UrlImage] text,
	[IsAdmin] bit not null default(1),
	[Status] bit not null default(1),
	
	foreign key ([IdParent]) references [Function]([Id]),
)

-- ----------------------------
-- Records of Function
-- ----------------------------
insert into [Function] ([Id], [Name], [Description], [IdParent], [UrlImage], [IsAdmin], [Status])
values
	('F1', N'User Management', '', NULL, '/Assets/Images/13.png', 1, 1),
	('F2', N'View user information', '', 'F1', '/Assets/Images/6.png', 1, 1),
	('F3', N'Add User', '', 'F1', '/Assets/Images/7.png', 1, 1),
	('F4', N'Update User', '', 'F1', '/Assets/Images/14.png', 1, 1),
	('F5', N'Delete User', '', 'F1', '/Assets/Images/8.png', 1, 1),
	('F6', N'Restore User', '', 'F1', '/Assets/Images/9.png', 1, 1),
	
	('F7', N'Feature Management', '', NULL, '/Assets/Images/18.png', 1, 1),
	('F8', N'View feature information', '', 'F7', '/Assets/Images/6.png', 1, 1),
	('F10', N'Delete feature', '', 'F7', '/Assets/Images/8.png', 1, 1),
	('F11', N'Restore feature', '', 'F7', '/Assets/Images/9.png', 1, 1),
	
	('F12', N'Role Management', '', NULL, '/Assets/Images/19.png', 1, 1),
	('F13', N'View role information', '', 'F12', '/Assets/Images/6.png', 1, 1),
	('F14', N'Add Role', '', 'F12', '/Assets/Images/7.png', 1, 1),
	('F18', N'Divide roles according to users', '', 'F12', '/Assets/Images/15.png', 1, 1),
	('F19', N'Divide roles according to features', '', 'F12', '/Assets/Images/16.png', 1, 1),
	('F20', N'Switch user roles', '', 'F12', '/Assets/Images/17.png', 1, 1),
	
	('F21', N'Reader Management', '', NULL, '/Assets/Images/3.png', 0, 1),
	('F22', N'View reader information', '', 'F21', '/Assets/Images/6.png', 0, 1),
	('F23', N'Add Reader', '', 'F21', '/Assets/Images/7.png', 0, 1),
	('F24', N'Delete reader', '', 'F21', '/Assets/Images/8.png', 0, 1),
	('F25', N'Restore reader', '', 'F21', '/Assets/Images/9.png', 0, 1),
	('F26', N'Search reader by identify number', '', 'F21', '/Assets/Images/10.png', 0, 1),
	('F27', N'Search reader by name', '', 'F21', '/Assets/Images/10.png', 0, 1),
	('F28', N'Transfer children to guardians', '', 'F21', '/Assets/Images/12.png', 0, 1),
	
	('F29', N'Book Title Management', '', NULL, '/Assets/Images/4.png', 0, 1),
	('F30', N'View Book Title Information', '', 'F29', '/Assets/Images/6.png', 0, 1),
	('F31', N'Add Book Title', '', 'F29', '/Assets/Images/7.png', 0, 1),
	
	('F32', N'Book ISBN Management', '', NULL, '/Assets/Images/4.png', 0, 1),
	('F33', N'View Book ISBN Information', '', 'F32', '/Assets/Images/6.png', 0, 1),
	('F34', N'Add Book ISBN', '', 'F32', '/Assets/Images/7.png', 0, 1),
	('F35', N'Update book ISBN status (Automatically)', '', 'F32', '/Assets/Images/10.png', 0, 1),
	
	('F36', N'Book Management', '', NULL, '/Assets/Images/4.png', 0, 1),
	('F37', N'View Book Information', '', 'F36', '/Assets/Images/6.png', 0, 1),
	('F38', N'Add Book', '', 'F36', '/Assets/Images/7.png', 0, 1),
	('F39', N'Search book by ISBN', '', 'F36', '/Assets/Images/10.png', 0, 1),
	('F40', N'Search book by name', '', 'F36', '/Assets/Images/10.png', 0, 1),
	
	('F41', N'Loan Slip Management', '', NULL, '/Assets/Images/10.png', 0, 1),
	('F42', N'Create new loan slip', '', 'F41', '/Assets/Images/10.png', 0, 1),
	
	('F43', N'Loan History Management', '', NULL, '/Assets/Images/10.png', 0, 1),
	('F44', N'Create new loan history slip', '', 'F43', '/Assets/Images/10.png', 0, 1),
	
	('F45', N'Category Management', '', NULL, '/Assets/Images/10.png', 1, 1),
	('F46', N'Add Category', '', 'F45', '/Assets/Images/10.png', 1, 1),
	('F47', N'Delete Category', '', 'F45', '/Assets/Images/10.png', 1, 1),
	('F48', N'Restore Category', '', 'F45', '/Assets/Images/10.png', 1, 1),
	('F49', N'Update Category', '', 'F45', '/Assets/Images/10.png', 1, 1),
	
	('F50', N'Publisher Management', '', NULL, '/Assets/Images/10.png', 1, 1),
	('F51', N'Add Publisher', '', 'F50', '/Assets/Images/10.png', 1, 1),
	('F52', N'Delete Publisher', '', 'F50', '/Assets/Images/10.png', 1, 1),
	('F53', N'Update Publisher', '', 'F50', '/Assets/Images/10.png', 1, 1),
	
	('F54', N'Author Management', '', NULL, '/Assets/Images/10.png', 1, 1),
	('F55', N'Add Author', '', 'F54', '/Assets/Images/10.png', 1, 1),
	('F56', N'Delete Author', '', 'F54', '/Assets/Images/10.png', 1, 1),
	('F57', N'Restore Author', '', 'F54', '/Assets/Images/10.png', 1, 1),
	('F58', N'Update Author', '', 'F54', '/Assets/Images/10.png', 1, 1),
	
	('F59', N'Translator Management', '', NULL, '/Assets/Images/10.png', 1, 1),
	('F60', N'Add Translator', '', 'F59', '/Assets/Images/10.png', 1, 1),
	('F61', N'Delete Translator', '', 'F59', '/Assets/Images/10.png', 1, 1),
	('F62', N'Restore Translator', '', 'F59', '/Assets/Images/10.png', 1, 1),
	('F63', N'Update Translator', '', 'F59', '/Assets/Images/10.png', 1, 1),
	
	('F64', N'Province Management', '', NULL, '/Assets/Images/10.png', 1, 1),
	('F65', N'Add Province', '', 'F64', '/Assets/Images/10.png', 1, 1),
	('F66', N'Delete Province', '', 'F64', '/Assets/Images/10.png', 1, 1),
	('F67', N'Restore Province', '', 'F64', '/Assets/Images/10.png', 1, 1),
	('F68', N'Update Province', '', 'F64', '/Assets/Images/10.png', 1, 1),
	
	('F69', N'Reason Management', '', NULL, '/Assets/Images/10.png', 1, 1),
	('F70', N'Add Penalty Reason', '', 'F69', '/Assets/Images/10.png', 1, 1),
	('F71', N'Delete Penalty Reason', '', 'F69', '/Assets/Images/10.png', 1, 1),
	('F72', N'Update Penalty Reason', '', 'F69', '/Assets/Images/10.png', 1, 1),
	
	('F73', N'Parameter Management', '', NULL, '/Assets/Images/10.png', 1, 1),
	('F74', N'Add Parameter', '', 'F73', '/Assets/Images/10.png', 1, 1),
	('F75', N'Delete Parameter', '', 'F73', '/Assets/Images/10.png', 1, 1),
	('F76', N'Restore Parameter', '', 'F73', '/Assets/Images/10.png', 1, 1),
	('F77', N'Update Parameter', '', 'F73', '/Assets/Images/10.png', 1, 1),

	('F78', N'Book Status Management', '', NULL, '/Assets/Images/10.png', 1, 1),
	('F79', N'Add Book Status', '', 'F78', '/Assets/Images/10.png', 1, 1),
	('F80', N'Delete Book Status', '', 'F78', '/Assets/Images/10.png', 1, 1),
	('F81', N'Restore Book Status', '', 'F78', '/Assets/Images/10.png', 1, 1),
	('F82', N'Update Book Status', '', 'F78', '/Assets/Images/10.png', 1, 1),

	('F83', N'Statistical', '', NULL, '/Assets/Images/10.png', 1, 1)
	

-- -------------------------------
-- Table 18: structure for RoleFunction
-- -------------------------------
create table [RoleFunction] (
	[Id] varchar(10) not null primary key,
	[IdRole] varchar(10) not null,
	[IdFunction] varchar(10) not null,
	
	foreign key ([IdRole]) references [Role]([Id]),
	foreign key ([IdFunction]) references [Function]([Id]),
)

-- ----------------------------
-- Records of RoleFunction
-- ----------------------------
insert into [RoleFunction] ([Id], [IdRole], [IdFunction])
values
	('RF1', 'R1', 'F1'),
	('RF2', 'R1', 'F2'),
	('RF3', 'R1', 'F3'),
	('RF4', 'R1', 'F4'),
	('RF5', 'R1', 'F5'),
	('RF6', 'R1', 'F6'),
	('RF7', 'R1', 'F7'),
	('RF8', 'R1', 'F8'),
	('RF10', 'R1', 'F10'),
	('RF11', 'R1', 'F11'),
	('RF12', 'R1', 'F12'),
	('RF13', 'R1', 'F13'),
	('RF14', 'R1', 'F14'),
	('RF18', 'R1', 'F18'),
	('RF19', 'R1', 'F19'),
	('RF20', 'R1', 'F20'),

	('RF78', 'R1', 'F78'),
	('RF79', 'R1', 'F82'),
	('RF80', 'R1', 'F83'),

	('RF21', 'R2', 'F21'),
	('RF22', 'R2', 'F22'),
	('RF23', 'R2', 'F23'),
	('RF24', 'R2', 'F24'),
	('RF25', 'R2', 'F25'),
	('RF26', 'R2', 'F26'),
	('RF27', 'R2', 'F27'),
	('RF28', 'R2', 'F28'),
	('RF29', 'R2', 'F29'),
	('RF30', 'R2', 'F30'),
	('RF31', 'R2', 'F31'),
	('RF32', 'R2', 'F32'),
	('RF33', 'R2', 'F33'),
	('RF34', 'R2', 'F34'),
	('RF35', 'R2', 'F35'),
	('RF36', 'R2', 'F36'),
	('RF37', 'R2', 'F37'),
	('RF38', 'R2', 'F38'),
	('RF39', 'R2', 'F39'),
	('RF40', 'R2', 'F40'),
	
	('RF41', 'R2', 'F41'),
	('RF42', 'R2', 'F42'),
	('RF43', 'R2', 'F43'),
	('RF44', 'R2', 'F44'),
	
	('RF45', 'R1', 'F45'),
	('RF46', 'R1', 'F46'),
	('RF47', 'R1', 'F47'),
	('RF48', 'R1', 'F48'),
	('RF49', 'R1', 'F49'),
	
	('RF50', 'R1', 'F50'),
	('RF51', 'R1', 'F51'),
	('RF52', 'R1', 'F52'),
	('RF53', 'R1', 'F53'),
	
	('RF54', 'R1', 'F54'),
	('RF55', 'R1', 'F55'),
	('RF56', 'R1', 'F56'),
	('RF57', 'R1', 'F57'),
	('RF58', 'R1', 'F58'),
	
	('RF59', 'R1', 'F59'),
	('RF60', 'R1', 'F60'),
	('RF61', 'R1', 'F61'),
	('RF62', 'R1', 'F62'),
	('RF63', 'R1', 'F63'),
	
	('RF64', 'R1', 'F64'),
	('RF65', 'R1', 'F65'),
	('RF66', 'R1', 'F66'),
	('RF67', 'R1', 'F67'),
	('RF68', 'R1', 'F68'),
	
	('RF69', 'R1', 'F69'),
	('RF70', 'R1', 'F70'),
	('RF71', 'R1', 'F71'),
	('RF72', 'R1', 'F72'),
	
	('RF73', 'R1', 'F73'),
	('RF74', 'R1', 'F74'),
	('RF75', 'R1', 'F75'),
	('RF76', 'R1', 'F76'),
	('RF77', 'R1', 'F77')

-- -------------------------------
-- Table 19: structure for LoanSlip
-- -------------------------------
create table LoanSlip (
	[Id] varchar(10) not null primary key,
	[IdUser] varchar(10) not null,
	[IdReader] varchar(10) not null,
	[Quantity] int not null,
	[LoanDate] DateTime not null,	
	[ExpDate] DateTime not null,
	[LoanPaid] decimal(12,3) not null,
	[Deposit] decimal(12,3) not null,
	
	foreign key ([IdReader]) references [Reader]([Id]),
	foreign key ([IdUser]) references [User]([Id])
)

-- -------------------------------
-- Table 20: structure for LoanDetail
-- -------------------------------
create table LoanDetail (
	[Id] varchar(10) not null primary key,
	[IdLoan] varchar(10) not null,
	[IdBook] int not null,
	[ExpDate] DateTime not null,
	
	foreign key ([IdLoan]) references [LoanSlip]([Id]),
	foreign key ([IdBook]) references [Book]([Id])
)

-- -------------------------------
-- Table 21: structure for LoanHistory
-- -------------------------------
create table LoanHistory (
	[Id] varchar(10) not null primary key,
	[IdUser] varchar(10) not null,
	[IdReader] varchar(10) not null,
	[Quantity] int not null,
	[LoanDate] DateTime not null,
	[ExpDate] DateTime not null,
	
	[LoanPaid] decimal(12,3) not null,
	[Deposit] decimal(12,3) not null,
	[FineMoney] decimal(12,3) not null, -- tiền phạt tổng
	[Total] decimal(12,3) not null,
	
	[Note] nvarchar(50),				-- ghi chú
	[CreateAt] DateTime not null,		-- Ngày độc giả trả sách
	
	foreign key ([IdReader]) references [Reader]([Id]),
	foreign key ([IdUser]) references [User]([Id])
)

-- -------------------------------
-- Table 22: structure for LoanDetailHistory
-- -------------------------------
create table LoanDetailHistory (
	[Id] varchar(10) not null primary key,
	[IdLoanHistory] varchar(10) not null,
	[IdBook] int not null,
	[ExpDate] DateTime not null,
	[PaidMoney] decimal(12,3) not null,	-- Tiền phạt
	[Note] nvarchar(50),				-- Ghi chú: Tình trạng sách
	
	foreign key ([IdLoanHistory]) references [LoanHistory]([Id]),
	foreign key ([IdBook]) references [Book]([Id])
)

-- -------------------------------
-- Table 23: structure for PenaltyReason
-- -------------------------------
create table PenaltyReason (
	[Id] varchar(10) not null primary key,
	[Name] nvarchar(100) not null,
	[Description] ntext not null default(''),
	[Price] decimal(12,3) not null,
	
	[CreatedAt] DateTime not null,
	[ModifiedAt] DateTime not null,
)

-- ----------------------------
-- Records of PenaltyReason
-- ----------------------------
insert into [PenaltyReason] ([Id], [Name], [Description], [Price], [CreatedAt], [ModifiedAt])
values
	('PR1', N'None', N'', 0.000, '2023-09-01 15:00', '2023-09-01 15:00'),
	('PR2', N'Lost the book', N'Khi độc giả làm mất sách, độc giả phải trả đúng số tiền hiện tại của cuốn sách đó và thủ thư sẽ lấy luôn Deposit của cuốn sách đó', 0.000, '2023-09-01 15:00', '2023-09-01 15:00'),
	('PR3', N'Spoil the book', N'Khi độc giả làm hư cuốn sách, thủ thư sẽ phạt số tiền tùy theo mức độ hư hại của cuốn sách, ngoài ra thủ thư có thể gửi thông báo đến admin để cập nhập lại giá tiền của cuốn sách', 0.000, '2023-09-01 15:00', '2023-09-01 15:00')



	
-- -------------------------------
-- Table 23: structure for Statistical
-- -------------------------------
create table Statistical (
	[DateTime] DateTime not null primary key,
	[Data] varchar(100) not null,
	[Description] ntext not null default(''),
)

-- ----------------------------
-- Records of Statistical
-- ----------------------------
insert into [Statistical] ([DateTime], [Data], [Description])
values
	('2024-03-25 12:00', '10|15|18', N''),
	('2024-03-26 12:00', '11|14|18', N''),
	('2024-03-27 12:00', '16|14|24', N''),
	('2024-03-28 12:00', '15|21|26', N''),
	('2024-03-29 12:00', '18|22|31', N''),
	('2024-03-30 12:00', '15|21|24', N'')