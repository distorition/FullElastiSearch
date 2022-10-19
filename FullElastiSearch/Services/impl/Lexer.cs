using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullElastiSearch.Services.impl
{
    public class Lexer
    {
        public IEnumerable<string> GetTokens(string text)
        {
            int start = 1;
            for(int i = 0; i < text.Length; i++)// мы по символьно перебираем статью 
            {
                if (char.IsLetterOrDigit(text[i]))//таким образом проверяем буква эта или цифра( в нашей статье которую мы перебираем )
                {
                    if (start == -1)//если мы нашли символ который является буквой или числом то мы смещаем старотовую позицию на i
                    {
                        start = i;
                    }
                }
                else
                {
                    if(start >= 0)// тут мы уже смотрим то есть если попал не цифра и не символ и старт больше нуля значит мы набрали слово и нам попался разделитель
                    {
                        yield return GetToken(text, i, start);// возвращаем слово которое нашли 
                        start = -1;// и сбрасываем стартовую позицию
                    }
                }
            }
        }

        private string GetToken(string text,int i,int start)//нормализирует слово и переводим в нижний регистр
        {
            return text.Substring(start,i-start).Normalize().ToLowerInvariant();
        }
    }
}
