SELECT quiz.id as quizid
     , quiz.title as title
	 , COUNT(q.id) as questioncount
     , ispublished
  FROM public.quiz
 INNER JOIN public.question q
    ON q.quizid = quiz.id
 WHERE quiz.userid = @userId
 GROUP BY quiz.id
     , quiz.title