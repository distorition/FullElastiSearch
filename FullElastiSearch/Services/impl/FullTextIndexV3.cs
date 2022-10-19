using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullElastiSearch.Services.impl
{
    public class FullTextIndexV3
    {
        private readonly Dictionary<string,HashSet<int>> _index= new Dictionary<string, HashSet<int>>();//таким образом мы будем искать определенное слово и все статьи в котором оно содержиться
        private readonly List<string> _content= new List<string>();
        private readonly Lexer _lexer= new Lexer();
        private readonly SimpleSearchV2 _search= new SimpleSearchV2();

        public void AddStringToIndex(string text)
        {
            int doucmendId=_content.Count;
            foreach(var token in _lexer.GetTokens(text))
            {
                if(_index.TryGetValue(token,out var set))
                {
                    set.Add(doucmendId);
                }
                else
                {
                    _index.Add(token, new HashSet<int>() { doucmendId});
                }
            }
            _content.Add(text);
        }

        public IEnumerable<int> Search(string word)
        {
            word=word.ToLowerInvariant();
            if(_index.TryGetValue(word,out var set))
            {
                return set;
            }
            return Enumerable.Empty<int>();
        }

        public IEnumerable<string> SearchText(string word)
        {
            var documentList=Search(word);
            foreach(var docId in documentList)
            {
                foreach(var match in _search.Search(word,_content[docId]))
                {
                    yield return match;
                }
            }
        }
    }
}
