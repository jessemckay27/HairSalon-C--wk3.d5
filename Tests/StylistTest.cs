using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HairSalonProject
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=desktop-ddsnb9e;Initial Catalog=hair_salon_test;Integrated Security=SSPI";
    }

    [Fact]
    public void Test_StylistsEmptyAtFirst_0()
    {
      int result = Stylist.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Stylist newStylist = new Stylist("Bob Weir");
      Stylist newStylist2 = new Stylist("Bob Weir");

      Assert.Equal(newStylist, newStylist2);
    }


    [Fact]
    public void Test_Save_SavesStylistToDatabase()
    {
      Stylist newStylist = new Stylist("Jerry Garcia");
      newStylist.Save();

      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{newStylist};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToStylistObject()
    {

      Stylist testStylist = new Stylist("Jerry Garcia");
      testStylist.Save();

      Stylist savedStylist = Stylist.GetAll()[0];

      int resultId = savedStylist.GetId();
      int testId = testStylist.GetId();

      Assert.Equal(resultId, testId);
    }

    [Fact]
    public void Test_Find_FindsStylistInDatabase()
    {
      Stylist newStylist = new Stylist("Jerry Garcia");
      newStylist.Save();

      Stylist foundStylist = Stylist.Find(newStylist.GetId());

      Assert.Equal(newStylist, foundStylist);
    }

    [Fact]
    public void Test_Find_GetClientsOfStylist()
    {
      Stylist newStylist = new Stylist("Mickey Hart");
      newStylist.Save();

      Client newClient1 = new Client("Donald Trump", newStylist.GetId());
      newClient1.Save();
      Client newClient2 = new Client("Hillary Clinton", newStylist.GetId());
      newClient2.Save();

      List<Client> testClientList = new List<Client> {newClient1, newClient2};
      List<Client> resultClientList = newStylist.GetClients();

      Assert.Equal(resultClientList, testClientList);
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}
