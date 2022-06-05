using System;

namespace SoteriaPet.Models
{
    public class Pet
    {
        public string id { get; set; }
        public string Name { get; set; }
        public Species Species { get; set; }
        public Gender Sex { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
    }

    public enum Species
    {
        Dog = 1,
        Cat = 2
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}