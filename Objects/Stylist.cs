using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HairSalonProject
{
  public class Stylist
  {
    private string _name;  //placeholder
    private int _id;  //placeholder

    public Stylist(string name, int id = 0)  //constructor
    {
      _name = name;
      _id = id;
    }

    public static List<Stylist> GetAll()  //makes list of all stylists
    {
      List<Stylist> allStylists = new List<Stylist>{};  //creates empty list to hold stylists

      SqlConnection conn = DB.Connection();  //creates connection object
      conn.Open();  //opens connection

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);  //creates sql command object, gets all info from stylists;
      SqlDataReader rdr = cmd.ExecuteReader();    //creates object to execute commands

      // string name = null;  //sets name as null in case of empty

      while(rdr.Read())   //loop for execute
      {
        string stylistName = rdr.GetString(1);  //placeholder for name from database
        int stylistId = rdr.GetInt32(0);  //placeholder for id from database
        Stylist newStylist = new Stylist(stylistName, stylistId);  //constructor for new stylist with info passed
        allStylists.Add(newStylist);  // adds the new stylist to the empty list of stylists
      }

      if (rdr != null)
      {
        rdr.Close();  //closes reader object when done
      }

      if (conn != null)
      {
        conn.Close();  //closes connection when done
      }

      return allStylists;  //returns list of stylists
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();  //creates connection object
      conn.Open();  //opens connection

      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name) OUTPUT INSERTED.id VALUES (@StylistName);", conn);  //creates new cmd object to add database values

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@StylistName";   //sets paramater objects Name parameter
      nameParameter.Value = this.GetName();   //sets paramaeter objects Value parameter
      cmd.Parameters.Add(nameParameter);  //sets cmd object paramaters and calls Add to pass them

      SqlDataReader rdr = cmd.ExecuteReader();  //creates object to execute commands


      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Stylist Find(int id)
    {
      SqlConnection conn = DB.Connection();  //creates connection object
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);
      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = id.ToString();
      cmd.Parameters.Add(stylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundStylistId = 0;
      string foundStylistName =  null;

      while(rdr.Read())
      {
        foundStylistId = rdr.GetInt32(0);
        foundStylistName = rdr.GetString(1);
      }

      Stylist foundStylist = new Stylist(foundStylistName, foundStylistId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundStylist;
    }

    public List<Client> GetClients()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylistId = @StylistId;", conn);
      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Client> clients = new List<Client> {};

      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientStylistId, clientId);
        clients.Add(newClient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return clients;
    }

    public int GetId()  //getter for stylist id
    {
      return _id;
    }

    public string GetName()  //gett for stylist name
    {
      return _name;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = this.GetId() == newStylist.GetId();
        bool nameEquality = this.GetName() == newStylist.GetName();
        return (idEquality && nameEquality);
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
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
