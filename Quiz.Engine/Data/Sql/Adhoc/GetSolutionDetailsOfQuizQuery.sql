SELECT quiz.title
     , q.id as questionid
	 , q.text as question
	 , a.text as answer
	 , s.point
	 , s.userid
  FROM public.solution s
 INNER JOIN public.quiz
    ON s.quizid = quiz.id
 INNER JOIN public.question q
    ON s.questionid = q.id
 INNER JOIN public.answer a
    ON s.answerid = a.id
 WHERE quiz.id = @quizId
   AND quiz.userid = @userId