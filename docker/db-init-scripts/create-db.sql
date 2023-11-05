CREATE TABLE IF NOT EXISTS "users" (
    "id" serial PRIMARY KEY,
    "name" VARCHAR(255) NOT NULL,
    "phone" VARCHAR(16) NOT NULL,
    "email" VARCHAR(255) NOT NULL
);