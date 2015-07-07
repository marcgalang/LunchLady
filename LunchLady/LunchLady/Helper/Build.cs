using LunchLady.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace LunchLady.Helper
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
                    List<user> partA = new List<user>() { mySet[i] };
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
            
            List<List<List<user>>> pairs = new List<List<List<user>>>();
            for (int i = 0; i < mySet.Count-1; i++)
            {
                for (int j = i + 1; j < mySet.Count; j++)
                {
                    List<List<user>> partition = new List<List<user>>();
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

        public static List<List<List<user>>> PartitionsB(List<user> mySet)
        {
            List<List<List<user>>> partitions = new List<List<List<user>>>();
            //myset[0] is a unit set
            List<user> firstPart = new List<user>() { mySet[0] };
            List<List<List<user>>> partitionsMissingFirstPart= new List<List<List<user>>>();
            List<user> remainder = mySet.ToList();
            remainder.RemoveAt(0);
            if (remainder.Count > 0)
            {
                partitionsMissingFirstPart=PartitionsB(remainder);
                foreach (List<List<user>> partition in partitionsMissingFirstPart)
                {
                    partition.Add(firstPart);
                    partitions.Add(partition);
                }

            }
            
            return partitions;
        }

        public static List<List<List<user>>> PartitionsC(List<user> mySet)
        {
            List<List<List<user>>> partitions = new List<List<List<user>>>();
            for (int i = 0; i < Math.Pow(2, mySet.Count - 1); i++)
            {
                string binary = Convert.ToString(i, 2);
                binary = String.Concat(Enumerable.Repeat("0", mySet.Count - binary.Length-1)) + binary;
                List<user> Part0 = new List<user>();
                List<user> Part1 = new List<user>();
                Part0.Add(mySet[0]);
                for (int j = 0; j < binary.Length; j++)
                {
                    if (binary[j].ToString() == "0")
                    {
                        Part0.Add(mySet[j+1]);
                    }
                    else
                    {
                        Part1.Add(mySet[j+1]);
                    }
                }
                List<List<user>> partition = new List<List<user>>();
                if (Part0.Count > 0) { partition.Add(Part0); }
                if (Part1.Count > 0) { partition.Add(Part1); }
               
                if (Part1.Count > 1)
                {
                    List<List<List<user>>> morePartitions = PartitionsC(Part1);
                    foreach (List<List<user>> subpartition in morePartitions)
                    {
                        subpartition.Insert(0,Part0);
                        partitions.Add(subpartition);
                    }
                }
                else { partitions.Add(partition);
                Debug.WriteLine(binary+" added partition "+partition);
                }
            }



                return partitions;


        }
    }

}