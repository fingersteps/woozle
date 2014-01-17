using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Woozle.Model;
using Woozle.Model.ModulePermissions;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence.Ef.Repository
{
    public partial class ModuleRepository : IModuleRepository
    {
        public IList<ModulePermissionsResult> FindModulePermissions(Role role, Session session)
        {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var selection = from module in Context.Get<Module>(session)
                             from function in module.Functions
                             from functionPermission in function.FunctionPermissions
                             from mandatorRoleFunctionPermission in functionPermission.MandatorRoles.DefaultIfEmpty() 
                             join permission in Context.Get<Permission>(session) on functionPermission.PermissionId equals permission.Id
                             group mandatorRoleFunctionPermission by new {functionPermissionId = functionPermission.Id, moduleId = module.Id, moduleName = module.Name, functionName = function.Name, permissionName = permission.Name } into g
                             select new ModulePermissionsResult()
                             {
                                 FunctionPermissionId = g.Key.functionPermissionId,
                                 ModuleId = g.Key.moduleId,
                                 ModuleName = g.Key.moduleName,
                                 FunctionName = g.Key.functionName,
                                 PermissionName = g.Key.permissionName,

                                 //Check if there is a record in table MandatorRoleFunctionPermission and set HasPermission to true if there is one
                                 HasPermission = g.Count(n => n != null && n.RoleId == role.Id && n.MandId == session.SessionObject.Mandator.Id) > 0
                             };
                var result = selection.ToList();

                stopwatch.Stop();
                this.Logger.Info(string.Format("FindModulePermissions took {0} ms.", stopwatch.ElapsedMilliseconds));

                return result;
        }

        public List<ModuleForMandator> FindModulesForMandator(Session session)
        {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var selection = from module in Context.Get<Module>(session)
                                join moduleTranslation in Context.Get<Translation>(session) on module.TranslationId equals moduleTranslation.Id
                                from mandator in module.Mandators
                                from function in module.Functions
                                join functionTranslation in Context.Get<Translation>(session) on function.TranslationId equals functionTranslation.Id
                                from moduleTranslationItem in module.Translation.TranslationItems.DefaultIfEmpty()
                                from functionTranslationItem in function.Translation.TranslationItems.DefaultIfEmpty()
                                where mandator.Id == session.SessionObject.Mandator.Id && 
                                (moduleTranslationItem == null || moduleTranslationItem.LanguageId == session.SessionObject.User.LanguageId) &&
                                (functionTranslationItem == null || functionTranslationItem.LanguageId == session.SessionObject.User.LanguageId)
                                orderby module.Sequence, module.Id, function.Sequence, function.Id
                                select new
                                {
                                    Module = module,
                                    ModuleTranslation = moduleTranslation,
                                    Function = function,
                                    FunctionTranslation = functionTranslation,
                                    ModuleTranslationItem = moduleTranslationItem,
                                    FunctionTranslationItem = functionTranslationItem
                                };
                var result = selection.ToList();

                var modules = new List<ModuleForMandator>();
                foreach(var item in result)
                {
                    var module = modules.FirstOrDefault(n => n.Id == item.Module.Id);
                    if(module == null)
                    {
                        module = new ModuleForMandator()
                        {
                            Id = item.Module.Id,
                            Name = item.Module.Name,
                            Translation = item.ModuleTranslation, 
                            Icon = item.Module.Icon, Functions = new ObservableCollection<Function>()};
                        module.Translation.TranslationItems = new ObservableCollection<TranslationItem>();
                        if (item.ModuleTranslationItem != null)
                        {
                            module.Translation.TranslationItems.Add(item.ModuleTranslationItem);
                        }
                        modules.Add(module);
                    }


                    var function = module.Functions.FirstOrDefault(n => n.Id == item.Function.Id);
                    if (function == null)
                    {
                        function = item.Function;
                        function.Translation = item.FunctionTranslation;
                        function.Translation.TranslationItems = new ObservableCollection<TranslationItem>();
                        if (item.ModuleTranslationItem != null)
                        {
                            function.Translation.TranslationItems.Add(item.FunctionTranslationItem);
                        }
                        module.Functions.Add(function);
                    }
                }

                stopwatch.Stop();
                this.Logger.Info(string.Format("FindModulesForMandator took {0} ms.", stopwatch.ElapsedMilliseconds));

                return modules;
        }
    }
}
