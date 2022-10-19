using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ElasticSearch.Entity;
using FullElastiSearch.Services;
using FullElastiSearch.Services.impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace FullElastiSearch
{
    internal class Sample02
    {
        static void Main(string[] args)
        {

            #region SearchV1
            var host = Host.CreateDefaultBuilder(args).ConfigureServices(setvice =>
            {
                setvice.AddDbContext<DocumentaDbContext>(options =>
                {
                    options.UseSqlServer(@"data source=DESKTOP-T8MKHUM\SQLEXPRESS;initial catalog=DocumentDatabase;User Id=DocumentsDatabasUser;Password=12345");
                });

            }).Build();

           var documentSet= DocumentExtractor.DocumentSet().Take(100).ToArray();// нам нужно будет взять нашу коллекцию перечислений и преобразовать в тип стринг а иначе у нас не получиться достать наши строки 

           // new SempleSearch().Search("monday", documentSet);
            new SimpleSearchV2().SearchV2("monday",documentSet);
            #endregion

            BenchmarkSwitcher.FromAssembly(typeof(Sample02).Assembly).Run(args,new BenchmarkDotNet.Configs.DebugInProcessConfig());//позволяем бенчмарку запускаться в отладчике

            BenchmarkRunner.Run<SearcBenchmarkv1>();//добавляем класс в тестирование на производительность
   
        }
        
    }

    [MemoryDiagnoser]//при выполнение этого теста мы будет анализировать память а точнее сколько памяти вы выделяем на данные методы
    [WarmupCount(1)]//прогрев то есть сколько раз мы запустим наш тест в холостую (обычно всегда все программы заупскают первый раз дольше ) так вот из за этого мы в начале заупстим в холостую эти атрибутом чтобы посомтреть скорость теста ууже потом в нормальном режиме
    [IterationCount(5)]//указываем сколько раз мы будем запускать наш тест а потом берем среднее    
    public class SearcBenchmarkv1
    {

        private readonly string[] _documentSet;

        public SearcBenchmarkv1()
        {
            _documentSet = DocumentExtractor.DocumentSet().Take(100).ToArray();// все что пишеться в конструкторе то не будет учитываться при проведении тестов быстроедйтсвия
        }

        [Benchmark]
        public void SimpleSearch()
        {
            new SimpleSearchV2().SearchV3("monday", _documentSet).ToArray();
        }
    }
}
