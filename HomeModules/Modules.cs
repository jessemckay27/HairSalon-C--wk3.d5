using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalonProject
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>{
        return View["index.cshtml"];
      };
      Get["/stylists"] = _ =>{
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Get["/stylists/new"] = _ =>{
        return View["stylist_new.cshtml"];
      };
      Post["/stylists/new"] = _ =>{
        Stylist newStylist = new Stylist(Request.Form["name"]);
        newStylist.Save();
        return View["success.cshtml"];
      };
      Get["/stylist/{id}"] = parameters =>{
       var newStylist = Stylist.Find(parameters.id);
       return View["stylist_view.cshtml", newStylist];
      };
      Get["/stylist/edit/{id}"] = parameters =>{
        Stylist newStylist = Stylist.Find(parameters.id);
        return View["stylist_edit.cshtml", newStylist];
      };
      Patch["/stylist/edit/{id}"] = parameters => {
        Stylist newStylist = Stylist.Find(parameters.id);
        newStylist.Update(Request.Form["name"]);
        return View["success_edit.cshtml"];
      };
      Get["/stylist/delete/{id}"] = parameters => {
        Stylist newStylist = Stylist.Find(parameters.id);
        return View["stylist_delete.cshtml", newStylist];
      };
      Delete["stylist/delete/{id}"] = parameters => {
        Stylist newStylist = Stylist.Find(parameters.id);
        newStylist.Delete();
        return View["success_stylist_delete.cshtml"];
      };
      Get["/clients/new"]= _ =>{
        List<Stylist> allStylists = Stylist.GetAll();
        return View["client_new.cshtml", allStylists];
      };
      Post["/clients/new"]= _ =>{
       Client newClient = new Client(Request.Form["name"], Request.Form["id"]);
       newClient.Save();
       return View["success_client.cshtml"];
      };
      Get["/client_edit/{id}"] = parameters => {
        Client newClient = Client.Find(parameters.id);
        return View["client_edit.cshtml", newClient];
      };
      Patch["/client_edit/{id}"] = parameters => {
        Client newClient = Client.Find(parameters.id);
        newClient.Update(Request.Form["name"]);
        return View["success_client.cshtml"];
      };
      Get["/client_delete/{id}"] = parameters => {
       Client newClient = Client.Find(parameters.id);
       return View["client_delete.cshtml", newClient];
      };
      Delete["/client_delete/{id}"] = parameters => {
        Client newClient = Client.Find(parameters.id);
        newClient.Delete();
        return View["success_client_delete.cshtml"];
      };
    }
  }
}
