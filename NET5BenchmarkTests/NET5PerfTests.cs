// Custom file header. Copyright and License info.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;

[TestClass]
public class PerfTests1
{

    private ImmutableList<int> _list = ImmutableList.Create(Enumerable.Range(0, 1_000).ToArray());
    [TestMethod]
    public void SumPerfTest()
    {
        int sum = 0;

        for (int i = 0; i < 1_000; i++)
            if (_list.Contains(i))
                sum += i;
    }

    [TestMethod]
    public void RegexPerfTest()
    {
        string _input = new HttpClient().GetStringAsync("http://www.gutenberg.org/cache/epub/1112/pg1112.txt").Result;
        new Regex(@"^.*\blove\b.*$", RegexOptions.Multiline | (RegexOptions.None));
        new Regex(@"^.*\bRomeo\b.*$", RegexOptions.Multiline | (RegexOptions.None));
        new Regex(@"^.*\bJuliet\b.*$", RegexOptions.Multiline | (RegexOptions.None));
        new Regex(@"^.*\blove\b.*$", RegexOptions.Multiline | (RegexOptions.None));
        new Regex(@"^.*\bRomeo\b.*$", RegexOptions.Multiline | (RegexOptions.None));
        new Regex(@"^.*\bJuliet\b.*$", RegexOptions.Multiline | (RegexOptions.None));
        new Regex(@"^.*\blove\b.*$", RegexOptions.Multiline | (RegexOptions.None));
        new Regex(@"^.*\bRomeo\b.*$", RegexOptions.Multiline | (RegexOptions.None));
        new Regex(@"^.*\bJuliet\b.*$", RegexOptions.Multiline | (RegexOptions.None));
        new Regex(@"^.*\blove\b.*$", RegexOptions.Multiline | (RegexOptions.None));
        new Regex(@"^.*\bRomeo\b.*$", RegexOptions.Multiline | (RegexOptions.None));
        new Regex(@"^.*\bJuliet\b.*$", RegexOptions.Multiline | (RegexOptions.None));
    }

    [TestMethod]
    public void JsonSerializationPerfTest()
    {
        MemoryStream _stream = new MemoryStream();
        DateTime[] _array = Enumerable.Range(0, 1000).Select(_ => DateTime.UtcNow).ToArray();

        for (int i = 0; i < 1000; i++)
        {
            _stream.Position = 0;
            JsonSerializer.SerializeAsync(_stream, _array);
        }

    }

    [TestMethod]
    public void MyTestMethod()
    {

    }
}

