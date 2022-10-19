using ElasticSearch.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullElastiSearch.Services.impl
{
    public class DocumentRepository: IDocumentRepository
    {
        private readonly DocumentaDbContext _context;

        public DocumentRepository(DocumentaDbContext context)
        {
            _context = context;
        }

        public void LoadDocument()
        {
            using(var streamReader=new StreamReader(AppContext.BaseDirectory + "data.txt"))//создаем читателя который будет читать наш документ
            {
                while (!streamReader.EndOfStream)
                {
                    var doc=streamReader.ReadLine().Split('\t');
                    if(doc.Length > 1&& int.TryParse(doc[0],out int id))//берем наши строкик и приводим их к типу инт
                    {
                        _context.Documents.Add(new Document //если оба значения типа инт то доавбляем её в базу данных
                        {
                            Id = id,
                            Content = doc[1]
                        });
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}
