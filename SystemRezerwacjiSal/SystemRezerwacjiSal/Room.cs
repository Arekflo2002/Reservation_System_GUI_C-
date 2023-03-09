using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiSal
{
    /// <summary>
    /// Publiczna klasa InvalidNumberException, ktora dziedziczy po klasie exception
    /// </summary>
    public class InvalidNumberException : Exception
    {
        /// <summary>
        /// Konstruktor nieparametryczny korzystajacy z konstruktora klasy bazowej
        /// </summary>
        public InvalidNumberException() : base() 
        { }
        /// <summary>
        /// Konstruktor parametryczny korzystajacy z konstruktora klasy bazowej
        /// </summary>
        public InvalidNumberException(string message) : base(message)
        { }

    }
    /// <summary>
    /// Publiczna klasa Room dziedziczaca po klasie Equipment oraz interfejsie IClonable
    /// </summary>
    public class Room : Equipment, ICloneable
    {
        #region Fields
        int number;
        double squareSurface;
        #endregion Fields

        #region Properties

        [DataMember]
        public int Number { get { return number; } 
            set
            {
                if(Checking_number(value))
                    number = value; 
            } 
        }
        [DataMember]
        
        public double SquareSurface {get { return squareSurface; } 
            set
            {
                if(Checking_number((int)value))
                    squareSurface = value; 
            }
        }
        #endregion Properties

        #region Constructors

        public Room() : base()
        { }
        public Room(string wiFi,int number) : base(wiFi)
        {
        }

        public Room(string wiFi, string projector) : base(wiFi, projector)
        {
        }

        public Room(string wiFi, string projector, string soundSystem) : base(wiFi, projector, soundSystem)
        {
        }

        public Room(string wiFi, string projector, string soundSystem, int numberofcharis)
            : base(wiFi, projector, soundSystem, numberofcharis)
        {
        }

        public Room(string wiFi, string projector, string soundSystem, int numberofcharis, string airConditioning)
            : base(wiFi, projector, soundSystem, numberofcharis, airConditioning)
        {
        }

        public Room(string wiFi, string projector, string soundSystem, int numberofcharis, string airConditioning,
            string illumination) : base(wiFi, projector, soundSystem, numberofcharis, airConditioning, illumination)
        {}

        public Room(string wiFi, string projector, string soundSystem, int numberofcharis, string airConditioning,
                    string illumination, int number) 
            : this(wiFi, projector, soundSystem, numberofcharis, airConditioning, illumination)
        {
            if(Checking_number(number))
                this.number = number;
        }

        public Room(string wiFi, string projector, string soundSystem, int numberofcharis, string airConditioning,
            string illumination,int number,double surface)
            : this(wiFi, projector, soundSystem, numberofcharis, airConditioning, illumination,number)
        {
            if(Checking_number((int)surface))
                this.SquareSurface = surface;
        }
        #endregion Constructors 

        #region Methods 

        /// <summary>
        /// Prywatna metoda, sprawdza czy podany argument jest dodatni lub rowny zero
        /// </summary>
        /// <param name="d">Argument typu int</param>
        /// <returns>True jak >= 0, Komunikat jak <0</returns>
        /// <exception cref="InvalidNumberException">Obiekt klasy InvalidNumberException, z komunikatem</exception>
        private bool Checking_number(int d)
        {
            if (d >= 0)
                return true;
            else
                throw new InvalidNumberException("These Properties cant be represented by that number!");
        }
        /// <summary>
        /// Publiczna metoda, nadpisanie metody ToString
        /// </summary>
        /// <returns>Zwraca wlasciwosc Number</returns>
        public override string ToString()
        {
            return $"{Number}";
        }
        /// <summary>
        /// Publiczna metoda pozwalajaca na stworzenie kopii obiektu klasy 
        /// </summary>
        /// <returns>Zwraca sklonowany obiekt klasy </returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

       

        #endregion Methods 
    }
}
