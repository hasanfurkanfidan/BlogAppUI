using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppUI.Models
{
    public class BlogAddModel
    {
        [Required(ErrorMessage ="Başlık alanı gereklidir")]
        [Display(Name ="Başlık")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Kısa açıklama alanı gereklidir")]
        [Display(Name = "Kısa Açıklama")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Açıklama alanı gereklidir")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        public int AppUserId { get; set; }
        public IFormFile Image { get; set; }
    }
}
