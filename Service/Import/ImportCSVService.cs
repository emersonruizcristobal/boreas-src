using Domain.Models;
using ERC.Framework.Bootstrapper;
using ERC.Framework.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Domain.Repositories.Locator;
using System.Text.RegularExpressions;

namespace Service.Import {
    public class ImportCSVService<T, TEnum> where T     : BaseModel<TEnum>, new() 
                                            where TEnum : struct, IComparable, IFormattable, IConvertible  {

        private HttpContext _context = HttpContext.Current;

        public Tuple<int, int> Import(Func<string[], T> method, HttpPostedFileBase file) {
            var repository      = new RepositoryFactory().GetRepositoryFor<T>();
            var appSettings     = ConfigurationManager.AppSettings;
            var serverDirectory = appSettings["CSVPath"];
            var filename        = string.Format(appSettings["CSVFilenameFormat"], Guid.NewGuid());
            var path            = Path.Combine(_context.Server.MapPath(serverDirectory), filename);
            var result          = new List<T>();
            int totalSuccess    = 0;
            int totalFailed     = 0;

            file.SaveAs(path);

            File.ReadLines(path)
                .ToList()
                .ForEach(a => {
                    try {
                        var x = CreateType(method, SplitStrings(a));
                        repository.Add(x);
                        totalSuccess++;
                    } catch {
                        totalFailed++;
                    }
                });
            repository.Save();
            return new Tuple<int, int>(totalSuccess, totalFailed);
        }

        public Tuple<int, int> ForEach(Action<string[]> method, HttpPostedFileBase file) {
            var appSettings     = ConfigurationManager.AppSettings;
            var serverDirectory = appSettings["CSVPath"];
            var filename        = string.Format(appSettings["CSVFilenameFormat"], Guid.NewGuid());
            var path            = Path.Combine(_context.Server.MapPath(serverDirectory), filename);
            var result          = new List<T>();
            int totalSuccess    = 0;
            int totalFailed     = 0;

            file.SaveAs(path);

            File.ReadLines(path)
                .ToList()
                .ForEach(a => {
                    try {
                        method(SplitStrings(a));
                        totalSuccess++;
                    } catch {
                        totalFailed++;
                    }
                });
            return new Tuple<int, int>(totalSuccess, totalFailed);
        }

        private T CreateType(Func<string[], T> method, string[] s) {
            return method(s);
        }

        private string[] SplitStrings(string s) {
            Regex regEx = new Regex(@"""[^""\r\n]*""|'[^'\r\n]*'|[^,\r\n]*");
            List<string> returnValues = new List<string>();

            Match matchResults = regEx.Match(s);

            while (matchResults.Success) {
                if (matchResults.Value != string.Empty) {
                    returnValues.Add(matchResults.Value);
                }
                matchResults = matchResults.NextMatch();
            }

            return returnValues.ToArray();
        }

    }
}
