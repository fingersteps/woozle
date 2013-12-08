using Funq;
using Woozle.Core.Model;
using Woozle.Core.Persistence.Repository;
using Woozle.Core.Persistence.Repository.Impl;
using Woozle.Model;
using Woozle.Persistence.Repository;

namespace Woozle.Core.Dependencies
{
    public class WoozleRepositoryDependencies : IWoozleDependency
    {
        public void Register(Container container)
        {
            container.RegisterAutoWiredAs<UserRepository, IUserRepository>();
            container.RegisterAutoWiredAs<UserRepository, IRepository<User>>();
            container.RegisterAutoWiredAs<ModuleRepository, IModuleRepository>();
            container.RegisterAutoWiredAs<StatusFieldRepository, IStatusFieldRepository>();
            container.RegisterAutoWiredAs<StatusRepository, IRepository<Status>>();
            container.RegisterAutoWiredAs<ModuleRepository, IRepository<Module>>();
            container.RegisterAutoWiredAs<CityRepository, IRepository<City>>();
            container.RegisterAutoWiredAs<CountryRepository, IRepository<Country>>();
            container.RegisterAutoWiredAs<LanguageRepository, IRepository<Language>>();
            container.RegisterAutoWiredAs<MandatorRepository, IRepository<Mandator>>();
            container.RegisterAutoWiredAs<RoleRepository, IRepository<Role>>();
            container.RegisterAutoWiredAs<MandatorRoleRepository, IRepository<MandatorRole>>();
            container.RegisterAutoWiredAs<FunctionPermissionRepository, IRepository<FunctionPermission>>();
            container.RegisterAutoWiredAs<TranslationRepository, IRepository<Translation>>();
            container.RegisterAutoWiredAs<PersonRepository, IRepository<Person>>();
            container.RegisterAutoWiredAs<TranslationItemRepository, IRepository<TranslationItem>>();
            container.RegisterAutoWiredAs<LocationRepository, IRepository<Location>>();
            container.RegisterAutoWiredAs<SettingRepository, IRepository<Setting>>();
            container.RegisterAutoWiredAs<MandatorGroupRepository, IRepository<MandatorGroup>>();
        }
    }
}
