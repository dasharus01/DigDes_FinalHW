using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace finalHW_client
{
     

    class Program
    {
        private static int BytsSize = 256000000;
        // записываем имя файла к имеющемуся пути
        public static string ConvertPath(string path)
        {
            int posishionEnd = path.Length - 1;

            while (posishionEnd >= 0 && path[posishionEnd] != '/' && path[posishionEnd] != '\\')
                --posishionEnd;
            return path.Substring(0, posishionEnd + 1) + "result.txt";
        }
        static void Main(string[] args)
        {
            //запрос пути к файлу
            Console.Write("Введите путь к файлу с текстом: ");
            
            String file = Console.ReadLine();
            //возникла проблема с открытием файла
            if (!File.Exists(file))
            {
                Console.WriteLine("Не удалось считать данные из файла");
                return;
            }
            //считываем данные из файла в массив строк
            string[] data = File.ReadAllLines(file, Encoding.UTF8);
            //настройка соединения
            string uri = "net.tcp://localhost:7061/CounterService";
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            
            binding.MaxReceivedMessageSize = BytsSize;
            binding.MaxBufferSize = BytsSize;
            var channel = new ChannelFactory<ICounterService>(binding);
            var endpoint = new EndpointAddress(uri);
            var proxy = channel.CreateChannel(endpoint);
            //учтановка соединения. Отправка пути к файлу
            Dictionary<string, int> word_result = proxy?.wordCountFun(data);
            //проверка, что с результатом всё ок
            if (word_result != null )
            {
                try
                {
                    //Открываем файл для записи

                    StreamWriter sw = new StreamWriter(ConvertPath(file));
                    // сортируем словарь по убыванию повторений
                    var sortdic = from pair in word_result 
                                  orderby pair.Value descending
                                  select pair;
                    // записываем в файл
                    foreach (KeyValuePair<string, int> valuePair in sortdic)
                    {
                        sw.WriteLine(valuePair.Key + "  " + valuePair.Value);
                    }

                    //закрываем файл
                    sw.Close();
                    Console.WriteLine("Программа успешно выполнена!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
            //если не всё ок, вывод проблемы
            else
            {
                Console.WriteLine("Имеются проблемы со словарем");
            }
        }
    }
}




