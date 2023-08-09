use master
go

drop database if exists PlantNestDB
create database PlantNestDB
go

use PlantNestDB
go

drop table if exists account
create table account 
(
	id int primary key identity,
	username varchar(250),
	"password" varchar(250),
	email varchar(250),
	phone varchar(250),
	"address" varchar(max),
	account_image varchar(max),
	role_id int,
	created date default getdate(),
	dob date,
	"status" bit
)
go

ALTER TABLE account
ADD fullname varchar(250);


drop table if exists "role"
create table "role"
(
	id int primary key identity,
	roleName varchar(250),
	"description" varchar(max),
	created date default getdate(),
)
go

drop table if exists "contact"
create table "contact" 
(
	id int primary key identity,
	"name" varchar(250),
	email varchar(250),
	"subject" varchar(250),
	"message" varchar(max),
	created date default getdate()
)
go

drop table if exists category
create table category
(
	id int identity primary key,
	category_id int null,
	category_image varchar(max),
	category_name varchar(250),
    created date default getdate(),
	"status" bit
)
go

drop table if exists orderDetail
create table orderDetail
(
	order_id int,
	product_id int,
	quantity int,
	comment_id int,
	total_price decimal(10),
	created date default getdate(),
	primary key(order_id, product_id)
)
go

drop table if exists "order"
create table "order"
(
	id int primary key identity,
	account_id int,
	payment_method varchar(250),
	total_order decimal(10),
	orderDate date default getdate(),
	orderTime time default getdate(),
	"status" varchar(250)
)
go


drop table if exists comment
create table comment (
	id int primary key identity,
	content varchar(max),
	rating int,
	created date default getdate(),
	account_id int,
	product_id int,
)
go

drop table if exists product
create table product 
(
	id int primary key identity,
	product_name varchar(250),
	regis_id varchar(max),
	"description" varchar(max),
	cost_price decimal(10), --giá nhập
	sell_price decimal(10), -- giá bán
	import_quantity int, -- số lượng nhập
	current_quantity int, -- số lượng hiện tại
	import_date date default getdate(), -- ngày nhập, tự động lấy ngày hiện tại
	expiration_date date, -- ngày hết hạn của sản phẩm
	supplier_id int, -- id của nhà cung cấp
	category_id int, -- id loại hàng
	"status" bit
)
go

drop table if exists cart
create table cart
(
	id int primary key identity,
	account_id int,
	regis_id varchar(max),
	quantity int,
)
go

drop table if exists favoriteCart
create table favoriteCart
(
	id int primary key identity,
	account_id int,
	regis_id varchar(max),
)
go

drop table if exists delivery
create table delivery 
(
	id int primary key identity,
	order_id int,
	delivery_date date default getdate(), -- ngày giao hàng, set default là ngày hiện tại
	receiving_date date, --ngày nhận hàng
	recipient_name varchar(250),
	recipient_address varchar(max),
	recipient_phone varchar(250),
	"message" varchar(max),
	"status" varchar(255)
)
go

drop table if exists supplier
create table supplier
(
	id int primary key identity,
	supplier_name varchar(255),
	"status" bit
)
go

drop table if exists "image"
create table "image"
(
	id int PRIMARY KEY identity,
	imageUrl varchar(max),
	product_id int
)
go


-- Foreign Key - Relationship

-- ACCOUNT => CART
alter table cart
add constraint fk_acccount_cart
foreign key (account_id) references account(id)

-- ACCOUNT => CART
alter table favoriteCart
add constraint fk_acccount_favoriteCart
foreign key (account_id) references account(id)

-- COMMENTS => PRODUCT
alter table comment
add constraint fk_commnents_product
foreign key (product_id) references product(id)

-- ACCOUNT => ROLE
alter table account
add constraint fk_account_role
foreign key (role_id) references "Role"(id)

-- ACCOUNT => ORDER
alter table "order"
add constraint fk_account_order
foreign key (account_id) references account(id)

-- ORDER -> DELIVERY
alter table delivery
add constraint fk_order_delivery
foreign key (order_id) references "order"(id)

-- product -> orderDetail
alter table orderDetail
add constraint fk_product_orderDetails
foreign key (product_id) references product(id)

-- "order" -> orderDetail
alter table orderDetail
add constraint fk_order_orderDetails
foreign key (order_id) references "order"(id)

-- product -> image
alter table "image"
add constraint fk_product_image
foreign key (product_id) references product(id)

-- category -> product
alter table product
add constraint fk_category_product
foreign key (category_id) references category(id)

-- category -> category
alter table category
add constraint fk_category_category
foreign key (category_id) references category(id)

-- supplier -> product
alter table product
add constraint fk_supplier_product
foreign key (supplier_id) references supplier(id)

-- INSERT DATA INTO TABLE
-- 1. ROLE TABLE
insert into "role" (roleName, "description") values ('Super Admin', 'This role use for Super Admins account')
insert into "role" (roleName, "description") values ('Admin', 'This role use for Admins account')
insert into "role" (roleName, "description") values ('Manager', 'This role use for Managers account')
insert into "role" (roleName, "description") values ('Client', 'This role use for Client account')

-- 3. SUPPLIER

insert into supplier (supplier_name, "status") values ( 'Costa Farms', 1) -- cây trồng
insert into supplier (supplier_name, "status") values ( 'The Sill', 1) -- cây trồng
insert into supplier (supplier_name, "status") values ( 'Planterina', 1) -- cây trồng

insert into supplier (supplier_name, "status") values ( 'Yara', 1) -- phân bón
insert into supplier (supplier_name, "status") values ( 'Nutrien', 1) -- phân bón
insert into supplier (supplier_name, "status") values ( 'The Mosaic Company', 1) -- phân bón

insert into supplier (supplier_name, "status") values ( 'John Deere', 1) -- nông cụ
insert into supplier (supplier_name, "status") values ( 'CNH Industrial', 1) -- nông cụ
insert into supplier (supplier_name, "status") values ( 'AGCO Corporation', 1) -- nông cụ

-- 4. CATEGORIES
insert into category (category_image, category_name, "status" ) values ( null, 'Houseplants', 1)
insert into category (category_image, category_name, "status" ) values ( null, 'Outdoor Plants', 1)
insert into category (category_image, category_name, "status" ) values ( null, 'Farm Tools', 1)
insert into category (category_image, category_name, "status" ) values ( null, 'Plant Pots', 1)
insert into category (category_image, category_name, "status" ) values ( null, 'Fertilizers', 1)



insert into category (category_id, category_image, category_name, "status" ) values ( 1, null, 'Desk Plants', 1)
insert into category (category_id, category_image, category_name, "status" ) values ( 1, null, 'Hanging Plants', 1)

insert into category (category_id, category_image, category_name, "status" ) values ( 3, null, 'Scissors', 1)
insert into category (category_id, category_image, category_name, "status" ) values ( 3, null, 'Plant spade', 1)
insert into category (category_id, category_image, category_name, "status" ) values ( 3, null, 'Watering Can', 1)

insert into category (category_id, category_image, category_name, "status" ) values ( 5, null, 'Potting Soil', 1)
insert into category (category_id, category_image, category_name, "status" ) values ( 5, null, 'Pesticide', 1)

-- 5. PRODUCT
insert into product ( product_name, "description", cost_price, sell_price, import_quantity, current_quantity, expiration_date, supplier_id, category_id, "status" )
values ( 'Product 1', 'nothing to show', 1000.5, 2000.3, 80, 50, '2025-08-09', 1, 1, 1)
insert into product ( product_name, "description", cost_price, sell_price, import_quantity, current_quantity, expiration_date, supplier_id, category_id, "status" )
values ( 'Product 2', 'nothing to show 2', 500, 1000.3, 20, 15, '2025-08-10', 1, 1, 1)

-- 6. ACCOUNT
insert into account ( username, "password", email, phone, "address", account_image, role_id, dob, "status")
values ( 'superadmin', 'superadmin123', 'superadmin@gmail.com', '', '', '', 1, '2003-02-05', 1)

insert into account ( username, "password", email, phone, "address", account_image, role_id, dob, "status")
values ( 'admin', 'admin123', 'admin@gmail.com', '', '', '', 1, '2003-02-05', 1)

insert into account ( username, "password", email, phone, "address", account_image, role_id, dob, "status")
values ( 'manager', 'manager123', 'manager@gmail.com', '', '', '', 1, '2003-02-05', 1)

insert into account ( username, "password", email, phone, "address", account_image, role_id, dob, "status")
values ( 'client', 'client123', 'client@gmail.com', '', '', '', 1, '2003-02-05', 1)