using AutoMapper;
using TaskTracker.Application.DTOs.Task;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Mappings;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        CreateMap<TaskEntity, TaskResponse>();
    }
}