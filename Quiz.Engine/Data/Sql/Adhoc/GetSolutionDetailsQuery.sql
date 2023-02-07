SELECT s.quizid
     , quiz.title
	 , q.text as question
     , q.id as questionid
	 , a.text as answer
	 , s.point
  FROM public.solution s
 INNER JOIN public.quiz
    ON s.quizid = quiz.id
 INNER JOIN public.question q
    ON s.questionid = q.id
 INNER JOIN public.answer a
    ON s.answerid = a.id
 WHERE s.userid = @userId