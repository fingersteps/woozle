using System.Collections.Generic;
using System.Linq;
using Woozle.Core.BusinessLogic.ModuleManagement;
using Woozle.Core.BusinessLogic.PermissionManagement;
using Woozle.Core.Common.PermissionManagement;
using Woozle.Core.Model;
using Woozle.Core.Model.ModulePermissions;
using Woozle.Core.Model.SessionHandling;
using Woozle.Core.Persistence.Repository;
using Woozle.Model;
using ModulePermissionsResult = Woozle.Core.Model.ModulePermissions.ModulePermissionsResult;

namespace Woozle.Core.BusinessLogic.Impl.ModuleManagement
{
    /// <summary>
    /// Contains the logic to manage modules.
    /// </summary>
    public class ModuleLogic : AbstractLogic, IModuleLogic
    {
        public IPermissionManager PermissionManager { get; set; }
        private readonly IModuleRepository moduleRepository;

        public ModuleLogic(IPermissionManager permissionManager, IModuleRepository moduleRepository)
        {
            PermissionManager = permissionManager;
            this.moduleRepository = moduleRepository;
        }

        #region IModuleLogic Members

        /// <summary>
        /// <see cref="IModuleLogic.GetModulesByMandator"/>
        /// </summary>
        public IList<ModuleForMandator> GetModulesByMandator(Session session)
        {
            var modules = this.moduleRepository.FindModulesForMandator(session);

            CheckFunctionPermissions(session, modules);
            FilterEmptyModules(modules);

            return modules;
        }

        private void CheckFunctionPermissions(Session session, IEnumerable<ModuleForMandator> modules)
        {
            foreach (ModuleForMandator module in modules)
            {
                var authorizedFunctions = new FixupCollection<Function>();
                foreach (Function function in module.Functions)
                {
                    if (PermissionManager.HasPermission(session.SessionObject, function.LogicalId, Permissions.PERMISSION_FUNCTION))
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

        public IList<ModulePermissionsResult> FindModulePermissions(Role role, Session session)
        {
            var modules = this.moduleRepository.FindModulePermissions(role, session);
            return modules;
        }

        #endregion
    }
}
