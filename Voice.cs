using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_07
{
    class Voice
    {
        /// <summary>
        /// Метод для выбора действий пользователя
        /// </summary>
        public void Makeyorvoice()
        {
            string key;//переменная для хранения выбора пользователя
            Console.WriteLine("Выберите действие, нажав соответствующую клавишу. 1.Открыть все записи ежедневника или " +
                "2. Открыть записи за определенные даты. 3. Внести запись. 4. Редактировать запись. 5. Удалить запись 6. Сортировать записи");
            key = Console.ReadLine();//считываем выбор
            string path = @"data.csv";// присваиваем переменнной путь к файлу с ежедневником
            Diary diary = new Diary(path);//создаем объект типа diary с параметром пути
            ///В зависимости от выбора действи1 передаем команду соотвествующему методу
            switch (key)
            {
                case "1":
                    diary.OpenAllDiayry();//Метод открывающий все записи
                    break;
                case "2":
                    diary.OpenForDays();//Метод открывающий  записи за определенный период
                    break;
                case "3":

                    diary.WriteNote();//Метод для внесения новой записи
                    break;
                case "4":
                    diary.EditNote();//Метод редактирующий определенную запись
                    break;
                case "5":
                    diary.DeleteNote();//Метод для удаления записи
                    break;
                case "6":
                    diary.SortNotes();//Метод сортировки записей
                    break;
            }
            Makeyorvoice();//Возвращаем пользователя к выбору действий
        }
    }
}
