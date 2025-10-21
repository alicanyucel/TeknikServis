using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg028812213323 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Add column if missing (nullable)
            migrationBuilder.Sql(@"
IF COL_LENGTH('dbo.Statuses', 'ProductId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Statuses] ADD [ProductId] uniqueidentifier NULL;
END
");

            // 2) If column exists but is NOT NULL, make it NULLABLE
            migrationBuilder.Sql(@"
IF COL_LENGTH('dbo.Statuses', 'ProductId') IS NOT NULL
BEGIN
    DECLARE @is_nullable int;
    SELECT @is_nullable = is_nullable
    FROM sys.columns 
    WHERE Name = 'ProductId' AND Object_ID = Object_ID('dbo.Statuses');

    IF (@is_nullable = 0)
    BEGIN
        ALTER TABLE [dbo].[Statuses] ALTER COLUMN [ProductId] uniqueidentifier NULL;
    END
END
");

            // 3) Drop default constraint on ProductId if any (to avoid Guid.Empty defaults)
            migrationBuilder.Sql(@"
DECLARE @df NVARCHAR(128);
SELECT @df = dc.name
FROM sys.default_constraints dc
JOIN sys.columns c ON c.default_object_id = dc.object_id
JOIN sys.tables t ON t.object_id = c.object_id
WHERE t.name = 'Statuses' AND c.name = 'ProductId';
IF @df IS NOT NULL
    EXEC('ALTER TABLE [dbo].[Statuses] DROP CONSTRAINT [' + @df + ']');
");

            // 4) Normalize bad data: set Guid.Empty to NULL
            migrationBuilder.Sql(@"
UPDATE [dbo].[Statuses] SET [ProductId] = NULL 
WHERE [ProductId] = '00000000-0000-0000-0000-000000000000';
");

            // 5) Create index if missing
            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Statuses_ProductId' AND object_id = OBJECT_ID('dbo.Statuses'))
    CREATE INDEX [IX_Statuses_ProductId] ON [dbo].[Statuses]([ProductId]);
");

            // 6) Add FK if missing
            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Statuses_Products_ProductId')
    ALTER TABLE [dbo].[Statuses] ADD CONSTRAINT [FK_Statuses_Products_ProductId]
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([Id]) ON DELETE CASCADE;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Statuses_Products_ProductId')
    ALTER TABLE [dbo].[Statuses] DROP CONSTRAINT [FK_Statuses_Products_ProductId];
");

            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Statuses_ProductId' AND object_id = OBJECT_ID('dbo.Statuses'))
    DROP INDEX [IX_Statuses_ProductId] ON [dbo].[Statuses];
");

            migrationBuilder.Sql(@"
IF COL_LENGTH('dbo.Statuses', 'ProductId') IS NOT NULL
    ALTER TABLE [dbo].[Statuses] DROP COLUMN [ProductId];
");
        }
    }
}
