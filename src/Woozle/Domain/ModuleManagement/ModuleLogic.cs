using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Woozle.Domain.PermissionManagement;
using Woozle.Model;
using Woozle.Model.ModulePermissions;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.ModuleManagement
{
    /// <summary>
    /// Contains the logic to manage modules.
    /// </summary>
    public class ModuleLogic : IModuleLogic
    {
        private readonly IModuleRepository moduleRepository;

        public ModuleLogic(IPermissionManager permissionManager, IModuleRepository moduleRepository)
        {
            PermissionManager = permissionManager;
            this.moduleRepository = moduleRepository;
        }

        public IPermissionManager PermissionManager { get; set; }


        /// <summary>
        /// <see cref="IModuleLogic.GetModulesByMandator"/>
        /// </summary>
        public IList<ModuleForMandator> GetModulesByMandator(SessionData sessionData)
        {
            Trace.TraceInformation("Get modules of the mandator");
            var modules = this.moduleRepository.FindModulesForMandator(sessionData);

            CheckFunctionPermissions(sessionData, modules);
            FilterEmptyModules(modules);

            return modules;
        }

        private void CheckFunctionPermissions(SessionData sessionData, IEnumerable<ModuleForMandator> modules)
        {
            foreach (ModuleForMandator module in modules)
            {
                var authorizedFunctions = new ObservableCollection<Function>();
                foreach (Function function in module.Functions)
                {
                    if (PermissionManager.HasPermission(sessionData, function.LogicalId, Permissions.PERMISSION_FUNCTION))
                    {
                        authorizedFunctions.Add(function);
                    }
                }

                //Show only functions which are authorized for the given session
                module.Functions = authorizedFunctions;
            }
        }

        private void FilterEmptyModules(List<ModuleForMandator> modules)
        {
            foreach (ModuleForMandator emptyModule in modules.Where(n => n.Functions.Count == 0).ToList())
            {
                var toRemove = modules.First(n => n.Id == emptyModule.Id);
                modules.Remove(toRemove);
            }
        }

        public IList<ModulePermissionsResult> FindModulePermissions(Role role, SessionData sessionData)
        {
            var modules = this.moduleRepository.FindModulePermissions(role, sessionData);
            return modules;
        }
    }
}
