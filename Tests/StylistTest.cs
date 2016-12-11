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
      DBConfiguration.ConnectionString = "Data Source=desktop-ddsnb9e;Initial Catalog=hair_salon;Integrated Security=SSPI";
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

    public void Dispose()
    {
      // Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}
