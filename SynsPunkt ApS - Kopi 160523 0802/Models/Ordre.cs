using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Ordre
    {
        public int OrdreNummer { get; set; }
        public DateTime Tidspunkt { get; set; }
        public DateTime Dato { get; set; }
        public double SamletPris { get; set; }
        public List<Models.VareLinje> SamletVare { get; set; }

        public Ordre(int ordreNummer, DateTime tidspunkt, DateTime dato, double samletPris, List<VareLinje> samletVare)
        {
            OrdreNummer = ordreNummer;
            Tidspunkt = tidspunkt;
            Dato = dato;
            SamletPris = samletPris;
            SamletVare = samletVare;
        }
    }
}