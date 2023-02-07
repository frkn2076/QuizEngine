SELECT quiz.id as quizid
     , quiz.title as title
	 , COUNT(q.id) as questioncount
  FROM public.quiz
 INNER JOIN public.question q
    ON q.quizid = quiz.id
 WHERE quiz.userid != @userId
   AND ispublished = true
 GROUP BY quiz.id
     , quiz.title