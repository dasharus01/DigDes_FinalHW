using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static finalHW_service.Program;
using word_count;

namespace finalHW_service
{
    // делаем невозможным осуществление одновременных действий по нескольким клиентам
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CounterService : ICounterService
    {
        public Dictionary<string, int> wordCountFun(string[] data)
        {
            //с использованием нескольких потоков
            var obj = typeof(word_count.wordCount).GetMethod("process_count_par", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            //передаем массив строк
            var result = obj?.Invoke(new word_count.wordCount(), new object[] { data });
            Dictionary<string, int> res  = result as Dictionary<string, int>;
            //проверка на результат
            if (result != null && result.GetType() == typeof(Dictionary<string, int>))
            {
                return res;
            }
            else
            {
                return null;
            }
        }
      

    }
       
    
}

