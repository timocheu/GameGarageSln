using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using GameGarage.Models.ViewModels;

namespace GameGarage.Infrastructure
{

    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }

        public PagingInfo? PageModel { get; set; }

        public string? PageAction { get; set; }

        public override void Process(TagHelperContext context,
                TagHelperOutput output)
        {
            if (ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper
                    = urlHelperFactory.GetUrlHelper(ViewContext);
                TagBuilder result = new TagBuilder("div");

                int totalPages = PageModel.TotalPages;
                int currentPage = PageModel.CurrentPage;
                int maxVisiblePages = 5; // How many numbers to show in the middle

                // 1. Determine the range of numbers to show
                int startPage = Math.Max(1, currentPage - 2);
                int endPage = Math.Min(totalPages, startPage + maxVisiblePages - 1);

                // Adjust startPage again if we are near the end
                if (endPage - startPage < maxVisiblePages - 1)
                {
                    startPage = Math.Max(1, endPage - maxVisiblePages + 1);
                }

                // --- Helper function to create the tag ---
                Func<int, TagBuilder> CreatePageLink = (pageNumber) =>
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { currentPage = pageNumber });
                    if (currentPage == pageNumber) tag.AddCssClass("active");
                    tag.InnerHtml.Append(pageNumber.ToString());
                    return tag;
                };

                // 2. Always show Page 1
                result.InnerHtml.AppendHtml(CreatePageLink(1));

                // 3. Show Left Ellipsis if necessary
                if (startPage > 2)
                {
                    TagBuilder dots = new TagBuilder("span");
                    dots.InnerHtml.Append("...");
                    result.InnerHtml.AppendHtml(dots);
                }

                // 4. Show the "Middle Window" numbers
                // Skip 1 and TotalPages since we handle them separately
                for (int i = startPage; i <= endPage; i++)
                {
                    if (i == 1 || i == totalPages) continue;
                    result.InnerHtml.AppendHtml(CreatePageLink(i));
                }

                // 5. Show Right Ellipsis if necessary
                if (endPage < totalPages - 1)
                {
                    TagBuilder dots = new TagBuilder("span");
                    dots.InnerHtml.Append("...");
                    result.InnerHtml.AppendHtml(dots);
                }

                // 6. Always show Last Page (if more than 1 page exists)
                if (totalPages > 1)
                {
                    result.InnerHtml.AppendHtml(CreatePageLink(totalPages));
                }

                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}

