using AutoMapper;
using WalletService.Domain;
using WalletService.Models;

namespace WalletService.Service.Helpers
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransactionsRequest, Transactions>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            //CreateMap<IncomingMessages, IncomingMessageResponse>();
            //CreateMap<IncomingMessages, MessageForSending>();
        }
    }
}
