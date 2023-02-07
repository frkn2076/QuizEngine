SELECT quiz.id as quizid
     , quiz.userid
	 , quiz.title
	 , quiz.ispublished
	 , q.id as questionid
	 , q.text as question
	 , a.id as answerid
	 , a.text as answer
	 , a.iscorrectanswer
  FROM quiz
 INNER JOIN question q
    ON quiz.id = q.quizid
 INNER JOIN answer a
    ON q.id = a.questionid
  WHERE quizid = @quizId
