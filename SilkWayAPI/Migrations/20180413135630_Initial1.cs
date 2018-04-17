using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SilkwayAPI.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightList",
                columns: table => new
                {
                    Flightid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Uid = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Opsstatus = table.Column<string>(nullable: true),
                    Call_sign_number = table.Column<string>(nullable: true),
                    Call_sign_code = table.Column<string>(nullable: true),
                    Aircraft_reg = table.Column<string>(nullable: true),
                    Aircraft_type = table.Column<string>(nullable: true),
                    Std = table.Column<DateTime>(nullable: true),
                    Sta = table.Column<DateTime>(nullable: true),
                    Est_blocktime = table.Column<DateTime>(nullable: true),
                    Est_takeofftime = table.Column<DateTime>(nullable: true),
                    Est_touchdowntime = table.Column<DateTime>(nullable: true),
                    Est_blockintime = table.Column<DateTime>(nullable: true),
                    Mvt_blocktime = table.Column<DateTime>(nullable: true),
                    Mvt_takeofftime = table.Column<DateTime>(nullable: true),
                    Mvt_touchdowntime = table.Column<DateTime>(nullable: true),
                    Mvt_blockintime = table.Column<DateTime>(nullable: true),
                    Acars_est_blockintime = table.Column<DateTime>(nullable: true),
                    Acars_blocktime = table.Column<DateTime>(nullable: true),
                    Acars_takeofftime = table.Column<DateTime>(nullable: true),
                    Acars_touchdowntime = table.Column<DateTime>(nullable: true),
                    Acars_blockintime = table.Column<DateTime>(nullable: true),
                    Revised_departure = table.Column<DateTime>(nullable: true),
                    Revised_arrival = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    Apt_dep = table.Column<string>(nullable: true),
                    Apt_arr_planned = table.Column<string>(nullable: true),
                    Apt_arr_actual = table.Column<string>(nullable: true),
                    Fuel = table.Column<string>(nullable: true),
                    Crew_compo = table.Column<string>(nullable: true),
                    Delays = table.Column<string>(nullable: true),
                    Modified_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightList", x => x.Flightid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightList");
        }
    }
}
