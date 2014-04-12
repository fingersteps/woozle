using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
using Woozle.Model.Values;

namespace Woozle.Persistence.Ef.Repository
{
    public partial class UserRepository : IUserRepository
    {
        #region IUserRepository Members

        public IList<UserSearchResult> FindByUserCriteria(UserSearchCriteria criteria, SessionData sessionData)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var selection = from u in Context.Get<User>(sessionData)
                            from userMandatorRole in u.UserMandatorRoles.DefaultIfEmpty() 
                            where u.Username.Contains(criteria.Username) &&
                                  (u.FirstName == null || u.FirstName.Contains(criteria.Firstname)) &&
                                  (u.LastName == null || u.LastName.Contains(criteria.Lastname)) &&
                                  userMandatorRole != null &&
                                  (userMandatorRole.MandatorRole.Mandator.Id == sessionData.Mandator.Id
                                      || (sessionData.Mandator.MandatorGroupId.HasValue && userMandatorRole.MandatorRole.Mandator.MandatorGroupId ==
                                      sessionData.Mandator.MandatorGroupId))
                            group userMandatorRole by new { u.Id, u.Username, u.FirstName, u.LastName, u.LastLogin, u.Language, u.FlagActiveStatusId } into g
                            select new UserSearchResult
                                {
                                    Id = g.Key.Id,
                                    Username = g.Key.Username,
                                    Firstname = g.Key.FirstName,
                                    Lastname = g.Key.LastName,
                                    LastLogin = g.Key.LastLogin,
                                    Language = g.Key.Language,
                                    FlagActiveStatusId = g.Key.FlagActiveStatusId
                                };

            var result = selection.ToList();

            stopwatch.Stop();
            Trace.TraceInformation("FindByUserCriteria took {0} ms.", stopwatch.ElapsedMilliseconds);

            return result;
        }

        public UserSearchForLoginResult FindForLogin(string username)
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var selection =
                    from u in Context.Get<User>(null)
                    from userMandatorRole in u.UserMandatorRoles
                    join language in Context.Get<Language>(null) on u.LanguageId equals language.Id
                    where u.Username == username &&
                          u.Status.Value == StatusConstants.ACTIVE
                    select new
                        {
                            User = u,
                            Language = language,
                            Mandators = u.UserMandatorRoles.Select(n => n.MandatorRole.Mandator)
                        };

                var resultSet = selection.FirstOrDefault();

                UserSearchForLoginResult result = null;
                if (resultSet != null)
                {
                    resultSet.User.Language = resultSet.Language;
                    result = new UserSearchForLoginResult() {User = resultSet.User, Mandators = resultSet.Mandators.Distinct(new MandatorComparator())};
                }

                stopwatch.Stop();

                Trace.TraceInformation(string.Format("FindForLogin took {0} ms.", stopwatch.ElapsedMilliseconds));

                return result;
            }
            catch (Exception e)
            {
                Trace.TraceError("Error while searching for user to authenticate", e);
                throw new PersistenceException(PersistenceOperation.SEARCH, e);
            }
        }

        public User LoadUser(int id, SessionData sessionData)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var selection = from user in Context.Get<User>(sessionData)
                            from userMandatorRole in user.UserMandatorRoles.DefaultIfEmpty()
                            join language in Context.Get<Language>(sessionData) on user.LanguageId equals language.Id
                            join status in Context.Get<Status>(sessionData) on user.FlagActiveStatusId equals status.Id
                            let statusTranslationItem = status.Translation.TranslationItems.FirstOrDefault(
                                n => n.LanguageId == sessionData.User.LanguageId)
                            where user.Id == id
                            select new
                                {
                                    user,
                                    userMandatorRoles = user.UserMandatorRoles,
                                    mandatorRole = userMandatorRole.MandatorRole,
                                    role = userMandatorRole.MandatorRole.Role,
                                    mandator = userMandatorRole.MandatorRole.Mandator,
                                    language,
                                    status,
                                    TranslatedStatusValue =
                                statusTranslationItem != null
                                    ? statusTranslationItem.Description
                                    : status.Translation.DefaultDescription,
                                };
            //Get all to load all joined entities (all roles, mandators for the current user)
            var result = selection.ToList().First();

            var userResult = result.user;
            userResult.UserMandatorRoles = result.userMandatorRoles;
            userResult.Status = result.status;
            userResult.Status.TranslatedValue = result.TranslatedStatusValue;

            userResult.Language = result.language;

            stopwatch.Stop();

            Trace.TraceInformation("LoadUser took {0} ms.", stopwatch.ElapsedMilliseconds);

            return userResult;
        }

        #endregion
    }
}
