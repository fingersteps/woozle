using System.Linq;
using AutoMapper;
using Woozle.Model.UserSearch;
using Woozle.Model.Validation.Creation;
using Woozle.Service.UserManagement;
using Woozle.Services.Authority;
using Woozle.Services.Location;
using Woozle.Services.Mandator;
using Woozle.Services.Modules;
using Woozle.Services.Modules.Settings;
using Woozle.Services.UserManagement;
using UserSearchResult = Woozle.Services.UserManagement.UserSearchResult;

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

            Mapper.CreateMap<CountryDto, Woozle.Model.Country>();
            Mapper.CreateMap<Woozle.Model.Country, CountryDto>();

   
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

            Mapper.CreateMap<LocationDto, Woozle.Model.Location>();
            Mapper.CreateMap<Woozle.Model.Location, LocationDto>();

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

            Mapper.CreateMap<ModuleGroupDto, Woozle.Model.ModuleGroup>();
            Mapper.CreateMap<Woozle.Model.ModuleGroup, ModuleGroupDto>();

            Mapper.CreateMap<Permission, Woozle.Model.Permission>()
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Woozle.Model.Permission, Permission>();

            Mapper.CreateMap<PersonDto, Woozle.Model.Person>();
            Mapper.CreateMap<Woozle.Model.Person, PersonDto>();

            Mapper.CreateMap<Role, Woozle.Model.Role>()
                  .ForMember(dest => dest.MandatorRoles, opt => opt.Ignore());
            Mapper.CreateMap<Woozle.Model.Role, Role>();

            Mapper.CreateMap<Woozle.Services.SaveResult<UserDto>, Woozle.Model.Validation.Creation.SaveResult<Woozle.Model.User>>();
            Mapper.CreateMap<ISaveResult<Woozle.Model.User>, Woozle.Services.SaveResult<UserDto>>();

            Mapper.CreateMap<SettingDto, Woozle.Model.Setting>();
            Mapper.CreateMap<Woozle.Model.Setting, SettingDto>();

            Mapper.CreateMap<Translation, Woozle.Model.Translation>()
                  .ForMember(n => n.Countries, opt => opt.Ignore())
                  .ForMember(n => n.Functions, opt => opt.Ignore())
                  .ForMember(n => n.Modules, opt => opt.Ignore())
                  .ForMember(n => n.Status, opt => opt.Ignore())
                  .ForMember(n => n.TranslationItems, opt => opt.Ignore());

            Mapper.CreateMap<Woozle.Model.Translation, Translation>();

            Mapper.CreateMap<TranslationItemDto, Woozle.Model.TranslationItem>();
            Mapper.CreateMap<Woozle.Model.TranslationItem, TranslationItemDto>();

            Mapper.CreateMap<UserDto, Woozle.Model.User>();
            Mapper.CreateMap<Woozle.Model.User, UserDto>();

            Mapper.CreateMap<UserMandatorRoleDto, Woozle.Model.UserMandatorRole>();
            Mapper.CreateMap<Woozle.Model.UserMandatorRole, UserMandatorRoleDto>()
                  .ForMember(dest => dest.UserDto, opt => opt.Ignore());

            Mapper.CreateMap<Users, UserSearchCriteria>();
            Mapper.CreateMap<UserSearchCriteria, Users>();

            Mapper.CreateMap<UserSearchResult, Woozle.Model.UserSearch.UserSearchResult>();
            Mapper.CreateMap<Woozle.Model.UserSearch.UserSearchResult, UserSearchResult>();

            Mapper.CreateMap<Locations, Woozle.Model.Location>();
            Mapper.CreateMap<Woozle.Model.Location, Locations>();

            Mapper.CreateMap<LocationDto, Woozle.Model.Location>();
            Mapper.CreateMap<Woozle.Model.Location, LocationDto>();

            Mapper.CreateMap<Woozle.Services.SaveResult<LocationDto>, Woozle.Model.Validation.Creation.SaveResult<Woozle.Model.Location>>();
            Mapper.CreateMap<ISaveResult<Woozle.Model.Location>, Woozle.Services.SaveResult<LocationDto>>();

            Mapper.CreateMap<Woozle.Services.SaveResult<SettingDto>, Woozle.Model.Validation.Creation.SaveResult<Woozle.Model.Setting>>();
            Mapper.CreateMap<ISaveResult<Woozle.Model.Setting>, Woozle.Services.SaveResult<SettingDto>>();

            Mapper.CreateMap<Woozle.Services.SaveResult<Mandator>, Woozle.Model.Validation.Creation.SaveResult<Woozle.Model.Mandator>>();
            Mapper.CreateMap<ISaveResult<Woozle.Model.Mandator>, Woozle.Services.SaveResult<Mandator>>();

            Mapper.CreateMap<ModuleForMandator, Woozle.Model.ModulePermissions.ModuleForMandator>();
            Mapper.CreateMap<Woozle.Model.ModulePermissions.ModuleForMandator, ModuleForMandator>();

            Mapper.CreateMap<ChangedModulePermission, Woozle.Model.ModulePermissions.ChangedModulePermission>();
            Mapper.CreateMap<Woozle.Model.ModulePermissions.ChangedModulePermission, ChangedModulePermission>();

            Mapper.CreateMap<ModulePermissionsResult, Woozle.Model.ModulePermissions.ModulePermissionsResult>();
            Mapper.CreateMap<Woozle.Model.ModulePermissions.ModulePermissionsResult, ModulePermissionsResult>();
        }

        private class ModuleTranslatedValueResolver : ValueResolver<Woozle.Model.ModulePermissions.ModuleForMandator, string>
        {
            protected override string ResolveCore(Woozle.Model.ModulePermissions.ModuleForMandator source)
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
