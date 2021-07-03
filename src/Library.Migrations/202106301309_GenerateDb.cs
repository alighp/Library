using FluentMigrator;

namespace Library.Migrations
{
    [Migration(202106301309)]
    public class _202106301309_GenerateDb : Migration
    {
        public override void Down()
        {
            Delete.Table("Lendings");
            Delete.Table("Books");
            Delete.Table("Members");
            Delete.Table("BookCategories");
        }

        public override void Up()
        {

            Create.Table("BookCategories")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString(100).NotNullable();

            Create.Table("Members")
                 .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                 .WithColumn("FirstName").AsString(50).NotNullable()
                 .WithColumn("LastName").AsString(50).NotNullable()
                 .WithColumn("BirthDate").AsDate().NotNullable()
                 .WithColumn("Address").AsString(100).NotNullable();

            Create.Table("Books")
                 .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                 .WithColumn("Title").AsString(100).NotNullable()
                 .WithColumn("Author").AsString(100).NotNullable()
                 .WithColumn("MinAge").AsByte().NotNullable()
                 .WithColumn("MaxAge").AsByte().NotNullable()
                 .WithColumn("CategoryId").AsInt32().NotNullable().ForeignKey("FK_Books_BookCategories", "BookCategories", "Id").OnDelete(System.Data.Rule.Cascade);

            Create.Table("Lendings")
                 .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                 .WithColumn("DeliveryDate").AsDate().Nullable()
                 .WithColumn("ReturnDate").AsDate().NotNullable()
                 .WithColumn("MemberId").AsInt32().NotNullable().ForeignKey("FK_Lendings_Members", "Members", "Id").OnDelete(System.Data.Rule.Cascade)
                 .WithColumn("BookId").AsInt32().NotNullable().ForeignKey("FK_Lendings_Books", "Books", "Id").OnDelete(System.Data.Rule.Cascade);


        }
    }
}
