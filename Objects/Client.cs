using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HairSalonProject
{
  public class Client
  {
    private string _name;
    private int _id;
    private int _stylistId;


    public Client(string name, int stylistId, int id = 0)
    {
      _name = name;
      _stylistId = stylistId;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }

    public int GetId()
    {
      return _id;
    }

    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool nameEquality = this.GetName() == newClient.GetName();
        bool idEquality = this.GetId() == newClient.GetId();
        bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
        return(idEquality && nameEquality && stylistEquality);
      }
    }

    public override int GetHashCode()  //overrides hash code default behavior
    {
      return _name.GetHashCode();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
