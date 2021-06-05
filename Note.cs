using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_07
{
    struct Note
    {
        #region Конструкторы
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number">номер записи</param>
        /// <param name="dateOfRecording">Дата занесения записи</param>
        /// <param name="date">Дата события</param>
        /// <param name="time">время события</param>
        /// <param name="record">наименование события</param>
        /// 
        public Note(int Number,DateTime DateOfRecording,DateTime Date, DateTime Time, string Record)
        {
            this.number = Number;
            this.dateOfRecording = DateOfRecording;
            this.date = Date;
            this.time = Time;
            this.record = Record;
            
        }
        #endregion
        #region Методы
        public string Print()
        {
            return $"{this.number,5} {this.dateOfRecording.ToShortDateString(),12} {this.date.ToShortDateString(),15} " +
                $"{this.time.ToLongTimeString(),8} {this.record,20}";
        }
        #endregion
        #region Свойства
        /// <summary>
        /// Номер записи
        /// </summary>
        public int Number { get { return this.number; } set { this.number = value; } }
        /// <summary>
        /// Дата занесения записи
        /// </summary>
        public DateTime DateOfRecording { get { return this.dateOfRecording; } set { this.dateOfRecording = value; } }
        /// <summary>
        /// Дата события
        /// </summary>
        public DateTime Date { get { return this.date; } set { this.date = value; } }
        /// <summary>
        /// Время события
        /// </summary>
        public DateTime Time { get { return this.time; } set { this.time = value; } }
        /// <summary>
        /// Событие
        /// </summary>
        public string Record { get { return this.record; } set { this.record = value; } }


        #endregion
        #region Поля
        /// <summary>
        /// Номер
        /// </summary>
        private int number;
        /// <summary>
        /// Дата занесения записи
        /// </summary>
        private DateTime dateOfRecording;
        /// <summary>
        /// Дата события
        /// </summary>
        private DateTime date;
        /// <summary>
        /// Время события
        /// </summary>
        private DateTime time;
        /// <summary>
        /// Событие
        /// </summary>
        private string record;
        
        #endregion
    }
}
