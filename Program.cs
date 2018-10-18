using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            Artist artist = Artists.FirstOrDefault(a => a.Hometown == "Mount Vernon");
            Console.WriteLine("Artist from Mount Vernon's name is {0} and age is {1}",artist.RealName, artist.Age);

            //Who is the youngest artist in our collection of artists?
            artist = Artists.FirstOrDefault(a => a.Age == Artists.Min(x => x.Age));
            Console.WriteLine("The youngest artist is {0}", artist.RealName);
            
            //Display all artists with 'William' somewhere in their real name
            IEnumerable<Artist> allArtists = Artists.FindAll(a => a.RealName.Contains("William"));

            foreach(Artist elem in allArtists) {
                Console.WriteLine("The artists name is {0}", elem.RealName);
            }

            // Display all groups with names less than 8 characters in length.
            foreach (Group elem in Groups.Where(g => g.GroupName.Length < 8)) {
                Console.WriteLine("The groups name has less than eigth characters {0} ", elem.GroupName);
            }

            //Display the 3 oldest artist from Atlanta
            allArtists = Artists.OrderByDescending(a => a.Age).Where(c => c.Hometown == "Atlanta").Take(3);
            foreach(Artist elem in allArtists) {
                Console.WriteLine("The old artists name is {0}", elem.RealName);
            }

            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            IEnumerable<Group> groups = from g in Groups 
                                        join a in Artists on g.Id equals a.GroupId
                                        where !(
                                            from a2 in Artists 
                                            where a2.Hometown == "New York City" 
                                            select a2.GroupId)
                                            .Contains(g.Id)
                                        select g;

            foreach(Group elem in groups.Distinct()) {
                Console.WriteLine("This group does not have members from NYC: {0}", elem.GroupName);
            }

            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            IEnumerable<Artist> artists = from a in Artists
                                          join g in Groups on a.GroupId equals g.Id
                                          where g.GroupName == "Wu-Tang Clan"
                                          select a;
            foreach(Artist a in artists.Distinct()){
                Console.WriteLine("This artists is in Wu Tang Clan: {0}", a.ArtistName);
            }
        }
    }
}
