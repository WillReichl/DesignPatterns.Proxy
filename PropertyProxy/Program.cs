using System;
using System.Collections.Generic;

namespace PropertyProxy
{
    public class Program
    {
        public class Property<T> : IEquatable<Property<T>> where T : new()
        {
            private T value;

            public T Value
            {
                get => value;
                set
                {
                    if (Equals(this.value, value)) return;
                    Console.WriteLine($"Assigning value to {value}");
                    this.value = value;
                }
            }

            public Property() : this(default(T))
            {

            }

            public Property(T value)
            {
                this.value = value;
            }

            public static implicit operator T(Property<T> property)
            {
                return property.value; // int n = p_int;
            }

            public static implicit operator Property<T>(T value)
            {
                return new Property<T>(value); // Property<int> p = 123;
            }

            public static bool operator ==(Property<T> left, Property<T> right)
            {
                return EqualityComparer<Property<T>>.Default.Equals(left, right);
            }

            public static bool operator !=(Property<T> left, Property<T> right)
            {
                return !(left == right);
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Property<T>);
            }

            public bool Equals(Property<T> other)
            {
                return other != null &&
                       EqualityComparer<T>.Default.Equals(value, other.value) &&
                       EqualityComparer<T>.Default.Equals(Value, other.Value);
            }
        }

        public class Creature
        {
            // In C#, this will create a new value on each value change
            //public Property<int> Agility { get; set; }

            private Property<int> agility = new Property<int>();

            public int Agility
            {
                get => agility.Value;
                set => agility.Value = value;
            }
        }

        static void Main(string[] args)
        {
            var c = new Creature();
            c.Agility = 10;
            c.Agility = 10; // assignment will be skipped due to equality
            c.Agility = 9;
        }
    }
}
