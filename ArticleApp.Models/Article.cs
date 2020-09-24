using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace ArticleApp.Models
{
    public class Article
    {
        /// <summary>
        ///일련번호
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 제목
        /// </summary>
        [Required] //필수입력 요소 : NULL 값을 허용하지 않아야 함을 강력하게 요청 ,System.ComponentModel.DataAnnotations 여기들어있음
        public string Title { get; set; }

        



    }
}
