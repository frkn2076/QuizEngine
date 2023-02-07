DELETE 
  FROM public.answer
 WHERE questionid = ANY (@questionIds)