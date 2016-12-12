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

      string originalName = testClient.GetName();
      testClient.Update("Natalie McKay");
      string updatedName = testClient.GetName();

      Assert.Equal(originalName, updatedName);
    }

    [Fact]
    public void Test_Delete_Deletes_Stylist_FromDatabase()
    {

      Client testClient1 = new Client("Dinese", 1);
      testClient1.Save();
      Client testClient2 = new Client("Damian", 2);
      testClient2.Save();

      //Act
      testClient1.Delete();
      List<Client> resultClients = Client.GetAll();
      List<Client> testClientList = new List<Client> {testClient2};

      Assert.Equal(testClientList, resultClients);
    }


    public void Dispose()
    {
      Client.DeleteAll();

    }
  }
}
