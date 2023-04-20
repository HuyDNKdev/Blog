using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTestWeb.Models
{
    public class StudentQuestViewModel
    {
        public Đề_Thi Đề_Thi { get; set; }
        public Học_Sinh Học_Sinh { get; set; }
        public Câu_Hỏi Câu_Hỏi { get; set; }
        public Bài_Làm Bài_Làm { get; set; }

    }
}