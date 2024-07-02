using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankss.Model
{
    public class Tank
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public string Typ { get; set; }
        public string KrajPochodzenia { get; set; }
        public Cannon Cannon { get; set; }
        public Armor Armor { get; set; }
        public Engine Engine { get; set; }
        public List<Army> Armies { get; set; }
        public string BackgroundImageSource { get; internal set; }

        public Tank()
        {
            Cannon = new Cannon();
            Armor = new Armor();
            Engine = new Engine();
            Armies = new List<Army>();
        }

    }
    public class Army
    {
        public string Nazwa { get; set; }
    }
    public class Armor
    {
        public int Przod { get; set; }
        public int Bok { get; set; }
        public int Tyl { get; set; }
    }
    public class Cannon
    {

        public string Nazwa { get; set; }
        public int SrUszkodzenia { get; set; }
        public double CzasLad { get; set; }
        public double Rozrzut { get; set; }
    }
    public class Engine
    {
        public int moc {  get; set; }
        public int pred_maks { get; set;}

    }
}
