using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence.Repository
{
    public partial class StatusFieldRepository : IStatusFieldRepository
    {
        public IList<Status> GetStatusInformation(string statusFieldName, Session session)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var selection = from statusField in Context.Get<StatusField>(session)
                            from status in statusField.Status
                            let translationItem =
                                status.Translation.TranslationItems.FirstOrDefault(
                                    n => n.LanguageId == session.SessionObject.User.LanguageId)
                            where statusField.Name == statusFieldName
                            select new
                                {
                                    status,
                                    TranslatedValue =
                                translationItem != null
                                    ? translationItem.Description
                                    : status.Translation.DefaultDescription,
                                };

            var result = selection.ToList();

            //Convert to list of status objects (directly create status via "select new Status () ..." is not supported.
            var statusList = new List<Status>();
            foreach (var item in result)
            {
                item.status.TranslatedValue = item.TranslatedValue;
                statusList.Add(item.status);
            }

            stopwatch.Stop();
            this.Logger.Info(string.Format("GetStatusInformation took {0} ms.", stopwatch.ElapsedMilliseconds));

            return statusList;
        }
    }
}
