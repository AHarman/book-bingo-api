using AutoMapper;
using BookBingoApi.Dtos.Goodreads.Search.Books;
using BookBingoApi.Dtos.Goodreads.Shelf;
using BookBingoApi.Models;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;

namespace BookBingoApi.AutoMapper
{
    public class GoodreadsProfile : Profile
    {
        public GoodreadsProfile()
        {
            CreateMap<Dtos.Goodreads.User.User, Models.User>();

            CreateMap<BookSearchAuthor, Author>();
            CreateMap<BookSearchAuthor, IEnumerable<Author>>()
                .ConvertUsing((src, dest, context) => new List<Author> { context.Mapper.Map<Author>(src) } );
            CreateMap<BookSearchBook, Book>();
            CreateMap<BookSearchWork, Book>()
                .IncludeMembers(work => work.Book);

            CreateMap<ShelfBook, Book>();
            CreateMap<ShelfAuthor, Author>();
            CreateMap<ShelfReview, BookshelfEntry>()
                .ForMember(dest => dest.BookRead, opt => opt.ConvertUsing<DateParser, string>())
                .ForMember(dest => dest.Added, opt => opt.ConvertUsing<DateParser, string>())
                .ForMember(dest => dest.LastUpdated, opt => opt.ConvertUsing<DateParser, string>())
                .ForMember(dest => dest.BookStarted, opt => opt.ConvertUsing<DateParser, string>());
        }

        private class DateParser : IValueConverter<string, DateTime?>
        {

            public DateTime? Convert(string sourceMember, ResolutionContext context)
            {
                if (string.IsNullOrEmpty(sourceMember)) return null;

                return DateTime.ParseExact(sourceMember, "ddd MMM dd HH:mm:ss zzz yyyy", null);
            }
        }
    }
}
