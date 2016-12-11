using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalonProject
{
  public class Stylist
  {
      private string _name;
      private int _id;

      public Stylist(string name, int id = 0)
      {
        _name = name;
        _id = id;
      }
      
  }
}
