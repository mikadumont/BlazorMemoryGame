// Custom file header. Copyright and License info.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class NET5AnalyzersTests
{
    public void ExpressionIsAlwaysTrueOrFalse(DateTime dateTime)
    {
        if (dateTime == null) // warning CS8073
        {
            return;
        }
    }

    public void M(DateTime? dateTime) // We accept a null DateTime
    {
        if (dateTime == null) // No Warnings
        {
            return;
        }
    }

    public void M1()
    {
        int length = 3;

        for (int i = 0; i < length; i++)
        {
            Span<int> numbers = stackalloc int[length]; // CA2014
            numbers[i] = i;
        }
    }

    public void M2(string str)
    {
        ReadOnlySpan<char> slice = str.AsSpan()[1..3]; // CA1831
    }

    public void M3()
    {
        try
        {
            throw new Exception();
        }
        catch (Exception ex)
        {
            // probably logging some info here...

            // Re-throwing caught exception changes stack information
            throw ex; // CA2200
        }
    }
}
