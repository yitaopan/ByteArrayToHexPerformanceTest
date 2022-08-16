// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;

Console.WriteLine("Hello, World!");

uint[] CreateLookup32()
{
    var result = new uint[256];
    for (var i = 0; i < 256; i++)
    {
        var s = i.ToString("x2");
        result[i] = s[0] + ((uint)s[1] << 16);
    }

    return result;
}
uint[] _Lookup32 = CreateLookup32();

string ByteArrayToHexViaStringBuilder(byte[] bytes)
{
    StringBuilder stringBuilder = new StringBuilder();
    for (int i = 0; i < bytes.Length; i++)
    {
        stringBuilder.Append(bytes[i].ToString("x2"));
    }
    return stringBuilder.ToString();
}

string ByteArrayToHexViaLookup32(byte[] bytes)
{
    var result = new char[bytes.Length * 2];
    for (int i = 0; i < bytes.Length; i++)
    {
        var val = _Lookup32[bytes[i]];
        result[2 * i] = (char)val;
        result[2 * i + 1] = (char)(val >> 16);
    }
    return new string(result);
}

Stopwatch stopwatch = new Stopwatch();
string testString = "";
int testTime = 1000;
int stringLengthCtrl = 1;
for (int i = 0; i < stringLengthCtrl; i++)
{
    testString += "sairyncgsdkjfhcnskdyjg";
}
byte [] testBytes = Encoding.UTF8.GetBytes(testString);

stopwatch.Start();
for (int i = 0; i < testTime; i++)
{
    ByteArrayToHexViaStringBuilder(testBytes);
}
stopwatch.Stop();
Console.WriteLine("ByteArrayToHexViaStringBuilder average uses: " + stopwatch.ElapsedTicks / testTime);

stopwatch.Reset();

stopwatch.Start();
for (int i = 0; i < testTime; i++)
{
    ByteArrayToHexViaLookup32(testBytes);
}
stopwatch.Stop();
Console.WriteLine("ByteArrayToHexViaLookup32 average uses: " + stopwatch.ElapsedTicks / testTime);