using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MetaShare.Common.Core.Presentation;
using TestPrimitive.Entities;
namespace TestPrimitive.Web.HtmlExtensions
{
	    public static class PagerHtmlExtension
    {
        private const string FirstPageTag = "|<";
        private const string LastPageTag = ">|";
        private const string PreviousTag = "<";
        private const string NextTag = ">";
        private const int MaxDisplayPageCount = 5;

        public static IHtmlContent RenderPager<T>(this IHtmlHelper helper, TargetPager<T> dataList, string url, object routeValues = null)
        {
            HtmlContentBuilder pagerHtmlContentBuilder = new HtmlContentBuilder();
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            if (dataList == null)
            {
                dataList = new TargetPager<T> {PageTotal = 0};
            }


            if (dataList.PageTotal <= 0)
            {
                return pagerHtmlContentBuilder;
            }

            if (dataList.PageIndex > dataList.PageTotal)
            {
                dataList.PageIndex = 1;
            }

            int start = 1;
            int end = dataList.PageTotal;

            if (dataList.PageTotal > MaxDisplayPageCount)
            {
                start = dataList.PageIndex - 4;
                end = dataList.PageIndex + 5;

                if (dataList.PageIndex - 4 <= 1)
                {
                    start = 1;
                    end = MaxDisplayPageCount;
                }
                if (dataList.PageIndex + 4 >= dataList.PageTotal)
                {
                    start = dataList.PageTotal - MaxDisplayPageCount + 1;
                    end = dataList.PageTotal;
                }
            }

            TagBuilder pagerDiv = new TagBuilder("div");
            pagerDiv.MergeAttribute("style", "display:inline-block;");

            TagBuilder pageDivGo = new TagBuilder("div");
            pageDivGo.AddCssClass("togoperpage");

            TagBuilder unsortList = new TagBuilder("ul");
            unsortList.AddCssClass("pagination");

            unsortList.InnerHtml.AppendHtml(RenderPageLi(dataList, FirstPageTag, url, routeValues));
            unsortList.InnerHtml.AppendHtml(RenderPageLi(dataList, PreviousTag, url, routeValues));

            for (int i = start; i <= end; i++)
            {
                unsortList.InnerHtml.AppendHtml(RenderPageLi(dataList, Convert.ToString(i), url, routeValues));
            }

            unsortList.InnerHtml.AppendHtml(RenderPageLi(dataList, NextTag, url, routeValues));
            unsortList.InnerHtml.AppendHtml(RenderPageLi(dataList, LastPageTag, url, routeValues));

            pageDivGo.InnerHtml.AppendHtml(RenderSearchPageAndPageSizeDiv(dataList, url, routeValues));

            pagerDiv.InnerHtml.AppendHtml(unsortList);
            pagerDiv.InnerHtml.AppendHtml(pageDivGo);

            return pagerDiv;
        }

        private static TagBuilder RenderSearchPageAndPageSizeDiv<T>(TargetPager<T> dataList, string url, object routeValues = null)
        {
            TagBuilder form = new TagBuilder("form");
            form.GenerateId("pageSizeAndPageToForm", "");

            if (routeValues != null)
            {
                PropertyInfo[] attributes = routeValues.GetType().GetProperties();

                foreach (PropertyInfo propertyInfo in attributes)
                {
                    TagBuilder hiddenDiv = new TagBuilder("input");
                    hiddenDiv.MergeAttribute("type", "hidden");
                    hiddenDiv.MergeAttribute("name", propertyInfo.Name);
                    hiddenDiv.MergeAttribute("value", Convert.ToString(propertyInfo.GetValue(routeValues, null)));
                    form.InnerHtml.AppendHtml(hiddenDiv);
                }
            }

            form.MergeAttribute("action", url);

            form.InnerHtml.AppendHtml(RenderToPageDiv("go_to_page", dataList));
            form.InnerHtml.AppendHtml(RenderSetSizeDiv(dataList, "per_page"));
            return form;
        }

        private static TagBuilder RenderSetSizeDiv<T>(TargetPager<T> dataList, string divClassName)
        {
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass(divClassName);
            TagBuilder span = new TagBuilder("span");
            span.InnerHtml.Append("Per page");
            div.InnerHtml.AppendHtml(span);

            TagBuilder select = new TagBuilder("select");
            select.GenerateId("pager_pagesize_selectid", "");
            select.MergeAttribute("name", "pageSize");
            const string pageEvent = "javascript:document.getElementById('pageSizeAndPageToForm').submit();";
            select.MergeAttribute("onchange", pageEvent);

            TagBuilder option = new TagBuilder("option");
            option.InnerHtml.Append("5");
            option.MergeAttribute("value", "5");

            TagBuilder option1 = new TagBuilder("option");
            option1.InnerHtml.Append("10");
            option1.MergeAttribute("value", "10");

            TagBuilder option2 = new TagBuilder("option");
            option2.InnerHtml.Append("15");
            option2.MergeAttribute("value", "15");

            TagBuilder option3 = new TagBuilder("option");
            option3.InnerHtml.Append("20");
            option3.MergeAttribute("value", "20");

            if (dataList.PageSize == 20)
            {
                option3.MergeAttribute("selected", "selected");
            }
            else if (dataList.PageSize == 15)
            {
                option2.MergeAttribute("selected", "selected");
            }
            else if (dataList.PageSize == 10)
            {
                option1.MergeAttribute("selected", "selected");
            }
            else
            {
                option.MergeAttribute("selected", "selected");
            }
            select.InnerHtml.AppendHtml(option);
            select.InnerHtml.AppendHtml(option1);
            select.InnerHtml.AppendHtml(option2);
            select.InnerHtml.AppendHtml(option3);
            div.InnerHtml.AppendHtml(select);

            return div;
        }

        private static TagBuilder RenderToPageDiv<T>(string divClassName, TargetPager<T> dataList)
        {
            TagBuilder toPageDiv = new TagBuilder("div");
            toPageDiv.AddCssClass(divClassName);
            TagBuilder span = new TagBuilder("span");
            span.InnerHtml.Append("To");
            toPageDiv.InnerHtml.AppendHtml(span);

            TagBuilder textArea = new TagBuilder("input");
            textArea.MergeAttribute("name", "pageIndex");
            textArea.GenerateId("pageIndexInputId", "");
            textArea.MergeAttribute("type", "text");
            toPageDiv.InnerHtml.AppendHtml(textArea);

            TagBuilder go = new TagBuilder("input");
            go.MergeAttribute("value", "Go");
            go.MergeAttribute("type", "button");

            string buttonEventString = "javascript:setPageIndex(" + dataList.PageIndex + "," + dataList.PageTotal + ");";

            go.MergeAttribute("onclick", buttonEventString);
            toPageDiv.InnerHtml.AppendHtml(go);
            return toPageDiv;
        }

        private static TagBuilder RenderPageLi<T>(TargetPager<T> dataList, string text, string url, object routeValues)
        {
            TagBuilder listItem = new TagBuilder("li");

            int pageNumber;

            if (text == PreviousTag)
            {
                pageNumber = dataList.PageIndex - 1;
            }
            else if (text == NextTag)
            {
                pageNumber = dataList.PageIndex + 1;
            }
            else if (text == FirstPageTag)
            {
                pageNumber = 1;
            }
            else if (text == LastPageTag)
            {
                pageNumber = dataList.PageTotal;
            }
            else
            {
                pageNumber = Convert.ToInt16(text, 10);
            }

            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (pageNumber > dataList.PageTotal)
            {
                pageNumber = dataList.PageTotal;
            }


            string targetUrl = url;
            if (routeValues != null)
            {
                PropertyInfo[] attributes = routeValues.GetType().GetProperties();

                foreach (PropertyInfo propertyInfo in attributes)
                {
                    if (targetUrl.Contains("?"))
                    {
                        targetUrl += "&";
                    }
                    else
                    {
                        targetUrl += "?";
                    }

                    targetUrl += propertyInfo.Name + "=" + propertyInfo.GetValue(routeValues, null);
                }
            }

            if (targetUrl.Contains("?"))
            {
                targetUrl += "&pageIndex=" + pageNumber + "&pageSize=" + dataList.PageSize;
            }
            else
            {
                targetUrl += "?pageIndex=" + pageNumber + "&pageSize=" + dataList.PageSize;
            }

            TagBuilder link = new TagBuilder("a");

            if (pageNumber != dataList.PageIndex)
            {
                link.MergeAttribute("href", targetUrl);
            }
            link.InnerHtml.Append(text);

            int currentIndex = 0;
            int.TryParse(text, out currentIndex);

            if (currentIndex == dataList.PageIndex)
            {
                listItem.AddCssClass("active");
            }
            listItem.InnerHtml.AppendHtml(link);
            return listItem;
        }

        #region AjaxRenderPager

        public static IHtmlContent AjaxRenderPager<T>(this IHtmlHelper helper, TargetPager<T> dataList, string action, string controller, string tagert, object routeValues = null)
        {
            HtmlContentBuilder pagerHtmlContentBuilder = new HtmlContentBuilder();

            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentNullException("action");
            }
            if (string.IsNullOrEmpty(controller))
            {
                throw new ArgumentNullException("controller");
            }
            if (dataList == null)
            {
                dataList = new TargetPager<T> { PageTotal = 0 };
            }

            if (dataList.PageTotal <= 0)
            {
                return pagerHtmlContentBuilder;
            }

            if (dataList.PageIndex > dataList.PageTotal)
            {
                dataList.PageIndex = 1;
            }

            int start = 1;
            int end = dataList.PageTotal;

            if (dataList.PageTotal > MaxDisplayPageCount)
            {
                if (dataList.PageIndex - (MaxDisplayPageCount - 1) <= 1)
                {
                    start = 1;
                    end = MaxDisplayPageCount;
                }

                if (dataList.PageIndex > MaxDisplayPageCount)
                {
                    int remainderForPageIndex = dataList.PageIndex % MaxDisplayPageCount;
                    int remainderForPageTotal = dataList.PageTotal % MaxDisplayPageCount;
                    int placePageForPageIndex = dataList.PageIndex / MaxDisplayPageCount;
                    int placePageForPageTotal = dataList.PageTotal / MaxDisplayPageCount;

                    if (remainderForPageTotal == 0)
                    {
                        if (remainderForPageIndex == 0)
                        {
                            start = (placePageForPageIndex - 1) * MaxDisplayPageCount + 1;
                        }
                        else
                        {
                            start = placePageForPageIndex * MaxDisplayPageCount + 1;
                        }
                        end = start + MaxDisplayPageCount - 1;
                    }

                    else
                    {
                        if (remainderForPageIndex == 0)
                        {
                            start = (placePageForPageIndex - 1) * MaxDisplayPageCount + 1;
                            end = start + MaxDisplayPageCount - 1;
                        }
                        else
                        {
                            start = placePageForPageIndex * MaxDisplayPageCount + 1;
                            if (placePageForPageIndex == placePageForPageTotal)
                            {
                                end = dataList.PageTotal;
                            }
                            else
                            {
                                end = start + MaxDisplayPageCount - 1;
                            }
                        }
                    }
                }
            }

            int firstPageNumber;
            int lastPageNumber;
            GetPageNumber(start, end, out firstPageNumber, out lastPageNumber);

            TagBuilder pagerDiv = new TagBuilder("div");
            pagerDiv.MergeAttribute("style", "display:inline-block;");
             
            TagBuilder pageDivGo = new TagBuilder("div");
            pageDivGo.AddCssClass("togoperpage");


            TagBuilder unsortList = new TagBuilder("ul");
            unsortList.AddCssClass("pagination");

            unsortList.InnerHtml.AppendHtml(AjaxRenderPageLi(helper, dataList, FirstPageTag, action, controller, routeValues, tagert, firstPageNumber, lastPageNumber)) ;
            unsortList.InnerHtml.AppendHtml(AjaxRenderPageLi(helper, dataList, PreviousTag, action, controller, routeValues, tagert, firstPageNumber, lastPageNumber));

            for (int i = start; i <= end; i++)
            {
                unsortList.InnerHtml.AppendHtml(AjaxRenderPageLi(helper, dataList, Convert.ToString(i), action, controller, routeValues, tagert, firstPageNumber, lastPageNumber));
            }

            unsortList.InnerHtml.AppendHtml(AjaxRenderPageLi(helper, dataList, NextTag, action, controller, routeValues, tagert, firstPageNumber, lastPageNumber));
            unsortList.InnerHtml.AppendHtml(AjaxRenderPageLi(helper, dataList, LastPageTag, action, controller, routeValues, tagert, firstPageNumber, lastPageNumber));

            pageDivGo.InnerHtml.AppendHtml(AjaxRenderSearchPageAndPageSizeDiv(helper, dataList, " col-static-24", routeValues, tagert, action, controller));
 
            pagerDiv.InnerHtml.AppendHtml(unsortList);
            pagerDiv.InnerHtml.AppendHtml(pageDivGo);

            return pagerDiv;
        }

        private static IHtmlContent AjaxRenderSearchPageAndPageSizeDiv<T>(IHtmlHelper helper, TargetPager<T> dataList, string divClassName, object routeValues, string updateTarget, string action, string controller)
        {
            HtmlContentBuilder pagerHtmlContentBuilder = new HtmlContentBuilder();
            
            string goelementId = updateTarget + "_pageIndexInputId";
            string perPageelementId = updateTarget + "_pager_pagesize_selectid";

            IHtmlContent ajaxLink = helper.ActionLink("Go", action, controller, routeValues, new { data_ajax = "true", data_ajax_method = "Post", data_ajax_mode = "replace" ,data_ajax_update = "#" + updateTarget, @class = "go", @onclick = "setJumpPagerLinkWithGo(" + dataList.PageIndex + "," + dataList.PageTotal + "," + goelementId + "," + perPageelementId + ", this)" });
            IHtmlContent ajaxLinkHide = helper.ActionLink("Go", action, controller, routeValues, new { data_ajax = "true", data_ajax_method = "Post", data_ajax_mode = "replace" ,data_ajax_update = "#" + updateTarget, @id = updateTarget + "_perPageHiddenGo", @hidden = "hidden", @onclick = "setJumpPagerLinkWithPerPage(" + dataList.PageIndex + "," + dataList.PageTotal + "," + perPageelementId + ", this )" });
            
            pagerHtmlContentBuilder.AppendHtml(AjaxRenderToPageDiv(dataList, ajaxLink,  updateTarget));
            pagerHtmlContentBuilder.AppendHtml(AjaxRenderSetSizeDiv(dataList, ajaxLinkHide, updateTarget));

            return pagerHtmlContentBuilder;
        }
        
        private static TagBuilder AjaxRenderToPageDiv<T>(TargetPager<T> dataList, IHtmlContent pagerGoUrl, string updateTarget)
        {
            TagBuilder toPageDiv = new TagBuilder("div");
            toPageDiv.AddCssClass("go_to_page");
            
            //TagBuilder spanTotalPage = new TagBuilder("span");
            //spanTotalPage.InnerHtml.AppendHtml("Total Page" + " " + dataList.PageTotal + " | ");
            //toPageDiv.InnerHtml.AppendHtml(spanTotalPage);

            TagBuilder span = new TagBuilder("span");
            span.InnerHtml.AppendHtml("To");
            toPageDiv.InnerHtml.AppendHtml(span);

            TagBuilder textArea = new TagBuilder("input");
            textArea.MergeAttribute("name", "pageIndex");
            textArea.GenerateId(updateTarget + "_pageIndexInputId","");
            textArea.MergeAttribute("type", "text");
            textArea.MergeAttribute("style", "width:50px;height:30px;");
            toPageDiv.InnerHtml.AppendHtml(textArea);
            toPageDiv.InnerHtml.AppendHtml(pagerGoUrl);

            return toPageDiv;
        }

        private static IHtmlContent AjaxRenderSetSizeDiv<T>(TargetPager<T> dataList, IHtmlContent pagePerPagerGoUrl, string updateTarget)
        {
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("per_page");
            TagBuilder span = new TagBuilder("span");
            span.InnerHtml.AppendHtml("Per Page");
            div.InnerHtml.AppendHtml(span);

            string perPageLinkId = updateTarget + "_perPageHiddenGo";
            TagBuilder select = new TagBuilder("select");
            select.GenerateId(updateTarget + "_pager_pagesize_selectid","");
            select.MergeAttribute("name", "pageSize");
            string pageEvent = "javascript:document.getElementById('" + perPageLinkId + "').click();";
            select.MergeAttribute("onchange", pageEvent);

            TagBuilder option = new TagBuilder("option");
            option.InnerHtml.AppendHtml("5");
            option.MergeAttribute("value", "5");


            TagBuilder option1 = new TagBuilder("option");
            option1.InnerHtml.AppendHtml("10");
            option1.MergeAttribute("value", "10");

            TagBuilder option2 = new TagBuilder("option");
            option2.InnerHtml.AppendHtml("15");
            option2.MergeAttribute("value", "15");

            TagBuilder option3 = new TagBuilder("option");
            option3.InnerHtml.AppendHtml("20");
            option3.MergeAttribute("value", "20");

            if (dataList.PageSize == 20)
            {
                option3.MergeAttribute("selected", "selected");
            }
            else if (dataList.PageSize == 15)
            {
                option2.MergeAttribute("selected", "selected");
            }
            else if (dataList.PageSize == 10)
            {
                option1.MergeAttribute("selected", "selected");
            }
            else
            {
                option.MergeAttribute("selected", "selected");
            }

            select.InnerHtml.AppendHtml(option);
            select.InnerHtml.AppendHtml(option1);
            select.InnerHtml.AppendHtml(option2);
            select.InnerHtml.AppendHtml(option3);

            div.InnerHtml.AppendHtml(select);
            div.InnerHtml.AppendHtml(pagePerPagerGoUrl);

            return div;
        }

        private static void GetPageNumber(int start, int end, out int firstPageNumber, out int lastPageNumber)
        {
            firstPageNumber = ((start / (end - start + 1)) - 1) * (end - start + 1) + 1;
            lastPageNumber = end + 1;
        }

        private static IHtmlContent AjaxRenderPageLi<T>(IHtmlHelper ajaxHelper, TargetPager<T> dataList, string text, string action, string controller, object routeValues, string updateTarget, int firstPageNumber, int lastPageNumber)
        {
            TagBuilder listItem = new TagBuilder("li");

            int pageNumber;


            if (text == PreviousTag)
            {
                pageNumber = dataList.PageIndex - 1;
            }
            else if (text == NextTag)
            {
                pageNumber = dataList.PageIndex + 1;
            }
            else if (text == FirstPageTag)
            {
                pageNumber = firstPageNumber;
            }
            else if (text == LastPageTag)
            {
                pageNumber = lastPageNumber;
            }
            else
            {
                pageNumber = Convert.ToInt16(text, 10);
            }

            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (pageNumber > dataList.PageTotal)
            {
                pageNumber = dataList.PageTotal;
            }

            if (pageNumber != dataList.PageIndex)
            {
                object routeParam = ContactObject(routeValues, new {pageIndex = pageNumber, pageSize = dataList.PageSize});
                IHtmlContent ajaxLink = ajaxHelper.ActionLink(text, action, controller, routeParam, new {data_ajax = "true", data_ajax_method = "Post", data_ajax_mode = "replace", data_ajax_update = "#" + updateTarget});
                listItem.InnerHtml.AppendHtml(ajaxLink);
            }
            else
            {
                TagBuilder aTagBuilder = new TagBuilder("a");
                aTagBuilder.InnerHtml.Append(text);
                listItem.InnerHtml.AppendHtml(aTagBuilder);
            }
            
            int currentIndex = 0;
            int.TryParse(text, out currentIndex);

            if (currentIndex == dataList.PageIndex)
            {
                listItem.AddCssClass("active");
            }
            
            return listItem;
        }


        private static object ContactObject(object objOne, object objAnothor)
        {
            Dictionary<string, object> dynamicObj = new Dictionary<string, object>();
            
            if (objOne != null)
            {
                PropertyInfo[] attributes1 = objOne.GetType().GetProperties();
                
                foreach (PropertyInfo propertyInfo in attributes1)
                {
                    dynamicObj.Add(propertyInfo.Name, propertyInfo.GetValue(objOne, null));
                    
                }
            }

            if (objAnothor != null)
            {
                PropertyInfo[] attributes2 = objAnothor.GetType().GetProperties();

                foreach (PropertyInfo propertyInfo in attributes2)
                {
                    dynamicObj.Add(propertyInfo.Name, propertyInfo.GetValue(objAnothor, null));

                }
            }

            return dynamicObj;
        }
        #endregion
    }
}