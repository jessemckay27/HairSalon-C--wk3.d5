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
    
    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}
