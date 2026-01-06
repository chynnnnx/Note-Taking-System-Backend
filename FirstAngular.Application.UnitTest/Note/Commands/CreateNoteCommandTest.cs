using AutoMapper;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Features.Notes.Commands.CreateNote;
using FirstAngular.Application.Features.Notes.DTOs;
using FirstAngular.Application.Interfaces;
using FirstAngular.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FirstAngular.Application.UnitTests.Note.Commands
{
    public class CreateNoteCommandTest
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsNoteDTO()
        {
             var command = new CreateNoteCommand ( "Test Note", "Content", false, null );

            var mockNoteRepo = new Mock<INoteRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.NoteRepository).Returns(mockNoteRepo.Object);
            mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var mockUserService = new Mock<ICurrentUserService>();
            mockUserService.Setup(u => u.UserId).Returns("user-123");

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateNoteCommand, NoteEntity>()
                   .ForMember(dest => dest.Id, opt => opt.Ignore())
                   .ForMember(dest => dest.UserId, opt => opt.Ignore());
                cfg.CreateMap<NoteEntity, NoteDTO>();
            });
            var mapper = mapperConfig.CreateMapper();

            var handler = new CreateNoteCommandHandler(mockUnitOfWork.Object, mockUserService.Object, mapper);

             var result = await handler.Handle(command, CancellationToken.None);

             Assert.True(result.Success);
            Assert.Equal("user-123", result.Data!.UserId);
            Assert.Equal(command.Title, result.Data.Title);
            Assert.Equal(command.Content, result.Data.Content);

            mockNoteRepo.Verify(r => r.AddAsync(It.Is<NoteEntity>(n => n.Title == command.Title)), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);

        }
    }
}
