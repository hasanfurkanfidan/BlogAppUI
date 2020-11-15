using BlogAppUI.ApiServices.Abstract;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppUI.TagHelpers
{
    [HtmlTargetElement("getcategoryname")]
    public class CategoryNameTagHelper : TagHelper
    {
        private readonly ICategoryApiService _categoryApiService;
        public CategoryNameTagHelper(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }
        public int Id { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var categories = await _categoryApiService.GetAll();
            var category = categories.FirstOrDefault(p => p.Id == Id);
            var html = $@"
        Şuanda aktif karatorisi  {category.Name} olan blogları görüyorsunuz.
        
        ";
            output.Content.SetHtmlContent(html);


        }
    }
}
