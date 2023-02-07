CREATE TABLE IF NOT EXISTS public.quiz (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    userid uuid NOT NULL,
    title VARCHAR (1000) NOT NULL,
    isPublished BOOLEAN NOT NULL
);