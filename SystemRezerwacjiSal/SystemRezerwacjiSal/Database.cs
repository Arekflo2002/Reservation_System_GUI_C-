using System;
using System.Timers;
using System.Collections.Generic;
using Timer = System.Timers.Timer;
using System.Runtime.Serialization;

namespace SystemRezerwacjiSal
{
    #region Interface
    /// <summary>
    /// Interfejs IOperatingBuildingList, definuje metody, ktore musza zostac zaimplementowane w klasie, ktora go uzywa
    /// </summary>
    interface IOperatingBuildingList
    {
        void Adding_building(Building b);
        void Removing_building(Building b);
        void ClearingAll_buildings();
        Building FindingBuilding_byaddress(string address);
    }
    /// <summary>
    /// Interfejs IOperatingClientList, definuje metody, ktore musza zostac zaimplementowane w klasie, ktora go uzywa
    /// </summary>
    interface IOperatingClientList
    {
        void Adding_client(Client c);
        void Removing_client(Client c);
        void ClearingAll_clients();
        Client FindingClient_byPesel(string p);
        List<Client> SortingClients_byPesel();
    }

    interface IOperatingReservations
    {
        void Adding_reservation(Reservation r);
        void Removing_reservation(Reservation r);
        List<Reservation> SortingReservations_byClientsPesel(List<Reservation> temp);
        List<Reservation> SortingReservations_byBuildingsName(List<Reservation> temp);
        List<Reservation> SortingReservations_byWhenMadeDate(List<Reservation> temp);
        List<Reservation> SortingReservations_byBookingDate(List<Reservation> temp);

        void ClearingAll_reservation();
        List<Reservation> FindingReservations_byClient(string pes);
        List<Reservation> FindingReservations_byBookingDate(DateTime? dt);
        List<Reservation> FindingReservations_byBuilding(string b);
        List<Reservation> FindingReservations_byRoom(Building b, Room r);
    }

    interface IOperatingXMLFiles
    {
        void SaveXML(string fname);
    }



    #endregion Interface


    [DataContract]
    [KnownType(typeof(Person))]
    [KnownType(typeof(Equipment))]

    /// <summary>
    /// Publiczna klasa DataBase, ktora dziedziczy po interfejsach IOperatingBuildingList, IOperatingClientList, IOperatingReservations, IOperatingXMLFiles
    /// </summary>
    public class DataBase : IOperatingBuildingList, IOperatingClientList, IOperatingReservations, IOperatingXMLFiles
    {
        #region Fields
        List<Building> listof_buildings; 
        List<Client> listof_clients;
        List<Reservation> listof_reservations;
        // List of reservations 
        #endregion Fields

        #region Properties
        [DataMember]
        public List<Reservation> Listof_reservations { get { return listof_reservations; } set { listof_reservations = value; } }
        [DataMember]
        public List<Building> Listof_buildings { get { return listof_buildings; } set { listof_buildings = value; } }
        [DataMember]
        public List<Client> Listof_clients { get { return listof_clients; } set { listof_clients = value; } }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Konstruktor bazowy, inicjalizuje sie przy tworzeniu obiektu klasy DataBase
        /// </summary>
        public DataBase()
        {
            listof_buildings = new List<Building>();
            listof_clients = new List<Client>();
            listof_reservations = new List<Reservation>();
        }
        #endregion Constructors

        #region Methods

        #region Methods for buildings

        /// <summary>
        /// Publiczna metoda, dodaje obiekt klasy budynek do listof_buildings
        /// </summary>
        /// <param name="b">Obiekt klasy Building</param>
        public void Adding_building(Building b)
        {
            try
            {
                listof_buildings.Add(b);
            }
            catch(ArgumentException)
            {
                throw new Exception("1");
            }
        }
        /// <summary>
        /// Publiczna metoda, usuwa obiekt z listof_buildings
        /// </summary>
        /// <param name="b">Obiekt klasy Building</param>
        public void Removing_building(Building b)
        {
            if (listof_buildings.Contains(b))
                listof_buildings.Remove(b);
            else
                throw new Exception("Cant remove ghost Item !");
        }
        /// <summary>
        /// Publiczna metoda, usuwa wszystkie obiekty z listy listof_buildings
        /// </summary>
        public void ClearingAll_buildings()
        {
            listof_buildings.Clear();
        }
        /// <summary>
        /// Publiczna metoda, szuka budynku o podanym adresie w liscie listof_buildings
        /// </summary>
        /// <param name="address">Adress budynku (string)</param>
        /// <returns>Zwraca budynek o podanym adresie</returns>
        public Building FindingBuilding_byaddress(string address)
        {
            if (listof_buildings.Exists(b => b.Address == address))
                return (Building)listof_buildings.Where(b => b.Address == address);
            else
                throw new ArgumentException("Building with that address doesnt exisst!");
        }

        #endregion Methods for buildings


        #region Methods for clients
        /// <summary>
        /// Publiczna metoda, dodaje obiekt klasy Client do listy listof_clients
        /// </summary>
        /// <param name="c">Obiekt klasy Client</param>
        public void Adding_client(Client c)
        {
            try
            {
                listof_clients.Add(c);
            }
            catch(ArgumentException)
            {
                throw new ArgumentException("Cant add this element!");
            }
        }
        /// <summary>
        /// Publiczna metoda , usuwa obiekt z listy listof_clients
        /// </summary>
        /// <param name="c">Obiekt klasy Client</param>
        public void Removing_client(Client c)
        {
            if (listof_clients.Contains(c))
                listof_clients.Remove(c);
            else
                throw new ArgumentException("Cant remove Ghost Item!");
        }
        /// <summary>
        /// Publiczna metoda, usuwa wszystkie obiekty z listy listof_clients
        /// </summary>
        public void ClearingAll_clients()
        {
            listof_clients.Clear();
        }

        /// <summary>
        /// Publiczna metoda, szuka klienta po podanym numerze peselu
        /// </summary>
        /// <param name="p">Numer pesel (string)</param>
        /// <returns>Zwraca klienta o podanym numerze peselu</returns>
        public Client FindingClient_byPesel(string p)
        {
            if (listof_clients.Exists(c => c.Pesel == p))
                return (Client)listof_clients.Where(c => c.Pesel == p);
            else
                throw new ArgumentException("Cant find Client with This pesel bcs he doesnt exist! ");
        }
        /// <summary>
        /// Publiczna metoda sortujaca liste clientow po peselu
        /// </summary>
        /// <returns>Zwraca posortowana liste klientow</returns>
        public List<Client> SortingClients_byPesel()
        {
            return (List<Client>)listof_clients.OrderBy(c => c.Pesel);
        }

        #endregion Methods for clients 


        #region Methods for reservations

        /// <summary>
        /// Publiczna metoda dodajaca obiekt klasy Reservation do listy listof_reservations
        /// </summary>
        /// <param name="r">Obiekt klasy Reservation</param>
        /// <exception cref="Exception">Komuniakt z klasy Exception</exception>
        public void Adding_reservation(Reservation r)
        {
            // Im Checking if somebody hadn't reserved this particural room on this particural date!
            foreach(Reservation temp in listof_reservations)
            {
                if (r.Structure == temp.Structure & r.Apartment == temp.Apartment & r.BookingDate == temp.BookingDate)
                    throw new InvalidDataException("Cant book the same room in one day!");
            }
            listof_reservations.Add(r);
        }
        /// <summary>
        /// Publiczna metoda usuwajaca podany obiekt z listy listof_reservations
        /// </summary>
        /// <param name="r">Obiekt klasy Reservation</param>
        public void Removing_reservation(Reservation r)
        {
            listof_reservations.Remove(r);   
        }
       
        private void Refreshing_reservations()
        {   
            // Function To check for expired reservations 
            foreach(Reservation r in listof_reservations)
            {
                if(r.BookingDate<DateTime.Now)
                {
                    Removing_reservation(r);
                }
            }
        }

        /// <summary>
        /// Publiczna metoda, ktora usuwa wszystkie obiekty z listy listof_reservations
        /// </summary>
        public void ClearingAll_reservation()
        {
            listof_reservations.Clear();
        }
        /// <summary>
        /// Publiczna metoda, ktora sortuje liiste listof_reservations po peselu klientow
        /// </summary>
        public List<Reservation> SortingReservations_byClientsPesel(List<Reservation> temp)
        {
            return temp.OrderBy(c => c.Customer.Pesel).ToList();
        }
        public List<Reservation> SortingReservations_byBookingDate(List<Reservation> temp)
        {
            return temp.OrderBy(c => c.BookingDate).ToList();
        }

        public List<Reservation> SortingReservations_byWhenMadeDate(List<Reservation> temp)
        {
            return temp.OrderBy(c => c.WhenMade).ToList();
        }
        public List<Reservation> SortingReservations_byBuildingsName(List<Reservation> temp)
        {
            return temp.OrderBy(c => c.Structure.Name).ToList();
        }
        /// <summary>
        /// Publiczna metoda, ktora wyszukuje rezerwacje z listof_reservations po kliencie
        /// </summary>
        /// <param name="c">Obiekt klasy Client</param>
        /// <returns>Zwraca liste rezerwacji danego klienta</returns>
        public List<Reservation> FindingReservations_byClient(string pes)
        {
            if (listof_reservations.Exists(p => p.Customer.Pesel == pes))
                return (List<Reservation>)listof_reservations.Where(p => p.Customer.Pesel == pes).ToList();
            else
                return null;
        }
        /// <summary>
        /// Publiczna metoda, ktora wyszukuje rezerwacje z listof_reservations po ich dacie
        /// </summary>
        /// <param name="dt">Interesujaca nas data</param>
        /// <returns>Zwraca liste rezerwacji o podanej dacie</returns>
        public List<Reservation> FindingReservations_byBookingDate(DateTime? dt)
        {
            // GUI things
            if (dt is null)
                return null;
            if (listof_reservations.Exists(p => p.BookingDate == dt))
                return (List<Reservation>)listof_reservations.Where(p => p.BookingDate == dt).ToList();
            else
                return null;
        }
        /// <summary>
        /// Publiczna metoda, ktora wyszukuje rezerwacje z listof_reservations po budynku
        /// </summary>
        /// <param name="b">Obiekt klasy Building</param>
        /// <returns>Zwraca liste rezerwacji dla danego budynku</returns>
        public List<Reservation> FindingReservations_byBuilding(string b)
        {
            if (listof_reservations.Exists(p => p.Structure.Address == b))
                return (List<Reservation>)listof_reservations.Where(p => p.Structure.Address == b).ToList();
            else
                return null;
        }
        /// <summary>
        /// Publiczna metoda, ktora korzysta z metody FindingReservations_byBuilding i wyszukuje rezerwacje dla podanej sali w budynku
        /// </summary>
        /// <param name="b">Obiekt klasy Building</param>
        /// <param name="r">Obiekt klasy Room</param>
        /// <returns>Zwraca liste rezerwacji przypadajacych na dana sale w konkretnym budynku</returns>
        public List<Reservation> FindingReservations_byRoom(Building b, Room r)
        {
            try
            {
                List<Reservation> all_r = FindingReservations_byBuilding(b.Name);
                List<Reservation> good_r = (List<Reservation>)all_r.Where(p => p.Apartment == r);
                return good_r;
            }
            catch(ArgumentNullException)
            {
                throw new Exception("Couldnt do it!");
            }
        }



        #endregion Methods for reservations


        #region Methods for XML
        /// <summary>
        /// Publiczna metoda, ktora pozwala zpaisac plik formacie XML
        /// </summary>
        /// <param name="fname">Nazwa pliku(string)</param>
        public void SaveXML(string fname)
        {
            using FileStream fs = new(fname, FileMode.Create);
            DataContractSerializer dc = new(typeof(DataBase));
            dc.WriteObject(fs, this);
        }
        /// <summary>
        /// Pobranie danych z pliku o podanej nazwie do programu
        /// </summary>
        /// <param name="fname">Nazwa pliku(string)</param>
        /// <returns>Zwraca dane zawierane w pliku</returns>
        public static DataBase? OpenXML(string fname)
        {
            if (!File.Exists(fname)) { return null; }
            using FileStream fs = new(fname, FileMode.Open);
            DataContractSerializer dc = new(typeof(DataBase));
            return dc.ReadObject(fs) as DataBase;
        }

        #endregion Methods for XML

        #endregion Methods

    }
}
