   DELETE 
     FROM public.question
    WHERE quizid = @quizId
RETURNING id