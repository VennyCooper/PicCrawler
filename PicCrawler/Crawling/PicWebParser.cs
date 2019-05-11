using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace PicCrawler.Crawling
{
    class PicWebParser : HtmlParser
    {
        private const string GALLERY_TITLE = "//html/head/title";
        private const string GALLERY_PAGE_LINKS = "//html/body/div[@class='gtb']/table[@class='ptb']/tr/td";
        private const string GALLERY_PIC_LINKS = "//html/body/div[@id='gdt']/div[@class='gdtm']/div/a";
        private const string PIC_LINK = "//html/body/div[@id='i1']/div[@id='i3']/a/img";

        private string GalleryName { get; set; }

        public override IEnumerable<string> RunParser()
        {
            GalleryName = DocNode.SelectSingleNode(GALLERY_TITLE).InnerHtml;
            Logger.SafeWriteLine($"Extract gallery name '{GalleryName}'");
            return new string[] { GalleryName }.Concat(GetFileUris()).ToArray();
        }


        protected override IEnumerable<string> GetFileUris()
        {
            var picPages = GetPicPagesFromAllGalleryPages().ToArray();
            HtmlDocument pageHtmlDoc;
            foreach (var picPage in picPages)
            {
                pageHtmlDoc = new HtmlDocument();
                pageHtmlDoc.Load(Common.GetPageHtml(picPage));
                yield return pageHtmlDoc.DocumentNode.SelectSingleNode(PIC_LINK).Attributes["src"].Value;
            }
        }

        private IEnumerable<string> GetPicPagesFromAllGalleryPages()
        {
            var galleryPageUris = GetGalleryPageUris().ToArray();
            IEnumerable<string> picPages = new List<string>();
            HtmlDocument galleryPageHtmlDoc;
            foreach (var galleryPageUri in galleryPageUris)
            {
                galleryPageHtmlDoc = new HtmlDocument();
                galleryPageHtmlDoc.Load(Common.GetPageHtml(galleryPageUri));
                picPages = picPages.Concat
                (
                    galleryPageHtmlDoc.DocumentNode.SelectNodes(GALLERY_PIC_LINKS).Select(x => x.Attributes["href"].Value)
                );
            }
            return picPages.ToArray();
        }

        private IEnumerable<string> GetGalleryPageUris()
        {
            var pageNodes = DocNode.SelectNodes(GALLERY_PAGE_LINKS).Skip(1);
            string page_1 = pageNodes.First().SelectSingleNode("a").Attributes["href"].Value;
            int totalPageCount = int.Parse(pageNodes.Skip(pageNodes.Count() - 2).First().SelectSingleNode("a").InnerHtml);
            Logger.SafeWriteLine($"Found {totalPageCount} pages in total for gallery '{GalleryName}'");
            var page_others = Enumerable.Range(1, totalPageCount - 1).Select(x => $"{page_1}?p={x}");
            return new string[] { page_1 }.Concat(page_others).ToArray();
        }
    }
}
