#region

using AutoMapper;
using Woozle.Model;
using Woozle.Model.UserSearch;
using Woozle.Model.Validation.Creation;
using Woozle.Service.UserManagement;
using Woozle.Services.Authority;
using Woozle.Services.Location;
using Woozle.Services.Mandator;
using Woozle.Services.Modules;
using Woozle.Services.Modules.Settings;
using Woozle.Services.UserManagement;
using City = Woozle.Services.Location.City;
using Function = Woozle.Model.Function;
using Language = Woozle.Services.Location.Language;
using MandatorRole = Woozle.Services.Authority.MandatorRole;
using ModuleForMandator = Woozle.Model.ModulePermissions.ModuleForMandator;
using Role = Woozle.Services.Authority.Role;
using Translation = Woozle.Model.Translation;

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


            Mapper.CreateMap<FunctionDto, Function>()
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Function, FunctionDto>();

            Mapper.CreateMap<FunctionPermissionDto, FunctionPermission>()
                  .ForMember(dest => dest.MandatorRoles, opt => opt.Ignore());
            Mapper.CreateMap<FunctionPermission, FunctionPermissionDto>();

            Mapper.CreateMap<Language, Model.Language>()
                  .ForMember(dest => dest.TranslationItems, opt => opt.Ignore())
                  .ForMember(dest => dest.Users, opt => opt.Ignore());
            Mapper.CreateMap<Model.Language, Language>();

            Mapper.CreateMap<LocationDto, Model.Location>();
            Mapper.CreateMap<Model.Location, LocationDto>();

            Mapper.CreateMap<MandatorDto, Model.Mandator>()
                  .ForMember(n => n.Modules, opt => opt.Ignore())
                  .ForMember(n => n.Locations, opt => opt.Ignore())
                  .ForMember(n => n.MandatorRoles, opt => opt.Ignore())
                  .ForMember(n => n.People, opt => opt.Ignore())
                  .ForMember(n => n.Settings, opt => opt.Ignore());

            Mapper.CreateMap<Model.Mandator, MandatorDto>();


            Mapper.CreateMap<MandatorRole, Model.MandatorRole>()
                  .ForMember(dest => dest.UserMandatorRoles, opt => opt.Ignore())
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Model.MandatorRole, MandatorRole>();

            Mapper.CreateMap<MandatorGroupDto, MandatorGroup>()
                  .ForMember(dest => dest.Mandators, opt => opt.Ignore());
            Mapper.CreateMap<MandatorGroup, MandatorGroupDto>();

            Mapper.CreateMap<ModuleDto, Module>()
                  .ForMember(dest => dest.Mandators, opt => opt.Ignore())
                  .ForMember(dest => dest.Functions, opt => opt.Ignore());
            Mapper.CreateMap<Module, ModuleDto>();

            Mapper.CreateMap<ModuleGroupDto, ModuleGroup>();
            Mapper.CreateMap<ModuleGroup, ModuleGroupDto>();

            Mapper.CreateMap<PermissionDto, Permission>()
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Permission, PermissionDto>();

            Mapper.CreateMap<PersonDto, Person>();
            Mapper.CreateMap<Person, PersonDto>();

            Mapper.CreateMap<Role, Model.Role>()
                  .ForMember(dest => dest.MandatorRoles, opt => opt.Ignore());
            Mapper.CreateMap<Model.Role, Role>();

            Mapper
                .CreateMap
                <SaveResultDto<UserDto>, Model.Validation.Creation.SaveResult<User>>();
            Mapper.CreateMap<ISaveResult<User>, SaveResultDto<UserDto>>();

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

            Mapper.CreateMap<UsersDto, UserSearchCriteria>();
            Mapper.CreateMap<UserSearchCriteria, UsersDto>();

            Mapper.CreateMap<UserSearchResultDto, UserSearchResult>();
            Mapper.CreateMap<UserSearchResult, UserSearchResultDto>();

            Mapper.CreateMap<Locations, Model.Location>();
            Mapper.CreateMap<Model.Location, Locations>();

            Mapper.CreateMap<LocationDto, Model.Location>();
            Mapper.CreateMap<Model.Location, LocationDto>();

            Mapper
                .CreateMap
                <SaveResultDto<LocationDto>,
                    Model.Validation.Creation.SaveResult<Model.Location>>();
            Mapper.CreateMap<ISaveResult<Model.Location>, SaveResultDto<LocationDto>>();

            Mapper
                .CreateMap
                <SaveResultDto<SettingDto>,
                    Model.Validation.Creation.SaveResult<Setting>>();
            Mapper.CreateMap<ISaveResult<Setting>, SaveResultDto<SettingDto>>();

            Mapper
                .CreateMap
                <SaveResultDto<MandatorDto>,
                    Model.Validation.Creation.SaveResult<Model.Mandator>>();
            Mapper.CreateMap<ISaveResult<Model.Mandator>, SaveResultDto<MandatorDto>>();

            Mapper.CreateMap<ModuleForMandatorDto, ModuleForMandator>();
            Mapper.CreateMap<ModuleForMandator, ModuleForMandatorDto>();

            Mapper.CreateMap<ChangedModulePermission, Model.ModulePermissions.ChangedModulePermission>();
            Mapper.CreateMap<Model.ModulePermissions.ChangedModulePermission, ChangedModulePermission>();

            Mapper.CreateMap<ModulePermissionsResult, Model.ModulePermissions.ModulePermissionsResult>();
            Mapper.CreateMap<Model.ModulePermissions.ModulePermissionsResult, ModulePermissionsResult>();
        }
    }
}