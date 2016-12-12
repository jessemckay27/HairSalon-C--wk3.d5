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
    public void Test_Save_SavesClientToDatabase()
    {
      Client newClient = new Client("Phil Lesh", 1);
      newClient.Save();

      List<Client> resultList = Client.GetAll();
      List<Client> testList = new List<Client>{newClient};

      Assert.Equal(resultList, testList);
    }

    [Fact]
      public void Test_ToFind_Client_InDatabase()
      {
        Client newClient = new Client("Bill Kreutzman", 1);
        newClient.Save();

        Client foundClient = Client.Find(newClient.GetId());

        Assert.Equal(foundClient, newClient);
      }


    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
