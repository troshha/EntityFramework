using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (VoiceRecorderContext db = new VoiceRecorderContext())
            {
                // создаем два объекта User
                VoiceRecorder note1 = new VoiceRecorder { Note = "Hi , Siri", Date = DateTime.ParseExact("20110916 11:02", "yyyyMMdd HH:mm",null) };
                VoiceRecorder note2 = new VoiceRecorder { Note = "Hello i am Brad", Date = DateTime.ParseExact("20151102 10:03", "yyyyMMdd HH:mm", null) };
                VoiceRecorder note3 = new VoiceRecorder { Note = "Hola", Date = DateTime.ParseExact("20110207 07:00", "yyyyMMdd HH:mm", null) };
                VoiceRecorder note4 = new VoiceRecorder { Note = "Привет", Date = DateTime.ParseExact("20191007 19:00", "yyyyMMdd HH:mm", null) };
                VoiceRecorder note5 = new VoiceRecorder { Note = "Hello i am Jhon", Date = DateTime.ParseExact("20161107 12:30", "yyyyMMdd HH:mm", null) };

                // добавляем их в бд
                db.Notes.Add(note1);
                db.Notes.Add(note2);
                db.Notes.Add(note3);
                db.Notes.Add(note4);
                db.Notes.Add(note5);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

                // получаем объекты из бд и выводим на консоль
                var notes = db.Notes;
                Console.WriteLine("Список объектов:");
                foreach (VoiceRecorder n in notes)
                {
                    Console.WriteLine("{0}.{1} - {2}", n.Id, n.Note, n.Date);
                }
                Console.WriteLine("Hours notes:");
                var hnotes = db.Notes.Where(p => p.Date.Hour == 11);
                foreach (var item in hnotes) { Console.WriteLine("Note: " + item.Note +" Hour: "+ item.Date.Hour); }
                Console.WriteLine("Day notes:");
                var dnotes = db.Notes.Where(p => p.Date.Day == 7);
                foreach (var item in dnotes) { Console.WriteLine("Note: " + item.Note + " Day: " + item.Date.Day); }
                Console.WriteLine("Month notes witch contains Hello:");
                var mnotes = db.Notes.Where(p => p.Date.Month == 9)
                    .Union(db.Notes.Where(p => p.Note.Contains("Hello"))); 
                    
                foreach (var item in mnotes) { Console.WriteLine("Note: " + item.Note + " Month: " + item.Date.Month); }
                Console.Write("Count of all notes:");
                int number1 = db.Notes.Count();
                Console.WriteLine(number1);
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [VoiceRecorders]");
                db.SaveChanges();
            }
            Console.Read();
            
        }
    }
}
