IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [ExerciseTypes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [MuscleGroup] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ExerciseTypes] PRIMARY KEY ([Id])
);

CREATE TABLE [Users] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

CREATE TABLE [Workouts] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Date] datetime2 NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Workouts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Workouts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [WorkoutExercises] (
    [Id] int NOT NULL IDENTITY,
    [Note] nvarchar(max) NOT NULL,
    [WorkoutId] int NOT NULL,
    [ExerciseTypeId] int NOT NULL,
    CONSTRAINT [PK_WorkoutExercises] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WorkoutExercises_ExerciseTypes_ExerciseTypeId] FOREIGN KEY ([ExerciseTypeId]) REFERENCES [ExerciseTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_WorkoutExercises_Workouts_WorkoutId] FOREIGN KEY ([WorkoutId]) REFERENCES [Workouts] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Sets] (
    [Id] int NOT NULL IDENTITY,
    [Repetitions] int NOT NULL,
    [Weight] decimal(18,2) NOT NULL,
    [WorkoutExerciseId] int NOT NULL,
    CONSTRAINT [PK_Sets] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Sets_WorkoutExercises_WorkoutExerciseId] FOREIGN KEY ([WorkoutExerciseId]) REFERENCES [WorkoutExercises] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Sets_WorkoutExerciseId] ON [Sets] ([WorkoutExerciseId]);

CREATE INDEX [IX_WorkoutExercises_ExerciseTypeId] ON [WorkoutExercises] ([ExerciseTypeId]);

CREATE INDEX [IX_WorkoutExercises_WorkoutId] ON [WorkoutExercises] ([WorkoutId]);

CREATE INDEX [IX_Workouts_UserId] ON [Workouts] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251010170251_InitialCreate', N'9.0.9');

CREATE TABLE [WorkoutTemplates] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_WorkoutTemplates] PRIMARY KEY ([Id])
);

CREATE TABLE [WorkoutTemplatesExercises] (
    [WorkoutTemplateId] int NOT NULL,
    [ExerciseTypeId] int NOT NULL,
    CONSTRAINT [PK_WorkoutTemplatesExercises] PRIMARY KEY ([WorkoutTemplateId], [ExerciseTypeId]),
    CONSTRAINT [FK_WorkoutTemplatesExercises_ExerciseTypes_ExerciseTypeId] FOREIGN KEY ([ExerciseTypeId]) REFERENCES [ExerciseTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_WorkoutTemplatesExercises_WorkoutTemplates_WorkoutTemplateId] FOREIGN KEY ([WorkoutTemplateId]) REFERENCES [WorkoutTemplates] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_WorkoutTemplatesExercises_ExerciseTypeId] ON [WorkoutTemplatesExercises] ([ExerciseTypeId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251012142310_AddWorkoutTemplates', N'9.0.9');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251012170605_MakeNavPropertiesNotRequired', N'9.0.9');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251013092540_ConfigureCascadeDelete', N'9.0.9');

COMMIT;
GO

