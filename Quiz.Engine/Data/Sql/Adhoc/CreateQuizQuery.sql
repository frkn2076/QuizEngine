INSERT INTO public.quiz
		  ( userid
		  , title 
		  , ispublished)
	 VALUES 
	      ( @userId
		  , @title
		  , @isPublished)
  RETURNING id
