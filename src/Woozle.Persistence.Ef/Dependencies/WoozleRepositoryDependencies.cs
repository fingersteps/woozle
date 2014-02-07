using ServiceStack.WebHost.Endpoints;
using Woozle.Model;
using Woozle.Persistence.Ef.Repository;

namespace Woozle.Persistence.Ef.Dependencies
{
    /// <summary>
    /// Repository Dependencies
    /// </summary>
    public class WoozleRepositoryDependencies
    {
        public void Register(IAppHost container)
        {
            container.RegisterAs<UserRepository, IUserRepository>();
            container.RegisterAs<UserRepository, IRepository<User>>();
            container.RegisterAs<ModuleRepository, IModuleRepository>();
            container.RegisterAs<StatusFieldRepository, IStatusFieldRepository>();
            container.RegisterAs<StatusRepository, IRepository<Status>>();
            container.RegisterAs<ModuleRepository, IRepository<Module>>();
            container.RegisterAs<CityRepository, IRepository<City>>();
            container.RegisterAs<CountryRepository, IRepository<Country>>();
            container.RegisterAs<LanguageRepository, IRepository<Language>>();
            container.RegisterAs<MandatorRepository, IRepository<Mandator>>();
            container.RegisterAs<RoleRepository, IRepository<Role>>();
            container.RegisterAs<UserMandatorRoleRepository, IRepository<UserMandatorRole>>();
            container.RegisterAs<MandatorRoleRepository, IRepository<MandatorRole>>();
            container.RegisterAs<FunctionPermissionRepository, IRepository<FunctionPermission>>();
            container.RegisterAs<TranslationRepository, IRepository<Translation>>();
            container.RegisterAs<PersonRepository, IRepository<Person>>();
            container.RegisterAs<TranslationItemRepository, IRepository<TranslationItem>>();
            container.RegisterAs<LocationRepository, IRepository<Location>>();
            container.RegisterAs<SettingRepository, IRepository<Setting>>();
            container.RegisterAs<MandatorGroupRepository, IRepository<MandatorGroup>>();
            container.RegisterAs<ExternalSystemRepository, IExternalSystemRepository>();
            container.RegisterAs<ExternalSystemTypeRepository, IRepository<ExternalSystemType>>();
        }
    }
}
