#region

using System.Linq;
using AutoMapper;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Model;
using Woozle.Model.UserSearch;
using Woozle.Model.Validation.Creation;
using Woozle.Services.Authority;
using Woozle.Services.Location;
using Woozle.Services.Modules.Settings;
using Woozle.Services.Navigation;
using Woozle.Services.UserManagement;
using City = Woozle.Services.Location.City;
using Country = Woozle.Services.Location.Country;
using Function = Woozle.Model.Function;
using FunctionPermission = Woozle.Services.Modules.FunctionPermission;
using Language = Woozle.Services.Location.Language;
using MandatorGroup = Woozle.Services.Mandator.MandatorGroup;
using MandatorRole = Woozle.Services.Authority.MandatorRole;
using Module = Woozle.Services.Modules.Module;
using ModuleForMandator = Woozle.Model.ModulePermissions.ModuleForMandator;
using ModuleGroup = Woozle.Services.Modules.ModuleGroup;
using Permission = Woozle.Services.Modules.Permission;
using Person = Woozle.Services.UserManagement.Person;
using Role = Woozle.Services.Authority.Role;
using Status = Woozle.Services.Fields.Status;
using StatusField = Woozle.Services.Fields.StatusField;
using Translation = Woozle.Model.Translation;
using TranslationItem = Woozle.Services.Location.TranslationItem;
using User = Woozle.Services.UserManagement.User;
using UserMandatorRole = Woozle.Services.UserManagement.UserMandatorRole;
using UserSearchResult = Woozle.Services.UserManagement.UserSearchResult;

#endregion

namespace Woozle.Services
{
    /// <summary>
    /// Mapping configuration betweeen Dto's and Model objects
    /// </summary>
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

            Mapper.CreateMap<Modules.Function, Function>()
                .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore())
                .ForMember(dest => dest.Module, opt => opt.Ignore())
                .ForMember(dest => dest.Translation, opt => opt.Ignore());

            Mapper.CreateMap<Function, Modules.Function>()
                .ForMember(dest => dest.Module, opt => opt.Ignore())
                .ForMember(dest => dest.Translation, opt => opt.MapFrom(n => n.Translation));

            Mapper.CreateMap<FunctionPermission, Model.FunctionPermission>()
                .ForMember(dest => dest.MandatorRoles, opt => opt.Ignore())
                .ForMember(dest => dest.Function, opt => opt.Ignore())
                .ForMember(dest => dest.Permission, opt => opt.Ignore());

            Mapper.CreateMap<Model.FunctionPermission, FunctionPermission>()
                .ForMember(dest => dest.Function, opt => opt.Ignore())
                .ForMember(dest => dest.Function, opt => opt.Ignore())
                .ForMember(dest => dest.Permission, opt => opt.Ignore());

            Mapper.CreateMap<Language, Model.Language>()
                .ForMember(dest => dest.TranslationItems, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore());
            Mapper.CreateMap<Model.Language, Language>();

            Mapper.CreateMap<Mandator.Mandator, Model.Mandator>()
                .ForMember(n => n.Modules, opt => opt.Ignore())
                .ForMember(n => n.Locations, opt => opt.Ignore())
                .ForMember(n => n.MandatorRoles, opt => opt.Ignore())
                .ForMember(n => n.People, opt => opt.Ignore())
                .ForMember(n => n.Settings, opt => opt.Ignore())
                .ForMember(n => n.MandatorGroup, opt => opt.Ignore())
                .ForMember(n => n.ExternalSystems, opt => opt.Ignore())
                .ForMember(n => n.TextFields, opt => opt.Ignore())
                .ForMember(n => n.Customers, opt => opt.Ignore())
                .ForMember(n => n.NumberRanges, opt => opt.Ignore());

            Mapper.CreateMap<Model.Mandator, Mandator.Mandator>()
                .ForMember(n => n.MandatorGroup, opt => opt.Ignore())
                .ForMember(n => n.City, opt => opt.MapFrom(n => n.City))
                .ForMember(n => n.MandatorGroup, opt => opt.MapFrom(n => n.MandatorGroup));

            Mapper.CreateMap<MandatorRole, Model.MandatorRole>()
                .ForMember(dest => dest.UserMandatorRoles, opt => opt.Ignore())
                .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Model.MandatorRole, MandatorRole>();

            Mapper.CreateMap<MandatorGroup, Model.MandatorGroup>()
                .ForMember(dest => dest.Mandators, opt => opt.Ignore());
            Mapper.CreateMap<Model.MandatorGroup, MandatorGroup>();

            Mapper.CreateMap<Module, Model.Module>()
                .ForMember(dest => dest.Mandators, opt => opt.Ignore())
                .ForMember(dest => dest.Functions, opt => opt.Ignore())
                .ForMember(dest => dest.ModuleGroup, opt => opt.Ignore());

            Mapper.CreateMap<Model.Module, Module>()
                .ForMember(dest => dest.ModuleGroup, opt => opt.Ignore());

            Mapper.CreateMap<ModuleGroup, Model.ModuleGroup>();
            Mapper.CreateMap<Model.ModuleGroup, ModuleGroup>();

            Mapper.CreateMap<Permission, Model.Permission>()
                .ForMember(dest => dest.FunctionPermissions, opt => opt.Ignore());
            Mapper.CreateMap<Model.Permission, Permission>();

            Mapper.CreateMap<Person, Model.Person>()
                .ForMember(dest => dest.Mandator, opt => opt.Ignore())
                .ForMember(dest => dest.Customers, opt => opt.Ignore());

            Mapper.CreateMap<Model.Person, Person>()
                .ForMember(dest => dest.Mandator, opt => opt.Ignore());

            Mapper.CreateMap<Role, Model.Role>()
                .ForMember(dest => dest.MandatorRoles, opt => opt.Ignore());
            Mapper.CreateMap<Model.Role, Role>();

            Mapper.CreateMap<SaveResultDto<User>, SaveResult<Model.User>>();
            Mapper.CreateMap<ISaveResult<Model.User>, SaveResultDto<User>>();

            Mapper.CreateMap<ISaveResult<Setting>, SaveResultDto<Setting>>();
            Mapper.CreateMap<SaveResultDto<Setting>, ISaveResult<Setting>>();

            Mapper.CreateMap<SettingDto, Setting>()
                .ForMember(dest => dest.Mandator, opt => opt.Ignore());
            Mapper.CreateMap<Setting, SettingDto>()
                .ForMember(dest => dest.Mandator, opt => opt.Ignore());

            Mapper.CreateMap<Location.Translation, Translation>()
                .ForMember(n => n.Countries, opt => opt.Ignore())
                .ForMember(n => n.Functions, opt => opt.Ignore())
                .ForMember(n => n.Modules, opt => opt.Ignore())
                .ForMember(n => n.Status, opt => opt.Ignore())
                .ForMember(n => n.TextFieldPlaceHolders, opt => opt.Ignore())
                .ForMember(n => n.TextFields, opt => opt.Ignore())
                .ForMember(n => n.TranslationItems, opt => opt.Ignore());

            Mapper.CreateMap<Translation, Location.Translation>();

            Mapper.CreateMap<TranslationItem, Model.TranslationItem>();
            Mapper.CreateMap<Model.TranslationItem, TranslationItem>();

            Mapper.CreateMap<User, Model.User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
            Mapper.CreateMap<Model.User, User>()
                  .ForMember(n => n.Status, opt => opt.Ignore());

            Mapper.CreateMap<UserMandatorRole, Model.UserMandatorRole>();
            Mapper.CreateMap<Model.UserMandatorRole, UserMandatorRole>()
                .ForMember(dest => dest.User, opt => opt.Ignore());

            Mapper.CreateMap<Users, UserSearchCriteria>();
            Mapper.CreateMap<UserSearchCriteria, Users>();

            Mapper.CreateMap<UserSearchResult, Model.UserSearch.UserSearchResult>();
            Mapper.CreateMap<Model.UserSearch.UserSearchResult, UserSearchResult>();

            Mapper.CreateMap<Locations, Model.Location>()
                .ForMember(dest => dest.Mandator, opt => opt.Ignore());

            Mapper.CreateMap<Model.Location, Locations>()
                .ForMember(dest => dest.Mandator, opt => opt.Ignore());

            Mapper.CreateMap<Location.Location, Model.Location>()
                .ForMember(dest => dest.Mandator, opt => opt.Ignore());
            Mapper.CreateMap<Model.Location, Location.Location>()
                .ForMember(dest => dest.Mandator, opt => opt.Ignore());

            Mapper.CreateMap<SaveResultDto<Location.Location>, SaveResult<Model.Location>>();
            Mapper.CreateMap<ISaveResult<Model.Location>, SaveResultDto<Location.Location>>();

            Mapper.CreateMap<SaveResultDto<SettingDto>, SaveResult<Setting>>();
            Mapper.CreateMap<ISaveResult<Setting>, SaveResultDto<SettingDto>>();

            Mapper.CreateMap<SaveResultDto<Mandator.Mandator>, SaveResult<Model.Mandator>>();
            Mapper.CreateMap<ISaveResult<Model.Mandator>, SaveResultDto<Mandator.Mandator>>();

            Mapper.CreateMap<Modules.ModuleForMandator, ModuleForMandator>();
            Mapper.CreateMap<ModuleForMandator, Modules.ModuleForMandator>();

            Mapper.CreateMap<ChangedModulePermission, Model.ModulePermissions.ChangedModulePermission>();
            Mapper.CreateMap<Model.ModulePermissions.ChangedModulePermission, ChangedModulePermission>();

            Mapper.CreateMap<ModulePermissionsResult, Model.ModulePermissions.ModulePermissionsResult>();
            Mapper.CreateMap<Model.ModulePermissions.ModulePermissionsResult, ModulePermissionsResult>();

            Mapper.CreateMap<Status, Model.Status>()
                .ForMember(dest => dest.People, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore());
            Mapper.CreateMap<Model.Status, Status>();

            Mapper.CreateMap<StatusField, Model.StatusField>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());

            Mapper.CreateMap<Model.StatusField, StatusField>();

            Mapper.CreateMap<Model.Country, Country>();
            Mapper.CreateMap<Country, Model.Country>();

            Mapper.CreateMap<ModuleForMandator, Header>()
                .ForMember(n => n.TranslatedValue, opt => opt.ResolveUsing(new ModuleTranslatedValueResolver()))
                .ForMember(n => n.Items, opt => opt.MapFrom(n => n.Functions));

            Mapper.CreateMap<Function, Item>()
                .ForMember(n => n.TranslatedValue, opt => opt.ResolveUsing(new FunctionTranslatedValueResolver()));

            Mapper.CreateMap<UserAuth, Model.User>().ConvertUsing<UserAuthToUserConverter>();
            Mapper.CreateMap<Model.User, UserAuth>().ConvertUsing<UserToUserAuthConverter>();
        }

        private class ModuleTranslatedValueResolver : ValueResolver<ModuleForMandator, string>
        {
            protected override string ResolveCore(ModuleForMandator source)
            {
                return GetTranslation(source.Translation);
            }
        }

        private class FunctionTranslatedValueResolver : ValueResolver<Function, string>
        {
            protected override string ResolveCore(Function source)
            {
                return GetTranslation(source.Translation);
            }
        }

        private static string GetTranslation(Translation translation)
        {
            var translationItem = translation.TranslationItems.FirstOrDefault();
            return translationItem != null ? translationItem.Description : translation.DefaultDescription;
        }

        private class UserAuthToUserConverter : TypeConverter<UserAuth, Model.User>
        {
            protected override Model.User ConvertCore(UserAuth source)
            {
                if (source == null)
                {
                    return null;
                }
                return new Model.User()
                {
                    Username = source.UserName,
                    PasswordHash = source.PasswordHash,
                    PasswordSalt = source.Salt,
                    FirstName = source.FirstName,
                    LastName = source.LastName,
                    Email = source.Email,
                    Language = new Model.Language() {Code = source.Language}
                };
            }
        }

        private class UserToUserAuthConverter : TypeConverter<Model.User, UserAuth>
        {
            protected override UserAuth ConvertCore(Model.User source)
            {
                if (source == null)
                {
                    return null;
                }
                return new UserAuth()
                {
                    UserName = source.Username,
                    PasswordHash = source.PasswordHash,
                    Salt = source.PasswordSalt,
                    FirstName = source.FirstName,
                    LastName = source.LastName
                };
            }
        }
    }
}