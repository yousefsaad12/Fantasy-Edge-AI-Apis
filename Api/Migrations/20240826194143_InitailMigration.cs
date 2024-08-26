using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitailMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElementTypes",
                columns: table => new
                {
                    ElementTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementTypes", x => x.ElementTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    PulseID = table.Column<int>(type: "int", nullable: false),
                    TeamDivision = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    WebName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    SquadNumber = table.Column<int>(type: "int", nullable: true),
                    News = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    NewsAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ChancePlayingNextRound = table.Column<int>(type: "int", nullable: true),
                    ChancePlayingThisRound = table.Column<int>(type: "int", nullable: true),
                    ElementTypeId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_ElementTypes_ElementTypeId",
                        column: x => x.ElementTypeId,
                        principalTable: "ElementTypes",
                        principalColumn: "ElementTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPerformance",
                columns: table => new
                {
                    TeamPerformanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Played = table.Column<int>(type: "int", nullable: false),
                    Win = table.Column<int>(type: "int", nullable: false),
                    Loss = table.Column<int>(type: "int", nullable: false),
                    Draw = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPerformance", x => x.TeamPerformanceId);
                    table.ForeignKey(
                        name: "FK_TeamPerformance_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayersPerformance",
                columns: table => new
                {
                    PlayerPerformanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Minutes = table.Column<int>(type: "int", nullable: false),
                    EventPoints = table.Column<int>(type: "int", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: false),
                    GoalsScored = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    CleanSheets = table.Column<int>(type: "int", nullable: false),
                    GoalsConceded = table.Column<int>(type: "int", nullable: false),
                    PenaltiesSaved = table.Column<int>(type: "int", nullable: false),
                    PenaltiesMissed = table.Column<int>(type: "int", nullable: false),
                    OwnGoals = table.Column<int>(type: "int", nullable: false),
                    YellowCards = table.Column<int>(type: "int", nullable: false),
                    RedCards = table.Column<int>(type: "int", nullable: false),
                    Saves = table.Column<int>(type: "int", nullable: false),
                    Bonus = table.Column<int>(type: "int", nullable: false),
                    BonusPointsSystem = table.Column<int>(type: "int", nullable: false),
                    DreamTeamCount = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersPerformance", x => x.PlayerPerformanceId);
                    table.ForeignKey(
                        name: "FK_PlayersPerformance_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayersStatistics",
                columns: table => new
                {
                    PlayerStatisticsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Influence = table.Column<int>(type: "int", nullable: false),
                    Creativity = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Threat = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    IctIndex = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ExpectedGoals = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ExpectedAssists = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ExpectedGoalInvolvements = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ExpectedGoalsConceded = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ExpectedGoalsPer90 = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ExpectedAssistsPer90 = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ExpectedGoalInvolvementsPer90 = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ExpectedGoalsConcededPer90 = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    GoalsConcededPer90 = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    StartsPer90 = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CleanSheetsPer90 = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersStatistics", x => x.PlayerStatisticsId);
                    table.ForeignKey(
                        name: "FK_PlayersStatistics_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayersTransfer",
                columns: table => new
                {
                    PlayerTransferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransfersIn = table.Column<int>(type: "int", nullable: false),
                    TransfersInEvent = table.Column<int>(type: "int", nullable: false),
                    TransfersOut = table.Column<int>(type: "int", nullable: false),
                    TransfersOutEvent = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersTransfer", x => x.PlayerTransferId);
                    table.ForeignKey(
                        name: "FK_PlayersTransfer_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayersValues",
                columns: table => new
                {
                    PlayerValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NowCost = table.Column<int>(type: "int", nullable: false),
                    CostChangeEvent = table.Column<int>(type: "int", nullable: false),
                    CostChangeStart = table.Column<int>(type: "int", nullable: false),
                    SelectedByPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ValueForm = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ValueSeason = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersValues", x => x.PlayerValueId);
                    table.ForeignKey(
                        name: "FK_PlayersValues_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_player_firstname_secondname",
                table: "Players",
                columns: new[] { "FirstName", "SecondName" });

            migrationBuilder.CreateIndex(
                name: "IX_Players_ElementTypeId",
                table: "Players",
                column: "ElementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "idx_player_performance_player",
                table: "PlayersPerformance",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "idx_player_statistics_player",
                table: "PlayersStatistics",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "idx_player_transfer_player",
                table: "PlayersTransfer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "idx_player_value_player",
                table: "PlayersValues",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "idx_team_performance_team",
                table: "TeamPerformance",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayersPerformance");

            migrationBuilder.DropTable(
                name: "PlayersStatistics");

            migrationBuilder.DropTable(
                name: "PlayersTransfer");

            migrationBuilder.DropTable(
                name: "PlayersValues");

            migrationBuilder.DropTable(
                name: "TeamPerformance");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "ElementTypes");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
