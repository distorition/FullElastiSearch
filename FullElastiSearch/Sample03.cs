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
    internal class Sample03
    {
        static void Main(string[] args)
        {

            
            var host = Host.CreateDefaultBuilder(args).ConfigureServices(setvice =>
            {
                setvice.AddDbContext<DocumentaDbContext>(options =>
                {
                    options.UseSqlServer(@"data source=DESKTOP-T8MKHUM\SQLEXPRESS;initial catalog=DocumentDatabase;User Id=DocumentsDatabasUser;Password=12345");
                });

            }).Build();

            FullTextIndexV1 fullTextIndexV1 = new FullTextIndexV1(host.Services.GetService<DocumentaDbContext>());
            fullTextIndexV1.BuildIndex();

            BenchmarkSwitcher.FromAssembly(typeof(Sample02).Assembly).Run(args, new BenchmarkDotNet.Configs.DebugInProcessConfig());
            BenchmarkRunner.Run<SearchBenchmarkv2>();
        }
    }
    [MemoryDiagnoser]//при выполнение этого теста мы будет анализировать память а точнее сколько памяти вы выделяем на данные методы
    [WarmupCount(1)]//прогрев то есть сколько раз мы запустим наш тест в холостую (обычно всегда все программы заупскают первый раз дольше ) так вот из за этого мы в начале заупстим в холостую эти атрибутом чтобы посомтреть скорость теста ууже потом в нормальном режиме
    [IterationCount(5)]//указываем сколько раз мы будем запускать наш тест а потом берем среднее    
   public class SearchBenchmarkv2
    {
        [Params("inter","Monday","not")]//таким обращом мы сможем сделать наши тесты для трех слов сразу
        public string Query { get; set; }
        private readonly FullTextIndexV3 _index;

        private readonly string[] _documentSet;

      
        public SearchBenchmarkv2()
        {
            _documentSet = DocumentExtractor.DocumentSet().Take(100).ToArray();
            _index = new FullTextIndexV3();
            foreach(var item in _documentSet)
            {
                _index.AddStringToIndex(item);
            }
        }

        [Benchmark(Baseline = true)]
        public void SimpleSearch()
        {
            new SimpleSearchV2().SearchV3(Query, _documentSet).ToArray();
        }

        [Benchmark]
        public void FulltextIndexSearch()
        {
            _index.SearchText(Query).ToArray();
        }
    }
}
