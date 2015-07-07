using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using LunchLady.Models;
using LunchLady.Helper;
using System.Diagnostics;



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
            Random rand = new Random();
            string rstring = "";

            //get a list of restaurants
            List<restaurant> restaurants = new List<restaurant>();
            rstring += "Getting the list of Restaurants:<br>-----------------------------------";
            List<List<object>> result = DataAccess.DataAccess.Read(Build.StringArray("restaurants"));
            foreach (List<object> row in result)
            {
                restaurant _restaurant = new restaurant();
                _restaurant.id = (int)row[0];
                _restaurant.name = (string)row[1];
                _restaurant.price_rating = (int)row[2];
                _restaurant.yelp_rating = (int)row[3];
                restaurants.Add(_restaurant);
                rstring += "<br>" + _restaurant.name;
            }

            //get a list of users
            List<user> users = new List<user>();
            rstring += "<br><br>Getting the list of Users:<br>-----------------------------------";
            List<List<object>> userResults = DataAccess.DataAccess.Read(Build.StringArray("users"));
            foreach (List<object> row in userResults)
            {
                user _user = new user();
                _user.id = (int)row[0];
                _user.first_name = (string)row[1];
                _user.last_name = (string)row[2];
                _user.budget = (int)row[3];
                _user.restaurant = restaurants.ToList();
                //set user's resto ratings
                int sum = 0;
                for (int i = 0; i < restaurants.Count; i++)
                {
                    List<List<object>> ratings = DataAccess.DataAccess.Read(Build.StringArray("user_restaurant"),Build.StringArray("declared_rating"), Build.StringArray("_user_id=" + _user.id, "restaurant_id=" + restaurants[i].id));
                    if (ratings.Count > 0)
                    { _user.restaurant[i].user_rating = (int)ratings[0][0]; }
                    else
                    { _user.restaurant[i].user_rating = 50; }
                    sum += _user.restaurant[i].user_rating;
                }

                _user.average_restaurant_rating = sum / restaurants.Count;
                users.Add(_user);
                rstring += "<br>" + _user.first_name + " " + _user.last_name + " Avg. Resto rating:" + _user.average_restaurant_rating;
            }

            //create a list of group partitions
            List<List<List<user>>> userPartitions = Build.PartitionsC(users);
            rstring += "<br><br>Here are the partitions so far:  count=" + userPartitions.Count.ToString() + "<br>-----------------------------------<br>";
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //foreach (List<List<user>> partition in userPartitions)
            //{
            //    foreach (List<user> part in partition)
            //    {
            //        rstring += "(" + string.Join<string>(",", part.Select(x => x.first_name).ToArray()) + ")";
            //    }
            //    rstring += "<br>";
            //}
            stopwatch.Stop();
            rstring += "That took " + stopwatch.Elapsed.ToString("c") + "<br>";
            Debug.WriteLine("Partitions done");

            List<List<user>> winningPartition = new List<List<user>>();
            List<int> winningSelection = new List<int>();
            int winningScore = -10000;
            //iterate through all partitions
            
            foreach (List<List<user>> partition in userPartitions)
            {
                rstring += "Partition:";
                foreach (List<user> group in partition)
                {
                    rstring += " (" + string.Join<string>(",", group.Select(x => x.first_name).ToArray()) + ")";
                }
                rstring += "<br>";

                int bestScore = -1000;
                List<int> bestSelections = new List<int>();
                for (int j = 0; j < 200; j++)
                {
                    int totalUserScore = 0;
                    List<int> selections = new List<int>();

                    //iterate through each group
                    for (int i = 0; i < partition.Count; i++)
                    {
                        //randomly choose restaurant 
                        int randomint = 0;
                        while (selections.Count < i + 1)
                        {
                            randomint = rand.Next(0, restaurants.Count);
                            if (!selections.Contains(randomint))
                            {
                                selections.Add(randomint);
                            }
                        }


                        //iterate through each user in group
                        foreach (user _user in partition[i])
                        {
                            totalUserScore += (int)_user.restaurant[randomint].user_rating;
                        }
                        //small group penalty
                        if (partition[i].Count < 3)
                        { totalUserScore -= 50 / partition[i].Count; }
                    }
                    int finalScore = totalUserScore * 100 / users.Count;
                    if (finalScore >= bestScore)
                    {
                        bestScore = finalScore;
                        bestSelections = selections.ToList();
                    }

                }
                rstring += "Score:" + bestScore;
                rstring += "[" + string.Join<int>(",", bestSelections.ToArray()) + "]<br>";
                for (int i = 0; i < partition.Count; i++)
                {
                    
                    rstring += ".....(" + string.Join<string>(",", partition[i].Select(x => x.first_name).ToArray()) + ")"+restaurants[bestSelections[i]].name;
                    rstring += "<br>";
                }
                rstring += "<br>";
                //present highest combo score as choice
                if (bestScore >= winningScore)
                {
                    winningScore = bestScore;
                    winningPartition = partition;
                    winningSelection = bestSelections.ToList();
                }

            }
            rstring += "<br><br><br>";
            rstring += "LunchLady has decided that:<br>";
            for (int i=0;i<winningPartition.Count;i++)
            {
                rstring+=String.Join(", ", winningPartition[i].Select(x=>x.first_name).ToArray(), 0, winningPartition[i].Count - 1) + ", and " + winningPartition[i].Select(x=>x.first_name).LastOrDefault();
                rstring+=" will have "+restaurants[winningSelection[i]].name+" for lunch today.<br>";
            }
            return rstring;
        }
        public List<List<object>> Get2ElementPartition(List<object> mySet)
        {
            List<List<object>> splitSets = new List<List<object>>();


            return splitSets;
        }

        public string test()
        {
            string rstring = "";
            List<user> userList = new List<user>();

            for (int i = 0; i < 4; i++)
            {
                user newuser = new user();
                newuser.first_name = i.ToString();
                userList.Add(newuser);
            }
            rstring += "Here is the list of users:<br>" + userList[0].first_name;

            for (int i = 1; i < userList.Count; i++)
            {
                rstring += ", " + userList[i].first_name;
            }
            List<List<List<user>>> userPartitions = Build.Partitions(userList);
            rstring += "<br>Here are the partitions so far:  count=" + userPartitions.Count.ToString() + "<br>";
            foreach (List<List<user>> partition in userPartitions)
            {
                foreach (List<user> part in partition)
                {
                    rstring += "(" + string.Join<string>(",", part.Select(x => x.first_name).ToArray()) + ")";
                }
                rstring += "<br>";
            }




            return rstring;
        }

    }
}

