using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using inConcert.Helper;
using LunchLady.Models;


namespace LunchLady.Controllers
{
    public class DorisController : Controller
    {
        // GET: Doris
        public ActionResult oldIndex()
        {
            return View();
        }

        public string Index()
        {
            string rstring = "";

            //get a list of restaurants
            List<restaurant> restaurants = new List<restaurant>();
            rstring += "Getting the list of Restaurants:";
            List<List<object>> result = DataAccess.DataAccess.Read(Build.StringArray("restaurants"));
            foreach (List<object> row in result)
            {
                restaurant _restaurant = new restaurant();
                _restaurant.id = (int)row[0];
                _restaurant.name = (string)row[1];
                _restaurant.price_rating = (int)row[2];
                _restaurant.yelp_rating = (int)row[3];
                restaurants.Add(_restaurant);
                rstring += "<br>.</t>" + _restaurant.name;
            }

            //get a list of users
            List<user> users = new List<user>();
            rstring += "<br>Getting the list of Users:";
            List<List<object>> result1 = DataAccess.DataAccess.Read(Build.StringArray("users"));
            foreach (List<object> row in result1)
            {
                user _user = new user();
                _user.id = (int)row[0];
                _user.first_name = (string)row[1];
                _user.last_name = (string)row[2];
                _user.budget = (int)row[3];
                users.Add(_user);
                rstring += "<br>.</t>" + _user.first_name+" "+_user.last_name;
            }

            //create a list of combination of groups

            //iterate through each combination of groups

            //      iterate through each group

            //          iterate through list of restaurants

            //              calculate group<->restaurant score

            //      calculate combination score

            //present highest combo score as choice




            return rstring;
        }


    }
}

