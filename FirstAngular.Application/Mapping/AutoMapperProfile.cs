using AutoMapper;
using FirstAngular.Application.Features.Categories.Commands.CreateCategory;
using FirstAngular.Application.Features.Categories.Commands.UpdateCategory;
using FirstAngular.Application.Features.Categories.DTOs;
using FirstAngular.Application.Features.Notes.Commands.CreateNote;
using FirstAngular.Application.Features.Notes.Commands.UpdateNote;
using FirstAngular.Application.Features.Notes.DTOs;
using FirstAngular.Application.Features.User.DTOs;
using FirstAngular.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            //users
            CreateMap<AppIdentityUser, UserDTO>()
                        .ForMember(dest => dest.FullName,
                                   opt => opt.MapFrom(src => $"{src.FirstName} {(!string.IsNullOrEmpty(src.MiddleInitial) ? src.MiddleInitial[0] + ". " : "")}{src.LastName}"))
                        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
 

            //notes
            CreateMap<CreateNoteCommand, NoteEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<UpdateNoteCommand, NoteEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.UserId, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
            CreateMap<NoteEntity, NoteDTO>();
            CreateMap<NoteEntity, UpdateNoteDTO>();



            //categories
            CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
            CreateMap<CreateCategoryCommand, CategoryEntity>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<UpdateCategoryCommand, CategoryEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
 


        }
    }
}
