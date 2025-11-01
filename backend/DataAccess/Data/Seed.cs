using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Data
{
    public class Seed
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Seed(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dataContext = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDataContextAsync()
        {
            if (await _roleManager.Roles.AnyAsync())
            {
                return;
            }

            var roles = new List<IdentityRole>
            {
                new IdentityRole {Name = "Admin"},
                new IdentityRole {Name = "User"}
            };

            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }

            if (_dataContext.WorkoutTemplates.Any() || _dataContext.ExerciseTypes.Any())
            {
                return;
            }

            #region Exercise Types Creation
            // Legs
            var squat = new ExerciseType() { Name = "Barbell Squat", MuscleGroup = "Legs", Description = "A fundamental compound exercise for lower body." };
            var legPress = new ExerciseType() { Name = "Leg Press", MuscleGroup = "Legs", Description = "Machine exercise targeting quadriceps and glutes." };
            var lunges = new ExerciseType() { Name = "Dumbbell Lunges", MuscleGroup = "Legs", Description = "Unilateral exercise for balance and leg strength." };
            var legExtension = new ExerciseType() { Name = "Leg Extension", MuscleGroup = "Legs", Description = "Isolation exercise for the quadriceps." };
            var legCurl = new ExerciseType() { Name = "Leg Curl", MuscleGroup = "Legs", Description = "Isolation exercise for the hamstrings." };
            var romanianDeadlift = new ExerciseType() { Name = "Romanian Deadlift", MuscleGroup = "Legs", Description = "Targets the posterior chain, especially hamstrings." };

            // Chest
            var benchPress = new ExerciseType() { Name = "Bench Press", MuscleGroup = "Chest", Description = "Core compound exercise for upper body strength." };
            var inclinePress = new ExerciseType() { Name = "Incline Dumbbell Press", MuscleGroup = "Chest", Description = "Targets the upper pectoral muscles." };
            var dips = new ExerciseType() { Name = "Dips", MuscleGroup = "Chest", Description = "Bodyweight exercise for chest and triceps." };
            var pushUps = new ExerciseType() { Name = "Push-ups", MuscleGroup = "Chest", Description = "Classic bodyweight chest exercise." };
            var dumbbellFly = new ExerciseType() { Name = "Dumbbell Fly", MuscleGroup = "Chest", Description = "Isolation exercise for stretching and building the chest." };

            // Back
            var deadlift = new ExerciseType() { Name = "Deadlift", MuscleGroup = "Back", Description = "Full-body lift for posterior chain strength." };
            var pullUp = new ExerciseType() { Name = "Pull-up", MuscleGroup = "Back", Description = "Upper-body exercise for back and bicep strength." };
            var bentOverRow = new ExerciseType() { Name = "Bent Over Row", MuscleGroup = "Back", Description = "Compound exercise for a thick back." };
            var latPulldown = new ExerciseType() { Name = "Lat Pulldown", MuscleGroup = "Back", Description = "Machine exercise to develop the latissimus dorsi." };
            var tBarRow = new ExerciseType() { Name = "T-Bar Row", MuscleGroup = "Back", Description = "A variation of the row for back thickness." };

            // Shoulders
            var overheadPress = new ExerciseType() { Name = "Overhead Press", MuscleGroup = "Shoulders", Description = "Key exercise for shoulder development." };
            var lateralRaises = new ExerciseType() { Name = "Lateral Raises", MuscleGroup = "Shoulders", Description = "Isolation exercise for the side deltoids." };
            var facePulls = new ExerciseType() { Name = "Face Pulls", MuscleGroup = "Shoulders", Description = "Excellent for rear delts and shoulder health." };
            var frontRaises = new ExerciseType() { Name = "Front Raises", MuscleGroup = "Shoulders", Description = "Isolates the anterior deltoid." };
            var shrugs = new ExerciseType() { Name = "Shrugs", MuscleGroup = "Shoulders", Description = "Targets the trapezius muscles." };

            // Arms
            var bicepCurl = new ExerciseType() { Name = "Bicep Curl", MuscleGroup = "Arms", Description = "Classic isolation exercise for biceps." };
            var tricepExtension = new ExerciseType() { Name = "Tricep Extension", MuscleGroup = "Arms", Description = "Isolation exercise for triceps." };
            var hammerCurls = new ExerciseType() { Name = "Hammer Curls", MuscleGroup = "Arms", Description = "Targets biceps and brachialis." };
            var tricepDips = new ExerciseType() { Name = "Tricep Dips", MuscleGroup = "Arms", Description = "Bodyweight or weighted exercise for triceps." };
            var preacherCurls = new ExerciseType() { Name = "Preacher Curls", MuscleGroup = "Arms", Description = "Isolates the biceps for a concentrated peak." };
            #endregion

            _dataContext.ExerciseTypes.AddRange(
                squat, legPress, lunges, legExtension, legCurl, romanianDeadlift,
                benchPress, inclinePress, dips, pushUps, dumbbellFly,
                deadlift, pullUp, bentOverRow, latPulldown, tBarRow,
                overheadPress, lateralRaises, facePulls, frontRaises, shrugs,
                bicepCurl, tricepExtension, hammerCurls, tricepDips, preacherCurls
            );

            var templates = new List<WorkoutTemplate>
            {
                new() { Name = "Full Body", Description = "A workout for the entire body.", TemplateExercises = new List<WorkoutTemplateExercise> { new() { ExerciseType = squat }, new() { ExerciseType = benchPress }, new() { ExerciseType = bentOverRow }, new() { ExerciseType = overheadPress }, new() { ExerciseType = bicepCurl } } },
                new() { Name = "Upper Body", Description = "Upper body workout.", TemplateExercises = new List<WorkoutTemplateExercise> { new() { ExerciseType = benchPress }, new() { ExerciseType = pullUp }, new() { ExerciseType = overheadPress }, new() { ExerciseType = bentOverRow }, new() { ExerciseType = lateralRaises } } },
                new() { Name = "Lower Body", Description = "Lower body workout.", TemplateExercises = new List<WorkoutTemplateExercise> { new() { ExerciseType = squat }, new() { ExerciseType = legPress }, new() { ExerciseType = lunges }, new() { ExerciseType = romanianDeadlift }, new() { ExerciseType = legCurl } } },
                new() { Name = "Push", Description = "Push movements (chest, shoulders, triceps).", TemplateExercises = new List<WorkoutTemplateExercise> { new() { ExerciseType = benchPress }, new() { ExerciseType = inclinePress }, new() { ExerciseType = overheadPress }, new() { ExerciseType = lateralRaises }, new() { ExerciseType = tricepExtension } } },
                new() { Name = "Pull", Description = "Pull movements (back, biceps).", TemplateExercises = new List<WorkoutTemplateExercise> { new() { ExerciseType = deadlift }, new() { ExerciseType = pullUp }, new() { ExerciseType = bentOverRow }, new() { ExerciseType = latPulldown }, new() { ExerciseType = bicepCurl } } },
                new() { Name = "Legs", Description = "Classic leg day.", TemplateExercises = new List<WorkoutTemplateExercise> { new() { ExerciseType = squat }, new() { ExerciseType = legPress }, new() { ExerciseType = lunges }, new() { ExerciseType = legExtension }, new() { ExerciseType = romanianDeadlift } } },
                new() { Name = "Chest & Triceps", Description = "A split for chest and triceps.", TemplateExercises = new List<WorkoutTemplateExercise> { new() { ExerciseType = benchPress }, new() { ExerciseType = inclinePress }, new() { ExerciseType = dips }, new() { ExerciseType = pushUps }, new() { ExerciseType = tricepExtension } } },
                new() { Name = "Back & Biceps", Description = "A split for back and biceps.", TemplateExercises = new List<WorkoutTemplateExercise> { new() { ExerciseType = pullUp }, new() { ExerciseType = bentOverRow }, new() { ExerciseType = latPulldown }, new() { ExerciseType = tBarRow }, new() { ExerciseType = bicepCurl } } },
                new() { Name = "Legs & Shoulders", Description = "A split for legs and shoulders.", TemplateExercises = new List<WorkoutTemplateExercise> { new() { ExerciseType = squat }, new() { ExerciseType = legPress }, new() { ExerciseType = overheadPress }, new() { ExerciseType = lateralRaises }, new() { ExerciseType = facePulls } } },
                new() { Name = "Arms", Description = "Isolated arm workout.", TemplateExercises = new List<WorkoutTemplateExercise> { new() { ExerciseType = bicepCurl }, new() { ExerciseType = hammerCurls }, new() { ExerciseType = preacherCurls }, new() { ExerciseType = tricepExtension }, new() { ExerciseType = tricepDips } } },
                new() { Name = "Chest", Description = "Isolated chest workout.", TemplateExercises = new List<WorkoutTemplateExercise> { new() { ExerciseType = benchPress }, new() { ExerciseType = inclinePress }, new() { ExerciseType = dumbbellFly }, new() { ExerciseType = dips }, new() { ExerciseType = pushUps } } },
                new() { Name = "Back", Description = "Isolated back workout.", TemplateExercises = new List<WorkoutTemplateExercise> { new() { ExerciseType = deadlift }, new() { ExerciseType = pullUp }, new() { ExerciseType = bentOverRow }, new() { ExerciseType = latPulldown }, new() { ExerciseType = tBarRow } } },
            };
            _dataContext.WorkoutTemplates.AddRange(templates);

            var adminUser = new User { UserName = "AdminUser", Email = "admin@example.com" };
            await _userManager.CreateAsync(adminUser, "AdminPa$$w0rd");
            await _userManager.AddToRoleAsync(adminUser, "Admin");

            var regularUser = new User { UserName = "RegularUser", Email = "user@example.com" };
            await _userManager.CreateAsync(regularUser, "UserPa$$w0rd");
            await _userManager.AddToRoleAsync(regularUser, "User");

            await _dataContext.SaveChangesAsync();
        }
    }
}