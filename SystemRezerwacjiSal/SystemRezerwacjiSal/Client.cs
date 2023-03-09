using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiSal
{
    /// <summary>
    /// Publiczna klasa WrongConvertToEnumException, ktora dziedziczy po klasie exception
    /// </summary>
    class WrongConvertToEnumException : Exception
    {
        public WrongConvertToEnumException() : base()
        { }
        public WrongConvertToEnumException(string message) : base(message)
        { }
    }
    /// <summary>
    /// Publiczna klasa Client, ktora dziedziczy po klasie Person
    /// </summary>
    public class Client : Person
    {
        #region Fields
        IsThere rODO;

        #endregion Fields

        #region Properties
        [DataMember]
        public IsThere RODO { get { return rODO; } 
            set 
            {
                if (Enum.TryParse(typeof(IsThere), value.ToString().ToLower(), out object? f))
                    rODO = (IsThere)f;
                else
                    throw new WrongConvertToEnumException("Cant Convert this value to 'IsThere'");
            }
        }
        #endregion Properties

        #region Constructors

        public Client() : base()
        { }
        public Client(string pesel) : base(pesel)
        {
            rODO = IsThere.no;
        }


        public Client(string pesel, string name) : base(pesel, name)
        { }
        

        public Client(string pesel, string name, string surname) : base(pesel, name, surname)
        { }

        public Client(string name, string surname, string pesel, string phoneNumber) : base(name, surname, pesel, phoneNumber)
        {
        }

        public Client(string name, string surname, string pesel, string phoneNumber,string Rodo) : base(name, surname, pesel, phoneNumber)
        {
            if (Enum.TryParse(typeof(IsThere), Rodo.ToLower(), out object? f))
            {
                rODO = (IsThere)f;
            }
            else
            {
                throw new WrongConvertToEnumException("This value cannot be converted to 'IsThere' ");
            }
        }


        #endregion Constructors

        #region Methods
        /// <summary>
        /// Publiczna metoda, ktora nadpisuje metode ToString
        /// </summary>
        /// <returns>Zwraca dane o kliencie, oraz informaje czy wyrazil zgode na przetwarzanie swoich danych</returns>
        public override string ToString()
        {
            return base.ToString() + $"\n {RODO} wyraził zgode RODO"; 
        }

        #endregion Methods

    }
}
