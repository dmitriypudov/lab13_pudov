using System;
using System.Windows.Forms;

namespace laba13_pudov
{
    public partial class Form1 : Form // объявляем класс формы
    {
        public Form1() // конструктор класса формы
        {
            InitializeComponent(); // инициализация компонентов формы
        }

        const double m = 0, sd = 0.015, k = m - 0.5 * sd * sd; // объявление и инициализация констант

        double tenge, rubles, day, boxG, temp, temp2; // объявление переменных

        private void timer1_Tick_1(object sender, EventArgs e) // функция-обработчик тика таймера
        {
            chart1.ChartAreas[0].AxisX.Maximum = Math.Ceiling(day); // устанавливаем максимальное значение оси X на графике

            Random rand = new Random(); // создаем объект генератора случайных чисел
            temp = rand.NextDouble(); // генерируем случайное число от 0 до 1
            temp2 = rand.NextDouble(); // генерируем еще одно случайное число от 0 до 1
            boxG = Math.Sqrt(-2.0 * Math.Log(temp)) * Math.Cos(2.0 * Math.PI * temp2); // вычисляем значение по формуле Бокса-Мюллера

            tenge = tenge * Math.Exp(k + sd * boxG); // вычисляем новое значение курса 
            rubles = rubles * Math.Exp(k + sd * boxG); // вычисляем новое значение курса 

            chart1.Series[0].Points.AddXY(day, rubles); // добавляем новую точку на график 
            chart1.Series[1].Points.AddXY(day, tenge); // добавляем новую точку на график 
            day++; // увеличиваем количество дней на 1
        }

        private void btn_Click(object sender, EventArgs e) // функция-обработчик нажатия на кнопку
        {
            day = 1; // сбрасываем количество дней в ноль
            tenge = (double)RubleEdit.Value; // инициализируем начальное значение курса 
            rubles = (double)TengeEdit.Value; // инициализируем начальное значение курса 

            chart1.Series[0].Points.Clear(); // очищаем данные на графике 
            chart1.Series[0].Points.AddXY(0, rubles); // добавляем начальную точку на график 
            chart1.Series[1].Points.Clear(); // очищаем данные на графике 
            chart1.Series[1].Points.AddXY(0, tenge); // добавляем начальную точку на график 

            timer1.Enabled = !timer1.Enabled; // запускаем или останавливаем таймер
            if (timer1.Enabled == false) // если таймер остановлен
            {
                btn.Text = "Старт"; // меняем текст кнопки на "Старт"
            }
            else // иначе
            {
                btn.Text = "Стоп"; // меняем текст кнопки на "Стоп"
            }
        }
    }
}