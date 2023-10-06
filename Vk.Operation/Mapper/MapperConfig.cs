using AutoMapper;
using Vk.Data.Domain;
using Vk.Schema;

namespace Vk.Operation.Mapper;

// Profile Automapper'dan gelmektedir
public class MapperConfig : Profile
{
    public MapperConfig()
    {
        /*
         *  CreateMap<CustomerRequest, Customer>();
         *              TSource      , TDestination
         * CustomerRequest nesnesini customer'a dönüştürür. Casting işlemi
         */
        
        // Customer İçin Mapper İşlemi
        CreateMap<CustomerRequest, Customer>();
        CreateMap<Customer, CustomerResponse>();
        
        // Account İçin Mapper İşlemi
        CreateMap<AccountRequest, Account>();
        CreateMap<Account, AccountResponse>()
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));

        // Card İçin Mapper İşlemi
        CreateMap<CardRequest, Card>();
        CreateMap<Card, CardResponse>()
            .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account.AccountNumber));
        
        // Address İçin Mapper İşlemi
        CreateMap<AddressRequest, Address>();
        CreateMap<Address, AddressResponse>()
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));

        // EftTranstaction İçin Mapper İşlemi
        CreateMap<EftTransactionRequest, EftTransactionResponse>();
        CreateMap<EftTransactionRequest, EftTransaction>();
        CreateMap<EftTransaction, EftTransactionResponse>()
            .ForMember(dest => dest.ReferenceNumber, opt => opt.MapFrom(src => src.ReferenceNumber));

        
        // AccountTransaction İçin Mapper İşlemi
        CreateMap<AccountTransactionRequest, AccountTransaction>();
        CreateMap<AccountTransaction, AccountTransactionResponse>()
            .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account.AccountNumber))
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Account.Customer.FirstName + " " + src.Account.Customer.LastName))
            .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Account.Customer.CustomerNumber));


        CreateMap<MoneyTransferRequest, MoneyTransferResponse>();
        
    }
}