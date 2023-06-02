using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace QuizSolver.Model
{
    public static class DataBaseManager
    {
        private static int ExecuteNonQuery(SQLiteConnection sqlite_conn, String commandText)
        {
            SQLiteCommand command = sqlite_conn.CreateCommand();
            command.CommandText = commandText;
            return command.ExecuteNonQuery();
        }

        private static void InsertQuestion(SQLiteConnection sqlite_conn, Question question)
        {
            SQLiteCommand insertQuery = new SQLiteCommand("INSERT INTO Questions VALUES (@number, @contents)", sqlite_conn);
            insertQuery.Parameters.Add("@number", DbType.Int32).Value = question.Number;
            insertQuery.Parameters.Add("@contents", DbType.String).Value = question.QuestionContents;
            insertQuery.ExecuteNonQuery();
        }

        private static void InsertQuestionAnswers(SQLiteConnection sqlite_conn, Question question)
        {
            foreach (Answer answer in question.Answers)
            {
                SQLiteCommand insertQuery = new SQLiteCommand("INSERT INTO Answers VALUES (@question_number, @number, @contents, @correct)", sqlite_conn);
                insertQuery.Parameters.Add("@question_number", DbType.Int32).Value = question.Number;
                insertQuery.Parameters.Add("@number", DbType.Int32).Value = answer.Number;
                insertQuery.Parameters.Add("@contents", DbType.String).Value = answer.Contents;
                insertQuery.Parameters.Add("@correct", DbType.Boolean).Value = answer.Correct;
                insertQuery.ExecuteNonQuery();
            }
        }

        public static void SaveQuizToDB(Quiz quiz, String path)
        {
            using (var connection = new SQLiteConnection($"Data Source={path}"))
            {
                connection.Open();

                ExecuteNonQuery(connection, "DROP TABLE IF EXISTS Questions;");
                ExecuteNonQuery(connection, "DROP TABLE IF EXISTS Answers;");

                ExecuteNonQuery(connection, "CREATE TABLE Questions (number INT PRIMARY KEY, contents TEXT);");
                ExecuteNonQuery(connection, "CREATE TABLE Answers (question_number INT, number INT, contents TEXT, correct INT);");

                foreach (Question question in quiz.Questions)
                {
                    InsertQuestion(connection, question);
                    InsertQuestionAnswers(connection, question);
                }

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM ANSWERS;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int questionNumber = reader.GetInt32(0);
                        int number = reader.GetInt32(1);
                        String contents = reader.GetString(2);
                        bool correct = reader.GetBoolean(3);

                        Debug.WriteLine($"{questionNumber}, {number}, {contents}, {correct}");
                    }
                }
            }
        }

        public static void SaveQuizToEncryptedDB(Quiz quiz, String path, String password)
        {
            if (System.IO.File.Exists(path))
                Model.Cryptography.DecryptFile(path, password);

            Model.DataBaseManager.SaveQuizToDB(quiz, path);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Model.Cryptography.EncryptFile(path, password);

        }

        private static List<Answer> GetQuestionAnswers(SQLiteConnection sqlite_conn, int questionNumber)
        {
            List<Answer> answers = new List<Answer>();
            SQLiteCommand selectQuery = new SQLiteCommand(
                "SELECT Answers.number, Answers.contents, correct " +
                "FROM Questions INNER JOIN Answers ON Questions.number = Answers.question_number " +
                "WHERE question_number = @question_number " +
                "ORDER BY Answers.number", sqlite_conn);

            selectQuery.Parameters.Add("@question_number", DbType.Int32).Value = questionNumber;

            using (var reader = selectQuery.ExecuteReader())
            {
                while (reader.Read())
                {
                    int number = reader.GetInt32(0);
                    String contents = reader.GetString(1);
                    bool correct = reader.GetBoolean(2);

                    answers.Add(new Answer(number, contents, correct));
                }
            }

            return answers;
        }

        public static Quiz ReadQuizFromDB(String path)
        {
            Quiz newQuiz = new Quiz();
            newQuiz.Name = Path.GetFileNameWithoutExtension(path);

            using (var connection = new SQLiteConnection($"Data Source={path}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Questions ORDER BY number";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int questionNumber = reader.GetInt32(0);
                        String contents = reader.GetString(1);
                        List<Answer> answers = GetQuestionAnswers(connection, questionNumber);
                        newQuiz.Questions.Add(new Question(questionNumber, contents, new ObservableCollection<Answer>(answers)));
                    }
                }
            }

            return newQuiz;
        }

        public static Quiz ReadQuizFromEncryptedDB(String path, String password)
        {
            Quiz newQuiz = new Quiz();
            Model.Cryptography.DecryptFile(path, password);
            newQuiz = Model.DataBaseManager.ReadQuizFromDB(path);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Model.Cryptography.EncryptFile(path, password);

            return newQuiz;
        }
    }
}
