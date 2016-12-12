using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HairSalonProject
{
  public class Client
  {
    private string _name;
    private int _stylistId;
    private int _id;

    public Client(string name, int stylistId, int id = 0)
    {
      _name = name;
      _stylistId = stylistId;
      _id = id;
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};  //creates empty list to hold stylists

      SqlConnection conn = DB.Connection();  //creates connection object
      conn.Open();  //opens connection

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);  //creates sql command object, gets all info from stylists;
      SqlDataReader rdr = cmd.ExecuteReader();    //creates object to execute commands

      while(rdr.Read())   //loop for execute
      {
        int clientId = rdr.GetInt32(0);  //placeholder for id from database
        string clientName = rdr.GetString(1);  //placeholder for name from database
        int stylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, stylistId);  //constructor for new stylist with info passed
        allClients.Add(newClient);  // adds the new stylist to the empty list of stylists
      }

      if (rdr != null)
      {
        rdr.Close();  //closes reader object when done
      }

      if (conn != null)
      {
        conn.Close();  //closes connection when done
      }

      return allClients;  //returns list of stylists
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();  //creates connection object
      conn.Open();  //opens connection

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, stylistId) OUTPUT INSERTED.id VALUES (@ClientName, @StylistId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@ClientName";   //sets paramater objects Name parameter
      nameParameter.Value = this.GetName();   //sets paramaeter objects Value parameter
      cmd.Parameters.Add(nameParameter);  //sets cmd object paramaters and calls Add to pass them

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";   //sets paramater objects Name parameter
      stylistIdParameter.Value = this.GetStylistId();   //sets paramaeter objects Value parameter
      cmd.Parameters.Add(stylistIdParameter);  //sets cmd object paramaters and calls Add to pass them

      SqlDataReader rdr = cmd.ExecuteReader();  //creates object to execute commands

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public string GetName()
    {
      return _name;
    }


    public int GetId()
    {
      return _id;
    }

    public int GetStylistId()
    {
      return _stylistId;
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
        bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
        bool idEquality = this.GetId() == newClient.GetId();
        return (nameEquality && stylistEquality && idEquality);
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
