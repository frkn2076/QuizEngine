CREATE TABLE IF NOT EXISTS public.answer (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    questionid uuid NOT NULL,
    text VARCHAR (1000) NOT NULL,
    iscorrectanswer BOOLEAN NOT NULL
);