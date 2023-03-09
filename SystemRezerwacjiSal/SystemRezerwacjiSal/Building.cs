using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SystemRezerwacjiSal
{
    #region Interface
    /// <summary>
    /// Interfejs IManagingBuilding, definuje metody, ktore musza zostac zaimplementowane w klasie, ktora go uzywa
    /// </summary>
    interface IManagingBuilding
    {
        void Adding_room(Room r);
        void Removing_room(Room r);
        void Clearing_allrooms();
        void Sorting_roomsbynumber();
        Room Finding_roombynumber(int number);

    }

    #endregion Interface
    /// <summary>
    /// Publiczna klasa Building, ktora dziedziczy po interfejsach IManagingBuilding i IComparable<Building>
    /// </summary>
    public class Building : IManagingBuilding, IComparable<Building>
    {
        #region Fields
        List<Room> rooms_inbuilding;
        string postcode;
        string address;
        string name;
        #endregion Fields

        #region Properties
        [DataMember]
        public string Name {get { return name; }set{ name = value; } }
        [DataMember]
        public List<Room> Rooms_inbuilding { get { return rooms_inbuilding; } set { rooms_inbuilding = value; } }
        [DataMember]
        public string Address { get { return address; } set { address = value; } }
        [DataMember]
        public string Postcode { get { return postcode; }
            set
            {
                if (check_postcode(value))
                {
                    postcode = value;
                }
                else
                {
                    postcode = "00-000";
                }
            }}
        #endregion Properties

        #region Constructors

        public Building()
        { }
        public Building(string address) : this()
        {
            this.address = address;
            rooms_inbuilding = new List<Room>();
        }

        public Building(string address,string postcode) : this(address)
        {
            if(check_postcode(postcode))
            {
                this.postcode = postcode;
            }
            else
            {
                this.postcode = "00-000";
            }
        }

        public Building(string address,string postcode,string name) : this(address,postcode)
        {
            this.name = name;
        }

        #endregion Constructors

        #region Methods
        /// <summary>
        /// Publiczna metoda, ktora dodaje podany pokoj do listy rooms_inbuilding
        /// </summary>
        /// <param name="r">Obiekt klasy Room</param>
        /// <exception cref="Exception">Obiekt klasy Exception z komunikatem, jesli istnieje pokoj o takim samym numerze</exception>
        public void Adding_room(Room r)
        {
            if (rooms_inbuilding.Exists(p=>p.Number == r.Number))
                throw new Exception("Room with this number already exist in this building!");
            else
                rooms_inbuilding.Add(r);
        }
        /// <summary>
        /// Publiczna metoda, ktora usuwa konkretny pokoj z listy rooms_inbuilding
        /// </summary>
        /// <param name="r">Obiekt klasy Room</param>
        public void Removing_room(Room r)
        {
            if (rooms_inbuilding.Contains(r))
                rooms_inbuilding.Remove(r);
            else
                throw new RoomNotExistingException("Cant remove room that doesnt Exist!");
        }
        /// <summary>
        /// Publiczna metoda usuwajaca wszystkie obiekty z listy rooms_inbuilding
        /// </summary>
        public void Clearing_allrooms()
        {
            rooms_inbuilding.Clear();
        }
        /// <summary>
        /// Publiczna metoda sortujaca liste pokoii po ich numerach
        /// </summary>
        public void Sorting_roomsbynumber()
        {
            if(rooms_inbuilding != null)
                rooms_inbuilding = rooms_inbuilding.OrderBy(r => r.Number).ToList();
        }
        /// <summary>
        /// Publiczna metoda, ktora wyszukuje z listy pokoj o podanym numerze
        /// </summary>
        /// <param name="number">Numer pokoju (int)</param>
        /// <returns>Zwraca pokoj o podanym numerze</returns>
        public Room Finding_roombynumber(int number)
        {
            if (rooms_inbuilding.Exists(c => c.Number == number))
                return (Room)rooms_inbuilding.Where(c => c.Number == number);
            else
                throw new RoomNotExistingException("Room with that number doesnt exist in this building!");
        }
        /// <summary>
        /// Publiczna metoda sprawdzajaca poprawnosc kodu pocztowego wedlug podanego schematu
        /// </summary>
        /// <param name="s">Kod pocztowy(string)</param>
        /// <returns>True jak jest poprawny, False jesli zly</returns>
        private bool check_postcode(string s)
        {
            if (Regex.IsMatch(s, @"\d\d-\d\d\d"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Prywatna metoda, ktora zamienia liste pokoii na jednego osobne stringi
        /// </summary>
        /// <returns>Zwraca wypisane pokoje z listy</returns>
        private string Listofrooms_Tostring()
        {
            string temp = "";
            foreach (Room r in rooms_inbuilding)
            {
                temp += r.ToString() + "\n";
            }
            return temp;
        }
        /// <summary>
        /// Publiczna metoda nadpisujaca metode ToString
        /// </summary>
        /// <returns>Zwraca nazwe budynku(string)</returns>
        public override string ToString()
        {
            return $"{Name}({Address})\n";
        }
        
        public int CompareTo(Building? build)
        {
            if(build is null) { return 1; }
            int result = Name.CompareTo(build.Name);
            if(result != 0 ) {  return result; }
            return Address.CompareTo(build.Address);
        }


        #endregion Methods

    }
}
