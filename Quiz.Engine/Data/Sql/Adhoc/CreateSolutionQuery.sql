INSERT INTO public.solution
		  ( userid
		  , quizid
		  , questionid 
		  , answerid
		  , point)
	 VALUES 
	      ( @userId
		  , @quizId
		  , @questionId 
		  , @answerId
		  , @point)
  RETURNING id
