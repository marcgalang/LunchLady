using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchLady.Models
{
    public class LunchLadyViewModel
    {
    }

    public class user
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int budget { get; set; }
        public int average_restaurant_rating { get; set; }
        public List<group> groups { get; set; }
        public List<restaurant> restaurant { get; set; }
    }

    public class group
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<user> users { get; set; }
        public int user_rating { get; set; }
    }

    public class restaurant
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<cuisine> cuisines { get; set; }
        public int price_rating { get; set; }
        public int yelp_rating { get; set; }
        public int user_rating { get; set; }
        public int user_declared_rating { get; set; }
        public int user_offered_count { get; set; }
        public int user_accepted_count { get; set; }

    }
    
    public class cuisine
    {
        public int id { get; set; }
        public string type { get; set; }
    }

    public class ladys_choice
    {
        public int id { get; set; }
        public group group { get; set; }
        public restaurant restaurant { get; set; }
        public DateTime date { get; set; }
    }

}