using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullElastiSearch.Services.impl
{
    public class SimpleSearchV2
    {

        public IEnumerable<string> Search(string word,string iteem)//поиск слов в одной строке в одном документе 
        {
            int pos = 0;
            while (true)
            {
                pos=iteem.IndexOf(word,pos);
                if(pos >= 0)
                {
                    yield return PrettyMatchV2(iteem,pos);  
                }
                else
                {
                    break;
                }
                pos++;
            }
        }

        public void SearchV1(string word, IEnumerable<string> data)//строка поиска(слово) и где мы будем его искать (источник данных)
        {
            foreach (var item in data)
            {
                if (item.Contains(word, StringComparison.InvariantCultureIgnoreCase))//таким образом мы смотрим есть ли наша строка которую ищем ,без учета регистра 
                {
                    Console.WriteLine(PrettyMatch(word,item));//если нашли слово в статье то выводим эту статью на экран
                }
            }
        }

        public string PrettyMatch(string word,string text)
        {
            int position=text.IndexOf(word);//находим позицию данного слово
            int start=Math.Max(0,position-50);//тут мы находим нашу стартовую позицию таким образом мы не сможем выйти в отрицательное число то есть если позиция 2 а у нас -50 будет -48 то есть старт будет 0
            int end=Math.Min(text.Length-1,start+100);//конечная позииция +100 символов в право
            return $"{(start == 0 ? "" : "..")}{text.Substring(start, end - start)}{end == text.Length - 1}";//
        }
        #region SearchV2
        public void SearchV2(string word, IEnumerable<string> data)
        {
            foreach (var item in data)
            {
                int pos = 0;
                while (true)
                {
                    pos = item.IndexOf(word, pos);//ищем первое вложение в подстроке
                    if (pos >= 0)
                    {
                        Console.WriteLine(PrettyMatchV2(item, pos));
                    }
                    else
                    {
                        break;
                    }
                    pos++;
                }
            }
        }
        public string PrettyMatchV2(string word, int pos)
        {
           
            int start = Math.Max(0, pos - 50);//тут мы находим нашу стартовую позицию таким образом мы не сможем выйти в отрицательное число то есть если позиция 2 а у нас -50 будет -48 то есть старт будет 0
            int end = Math.Min(word.Length - 1, start + 100);//конечная позииция +100 символов в право
            return $"{(start == 0 ? "" : "..")}{word.Substring(start, end - start)}{end == word.Length - 1}";//
        }
        #endregion


        public IEnumerable<string> SearchV3(string word,IEnumerable<string> data)
        {
            foreach(var item in data)
            {
                int pos = 0;
                while (true)
                {
                    pos=item.IndexOf(word, pos);
                    if(pos >= 0)
                    {
                        yield return PrettyMatchV2(item, pos);
                    }
                    else
                    {
                        break;
                    }
                }
                
                pos++;
            }
        }
    }
}
