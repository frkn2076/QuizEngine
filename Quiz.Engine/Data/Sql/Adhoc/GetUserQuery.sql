SELECT id
  FROM public.user
 WHERE email = @email
   AND password = @password

