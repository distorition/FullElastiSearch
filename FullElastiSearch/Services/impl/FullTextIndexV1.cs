using ElasticSearch.Entity;
using FullElastiSearch.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullElastiSearch.Services.impl
{
    public class FullTextIndexV1
    {
        private readonly DocumentaDbContext context;
        private readonly Lexer lexer= new Lexer();  
        public FullTextIndexV1(DocumentaDbContext context1=null)
        {
            this.context = context1;
        }


        public void BuildIndex()
        {
            foreach(var document in context.Documents.ToArray())//образаемся ко всем Documents которые находятся внутри базы данных
            {
                foreach(var token in lexer.GetTokens(document.Content))// таким образом мы берем статьи из нашых документов и разбиваем их на слова 
                {
                    var word=context.Words.FirstOrDefault(x => x.Text == token);//обращаемся к нашей бд и смотрим есть ли такое слово    
                    int wordId = 0;
                    if (word == null)// если слова нет то добавляем 
                    {
                        var wordObj = new Word
                        {
                            Text = token,
                        };
                        context.Words.Add(wordObj);
                        context.SaveChanges();
                        wordId = wordObj.Id;
                    }
                    else//если слова есть то сохранием его айди 
                    {
                        wordId = word.Id;
                    }

                    var wordDocument=context.WordDocuments.FirstOrDefault(wd=>wd.WordId == wordId&&wd.DocumentId==document.Id);// обращаемся к таблице и смотри айди документа и айди слова если они не равны то добавялем 
                    if(wordDocument == null)
                    {
                        context.WordDocuments.Add(new WordDocument
                        {
                            DocumentId = document.Id,
                            WordId = wordId,
                        });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
