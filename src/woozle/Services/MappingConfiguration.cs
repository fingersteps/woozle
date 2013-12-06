using System.Linq;
using AutoMapper;
using Woozle.Core.Model.UserSearch;
using Woozle.Core.Services.Stack.ServiceModel;
using Woozle.Core.Services.Stack.ServiceModel.Authority;
using Woozle.Core.Services.Stack.ServiceModel.LocationManagement;
using Woozle.Core.Services.Stack.ServiceModel.Mandator;
using Woozle.Core.Services.Stack.ServiceModel.ModuleManagement;
using Woozle.Core.Services.Stack.ServiceModel.Person;
using Woozle.Core.Services.Stack.ServiceModel.Settings;
using Woozle.Core.Services.Stack.ServiceModel.Translation;
using Woozle.Core.Services.Stack.ServiceModel.UserManagement;
using UserSearchResult = Woozle.Core.Services.Stack.ServiceModel.UserManagement.UserSearchResult;

namespace Woozle.Core.Services.Stack.ServiceWoozle.Model.Mapping
{
    public static class MappingConfiguration
    {
        public static void Configure()
        {
            Mapper.Configuration.AllowNullDestinationValues = true;

            Mapper.CreateMap<City, Woozle.Model.City>()
                  .ForMember(dest => dest.Mandators, opt => opt.Ignore())
                  .ForMember(dest => dest.People, opt => opt.Ignore())
                  .ForMember(dest => dest.Locations, opt => opt.Ignore());
            Mapper.CreateMap<Woozle.Model.City, City>();

            Mapper.CreateMap<Country, Woozle.Model.Country>();
            Mapper.CreateMap<Woozle.Model.Country, Country>();

   
            Mapper.CreateMap<Function, Woozle.Model.Function>()
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Woozle.Model.Function, Function>();

            Mapper.CreateMap<FunctionPermission, Woozle.Model.FunctionPermission>()
                  .ForMember(dest => dest.MandatorRoles, opt => opt.Ignore());
            Mapper.CreateMap<Woozle.Model.FunctionPermission, FunctionPermission>();

            Mapper.CreateMap<Language, Woozle.Model.Language>()
                  .ForMember(dest => dest.TranslationItems, opt => opt.Ignore())
                  .ForMember(dest => dest.Users, opt => opt.Ignore());
            Mapper.CreateMap<Woozle.Model.Language, Language>();

            Mapper.CreateMap<Location, Woozle.Model.Location>();
            Mapper.CreateMap<Woozle.Model.Location, Location>();

            Mapper.CreateMap<Mandator, Woozle.Model.Mandator>()
                  .ForMember(n => n.Modules, opt => opt.Ignore())
                  .ForMember(n => n.Locations, opt => opt.Ignore())
                  .ForMember(n => n.MandatorRoles, opt => opt.Ignore())
                  .ForMember(n => n.People, opt => opt.Ignore())
                  .ForMember(n => n.Settings, opt => opt.Ignore());

            Mapper.CreateMap<Woozle.Model.Mandator, Mandator>();


            Mapper.CreateMap<MandatorRole, Woozle.Model.MandatorRole>()
                  .ForMember(dest => dest.UserMandatorRoles, opt => opt.Ignore())
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Woozle.Model.MandatorRole, MandatorRole>();

            Mapper.CreateMap<MandatorGroup, Woozle.Model.MandatorGroup>()
                  .ForMember(dest => dest.Mandators, opt => opt.Ignore());
            Mapper.CreateMap<Woozle.Model.MandatorGroup, MandatorGroup>();

            Mapper.CreateMap<Module, Woozle.Model.Module>()
                  .ForMember(dest => dest.Mandators, opt => opt.Ignore())
                  .ForMember(dest => dest.Functions, opt => opt.Ignore());
            Mapper.CreateMap<Woozle.Model.Module, Module>();

            Mapper.CreateMap<ModuleGroup, Woozle.Model.ModuleGroup>();
            Mapper.CreateMap<Woozle.Model.ModuleGroup, ModuleGroup>();

            Mapper.CreateMap<Permission, Woozle.Model.Permission>()
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Woozle.Model.Permission, Permission>();

            Mapper.CreateMap<Person, Woozle.Model.Person>();
            Mapper.CreateMap<Woozle.Model.Person, Person>();

            Mapper.CreateMap<Role, Woozle.Model.Role>()
                  .ForMember(dest => dest.MandatorRoles, opt => opt.Ignore());
            Mapper.CreateMap<Woozle.Model.Role, Role>();

            Mapper.CreateMap<SaveResult<User>, Woozle.Core.Model.Validation.Creation.SaveResult<Woozle.Model.User>>();
            Mapper.CreateMap<Woozle.Core.Model.Validation.Creation.ISaveResult<Woozle.Model.User>, SaveResult<User>>();

            Mapper.CreateMap<Setting, Woozle.Model.Setting>();
            Mapper.CreateMap<Woozle.Model.Setting, Setting>();

            Mapper.CreateMap<Translation, Woozle.Model.Translation>()
                  .ForMember(n => n.Countries, opt => opt.Ignore())
                  .ForMember(n => n.Functions, opt => opt.Ignore())
                  .ForMember(n => n.Modules, opt => opt.Ignore())
                  .ForMember(n => n.Status, opt => opt.Ignore())
                  .ForMember(n => n.TranslationItems, opt => opt.Ignore());

            Mapper.CreateMap<Woozle.Model.Translation, Translation>();

            Mapper.CreateMap<TranslationItem, Woozle.Model.TranslationItem>();
            Mapper.CreateMap<Woozle.Model.TranslationItem, TranslationItem>();

            Mapper.CreateMap<User, Woozle.Model.User>();
            Mapper.CreateMap<Woozle.Model.User, User>();

            Mapper.CreateMap<UserMandatorRole, Woozle.Model.UserMandatorRole>();
            Mapper.CreateMap<Woozle.Model.UserMandatorRole, UserMandatorRole>()
                  .ForMember(dest => dest.User, opt => opt.Ignore());

            Mapper.CreateMap<Users, UserSearchCriteria>();
            Mapper.CreateMap<UserSearchCriteria, Users>();

            Mapper.CreateMap<UserSearchResult, Woozle.Core.Model.UserSearch.UserSearchResult>();
            Mapper.CreateMap<Woozle.Core.Model.UserSearch.UserSearchResult, UserSearchResult>();

            Mapper.CreateMap<Locations, Woozle.Model.Location>();
            Mapper.CreateMap<Woozle.Model.Location, Locations>();

            Mapper.CreateMap<Location, Woozle.Model.Location>();
            Mapper.CreateMap<Woozle.Model.Location, Location>();

            Mapper.CreateMap<SaveResult<Location>, Woozle.Core.Model.Validation.Creation.SaveResult<Woozle.Model.Location>>();
            Mapper.CreateMap<Woozle.Core.Model.Validation.Creation.ISaveResult<Woozle.Model.Location>, SaveResult<Location>>();

            Mapper.CreateMap<SaveResult<Setting>, Woozle.Core.Model.Validation.Creation.SaveResult<Woozle.Model.Setting>>();
            Mapper.CreateMap<Woozle.Core.Model.Validation.Creation.ISaveResult<Woozle.Model.Setting>, SaveResult<Setting>>();

            Mapper.CreateMap<SaveResult<Mandator>, Woozle.Core.Model.Validation.Creation.SaveResult<Woozle.Model.Mandator>>();
            Mapper.CreateMap<Woozle.Core.Model.Validation.Creation.ISaveResult<Woozle.Model.Mandator>, SaveResult<Mandator>>();

            Mapper.CreateMap<ModuleForMandator, Woozle.Core.Model.ModulePermissions.ModuleForMandator>();
            Mapper.CreateMap<Woozle.Core.Model.ModulePermissions.ModuleForMandator, ModuleForMandator>();

            Mapper.CreateMap<ChangedModulePermission, Woozle.Core.Model.ModulePermissions.ChangedModulePermission>();
            Mapper.CreateMap<Woozle.Core.Model.ModulePermissions.ChangedModulePermission, ChangedModulePermission>();

            Mapper.CreateMap<ModulePermissionsResult, Woozle.Core.Model.ModulePermissions.ModulePermissionsResult>();
            Mapper.CreateMap<Woozle.Core.Model.ModulePermissions.ModulePermissionsResult, ModulePermissionsResult>();
        }

        private class ModuleTranslatedValueResolver : ValueResolver<Woozle.Core.Model.ModulePermissions.ModuleForMandator, string>
        {
            protected override string ResolveCore(Woozle.Core.Model.ModulePermissions.ModuleForMandator source)
            {
                return GetTranslation(source.Translation);
            }
        }

        private class FunctionTranslatedValueResolver : ValueResolver<Woozle.Model.Function, string>
        {
            protected override string ResolveCore(Woozle.Model.Function source)
            {
                return GetTranslation(source.Translation);
            }
        }

        private static string GetTranslation(Woozle.Model.Translation translation)
        {
            var translationItem = translation.TranslationItems.FirstOrDefault();
            return translationItem != null ? translationItem.Description : translation.DefaultDescription;
        }
    }
}
