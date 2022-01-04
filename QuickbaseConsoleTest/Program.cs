using Quickbase;
using QuickbaseConsoleTest;
using System.Text.Json;

IQbContext context = new QbContext()
{
    Realm = "5yintelligence.quickbase.com",
    Usertoken = "b5imym_nwc7_r4nevnbtewpskbzf6kw9c5qqevp"
};

Dictionary<string, string> data = new Dictionary<string, string>();
data.Add("FirstName", "10");

var contact1 = new Contact() { FirstName = "Mark", LastName = "Warstler" };
var contact2 = new Contact() { FirstName = "Kevin", LastName = "Shuler", Title = "CEO", RecordID = "17", DateCreated = "01-13-1981"};
contact2.MapPropertiesToFids(data);

ContactList contactList = new ContactList();
if (contact1.Title == "CEO")
{
    contactList.AddRecord(contact1); 
}
if (contact2.Title == "CEO")
{
    contactList.AddRecord(contact2); 
}

QbUpsertRecordsRequest<Contact> request = new QbUpsertRecordsRequest<Contact>(context, contactList);

QbReadRecordsRequest<Contact> readRequest = new QbReadRecordsRequest<Contact>(context, contactList, where: "{3.EX.'17'}", select: new List<object>() { 3,6,7});

request.Send();

ContactList newContactRecords = (ContactList)await readRequest.Read();

Contact contactNumber1 = (Contact)newContactRecords.QbRecords[0];

Console.WriteLine(contactNumber1.FirstName);

