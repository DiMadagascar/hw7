using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homework_07
{
    struct Diary
    {
        private string path; // путь к файлу с данными
        private Note[] notes; // Основной массив для хранения данных
        int index; // текущий элемент для добавления в notes
        /// <summary>
        /// Констрктор
        /// </summary>
        /// <param name="Path">Путь в файлу с данными</param>
        public Diary(string Path)
        {
            this.path = Path; // Сохранение пути к файлу с данными
            this.index = 0; // текущая позиция для добавления сотрудника в workers
            this.titles = new string[0]; // инициализаия массива заголовков   
            this.notes = new Note[1]; // инициализаия массива сотрудников.    | изначально предпологаем, что данных нет

            this.Load(); // Загрузка данных
        }
        string[] titles;//массив заголовков
        /// <summary>
        /// Метод для выбора действий пользователя
        /// </summary>
        public void Voice()
        {
            string key;//переменная для хранения выбора пользователя
            Console.WriteLine("Выберите действие, нажав соответствующую клавишу. 1.Открыть все записи ежедневника или " +
                "2. Открыть записи за определенные даты. 3. Внести запись. 4. Редактировать запись. 5. Удалить запись 6. Сортировать записи");
            key = Console.ReadLine();//считываем выбор
            ///В зависимости от выбора действи1 передаем команду соотвествующему методу
            switch (key)
            {
                case "1":
                    OpenAllDiayry();//Метод открывающий все записи
                    break;
                case "2":
                    OpenForDays();//Метод открывающий  записи за определенный период
                    break;
                case "3":
                    
                    WriteNote();//Метод для внесения новой записи
                    break;
                case "4":
                    EditNote();//Метод редактирующий определенную запись
                    break;
                case "5":
                    DeleteNote();//Метод для удаления записи
                    break;
                case "6":
                    SortNotes();//Метод сортировки записей
                    break;
            }
            Voice();//Возвращаем пользователя к выбору действий
        }
        /// <summary>
        /// Метод загрузки данных
        /// </summary>
        public void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                titles = sr.ReadLine().Split(',');


                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split(',');

                    Add(new Note(Convert.ToInt32(args[0]), Convert.ToDateTime(args[1]), Convert.ToDateTime(args[2]),
                        Convert.ToDateTime(args[3]), args[4]));
                }
            }
        }
        /// <summary>
        /// Метод для увеличения размера массива
        /// </summary>
        /// <param name="Flag"></param>
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.notes, this.notes.Length +1);
            }
        }
        /// <summary>
        /// Метод добавления записи в массив
        /// </summary>
        /// <param name="ConcreteNote"></param>
        public void Add(Note ConcreteNote)
        {
            this.Resize(index >= this.notes.Length);
            this.notes[index] = ConcreteNote;
            this.index++;
        }

        /// <summary>
        /// Метод открывающий все записи
        /// </summary>
                public void OpenAllDiayry()
        {
            Console.WriteLine($"{this.titles[0],5} {this.titles[1],12} {this.titles[2],15} {this.titles[3],8} {this.titles[4],20}");

            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this.notes[i].Print());
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Метод открывающий  записи за определенный период
        /// </summary>
        public void OpenForDays()
        {
            Console.WriteLine("Введите желаемый диапазон дат в формате дд.мм.гггг:");
            Console.WriteLine("От - ");
            DateTime fromdate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("До - ");
            DateTime todate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine($"{this.titles[0],5} {this.titles[1],12} {this.titles[2],15} {this.titles[3],8} {this.titles[4],20}");

            for (int i = 0; i < index; i++)
            {
                if ((this.notes[i].Date>= fromdate)&(this.notes[i].Date<=todate))
                Console.WriteLine(this.notes[i].Print());
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Метод для внесения новой записи
        /// </summary>
        public void WriteNote()
        {
            
            DateTime newdateofrecording = DateTime.Now;
            Console.WriteLine("Введите дату события в формате дд.мм.гггг:");
            DateTime    newdate= Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введите время события в формате чч:мм:");
            DateTime newtime = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введите  событие:");
            string newrecord = Console.ReadLine();
            Add(new Note(Convert.ToInt32(index+1), Convert.ToDateTime(newdateofrecording), Convert.ToDateTime(newdate), 
                Convert.ToDateTime(newtime), newrecord));
            Save();
            
        }
        /// <summary>
        /// Метод сохранения данных
        /// </summary>
        public void Save()
        {

            string temp = String.Format("{0},{1},{2},{3},{4}",
                                           this.titles[0],
                                           this.titles[1],
                                           this.titles[2],
                                           this.titles[3],
                                           this.titles[4]);

            File.WriteAllText(path, $"{temp}\n", Encoding.UTF8);

            for (int i = 0; i < index; i++)
            {
                temp = String.Format("{0},{1},{2},{3},{4}",
                               this.notes[i].Number,
                               this.notes[i].DateOfRecording.ToShortDateString(),
                               this.notes[i].Date.ToShortDateString(),
                               this.notes[i].Time.ToLongTimeString(),
                               this.notes[i].Record);
                File.AppendAllText(path, $"{temp}\n", Encoding.UTF8);
            }



        }
        /// <summary>
        /// Метод редактирующий определенную запись
        /// </summary>
        public void EditNote()
        {
            OpenAllDiayry();
            Console.WriteLine("Какую запись Вы хотите отредактировать? Впишите номер");
            int newnumber = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < index; i++)
            {
                if (this.notes[i].Number == newnumber)
                {
                    notes[i].DateOfRecording= DateTime.Now;
                    Console.WriteLine("Введите дату события в формате дд.мм.гггг:");
                    notes[i].Date = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Введите время события в формате чч:мм:");
                    notes[i].Time = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Введите  событие:");
                    notes[i].Record = Console.ReadLine();
                    Save();
                    
                    break;
                }
            }

        }
        /// <summary>
        /// Метод удаляющий определенную запись
        /// </summary>
        public void DeleteNote()
        {
            OpenAllDiayry();
            Console.WriteLine("Какую запись Вы хотите удалить? Впишите номер");
            int newnumber = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < index; i++)
            {
                if (this.notes[i].Number == newnumber)
                {
                    for (int a = i; a < this.notes.Length - 1; a++)
                    {
                        // moving elements downwards, to fill the gap at [index]
                        notes[a] = notes[a + 1];
                        notes[a].Number--;
                    }
                    // finally, let's decrement Array's size by one
                    Array.Resize(ref notes, notes.Length - 1);
                    index--;
                    break;
                }
            }
            Save();

        }
        /// <summary>
        /// Метод сортировки по определенному полю
        /// </summary>
        public void SortNotes()
        {
            Console.WriteLine("По какому полю хотите сортировать записи: 1. Дата занесения записи 2. дата события 3. Время события" +
                " 4. Событие 5. По номеру");
            string sortindex = Console.ReadLine();
            
            switch (sortindex)
            {
                case "1":
                    var sortnotes = from t in notes // определяем каждый объект из notes как t
                                        
                                        orderby t.DateOfRecording  // упорядочиваем по возрастанию
                                        select t; // выбираем объект
                    foreach (Note note in  sortnotes)
            {
                        Console.WriteLine(note.Print());


                    }

                    break;
                case "2":
                     sortnotes = from t in notes // определяем каждый объект из teams как t

                                    orderby t.Date  // упорядочиваем по возрастанию
                                    select t; // выбираем объект
                    foreach (Note note in  sortnotes)
            {
                        Console.WriteLine(note.Print());


                    }
                    break;
                case "3":
                     sortnotes = from t in notes // определяем каждый объект из teams как t

                                    orderby t.Time  // упорядочиваем по возрастанию
                                    select t; // выбираем объект
                    foreach (Note note in sortnotes)
                    {
                        Console.WriteLine(note.Print());


                    }

                    break;
                case "4":
                     sortnotes = from t in notes // определяем каждый объект из teams как t

                                    orderby t.Record  // упорядочиваем по возрастанию
                                    select t; // выбираем объект
                    foreach (Note note in sortnotes)
                    {
                        Console.WriteLine(note.Print());


                    }
                    break;
                case "5":
                     sortnotes = from t in notes // определяем каждый объект из teams как t

                                    orderby t.Number  // упорядочиваем по возрастанию
                                    select t; // выбираем объект
                    foreach (Note note in sortnotes)
                    {
                        Console.WriteLine(note.Print());


                    }
                    break;
            }
            
            Console.ReadKey();

            
        }
    }
}
