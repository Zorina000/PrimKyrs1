using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimKyrs1
{
    /// <summary>
    /// класс страны
    /// </summary>
    public class Count
    {
        /// <summary>
        /// Id страны
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// название страны
        /// </summary>
        public string? Country { get; set; }
        /// <summary>
        /// название столицы
        /// </summary>
        public string? Capital { get; set; }
        /// <summary>
        /// название континента
        /// </summary>
        public string? Continent { get; set; }
        /// <summary>
        /// население страны
        /// </summary>
        public int Population { get; set; }
        /// <summary>
        /// площадь страны
        /// </summary>
        public double Area { get; set; }

        public override string ToString()
        {
            return Country + "  " + Capital + "  " + Continent + "  " + Population + "  " + Area;
        }
    }
}
