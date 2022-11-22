using BL_Projectwerk.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAdmin.Domein
{
    public class VisitAdmin
    {
        public VisitAdmin(Visit bezoeker)
        {
            Bezoeker = bezoeker;
            ZetName(bezoeker);
            ZetWerknemer(bezoeker);
            ZetBegin(bezoeker);
            ZetEinde(bezoeker);
        }

        public Visit Bezoeker { get; set; }
        public string Name { get; set; }
        public string Werknemer { get; set; }
        public string Begin { get; set; }
        public string Einde { get; set; }
        private void ZetName(Visit bezoeker)
        {
            Name = $"{bezoeker.Visitor.LastName}, {bezoeker.Visitor.FirstName}";
        }
        private void ZetWerknemer(Visit bezoeker)
        {
            Werknemer = $"{bezoeker.Contact.LastName}, {bezoeker.Contact.FirstName}";
        }
        private void ZetBegin(Visit bezoeker)
        {
            Begin = $"{bezoeker.StartingTime.Day}-{bezoeker.StartingTime.Month}-{bezoeker.StartingTime.Year}, {bezoeker.StartingTime.Hour}h{bezoeker.StartingTime.Minute}";
        }
        private void ZetEinde(Visit bezoeker)
        {
            if (bezoeker.EndingTime == bezoeker.StartingTime)
            {
                Einde = "--";
            }
            else { Einde = $"{bezoeker.EndingTime.Day}-{bezoeker.EndingTime.Month}-{bezoeker.EndingTime.Year}, {bezoeker.EndingTime.Hour}h{bezoeker.EndingTime.Minute}"; }
        }
    }
}
