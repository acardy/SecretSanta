using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace SecretSanta
{
    public class Program
    {
        static void Main(string[] args)
        {
            var matches = new List<Match>();
            var people = new List<Person>()
                {
                    new Person() {Name = "Person 1", Email = "person1@gmail.com"},
                    new Person() {Name = "Person 2", Email = "person2@hotmail.com"},
                    new Person() {Name = "Person 3", Email = "person3@aol.com"},
                    new Person() {Name = "Person 4", Email = "person4@blah.com"},
                    new Person() {Name = "Person 5", Email = "person5@blah.com"},
                    new Person() {Name = "Person 6", Email = "person6@blah.com"},
                    new Person() {Name = "Person 7", Email = "person7@blah.com"},
                };

            var rand = new Random();
            var randomPeople = people.OrderBy(p => rand.Next()).ToList();

            foreach (var giver in randomPeople)
            {
                var reciever = (giver == randomPeople.Last()) ? randomPeople.ElementAt(0) : randomPeople.MextFrom(giver);
                matches.Add(new Match() { Giver = giver, Receiver = reciever });
            }

            Console.Write("Enter your Gmail address: ");
            var emailAddress = Console.ReadLine();

            Console.Write("Enter your Gmail password: ");
            var password = Console.ReadLine();

            EmailMatches(matches, emailAddress, password);

            Console.ReadKey();
        }

        private static void EmailMatches(List<Match> matches, string emailAddress, string password)
        {
            foreach (var match in matches)
            {
#if DEBUG
                Console.WriteLine("{0} gets a present for {1}", match.Giver.Name, match.Receiver.Name);
                continue;
#endif
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailAddress, password)
                };

                using (var message = new MailMessage(emailAddress, match.Giver.Email)
                {
                    Subject = "Secret Santa!",
                    Body = "This is an auto generated email, your secret santa is: " + match.Receiver.Name
                })
                {
                    smtp.Send(message);
                }
            }
        }
    }

    public class Person
    {
        public string Name{ get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Match
    {
        public Person Giver { get; set; }
        public Person Receiver { get; set; }
    }
}
