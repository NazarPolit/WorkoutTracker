using AutoMapper;
using BusinessLogic.Dtos;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helper
{
	public class MappingProfiles: Profile
	{
        public MappingProfiles()
        {
			CreateMap<CreateAndUpdateExerciseTypeDto, ExerciseType>();
			CreateMap<ExerciseType, CreateAndUpdateExerciseTypeDto>();
			CreateMap<ExerciseType, ExerciseTypeDto>();
			CreateMap<ExerciseTypeDto, ExerciseType>();
			CreateMap<WorkoutTemplate, WorkoutTemplateDto>();

			CreateMap<WorkoutTemplate, WorkoutTemplateDetailDto>()
				.ForMember(dest => dest.Exercises, 
						   opt => opt.MapFrom(src => src.TemplateExercises.Select(te => te.ExerciseType)));

			CreateMap<Workout, WorkoutDto>();
			CreateMap<CreateAndUpdateWorkoutDto, Workout>();

			CreateMap<Workout, WorkoutDetailDto>()
				.ForMember(dest => dest.Exercises,
							opt => opt.MapFrom(src => src.WorkoutExercises));

			CreateMap<WorkoutExercise, WorkoutExerciseDto>()
				.ForMember(dest => dest.ExerciseName, opt => opt.MapFrom(src => src.ExerciseType.Name));

			CreateMap<Set, SetDto>();

			CreateMap<CreateWorkoutExerciseDto, WorkoutExercise>();

			CreateMap<CreateSetDto, Set>();
			CreateMap<UpdateSetDto, Set>();
		}
    }
}
