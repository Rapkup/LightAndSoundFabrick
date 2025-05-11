using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemataryFabrick.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderStateEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:order_state", "stock,denied,approved_by_manager,proccessed_by_tech_lead,done")
                .Annotation("Npgsql:Enum:order_type", "rent,individual")
                .Annotation("Npgsql:Enum:payment_status", "unpaid,payment_confirmation,paid")
                .Annotation("Npgsql:Enum:product_state", "available,in_repair,out_of_stock")
                .Annotation("Npgsql:Enum:user_type", "guest,individual_customer,legal_customer,director,order_manager,worker,tech_order_lead")
                .Annotation("Npgsql:Enum:work_task_state", "in_progress,completed,assigned,not_assigned")
                .OldAnnotation("Npgsql:Enum:order_type", "rent,individual")
                .OldAnnotation("Npgsql:Enum:payment_status", "unpaid,payment_confirmation,paid")
                .OldAnnotation("Npgsql:Enum:product_state", "available,in_repair,out_of_stock")
                .OldAnnotation("Npgsql:Enum:user_type", "guest,individual_customer,legal_customer,director,order_manager,worker,tech_order_lead")
                .OldAnnotation("Npgsql:Enum:work_task_state", "in_progress,completed,assigned,not_assigned");

            migrationBuilder.AddColumn<int>(
                name: "OrderState",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "Orders");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:order_type", "rent,individual")
                .Annotation("Npgsql:Enum:payment_status", "unpaid,payment_confirmation,paid")
                .Annotation("Npgsql:Enum:product_state", "available,in_repair,out_of_stock")
                .Annotation("Npgsql:Enum:user_type", "guest,individual_customer,legal_customer,director,order_manager,worker,tech_order_lead")
                .Annotation("Npgsql:Enum:work_task_state", "in_progress,completed,assigned,not_assigned")
                .OldAnnotation("Npgsql:Enum:order_state", "stock,denied,approved_by_manager,proccessed_by_tech_lead,done")
                .OldAnnotation("Npgsql:Enum:order_type", "rent,individual")
                .OldAnnotation("Npgsql:Enum:payment_status", "unpaid,payment_confirmation,paid")
                .OldAnnotation("Npgsql:Enum:product_state", "available,in_repair,out_of_stock")
                .OldAnnotation("Npgsql:Enum:user_type", "guest,individual_customer,legal_customer,director,order_manager,worker,tech_order_lead")
                .OldAnnotation("Npgsql:Enum:work_task_state", "in_progress,completed,assigned,not_assigned");
        }
    }
}
