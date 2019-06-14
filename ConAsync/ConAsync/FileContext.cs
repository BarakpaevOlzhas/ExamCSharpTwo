namespace ConAsync
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FileContext : DbContext
    {
        public FileContext()
            : base("name=FileContext")
        {
        }

        public DbSet<File> Files { set; get; }
    }
}