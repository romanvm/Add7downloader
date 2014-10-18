using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

using HtmlAgilityPack;

namespace Add7downloader
{
    class Addic7ed
    {
        const string SITE = "http://www.addic7ed.com";        

        static string[] loadPage(string url, string referrer = null)
        {
            string page;
            string responseURI;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:23.0) Gecko/20100101 Firefox/23.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Host = "www.addic7ed.com";
            request.Headers.Add("Accept-Charset", "UTF-8");
            request.AllowAutoRedirect = true;            
            if (referrer != null)
            {
                request.Referer = referrer;
            }
            try
            {
                WebResponse response = request.GetResponse();
                responseURI = response.ResponseUri.AbsoluteUri;
                Stream httpStream = response.GetResponseStream();                
                using (var reader = new StreamReader(httpStream))
                {
                    page = reader.ReadToEnd();
                }                
            }
            catch (WebException ex)
            {
                page = null;
                responseURI = null;
            }
            return new string[2] { page, responseURI };
        }

        public static object[] SearchEpisode(string showName, string season, string episode, string language = "English")
        {
            var listing = new List<string[]>();
            string episodeURL;
            string show = WebUtility.UrlEncode(showName.Replace(":", ""));
            string searchURL = String.Format("{0}/search.php?search={1}+{2}x{3}&Submit=Search", SITE, show, season, episode);
            string[] searchResult = loadPage(searchURL);
            if (searchResult[0] != null)
            {
                string resultsPage = searchResult[0];
                Match checkEpisode = Regex.Match(resultsPage, "(<table width=\"100%\" border=\"0\" align=\"center\" class=\"tabel95\">)");
                if (checkEpisode.Groups.Count > 1)
                {
                    listing = EpisodeParse(resultsPage, language);
                    episodeURL = searchResult[1];
                }
                else
                {
                    episodeURL = "";
                    MatchCollection episodes = Regex.Matches(resultsPage, "<td><a href=\"(.*?)\" debug=\".*?\">(.*?)</a></td>");
                    if (episodes != null)
                    {
                        foreach (Match ep in episodes)
                        {
                            string url = SITE + "/" + ep.Groups[1].Value;
                            string name = ep.Groups[2].Value;
                            var item = new string[2] { url, name };
                            listing.Add(item);
                        }
                    }                    
                }
            }       
            else
            {
                listing = null;
                episodeURL = null;
            }
            return new object[2] { listing, episodeURL };
        }

        public static List<string[]> GetEpisode(string episodeURL, string language = "English")
        {
            string page = loadPage(episodeURL)[0];
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

        public static List<string[]> EpisodeParse(string episodePage, string language = "English")
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

        public static int SubDownload(string url, string referrer, string filename = "subtitles.srt")
        {            
            string subtitles = loadPage(url, referrer)[0];
            if (subtitles == null)
            {
                return 0;
            }
            else
            {
                if (subtitles.Substring(0, 9) != "<!DOCTYPE")
                {
                    using (var fileWriter = new StreamWriter(filename))
                    {
                        fileWriter.Write(subtitles);                        
                    }
                    return 1;
                }
                else
                {
                    return -1;
                }                
            }           
        }
    }
}
