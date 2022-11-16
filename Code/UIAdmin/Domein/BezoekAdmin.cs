using BL_Projectwerk.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAdmin.Domein
{
    public class BezoekAdmin
    {
        public BezoekAdmin(Bezoek bezoek)
        {
            Bezoeker = bezoek;
            ZetName(bezoek);
            ZetWerknemer(bezoek);
            ZetBegin(bezoek);
            ZetEinde(bezoek);
        }

        public Bezoek Bezoeker { get; set; }
        public string Name { get; set; }
        public string Werknemer { get; set; }
        public string Begin { get; set; }
        public string Einde { get; set; }
        private void ZetName(Bezoek bezoek)
        {
            Name = $"{bezoek.Bezoeker.Naam}, {bezoek.Bezoeker.Voornaam}";
        }
        private void ZetWerknemer(Bezoek bezoek)
        {
            Werknemer = $"{bezoek.Contactpersoon.Naam}, {bezoek.Contactpersoon.Voornaam}";
        }
        private void ZetBegin(Bezoek bezoek)
        {
            Begin = $"{bezoek.StartTijd.Day}-{bezoek.StartTijd.Month}-{bezoek.StartTijd.Year}, {bezoek.StartTijd.Hour}h{bezoek.StartTijd.Minute}";
        }
        private void ZetEinde(Bezoek bezoek)
        {
            if (bezoek.EindTijd == bezoek.StartTijd)
            {
                Einde = "-";
            }
            else { Einde = $"{bezoek.EindTijd.Day}-{bezoek.EindTijd.Month}-{bezoek.EindTijd.Year}, {bezoek.EindTijd.Hour}h{bezoek.EindTijd.Minute}"; }
        }
    }
}
