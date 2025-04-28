SQL-скрипт

-- Создать базу данных
CREATE DATABASE "Quiz_DB";
\c "Quiz_DB";

-- Таблица Answer
CREATE TABLE "Answers" (
    "Id" UUID PRIMARY KEY,
    "Title" TEXT NOT NULL
);

-- Таблица Category
CREATE TABLE "Categories" (
    "Id" UUID PRIMARY KEY,
    "Title" TEXT NOT NULL
);

-- Таблица Question
CREATE TABLE "Questions" (
    "Id" UUID PRIMARY KEY,
    "Title" TEXT NOT NULL,
    "AnswerId" UUID NOT NULL,
    FOREIGN KEY ("AnswerId") REFERENCES "Answers"("Id")
);

-- Таблица связи Question-Category (многие-ко-многим)
CREATE TABLE "CategoryQuestion" (
    "CategoriesId" UUID NOT NULL,
    "QuestionsId" UUID NOT NULL,
    PRIMARY KEY ("CategoriesId", "QuestionsId"),
    FOREIGN KEY ("CategoriesId") REFERENCES "Categories"("Id"),
    FOREIGN KEY ("QuestionsId") REFERENCES "Questions"("Id")
);

-- Вставка 20 категорий
INSERT INTO "Categories" ("Id", "Title") VALUES
(uuid_generate_v4(), 'Математика'),
(uuid_generate_v4(), 'История'),
(uuid_generate_v4(), 'География'),
(uuid_generate_v4(), 'Физика'),
(uuid_generate_v4(), 'Химия'),
(uuid_generate_v4(), 'Биология'),
(uuid_generate_v4(), 'Литература'),
(uuid_generate_v4(), 'Искусство'),
(uuid_generate_v4(), 'Музыка'),
(uuid_generate_v4(), 'Технологии'),
(uuid_generate_v4(), 'Кино'),
(uuid_generate_v4(), 'Спорт'),
(uuid_generate_v4(), 'Политика'),
(uuid_generate_v4(), 'Экономика'),
(uuid_generate_v4(), 'Психология'),
(uuid_generate_v4(), 'Философия'),
(uuid_generate_v4(), 'Программирование'),
(uuid_generate_v4(), 'Астрономия'),
(uuid_generate_v4(), 'Языки'),
(uuid_generate_v4(), 'Культура');

-- Вставка 50 ответов
INSERT INTO "Answers" ("Id", "Title") VALUES
(uuid_generate_v4(), 'Ответ 1'),
(uuid_generate_v4(), 'Ответ 2'),
(uuid_generate_v4(), 'Ответ 3'),
(uuid_generate_v4(), 'Ответ 4'),
(uuid_generate_v4(), 'Ответ 5'),
(uuid_generate_v4(), 'Ответ 6'),
(uuid_generate_v4(), 'Ответ 7'),
(uuid_generate_v4(), 'Ответ 8'),
(uuid_generate_v4(), 'Ответ 9'),
(uuid_generate_v4(), 'Ответ 10'),
(uuid_generate_v4(), 'Ответ 11'),
(uuid_generate_v4(), 'Ответ 12'),
(uuid_generate_v4(), 'Ответ 13'),
(uuid_generate_v4(), 'Ответ 14'),
(uuid_generate_v4(), 'Ответ 15'),
(uuid_generate_v4(), 'Ответ 16'),
(uuid_generate_v4(), 'Ответ 17'),
(uuid_generate_v4(), 'Ответ 18'),
(uuid_generate_v4(), 'Ответ 19'),
(uuid_generate_v4(), 'Ответ 20'),
(uuid_generate_v4(), 'Ответ 21'),
(uuid_generate_v4(), 'Ответ 22'),
(uuid_generate_v4(), 'Ответ 23'),
(uuid_generate_v4(), 'Ответ 24'),
(uuid_generate_v4(), 'Ответ 25'),
(uuid_generate_v4(), 'Ответ 26'),
(uuid_generate_v4(), 'Ответ 27'),
(uuid_generate_v4(), 'Ответ 28'),
(uuid_generate_v4(), 'Ответ 29'),
(uuid_generate_v4(), 'Ответ 30'),
(uuid_generate_v4(), 'Ответ 31'),
(uuid_generate_v4(), 'Ответ 32'),
(uuid_generate_v4(), 'Ответ 33'),
(uuid_generate_v4(), 'Ответ 34'),
(uuid_generate_v4(), 'Ответ 35'),
(uuid_generate_v4(), 'Ответ 36'),
(uuid_generate_v4(), 'Ответ 37'),
(uuid_generate_v4(), 'Ответ 38'),
(uuid_generate_v4(), 'Ответ 39'),
(uuid_generate_v4(), 'Ответ 40'),
(uuid_generate_v4(), 'Ответ 41'),
(uuid_generate_v4(), 'Ответ 42'),
(uuid_generate_v4(), 'Ответ 43'),
(uuid_generate_v4(), 'Ответ 44'),
(uuid_generate_v4(), 'Ответ 45'),
(uuid_generate_v4(), 'Ответ 46'),
(uuid_generate_v4(), 'Ответ 47'),
(uuid_generate_v4(), 'Ответ 48'),
(uuid_generate_v4(), 'Ответ 49'),
(uuid_generate_v4(), 'Ответ 50');

-- Вставка 50 вопросов
-- Для упрощения примера связываем вопрос с ответом 1 к 1
DO $$
DECLARE 
    i INT := 1;
    answer_id UUID;
BEGIN
    FOR answer_id IN SELECT "Id" FROM "Answers" LOOP
        INSERT INTO "Questions" ("Id", "Title", "AnswerId")
        VALUES (uuid_generate_v4(), CONCAT('Вопрос ', i), answer_id);
        i := i + 1;
    END LOOP;
END $$;

-- Связка вопросов с категориями
-- Для примера случайная привязка (например, первые 10 вопросов к первой категории и так далее)
WITH question_ids AS (
    SELECT "Id", ROW_NUMBER() OVER () as rn FROM "Questions"
),
category_ids AS (
    SELECT "Id", ROW_NUMBER() OVER () as rn FROM "Categories"
)
INSERT INTO "CategoryQuestion" ("CategoriesId", "QuestionsId")
SELECT 
    (SELECT "Id" FROM category_ids WHERE rn = ((q.rn - 1) % 20) + 1),
    q."Id"
FROM question_ids q;
