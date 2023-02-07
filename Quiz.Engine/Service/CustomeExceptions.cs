namespace Quiz.Engine.Service;

public class CustomeException : Exception
{
    public CustomeException(string message) : base(message) { }
}

public class UserAlreadyExistsException : CustomeException
{
    public UserAlreadyExistsException() : base("User already exists") { }
}

public class WrongCredentialsException : CustomeException
{
    public WrongCredentialsException() : base("Wrong credentials") { }
}

public class UserIsNotInTheSystemAnymoreException : CustomeException
{
    public UserIsNotInTheSystemAnymoreException() : base("User is not in system anymore") { }
}

public class QuizRecordNotFoundException : CustomeException
{
    public QuizRecordNotFoundException() : base("Quiz record not found") { }
}

public class QuestionRecordNotFoundException : CustomeException
{
    public QuestionRecordNotFoundException() : base("Question record not found") { }
}

public class UsersCannotTakeThierOwnQuizException : CustomeException
{
    public UsersCannotTakeThierOwnQuizException() : base("Users cannot take their own quiz") { }
}

public class QuizBelongsToAnotherUserException : CustomeException
{
    public QuizBelongsToAnotherUserException() : base("Quiz belongs to another user") { }
}

public class UnpublishedQuizCannotBeDeletedException : CustomeException
{
    public UnpublishedQuizCannotBeDeletedException() : base("Unpublished quiz cannot be deleted") { }
}

public class PublishedQuizCannotBeUpdatedException : CustomeException
{
    public PublishedQuizCannotBeUpdatedException() : base("Published quiz cannot be updated") { }
}

public class NoQuizFoundBelongsToUserException : CustomeException
{
    public NoQuizFoundBelongsToUserException() : base("No quiz found belongs to user") { }
}

public class QuizHasNotPublishedYetException : CustomeException
{
    public QuizHasNotPublishedYetException() : base("Quiz has not published yet") { }
}

public class SameQuizCannotBeTakenMoreThanOnceException : CustomeException
{
    public SameQuizCannotBeTakenMoreThanOnceException() : base("Same quiz cannot be taken more than once") { }
}

public class SameQuestionCannotBeAnsweredMoreThanOnceException : CustomeException
{
    public SameQuestionCannotBeAnsweredMoreThanOnceException() : base("Same question cannot be answered more than once") { }
}

public class QuestionNotFoundInQuizException : CustomeException
{
    public QuestionNotFoundInQuizException() : base("Question not found in quiz") { }
}

public class QuizTitleCannotBeFoundException : CustomeException
{
    public QuizTitleCannotBeFoundException() : base("Quiz title cannot be found") { }
}

public class QuizQuestionCannotBeFoundException : CustomeException
{
    public QuizQuestionCannotBeFoundException() : base("Quiz question cannot be found") { }
}

public class SolutionNotFoundException : CustomeException
{
    public SolutionNotFoundException() : base("Solution not found") { }
}

public class ModelValidationFailedException : CustomeException
{
    public ModelValidationFailedException(string message) : base(message) { }
}

public class RefreshTokenIsNotValidException : CustomeException
{
    public RefreshTokenIsNotValidException() : base("Refresh token is not valid") { }
}

public class RefreshTokenCanBeUsedForRefreshingTokenOnlyException : CustomeException
{
    public RefreshTokenCanBeUsedForRefreshingTokenOnlyException() : base("Refresh token can be used for refreshing token only") { }
}

public class SomethingWentWrongException : Exception
{
    public SomethingWentWrongException() : base("Something went wrong") { }
}
