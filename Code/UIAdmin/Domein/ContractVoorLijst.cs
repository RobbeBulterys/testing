using BL_Projectwerk.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAdmin.Domein
{
    public class ContractVoorLijst
    {
        public ContractVoorLijst(Bezoek bezoeker)
        {
            Bezoeker = bezoeker;
            ZetName(bezoeker);
            ZetWerknemer(bezoeker);
            ZetBegin(bezoeker);
            ZetEinde(bezoeker);
        }

        public Bezoek Bezoeker { get; set; }
        public string Name { get; set; }
        public string Werknemer { get; set; }
        public string Begin { get; set; }
        public string Einde { get; set; }
        private void ZetName(Bezoek bezoeker)
        {
            Name = $"{bezoeker.Bezoeker.Naam}, {bezoeker.Bezoeker.Voornaam}";
        }
        private void ZetWerknemer(Bezoek bezoeker)
        {
            Werknemer = $"{bezoeker.Contactpersoon.Naam}, {bezoeker.Contactpersoon.Voornaam}";
        }
        private void ZetBegin(Bezoek bezoeker)
        {
            Begin = $"{bezoeker.StartTijd.Day}-{bezoeker.StartTijd.Month}-{bezoeker.StartTijd.Year}, {bezoeker.StartTijd.Hour}h{bezoeker.StartTijd.Minute}";
        }
        private void ZetEinde(Bezoek bezoeker)
        {
            if (bezoeker.EindTijd == bezoeker.StartTijd)
            {
                Einde = "--";
            }
            else { Einde = $"{bezoeker.EindTijd.Day}-{bezoeker.EindTijd.Month}-{bezoeker.EindTijd.Year}, {bezoeker.EindTijd.Hour}h{bezoeker.EindTijd.Minute}"; }
        }
    }
}
