using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppUI.Models
{
    public class CategoryWithBlogsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BlogsCount { get; set; }
    }
}
