using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Qx.Workflow.Processor.Migrations
{
    /// <inheritdoc />
    public partial class r0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleter = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deletion_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleter = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deletion_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workflow_instances",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transaction_no = table.Column<string>(type: "text", nullable: false),
                    workflow_id = table.Column<long>(type: "bigint", nullable: false),
                    workflow_title = table.Column<string>(type: "text", nullable: false),
                    workflow = table.Column<string>(type: "text", nullable: false),
                    current_node = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    process_end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    promoter_id = table.Column<long>(type: "bigint", nullable: false),
                    promoter_title = table.Column<string>(type: "text", nullable: false),
                    promoter_dept_id = table.Column<long>(type: "bigint", nullable: false),
                    promoter_dept_title = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<long>(type: "bigint", nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workflow_instances", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workflows",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    catalog = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<long>(type: "bigint", nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleter = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deletion_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workflows", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "depts",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    manager_id = table.Column<long>(type: "bigint", nullable: true),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleter = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deletion_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_depts", x => x.id);
                    table.ForeignKey(
                        name: "fk_depts_depts_parent_id",
                        column: x => x.parent_id,
                        principalSchema: "dbo",
                        principalTable: "depts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_depts_users_manager_id",
                        column: x => x.manager_id,
                        principalSchema: "dbo",
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role_user",
                schema: "dbo",
                columns: table => new
                {
                    members_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_user", x => new { x.members_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_role_user_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "dbo",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_user_users_members_id",
                        column: x => x.members_id,
                        principalSchema: "dbo",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "approval_logs",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workflow_instance_id = table.Column<long>(type: "bigint", nullable: false),
                    node_id = table.Column<long>(type: "bigint", nullable: false),
                    node_title = table.Column<string>(type: "text", nullable: false),
                    operator_id = table.Column<long>(type: "bigint", nullable: false),
                    operator_title = table.Column<string>(type: "text", nullable: false),
                    action = table.Column<int>(type: "integer", nullable: true),
                    operator_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    remark = table.Column<string>(type: "text", nullable: true),
                    extra_data = table.Column<string>(type: "text", nullable: true),
                    is_todo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approval_logs", x => x.id);
                    table.ForeignKey(
                        name: "fk_approval_logs_workflow_instances_workflow_instance_id",
                        column: x => x.workflow_instance_id,
                        principalSchema: "dbo",
                        principalTable: "workflow_instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflow_nodes",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workflow_id = table.Column<long>(type: "bigint", nullable: false),
                    flag = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    people = table.Column<string>(type: "text", nullable: true),
                    sign_method = table.Column<int>(type: "integer", nullable: false),
                    pass_rate = table.Column<float>(type: "real", nullable: false),
                    next_step = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workflow_nodes", x => x.id);
                    table.ForeignKey(
                        name: "fk_workflow_nodes_workflows_workflow_id",
                        column: x => x.workflow_id,
                        principalSchema: "dbo",
                        principalTable: "workflows",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dept_user",
                schema: "dbo",
                columns: table => new
                {
                    dept_id = table.Column<long>(type: "bigint", nullable: false),
                    members_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dept_user", x => new { x.dept_id, x.members_id });
                    table.ForeignKey(
                        name: "fk_dept_user_depts_dept_id",
                        column: x => x.dept_id,
                        principalSchema: "dbo",
                        principalTable: "depts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_dept_user_users_members_id",
                        column: x => x.members_id,
                        principalSchema: "dbo",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_approval_logs_workflow_instance_id",
                schema: "dbo",
                table: "approval_logs",
                column: "workflow_instance_id");

            migrationBuilder.CreateIndex(
                name: "ix_dept_user_members_id",
                schema: "dbo",
                table: "dept_user",
                column: "members_id");

            migrationBuilder.CreateIndex(
                name: "ix_depts_manager_id",
                schema: "dbo",
                table: "depts",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "ix_depts_parent_id",
                schema: "dbo",
                table: "depts",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_user_role_id",
                schema: "dbo",
                table: "role_user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_workflow_nodes_workflow_id",
                schema: "dbo",
                table: "workflow_nodes",
                column: "workflow_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approval_logs",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "dept_user",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "role_user",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "workflow_nodes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "workflow_instances",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "depts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "workflows",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "users",
                schema: "dbo");
        }
    }
}
