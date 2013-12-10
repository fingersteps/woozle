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
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore())
                  .ForMember(dest => dest.Module, opt => opt.Ignore());

            Mapper.CreateMap<Function, FunctionDto>()
                  .ForMember(dest => dest.ModuleDto, opt => opt.Ignore());

            Mapper.CreateMap<FunctionPermissionDto, FunctionPermission>()
                  .ForMember(dest => dest.MandatorRoles, opt => opt.Ignore())
                  .ForMember(dest => dest.Function, opt => opt.Ignore())
                  .ForMember(dest => dest.Permission, opt => opt.Ignore());

            Mapper.CreateMap<FunctionPermission, FunctionPermissionDto>()
                  .ForMember(dest => dest.FunctionDto, opt => opt.Ignore())
                  .ForMember(dest => dest.FunctionDto, opt => opt.Ignore())
                  .ForMember(dest => dest.PermissionDto, opt => opt.Ignore());

            Mapper.CreateMap<Language, Model.Language>()
                  .ForMember(dest => dest.TranslationItems, opt => opt.Ignore())
                  .ForMember(dest => dest.Users, opt => opt.Ignore());
            Mapper.CreateMap<Model.Language, Language>();

            Mapper.CreateMap<LocationDto, Model.Location>()
                  .ForMember(dest => dest.Mandator, opt => opt.Ignore());

            Mapper.CreateMap<Model.Location, LocationDto>()
                  .ForMember(dest => dest.MandatorDto, opt => opt.Ignore());

            Mapper.CreateMap<MandatorDto, Model.Mandator>()
                  .ForMember(n => n.Modules, opt => opt.Ignore())
                  .ForMember(n => n.Locations, opt => opt.Ignore())
                  .ForMember(n => n.MandatorRoles, opt => opt.Ignore())
                  .ForMember(n => n.People, opt => opt.Ignore())
                  .ForMember(n => n.Settings, opt => opt.Ignore())
                  .ForMember(n => n.MandatorGroup, opt => opt.Ignore());

            Mapper.CreateMap<Model.Mandator, MandatorDto>()
                  .ForMember(n => n.MandatorGroupDto, opt => opt.Ignore());

            Mapper.CreateMap<MandatorRole, Model.MandatorRole>()
                  .ForMember(dest => dest.UserMandatorRoles, opt => opt.Ignore())
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore())
                  .ForMember(dest => dest.Mandator, opt => opt.Ignore());
            Mapper.CreateMap<Model.MandatorRole, MandatorRole>()
                  .ForMember(dest => dest.MandatorDto, opt => opt.Ignore());

            Mapper.CreateMap<MandatorGroupDto, MandatorGroup>()
                  .ForMember(dest => dest.Mandators, opt => opt.Ignore());
            Mapper.CreateMap<MandatorGroup, MandatorGroupDto>();

            Mapper.CreateMap<ModuleDto, Module>()
                  .ForMember(dest => dest.Mandators, opt => opt.Ignore())
                  .ForMember(dest => dest.Functions, opt => opt.Ignore())
                  .ForMember(dest => dest.ModuleGroup, opt => opt.Ignore());

            Mapper.CreateMap<Module, ModuleDto>()
                  .ForMember(dest => dest.ModuleGroupDto, opt => opt.Ignore());

            Mapper.CreateMap<ModuleGroupDto, ModuleGroup>();
            Mapper.CreateMap<ModuleGroup, ModuleGroupDto>();

            Mapper.CreateMap<PermissionDto, Permission>()
                  .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Permission, PermissionDto>();

            Mapper.CreateMap<PersonDto, Person>()
                  .ForMember(dest => dest.Mandator, opt => opt.Ignore());

            Mapper.CreateMap<Person, PersonDto>()
                  .ForMember(dest => dest.MandatorDto, opt => opt.Ignore());

            Mapper.CreateMap<Role, Model.Role>()
                  .ForMember(dest => dest.MandatorRoles, opt => opt.Ignore());
            Mapper.CreateMap<Model.Role, Role>();

            Mapper.CreateMap<SaveResultDto<UserDto>, SaveResult<User>>();
            Mapper.CreateMap<ISaveResult<User>, SaveResultDto<UserDto>>();

            Mapper.CreateMap<SettingDto, Setting>()
                  .ForMember(dest => dest.Mandator, opt => opt.Ignore());
            Mapper.CreateMap<Setting, SettingDto>()
                  .ForMember(dest => dest.MandatorDto, opt => opt.Ignore());

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

            Mapper.CreateMap<UserMandatorRoleDto, UserMandatorRole>()
                  .ForMember(dest => dest.User, opt => opt.Ignore());
            Mapper.CreateMap<UserMandatorRole, UserMandatorRoleDto>()
                  .ForMember(dest => dest.UserDto, opt => opt.Ignore());

            Mapper.CreateMap<UsersDto, UserSearchCriteria>();
            Mapper.CreateMap<UserSearchCriteria, UsersDto>();

            Mapper.CreateMap<UserSearchResultDto, UserSearchResult>();
            Mapper.CreateMap<UserSearchResult, UserSearchResultDto>();

            Mapper.CreateMap<Locations, Model.Location>()
                  .ForMember(dest => dest.Mandator, opt => opt.Ignore());

            Mapper.CreateMap<Model.Location, Locations>()
                  .ForMember(dest => dest.MandatorDto, opt => opt.Ignore());

            Mapper.CreateMap<LocationDto, Model.Location>()
                  .ForMember(dest => dest.Mandator, opt => opt.Ignore());
            Mapper.CreateMap<Model.Location, LocationDto>()
                  .ForMember(dest => dest.MandatorDto, opt => opt.Ignore());

            Mapper.CreateMap<SaveResultDto<LocationDto>,SaveResult<Model.Location>>();
            Mapper.CreateMap<ISaveResult<Model.Location>, SaveResultDto<LocationDto>>();

            Mapper.CreateMap<SaveResultDto<SettingDto>, SaveResult<Setting>>();
            Mapper.CreateMap<ISaveResult<Setting>, SaveResultDto<SettingDto>>();

            Mapper.CreateMap<SaveResultDto<MandatorDto>,SaveResult<Model.Mandator>>();
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