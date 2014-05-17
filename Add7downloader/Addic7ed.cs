using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;

namespace Add7downloader
{
    class Addic7ed
    {
        private const string SITE = "http://www.addic7ed.com";
        private WebClient loader;

        public Addic7ed()
        {
            loader = new WebClient();
            loader.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; rv:23.0) Gecko/20100101 Firefox/23.0");
            loader.Headers.Add("Host", "www.addic7ed.com");
            loader.Headers.Add("Accept-Charset", "UTF-8");
            loader.Encoding = Encoding.UTF8;
        }

        private string loadPage(string url)
        {
            try
            {
                string page = loader.DownloadString(url);
                return page;
            }
            catch (WebException ex)
            {
                return null;
            }
        }

        public List<string[]> SearchEpisode(string showName, string season, string episode, string language = "English")
        {
            var listing = new List<string[]>();
            string show = WebUtility.UrlEncode(showName.Replace(":", ""));
            string searchURL = String.Format("{0}/search.php?search={1}+-+{2}+{3}&Submit=Search", SITE, show, season, episode);
            string resultsPage = loadPage(searchURL);
            MatchCollection parse = Regex.Matches(resultsPage, "<td><a href=\"(.*?)\" debug=\".*?\">(.*?)</a></td>");
            if (parse != null)
            {
                foreach (Match ep in parse)
                {
                    string url = SITE + "/" + ep.Groups[1].Value;
                    string name = ep.Groups[2].Value;
                    var item = new string[2] { url, name };
                    listing.Add(item);
                }
            }
            else
            {
                listing = null;
            }
            return listing;
        }

        public List<string[]> GetEpisode(string episodeURL, string language = "English")
        {
            string page = loadPage(episodeURL);
            if (page != null)
            {
                List<string[]> listing = EpisodeParse(page, language);
                return listing;
            }
            else
            {
                return null;
            }
        }

        public List<string[]> EpisodeParse(string episodePage, string language = "English")
        {
            var listing = new List<string[]>();
            var document = new HtmlDocument();
            document.LoadHtml(episodePage);
            var subCells = document.DocumentNode.SelectNodes(".//table[@width='100%' and @border='0' and @align='center' and @class='tabel95']");
            foreach (HtmlNode cell in subCells)
            {
                HtmlNode subLanguageNode = cell.SelectSingleNode(".//td[@class='language']");
                string subLanguage = subLanguageNode.InnerText;
                if (subLanguage.Contains(language))
                {
                    var subItem = new string[2];
                    // Parse subtitles path
                    HtmlNode downloadNode = cell.SelectSingleNode(".//a[@class='buttonDownload' and contains(.,'most updated')]");
                    if (downloadNode == null)
                    {
                        downloadNode = cell.SelectSingleNode(".//a[@class='buttonDownload' and contains(.,'Download')]");
                    }
                    string downloadLink = SITE + downloadNode.Attributes["href"].Value;
                    subItem[0] = downloadLink;
                    // Parse subtitles version
                    HtmlNode versionNode = cell.SelectSingleNode(".//td[@colspan='3' and @align='center' and @class='NewsTitle']");
                    string versionText = versionNode.InnerText;
                    Match versionMatch = Regex.Match(versionText, "Version (.*?),");
                    string version = versionMatch.Groups[1].Value;
                    HtmlNode worksWithNode = cell.SelectSingleNode(".//td[@class='newsDate' and @colspan='3']");
                    string worksWith = worksWithNode.InnerText;
                    worksWith = worksWith.Replace("\n", "").Replace("\t", "").Replace("  ", "");
                    if (worksWith != "")
                    {
                        version += ", " + worksWith;
                    }
                    HtmlNode hearingImpairedNode = cell.SelectSingleNode(".//img[@title='Hearing Impaired']");
                    if (hearingImpairedNode != null)
                    {
                        version += " - HI";
                    }
                    subItem[1] = version;
                    listing.Add(subItem);
                }
            }
            return listing;
        }
    }
}
