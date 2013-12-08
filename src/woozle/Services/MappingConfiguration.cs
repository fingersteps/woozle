#region

using AutoMapper;
using Woozle.Model;
using Woozle.Model.UserSearch;
using Woozle.Model.Validation.Creation;
using Woozle.Service.UserManagement;
using Woozle.Services.Authority;
using Woozle.Services.Location;
using Woozle.Services.Modules;
using Woozle.Services.Modules.Settings;
using Woozle.Services.UserManagement;
using City = Woozle.Services.Location.City;
using Function = Woozle.Model.Function;
using FunctionPermission = Woozle.Services.Modules.FunctionPermission;
using Language = Woozle.Services.Location.Language;
using MandatorGroup = Woozle.Services.Mandator.MandatorGroup;
using MandatorRole = Woozle.Services.Authority.MandatorRole;
using Module = Woozle.Services.Modules.Module;
using ModuleForMandator = Woozle.Model.ModulePermissions.ModuleForMandator;
using Permission = Woozle.Services.Modules.Permission;
using Role = Woozle.Services.Authority.Role;
using Translation = Woozle.Model.Translation;
using UserSearchResult = Woozle.Services.UserManagement.UserSearchResult;

#endregion

namespace Woozle.Services
{
    public static class MappingConfiguration
    {
        public static void Configure()
        {
            Mapper.Configuration.AllowNullDestinationValues = true;

            Mapper.CreateMap<City, Model.City>()
                  .ForMember(dest => dest.Mandators, opt => opt.Ignore())
                  .ForMember(dest => dest.People, opt => opt.Ignore())
                  .ForMember(dest => dest.Locations, opt => opt.Ignore());
            Mapper.CreateMap<Model.City, City>();

            Mapper.CreateMap<CountryDto, Country>();
            Mapper.CreateMap<Country, CountryDto>();


            Mapper.CreateMap<Modules.Function, Function>()
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Function, Modules.Function>();

            Mapper.CreateMap<FunctionPermission, Model.FunctionPermission>()
                  .ForMember(dest => dest.MandatorRoles, opt => opt.Ignore());
            Mapper.CreateMap<Model.FunctionPermission, FunctionPermission>();

            Mapper.CreateMap<Language, Model.Language>()
                  .ForMember(dest => dest.TranslationItems, opt => opt.Ignore())
                  .ForMember(dest => dest.Users, opt => opt.Ignore());
            Mapper.CreateMap<Model.Language, Language>();

            Mapper.CreateMap<LocationDto, Model.Location>();
            Mapper.CreateMap<Model.Location, LocationDto>();

            Mapper.CreateMap<Mandator.Mandator, Model.Mandator>()
                  .ForMember(n => n.Modules, opt => opt.Ignore())
                  .ForMember(n => n.Locations, opt => opt.Ignore())
                  .ForMember(n => n.MandatorRoles, opt => opt.Ignore())
                  .ForMember(n => n.People, opt => opt.Ignore())
                  .ForMember(n => n.Settings, opt => opt.Ignore());

            Mapper.CreateMap<Model.Mandator, Mandator.Mandator>();


            Mapper.CreateMap<MandatorRole, Model.MandatorRole>()
                  .ForMember(dest => dest.UserMandatorRoles, opt => opt.Ignore())
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Model.MandatorRole, MandatorRole>();

            Mapper.CreateMap<MandatorGroup, Model.MandatorGroup>()
                  .ForMember(dest => dest.Mandators, opt => opt.Ignore());
            Mapper.CreateMap<Model.MandatorGroup, MandatorGroup>();

            Mapper.CreateMap<Module, Model.Module>()
                  .ForMember(dest => dest.Mandators, opt => opt.Ignore())
                  .ForMember(dest => dest.Functions, opt => opt.Ignore());
            Mapper.CreateMap<Model.Module, Module>();

            Mapper.CreateMap<ModuleGroupDto, ModuleGroup>();
            Mapper.CreateMap<ModuleGroup, ModuleGroupDto>();

            Mapper.CreateMap<Permission, Model.Permission>()
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Model.Permission, Permission>();

            Mapper.CreateMap<PersonDto, Person>();
            Mapper.CreateMap<Person, PersonDto>();

            Mapper.CreateMap<Role, Model.Role>()
                  .ForMember(dest => dest.MandatorRoles, opt => opt.Ignore());
            Mapper.CreateMap<Model.Role, Role>();

            Mapper
                .CreateMap
                <SaveResult<UserDto>, Model.Validation.Creation.SaveResult<User>>();
            Mapper.CreateMap<ISaveResult<User>, SaveResult<UserDto>>();

            Mapper.CreateMap<SettingDto, Setting>();
            Mapper.CreateMap<Setting, SettingDto>();

            Mapper.CreateMap<Location.Translation, Translation>()
                  .ForMember(n => n.Countries, opt => opt.Ignore())
                  .ForMember(n => n.Functions, opt => opt.Ignore())
                  .ForMember(n => n.Modules, opt => opt.Ignore())
                  .ForMember(n => n.Status, opt => opt.Ignore())
                  .ForMember(n => n.TranslationItems, opt => opt.Ignore());

            Mapper.CreateMap<Translation, Location.Translation>();

            Mapper.CreateMap<TranslationItemDto, TranslationItem>();
            Mapper.CreateMap<TranslationItem, TranslationItemDto>();

            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<User, UserDto>();

            Mapper.CreateMap<UserMandatorRoleDto, UserMandatorRole>();
            Mapper.CreateMap<UserMandatorRole, UserMandatorRoleDto>()
                  .ForMember(dest => dest.UserDto, opt => opt.Ignore());

            Mapper.CreateMap<Users, UserSearchCriteria>();
            Mapper.CreateMap<UserSearchCriteria, Users>();

            Mapper.CreateMap<UserSearchResult, Model.UserSearch.UserSearchResult>();
            Mapper.CreateMap<Model.UserSearch.UserSearchResult, UserSearchResult>();

            Mapper.CreateMap<Locations, Model.Location>();
            Mapper.CreateMap<Model.Location, Locations>();

            Mapper.CreateMap<LocationDto, Model.Location>();
            Mapper.CreateMap<Model.Location, LocationDto>();

            Mapper
                .CreateMap
                <SaveResult<LocationDto>,
                    Model.Validation.Creation.SaveResult<Model.Location>>();
            Mapper.CreateMap<ISaveResult<Model.Location>, SaveResult<LocationDto>>();

            Mapper
                .CreateMap
                <SaveResult<SettingDto>,
                    Model.Validation.Creation.SaveResult<Setting>>();
            Mapper.CreateMap<ISaveResult<Setting>, SaveResult<SettingDto>>();

            Mapper
                .CreateMap
                <SaveResult<Mandator.Mandator>,
                    Model.Validation.Creation.SaveResult<Model.Mandator>>();
            Mapper.CreateMap<ISaveResult<Model.Mandator>, SaveResult<Mandator.Mandator>>();

            Mapper.CreateMap<Modules.ModuleForMandator, ModuleForMandator>();
            Mapper.CreateMap<ModuleForMandator, Modules.ModuleForMandator>();

            Mapper.CreateMap<ChangedModulePermission, Model.ModulePermissions.ChangedModulePermission>();
            Mapper.CreateMap<Model.ModulePermissions.ChangedModulePermission, ChangedModulePermission>();

            Mapper.CreateMap<ModulePermissionsResult, Model.ModulePermissions.ModulePermissionsResult>();
            Mapper.CreateMap<Model.ModulePermissions.ModulePermissionsResult, ModulePermissionsResult>();
        }
    }
}