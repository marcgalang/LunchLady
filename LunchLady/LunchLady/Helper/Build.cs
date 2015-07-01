using LunchLady.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inConcert.Helper
{
    public class Build
    {
        public static string[] StringArray(params string[] s)
        {
            return s;
        }
        public static List<List<List<user>>> Partitions(List<user> mySet){
            List<List<List<user>>> partitions = new List<List<List<user>>>();
            //partA have length 0
            partitions.Add(new List<List<user>>() {mySet});
            
            //partA have length 1
            if (mySet.Count == 2)
            {
                List<List<user>> partition = new List<List<user>>();
                List<user> partA = new List<user>() { mySet[0] };
                List<user> partB = new List<user>() { mySet[1] };
                partition.Add(partA);
                partition.Add(partB);
                partitions.Add(partition);
            }
            if (mySet.Count > 2)
            {
                for (int i = 0; i < mySet.Count; i++)
                {
                    List<List<user>> partition = new List<List<user>>();
                    List<user> partA = new List<user>() { mySet[0] };
                    List<user> partB = mySet.ToList();
                    partB.RemoveAt(i);
                    partition.Add(partA);
                    partition.Add(partB);
                    partitions.Add(partition);
                }

                //PartA have length 2
                List<List<List<user>>> morepartitions = makePairs(mySet);
                foreach (List<List<user>> partition in morepartitions)
                {
                    partitions.Add(partition);
                }

                //PartA have length 3
                for (int i = 0; i<mySet.Count; i++)
                {
                    List<user> partialSet = mySet.ToList();
                    partialSet.RemoveAt(i);
                    List<List<List<user>>> pieces = makePairs(partialSet);
                    foreach (List<List<user>> piece in pieces)
                    {
                        piece[0].Add(mySet[i]);
                        partitions.Add(piece);
                    }
                }


                
            }
            return partitions;
        }
        public static List<List<List<user>>> makePairs(List<user> mySet)
        {
            List<List<user>> partition = new List<List<user>>();
            List<List<List<user>>> pairs = new List<List<List<user>>>();
            for (int i = 0; i < mySet.Count-2; i++)
            {
                for (int j = i + 1; j < mySet.Count; j++)
                {
                    List<user> partA = new List<user>() { mySet[i] };
                    partA.Add(mySet[j]);
                    List<user> partB = mySet.ToList();
                    partB.RemoveAt(j);
                    partB.RemoveAt(i);
                    partition.Add(partA);
                    partition.Add(partB);
                    pairs.Add(partition);
                }
            }
            return pairs;
        }
    }

}