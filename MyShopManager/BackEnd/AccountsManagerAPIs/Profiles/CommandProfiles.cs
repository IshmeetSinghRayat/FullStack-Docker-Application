using AccountsManagerAPIs.DTOs;
using AccountsManagerAPIs.Models;
using AutoMapper;

namespace AccountsManagerAPIs.Profiles
{
    public class CommandProfiles: Profile
    {
        public CommandProfiles()
        {
            // Source => Taget
            CreateMap<Accounts, ReadAccountDetailsDto>();
            CreateMap<ReadAccountDetailsDto, Accounts>();
        }
    }
}
