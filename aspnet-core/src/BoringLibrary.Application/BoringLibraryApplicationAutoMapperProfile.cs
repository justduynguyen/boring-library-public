using AutoMapper;
using BoringLibrary.BookBorrowers;
using BoringLibrary.Books;
using BoringLibrary.CustomUsers;
using Volo.Abp.Identity;

namespace BoringLibrary;

public class BoringLibraryApplicationAutoMapperProfile : Profile
{
    public BoringLibraryApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<IdentityUser, UserDto>();
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<BookBorrowerWithDetails, BookBorrowerWithDetailsDto>();
    }
}