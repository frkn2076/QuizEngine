CREATE TABLE IF NOT EXISTS public.question (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    quizid uuid NOT NULL,
    text VARCHAR (1000) NOT NULL,
    type NUMERIC (1)  NOT NULL
);