using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiSal
{
    #region Exceptions 
    /// <summary>
    /// Publiczna klasa RoomNotExistingException, ktora dziedziczy po klasie exception
    /// </summary>
    public class RoomNotExistingException : Exception
    {
        public RoomNotExistingException() : base()
        { }

        public RoomNotExistingException(string message) : base(message)
        { }

        public Person Person
        {
            get => default;
            set
            {
            }
        }
    }

    public class InvalidDataException : Exception
    {
        public InvalidDataException() : base()
        { }

        public InvalidDataException(string message) : base(message)
        { }
    }
    #endregion Exceptions

    public class Reservation
    {
        #region Fields
        Client customer;
        Building structure;
        Room apartment;
        DateTime whenMade;
        DateTime bookingDate;
        #endregion Fields

        #region Properties
        [DataMember]
        public Client Customer { get { return customer; } set { customer = value; } }
        
        [DataMember]
        public Building Structure { get { return structure; } set { structure = value; } }
        [DataMember]
        public DateTime WhenMade { get { return whenMade; } set { whenMade = value; } }
        [DataMember]
        public DateTime BookingDate { get { return bookingDate; } 
            set 
            {   
                if(IsDateCorrect(value))
                    bookingDate = value; 
            } 
        }
        [DataMember]
        public Room Apartment
        {
            get { return apartment; }
            set
            {
                apartment = value;
            }
        }

        #endregion Properties

        #region Constructors
        public Reservation()
        {
            whenMade = DateTime.Now;
        }

        public Reservation(Client client ,Building building,Room room, string bookingDate) :this()
        {
            // Things for client 
            customer = client;

            // Things for buildings
            structure = building;

            //Things for rooms 
            apartment = room;

            // Things for booking Date
            if (DateTime.TryParseExact(bookingDate,new string[]{ "dd-MM-yyyy", "dd/MM/yyyy", "yyyy-MM-dd", "yyyy/MM/dd" },
                null, System.Globalization.DateTimeStyles.None,out DateTime data))
            {
                if(IsDateCorrect(data))
                    this.bookingDate = data;

            }
            else
            {
                throw new InvalidDataException("Parsing Gone Wrong!");
            }


        }

        #endregion Constructors

        #region Methods
        /// <summary>
        /// Prywatna metoda, ktora sprawdza czy dany pokoj znajduje sie w danym budynku
        /// </summary>
        /// <param name="b">Obiekt klasy Building</param>
        /// <param name="r">Obiekt klasy Room</param>
        /// <returns>True gdy dany pokoj istnieje w danym budynku,False jesli nie</returns>
        /// <exception cref="RoomNotExistingException">Obiekt klasy RoomNotExistingException z komunikatem</exception>
        private bool IsRoomInThisBuilding(Room r)
        {
            if (structure != null)
            {
                if (structure.Rooms_inbuilding.Contains(r))
                    return true;
                else
                    throw new RoomNotExistingException("This Room does not exist in this building!");
            }
            return false;
            
            
        }
        /// <summary>
        /// Prywatna metoda, ktora sprawdza czy data jest pozniejsza lub terazniejsza
        /// </summary>
        /// <param name="t">Data, ktora chcemy sprawdzic</param>
        /// <returns>True jesli jest poprawna, komunikat jesli nie</returns>
        /// <exception cref="InvalidDataException"></exception>
        private bool IsDateCorrect(DateTime t)
        {
            if (t >= DateTime.Now)
                return true;
            else
                throw new InvalidDataException("The Date should not be before NOW !");
            

        }
        /// <summary>
        /// Publiczna metoda, ktora nadpisuje metode ToString
        /// </summary>
        /// <returns>Zwraca imie i nazwisko osoby oraz numer pokoju i date jej rezerwacji (string)</returns>
        public override string ToString()
        {
            return $" Osoba {Customer.Name} {Customer.Surname} zarezerwowała salę nr.{Apartment.Number} " +
                $"na dzien {BookingDate.ToShortDateString()}\n";
        }

        #endregion Methods


    }
}
