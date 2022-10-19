using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullElastiSearch.Services.impl
{
    public class SempleSearch
    {
        public void Search(string word, IEnumerable<string> data)//строка поиска(слово) и где мы будем его искать (источник данных)
        {
            foreach (var item in data)
            {
                if (item.Contains(word, StringComparison.InvariantCultureIgnoreCase))//таким образом мы смотрим есть ли наша строка которую ищем ,без учета регистра 
                {
                    Console.WriteLine(item);//если нашли слово в статье то выводим эту статью на экран
                }
            }
        }
    }
}
