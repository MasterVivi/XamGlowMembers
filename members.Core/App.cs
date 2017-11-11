using members.Core.Models;
using members.Core.Models.DTOs;
using members.Core.Models.Interfaces;
using members.Core.Services.Network;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace members
{
    public class App : MvxApplication
    {
        /// <summary>
        /// Setup IoC (Inversion of control) registrations.
        /// </summary>
        public App()
        {
            // Register general classes for Mvx
            Mvx.ConstructAndRegisterSingleton<IAPIService, APIService>();

            // Register the AppStart functionality as a sinleton to the
            // given interface
            var appStart = new AppStart();
            Mvx.RegisterSingleton<IMvxAppStart>(appStart);

            CreateMaps();
        }

        /// <summary>
        /// Create the necessary mappings for AutoMapper to function
        /// </summary>
        private void CreateMaps()
        {
            AutoMapper.Mapper.Initialize((cfg) =>
            {
                cfg.CreateMap<IMember, MemberDTO>();
                cfg.CreateMap<MemberDTO, IMember>();
                cfg.CreateMap<IMember, Member>();
                cfg.CreateMap<Member, IMember>();
                cfg.CreateMap<Member, MemberDTO>();
                cfg.CreateMap<MemberDTO, Member>();

                cfg.CreateMap<IMembership, MembershipDTO>();
                cfg.CreateMap<MembershipDTO, IMembership>();
                cfg.CreateMap<IMembership, Membership>();
                cfg.CreateMap<Membership, IMembership>();
                cfg.CreateMap<Membership, MembershipDTO>();
                cfg.CreateMap<MembershipDTO, Membership>();
            });
        }
    }
}