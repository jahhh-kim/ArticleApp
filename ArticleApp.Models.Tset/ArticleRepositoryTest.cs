using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ArticleApp.Models.Tset
{
    [TestClass]
    public class ArticleRepositoryTest
    {
        [TestMethod]
        public async Task ArticleRepositoryAllMethodTest()
        {
            var options = new DbContextOptionsBuilder<ArticleAppDbContext>()
            //    .UseInMemoryDatabase(databaseName: "ArticlaApp").Options;
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ArticleApp;Trusted_Connection=True;").Options;
    
            
            //AddAsync() Method Test
            using(var context = new ArticleAppDbContext(options))
            {
                //Repository Object Creation
                var repository = new ArticleRepository(context);
                var model = new Article { Title = "[1] 게시판 시작" , Created=DateTime.Now};
                await repository.AddArticleAsync(model);
                await context.SaveChangesAsync();

            }
            using (var context = new ArticleAppDbContext(options))
            {
                Assert.AreEqual(1, await context.Articles.CountAsync());
                var model = await context.Articles.Where(m => m.Id == 1).SingleOrDefaultAsync();
                Assert.AreEqual("[1] 게시판 시작", model?.Title);
            }

            //GetAllAsync() Method Test
            using (var context = new ArticleAppDbContext(options))
            {
                    var repository = new ArticleRepository(context);
                    var model = new Article { Title = "[2] 게시판 가동", Created = DateTime.Now };
                    await context.SaveChangesAsync();
                    context.Articles.Add(model);
                    context.Articles.Add(new Article { Title = "[3] 게시판 중지", Created = DateTime.Now });
                    await context.SaveChangesAsync();
            }

            using (var context = new ArticleAppDbContext(options))
            {
                var repository = new ArticleRepository(context);
                var models = await repository.GetArticlesAsync();
                Assert.AreEqual(3, models.Count);
            }

            //GetByIdAsync() Method Test
            using (var context = new ArticleAppDbContext(options))
            {
              
            }

            using (var context = new ArticleAppDbContext(options))
            {
                var repository = new ArticleRepository(context);
                var models = await repository.GetArticleByIdAsync(2);
                Assert.IsTrue(models.Title.Contains("가동"));
                Assert.AreEqual("[2] 게시판 가동", models.Title);
            }


            //GetEditAsync() Method Test
            using (var context = new ArticleAppDbContext(options))
            {

            }

            using (var context = new ArticleAppDbContext(options))
            {
                var repository = new ArticleRepository(context);
                var models = await repository.GetArticleByIdAsync(2);
                models.Title = "[2] 게시판 바쁨";
                await repository.EditArticleAsync(models);
                await context.SaveChangesAsync();

                Assert.AreEqual("[2] 게시판 바쁨",(await context.Articles.Where(m=>m.Id==2).SingleOrDefaultAsync()).Title);
            }

            //GetDeleteAsync() Method Test
            using (var context = new ArticleAppDbContext(options))
            {

            }

            using (var context = new ArticleAppDbContext(options))
            {
                var repository = new ArticleRepository(context);
              
                await repository.DeleteArticleAsync(2);
                await context.SaveChangesAsync();

                Assert.AreEqual(2, await context.Articles.CountAsync());
                Assert.IsNull(await repository.GetArticleByIdAsync(2));
            }


            //pagingAsync() Method Test
            using (var context = new ArticleAppDbContext(options))
            {

            }

            using (var context = new ArticleAppDbContext(options))
            {
                int pageIndex = 0;
                int pageSize = 1;

                var repository = new ArticleRepository(context);
                var models = await repository.GetAllAsync(pageIndex,pageSize);
                await context.SaveChangesAsync();

                Assert.AreEqual("[3] 게시판 중지", models.Records.FirstOrDefault().Title);
                Assert.AreEqual(2, models.TotalRecords);
            }
        }
    }
}
