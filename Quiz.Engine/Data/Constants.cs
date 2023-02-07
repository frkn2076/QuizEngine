namespace Quiz.Engine.Data;

public static class Constants
{
    public const string SqlFileExtension = ".sql";

    public static string AdhocFolderPath = Path.Combine("Data", "Sql", "Adhoc");

    public static string SchemasFolderPath = Path.Combine("Data", "Sql", "Schemas");

    public static class Queries
    {
        public const string CreateUserQuery = nameof(CreateUserQuery);

        public const string GetUserQuery = nameof(GetUserQuery);

        public const string GetUserByEmailQuery = nameof(GetUserByEmailQuery);

        public const string CreateQuizQuery = nameof(CreateQuizQuery);

        public const string CreateQuestionQuery = nameof(CreateQuestionQuery);

        public const string CreateAnswerQuery = nameof(CreateAnswerQuery);

        public const string GetQuizQuery = nameof(GetQuizQuery);

        public const string UpdateQuizQuery = nameof(UpdateQuizQuery);

        public const string DeleteQuestionQuery = nameof(DeleteQuestionQuery);

        public const string DeleteAnswerQuery = nameof(DeleteAnswerQuery);

        public const string DeleteQuizQuery = nameof(DeleteQuizQuery);

        public const string GetOtherUsersQuizzesQuery = nameof(GetOtherUsersQuizzesQuery);

        public const string CreateSolutionQuery = nameof(CreateSolutionQuery);

        public const string GetSolutionsQuery = nameof(GetSolutionsQuery);

        public const string GetSolutionDetailsQuery = nameof(GetSolutionDetailsQuery);

        public const string GetSolutionDetailsOfQuizQuery = nameof(GetSolutionDetailsOfQuizQuery);

        public const string GetQuestionTypeQuery = nameof(GetQuestionTypeQuery);

        public const string GetQuizAllQuery = nameof(GetQuizAllQuery);

        public const string GetUserByIdQuery = nameof(GetUserByIdQuery);

        public const string GetQuizzesOfUserQuery = nameof(GetQuizzesOfUserQuery);
    }
}
