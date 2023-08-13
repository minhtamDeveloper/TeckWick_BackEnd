using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantNestBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class newdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_id = table.Column<int>(type: "int", nullable: true),
                    category_image = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    category_name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    created = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__category__3213E83F01300EFA", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_category",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "contact",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    subject = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    created = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__contact__3213E83F8D06CF42", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    created = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__role__3213E83F69825B13", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    supplier_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__supplier__3213E83FED92903D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullname = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    username = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    password = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    phone = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    account_image = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: true),
                    created = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    dob = table.Column<DateTime>(type: "date", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    sercurityCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__account__3213E83F00B0DDE1", x => x.id);
                    table.ForeignKey(
                        name: "fk_account_role",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    cost_price = table.Column<decimal>(type: "decimal(10,0)", nullable: true),
                    sell_price = table.Column<decimal>(type: "decimal(10,0)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    supplier_id = table.Column<int>(type: "int", nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__product__3213E83FF9D86EBF", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_product",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_supplier_product",
                        column: x => x.supplier_id,
                        principalTable: "supplier",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cart__3213E83FB7A884B6", x => x.id);
                    table.ForeignKey(
                        name: "fk_acccount_cart",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "favoriteCart",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__favorite__3213E83FFFF41E57", x => x.id);
                    table.ForeignKey(
                        name: "fk_acccount_favoriteCart",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_id = table.Column<int>(type: "int", nullable: true),
                    payment_method = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    total_order = table.Column<decimal>(type: "decimal(10,0)", nullable: true),
                    orderDate = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    orderTime = table.Column<TimeSpan>(type: "time", nullable: true, defaultValueSql: "(getdate())"),
                    status = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order__3213E83FCA01C699", x => x.id);
                    table.ForeignKey(
                        name: "fk_account_order",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    created = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    account_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__comment__3213E83F25ABCA18", x => x.id);
                    table.ForeignKey(
                        name: "fk_commnents_product",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imageUrl = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__image__3213E83F6EDF4317", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_image",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "delivery",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    delivery_date = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    receiving_date = table.Column<DateTime>(type: "date", nullable: true),
                    recipient_name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    recipient_address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    recipient_phone = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__delivery__3213E83F79CF5C21", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_delivery",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "orderDetail",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    comment_id = table.Column<int>(type: "int", nullable: true),
                    total_price = table.Column<decimal>(type: "decimal(10,0)", nullable: true),
                    created = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orderDet__022945F64976148A", x => new { x.order_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_order_orderDetails",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_product_orderDetails",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_role_id",
                table: "account",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_account_id",
                table: "cart",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_category_category_id",
                table: "category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_product_id",
                table: "comment",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_id",
                table: "delivery",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_favoriteCart_account_id",
                table: "favoriteCart",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_image_product_id",
                table: "image",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_account_id",
                table: "order",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_orderDetail_product_id",
                table: "orderDetail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_supplier_id",
                table: "product",
                column: "supplier_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "contact");

            migrationBuilder.DropTable(
                name: "delivery");

            migrationBuilder.DropTable(
                name: "favoriteCart");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "orderDetail");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
