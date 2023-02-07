CREATE TABLE IF NOT EXISTS public.user (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    email VARCHAR (100) UNIQUE NOT NULL,
    password VARCHAR (100) NOT NULL
);