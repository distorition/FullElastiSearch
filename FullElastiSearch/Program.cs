using ElasticSearch.Entity;
using FullElastiSearch.Services;
using FullElastiSearch.Services.impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    //static void Main(string[] args)
    //{
    //    var host = Host.CreateDefaultBuilder(args).ConfigureServices(setvice =>
    //    {
    //        setvice.AddDbContext<DocumentaDbContext>(options =>
    //        {
    //            options.UseSqlServer(@"data source=DESKTOP-T8MKHUM\SQLEXPRESS;initial catalog=DocumentDatabase;User Id=DocumentsDatabasUser;Password=12345");
    //        });

    //        setvice.AddTransient<IDocumentRepository, DocumentRepository>();

    //    }).Build();


    //    host.Services.GetRequiredService<IDocumentRepository>().LoadDocument();
    //}
}
