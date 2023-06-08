// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Net;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.GZip;
using Newtonsoft.Json;
using Tyket.Parser.Model;

DateTime start = DateTime.Now;

string url = "https://staging.media.tyket.app/dotnet-test-data.json.gz";

long total = 0, totalFlorida=0;
double sumIncomeFlorida=0;
ContactModel highestContact = new ContactModel() { Yearly_Income = double.MinValue };

HttpClient httpClient = new HttpClient();
using (System.IO.Stream fs = await httpClient.GetStreamAsync(url))
using (GZipInputStream gzipStream = new GZipInputStream(fs))
using (StreamReader streamReader = new StreamReader(gzipStream))
using (JsonTextReader reader = new JsonTextReader(streamReader))
{
    reader.SupportMultipleContent = true;

    var serializer = new JsonSerializer();
    while (reader.Read())
    {
        if (reader.TokenType == JsonToken.StartObject)
        {
            total++;
            var contact = serializer.Deserialize<ContactModel>(reader);
            if (contact.Address.State_Name == "Florida")
            {
                totalFlorida++;
                sumIncomeFlorida += contact.Yearly_Income;
                if (contact.Yearly_Income > highestContact.Yearly_Income)
                {
                    highestContact = contact;
                }
            }
        }
    }
}

Console.WriteLine("How many items are there?");
Console.WriteLine(total);
Console.WriteLine("How many items with the state of \"Florida\" are there?");
Console.WriteLine(totalFlorida);
Console.WriteLine("What are the id, full name and income for the person with the highest income in the state of \"Florida\"?");
Console.WriteLine(highestContact.ToString());
Console.WriteLine("What is the average income for the state of \"Florida\"?");
Console.WriteLine(String.Format("{0:C}", sumIncomeFlorida / totalFlorida));

DateTime end = DateTime.Now;
Console.WriteLine();
Console.WriteLine($"Total Time: {end.Subtract(start).TotalMilliseconds}ms");