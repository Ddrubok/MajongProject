using GameDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // //Create
            //using (GameDbContext db = new GameDbContext()) 
            // {
            //     TestDb testDb = new TestDb();
            //     testDb.Name = "Doobok";

            //     db.Tests.Add(testDb);

            //     db.SaveChanges();
            // }

            ////Read
            //using(GameDbContext db = new GameDbContext())
            //{
            //    TestDb? findDb = db.Tests.FirstOrDefault(t => t.Name == "Doobok");
            //    if(findDb != null)
            //    {
            //        int check = findDb.TestDbId;
            //    }
            //}

            ////update
            //using (GameDbContext db = new GameDbContext())
            //{
            //    TestDb? findDb = db.Tests.FirstOrDefault(t => t.Name == "Doobok");
            //    if (findDb != null)
            //    {
            //        findDb.Name = "Handsome Doobok";
            //        db.SaveChanges();
            //    }
            //}

            //delete
            using (GameDbContext db = new GameDbContext())
            {
                //찾아서 삭제
                //TestDb? findDb = db.Tests.FirstOrDefault(t => t.Name == "Doobok");
                //if (findDb != null)
                //{
                //    db.Tests.Remove(findDb);
                //    db.SaveChanges();
                //}

                ////Entity Tracking 삭제
                //{
                //    TestDb testDb = new TestDb()
                //    {
                //        TestDbId = 1
                //    };
                //    var entry = db.Tests.Entry(testDb);
                //    entry.State =EntityState.Deleted;
                //    db.SaveChanges();

                //}

                //EF Core 7
                {
                    db.Tests.Where(a => a.Name.Contains("Doobok")).ExecuteDelete();
                }

            }
        }
    }
}


