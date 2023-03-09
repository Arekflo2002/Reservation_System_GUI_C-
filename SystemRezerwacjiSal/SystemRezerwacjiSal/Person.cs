using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SystemRezerwacjiSal
{
    #region Exceptions 
    /// <summary>
    /// Publiczna klasa InvalidPeselException, ktora dziedziczy po klasie Exception
    /// </summary>
    public class InvalidPeselException : Exception
    {
        /// <summary>
        /// Konstruktor nieparametryczny korzystajacy z konstruktora klasy bazowej
        /// </summary>
        public InvalidPeselException() : base()
        { }
        /// <summary>
        /// Konstruktor parametryczny korzystajacy z konstruktora klasy bazowej
        /// </summary>
        public InvalidPeselException(string message) : base(message)
        { }
    }

    /// <summary>
    /// Publiczna klasa InvalidPhoneException, ktora dziedzicy po klasie Exception
    /// </summary>
    public class InvalidPhoneException : Exception
    {
        public InvalidPhoneException() : base()
        { }

        public InvalidPhoneException(string message) : base(message)
        { }
    }
    #endregion Exceptions 

    [DataContract]
    
    /// <summary>
    /// Abstrakcyjna klasa Person, definiuje jakie dane ma osoba
    /// </summary>
    public abstract class Person 
    {
       
        #region Fields 
        string name;
        string surname;
        string pesel;
        string phoneNumber;
        #endregion Fields

        #region Properties
        [DataMember]
   
        public string Name { get { return name; } set { name = value; } }
        [DataMember]
        public string Surname { get { return surname; } set { surname = value; } }
        [DataMember]
        
        public string Pesel{ get { return pesel; }
            set
            {
                if (Checking_Pesel(value))
                {
                    pesel = value;
                }
            }
        }
        [DataMember]
        public string PhoneNumber { get { return phoneNumber; }
        set
            {
                if(Checking_phoneNB(value))
                {
                    phoneNumber = value;
                }

            }
        }

        #endregion Properties

        #region Constructors
       
        public Person()
        { }
        
        public Person(string pesel)
        {
            if(Checking_Pesel(pesel))
                this.pesel = pesel;
        }
        
        public Person(string pesel,string name) : this(pesel)
        {
            this.name = name;
        }
        
        public Person(string pesel,string name,string surname) : this(pesel, name)
        {
            this.surname = surname;
        }
        
        public Person(string name, string surname,string pesel,string phoneNumber) : this(name,surname,pesel)
        {
            if(Checking_phoneNB(phoneNumber))
                this.phoneNumber = phoneNumber;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Nadpisanie metody ToString
        /// </summary>
        /// <returns>Zwraca dane o osobie(Imie,Nazwisko, Pesel,Numer telefonu)</returns>
        public override string ToString()
        {
            return $"{Name} {Surname}({Pesel}), Tel. : {PhoneNumber}";
        }
        /// <summary>
        /// Sprawdzajaca poprawnosc peselu
        /// </summary>
        /// <param name="p">Pesel osoby(string)</param>
        /// <returns>True jesli poprawny, False jesli zly</returns>
        /// <exception cref="InvalidPeselException">Obiekt klasy InvalidPeselException, z komunikatem</exception>
        private bool Checking_Pesel(string p)
        {
            if (Regex.IsMatch(p, @"\d{11}"))
            {
                return true;
            }
            else
            {
                throw new InvalidPeselException("Zły Pesel!");
            }
        }
        /// <summary>
        /// Sprawdza poprawnosc numeru telefonu
        /// </summary>
        /// <param name="phone">Telefon osoby(string)</param>
        /// <returns>True jesli poprawny, False jesli zly</returns>
        /// <exception cref="InvalidPhoneException">Obiekt klasy InvalidPhoneException, z komunikatem</exception>
        private bool Checking_phoneNB(string phone)
        {

            if (Regex.IsMatch(phone, @"\d{9}"))
            {
                return true;
            }
            else
            {
                throw new InvalidPhoneException("Zly numer telefonu!");
            }
        }


        #endregion Methods
    }
}
