using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullElastiSearch.Services.impl
{
    public class DocumentExtractor:IDocumentExtractor//класс который находит строки данных в документе 
    {
        public static IEnumerable<string> DocumentSet()
        {
            return ReadDocumnet(AppContext.BaseDirectory + "data.txt");
        }

        private static IEnumerable<string> ReadDocumnet(string fileName)
        {
            using(var streamReader= new StreamReader(fileName))//добавялем читателя 
            {
                while (!streamReader.EndOfStream)
                {
                    var doc=streamReader.ReadLine()?.Split('\t');//читаем строки    
                    yield return doc[1];// таким образом мы возвращаем перечисление строк чтобы мы потом смогли бы их перебрать
                }
            }
        }
    }
}
