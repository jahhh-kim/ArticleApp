using Dul.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArticleApp.Models
{
    public interface IArticleRepository
    {
        Task<Article> AddArticleAsync(Article model); // 입력
        Task<List<Article>> GetArticlesAsync();         // 출력
        Task<Article> GetArticleByIdAsync(int id);     //상세
        Task<Article> EditArticleAsync(Article model); //수정
        Task DeleteArticleAsync(int id); 
        Task<PagingResult<Article>> GetAllAsync(int pageIndex, int pageSize);

    }
}
