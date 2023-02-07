CREATE TABLE IF NOT EXISTS public.solution (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    userid uuid NOT NULL,
    quizid uuid NOT NULL,
    questionid uuid NOT NULL,
    answerid uuid NOT NULL,
    point DOUBLE PRECISION NOT NULL
);