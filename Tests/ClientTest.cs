using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HairSalonProject
{
  public class ClientTest: IDisposable
  {
    public ClientTest()
    {
      // DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI";
      DBConfiguration.ConnectionString = "Data Source=desktop-ddsnb9e;Initial Catalog=hair_salon_test;Integrated Security=SSPI";
    }

    [Fact]
    public void Test_ClientsEmptyAtFirst_0()
    {
      int result = Client.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Client newClient1 = new Client("Jerry Garcia", 1);
      Client newClient2 = new Client("Jerry Garcia", 1);

      Assert.Equal(newClient1, newClient2);
    }

    [Fact]
    public void Test_SavesClientToDatabase()
    {
      Client newClient = new Client("Phil Lesh", 1);
      newClient.Save();

      List<Client> resultList = Client.GetAll();
      List<Client> testList = new List<Client>{newClient};

      Assert.Equal(resultList, testList);
    }

    [Fact]
    public void Test_FindClientInDatabase()
    {
      Client newClient = new Client("Bill Kreutzman", 1);
      newClient.Save();

      Client foundClient = Client.Find(newClient.GetId());

      Assert.Equal(foundClient, newClient);
    }

    [Fact]
    public void Test_UpdateClientNameInDatabase()
    {
      Client testClient = new Client("Jesse McKay", 1);
      testClient.Save();
      testClient.Update("Natalie McKay");
      testClient.Save();

      string updatedName = testClient.GetName();
      string testName = "Natalie McKay";

      Assert.Equal(updatedName, testName);
    }

    [Fact]
    public void Test_Deletes_Stylist_FromDatabase()
    {

      Client newClient1 = new Client("Peter Griffin", 1);
      newClient1.Save();
      Client newClient2 = new Client("Stewie Griffin", 2);
      newClient2.Save();

      newClient1.Delete();
      List<Client> deletedClientList = Client.GetAll();
      List<Client> literalClientList = new List<Client> {newClient2};

      Assert.Equal(deletedClientList, literalClientList);
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}
