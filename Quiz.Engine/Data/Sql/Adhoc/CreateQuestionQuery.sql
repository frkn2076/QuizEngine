INSERT INTO public.question
		  ( quizid
		  , text 
		  , type)
	 VALUES 
	      ( @quizId
		  , @text
		  , @type)
  RETURNING id
