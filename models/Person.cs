using System.Text.Json.Serialization;

namespace FinalExam.Models {
    public class Person {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Person(int id, string firstName, string lastName) {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}