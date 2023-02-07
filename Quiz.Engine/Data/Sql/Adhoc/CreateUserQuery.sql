INSERT INTO public.user
		  ( email
		  , password )
	 VALUES 
	      ( @email
		  , @password )
  RETURNING id
