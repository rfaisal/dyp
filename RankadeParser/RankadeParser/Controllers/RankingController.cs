using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HtmlAgilityPack;
using RankadeParser.Models;
using RankadeParser.Utils;

namespace RankadeParser.Controllers
{
    public class RankingController : ApiController
    {
        private const string RankadeUri = "https://rankade.com/microsoft-vancouver";

        // GET api/ranking
        public IEnumerable<PlayerRank> Get()
        {
            var web = new HtmlWeb();
            var document = web.Load(RankadeUri);

            var rankingDiv = GetDivsByClass(document.DocumentNode, "group-page-rankings").FirstOrDefault();
            var rowDivs = GetDivsByClass(rankingDiv, "row");

            return rowDivs.Select(_ => new PlayerRank(ParseName(_), ParseRank(_)));
        }

        #region Private Methods
        /// <summary>
        /// Parses the name of the player from a given row 
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>name</returns>
        private string ParseName(HtmlNode row)
        {
            return GetDivsByClass(row, "playerCol").FirstOrDefault()?.InnerText?.Trim()?.Replace("*", "") ?? string.Empty;
        }

        /// <summary>
        /// Parses rank of the player for a row
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>rank</returns>
        private int ParseRank(HtmlNode row)
        {
            var innerText = GetDivsByClass(row, "reeCol").FirstOrDefault()?.InnerText?.Trim() ?? string.Empty;
            var splitted = innerText.Split('\n');
            int rank;
            if (splitted.Length > 0 && int.TryParse(splitted[0], out rank))
            {
                return rank;
            }

            return -1;
        }

        /// <summary>
        /// Gets all the divs by its class name
        /// </summary>
        /// <param name="doc">parent</param>
        /// <param name="className">class name</param>
        /// <returns>List of divs</returns>
        private IEnumerable<HtmlNode> GetDivsByClass(HtmlNode doc, string className)
        {
            return
                doc?.Descendants("div")?
                    .Where(
                        _ =>
                            _.Attributes["class"]?.Value?.Contains(className, StringComparison.InvariantCultureIgnoreCase) ?? false)
                ?? Enumerable.Empty<HtmlNode>();
        }
        #endregion
    }
}
