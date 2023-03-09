using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SystemRezerwacjiSal
{
    public enum IsThere {yes,no }
    [DataContract]
    /// <summary>
    /// Abstrakcyjna klasa Equipment, definiuje jaki sprzet jest w pokoju
    /// </summary>
    public abstract class Equipment
    {
        #region Fields

        IsThere wifi;
        IsThere projector;
        IsThere soundSystem;
        int numberof_chairs;
        IsThere airConditioning;
        IsThere illumination;

        #endregion Fields

        #region Properties
        [DataMember]
        public IsThere Wifi { get { return wifi; } set { wifi = value; } }
        [DataMember]
        public IsThere Projector { get { return projector; } set { projector = value; } }
        [DataMember]
        public IsThere  SoundSystem { get { return soundSystem; } set { soundSystem = value; } }
        [DataMember]
        public int Numberof_chairs { get { return numberof_chairs; } set { numberof_chairs = value; } }
        [DataMember]
        public IsThere AirConditioning { get { return airConditioning; } set { airConditioning = value; } }
        [DataMember]
        public IsThere Illumination {  get { return illumination; } set { illumination = value; } }

        #endregion Properties

        #region Constructors

        public Equipment()
        {
            wifi = IsThere.no;
            projector = IsThere.no;
            soundSystem = IsThere.no;
            numberof_chairs = 0;
            airConditioning = IsThere.no;
            illumination = IsThere.no;
        }

        public Equipment(string wiFi) : this()
        {
            if(Enum.TryParse(typeof(IsThere),wiFi.ToLower(), out object? f)) 
            {
                wifi = (IsThere)f;
            }
            else
                throw new WrongConvertToEnumException("Cant Convert this value to 'IsThere'");
        }

        public Equipment(string wiFi,string projector) : this(wiFi)
        {
            if (Enum.TryParse(typeof(IsThere),projector.ToLower(), out object? f))
            {
                this.projector = (IsThere)f;
            }
            else
                throw new WrongConvertToEnumException("Cant Convert this value to 'IsThere'");
        }

        public Equipment(string wiFi, string projector, string soundSystem) : this(wiFi,projector)
        {
            if (Enum.TryParse(typeof(IsThere), soundSystem.ToLower(), out object? f))
            {
                this.soundSystem = (IsThere)f;
            }
            else
                throw new WrongConvertToEnumException("Cant Convert this value to 'IsThere'");
        }

        public Equipment(string wiFi,string projector,string soundSystem,int numberofcharis ) : this(wiFi,projector,soundSystem)
        {
            this.numberof_chairs = numberofcharis;
        }

        public Equipment(string wiFi,string projector,string soundSystem,int numberofcharis,string airConditioning) 
        :this(wiFi,projector,soundSystem,numberofcharis)
        {
            if (Enum.TryParse(typeof(IsThere), airConditioning.ToLower(), out object? f))
            {
                this.airConditioning = (IsThere)f;
            }
            else
                throw new WrongConvertToEnumException("Cant Convert this value to 'IsThere'");
        }

        public Equipment(string wiFi, string projector, string soundSystem, int numberofcharis, string airConditioning, string illumination)
            :this(wiFi,projector,soundSystem, numberofcharis,airConditioning)
        {
            if (Enum.TryParse(typeof(IsThere), illumination.ToLower(), out object? f))
            {
                this.illumination = (IsThere)f;
            }
            else
                throw new WrongConvertToEnumException("Cant Convert this value to 'IsThere'");
        }



        #endregion Constructors

        #region Methods
        /// <summary>
        /// Publiczna metoda, nadpiusje metode ToString
        /// </summary>
        /// <returns>Zwraca informacje o dostepnosci rzeczy w pokoju </returns>
        public override string ToString()
        {
            return $"{nameof(Wifi)} - {Wifi}"
                + $"\n{nameof(Projector)} - {projector}"
                + $"\n{nameof(SoundSystem)} - {SoundSystem}"
                + $"\nNumber of chairs - {Numberof_chairs}"
                + $"\n{nameof(AirConditioning)} - {AirConditioning}"
                + $"\n{nameof(Illumination)} - {Illumination}";
        }


        #endregion Methods


    }
}
