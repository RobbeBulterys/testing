﻿using BL_Projectwerk.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Interfaces
{
    public interface IBezoekerRepository
    {
        public bool BestaatBezoeker(Bezoeker bezoeker);
        bool BestaatBezoeker(int value);
        Bezoeker GeefBezoeker();
        Bezoeker GeefBezoeker(int persoonId);
        Bezoeker GeefBezoekers();
        IEnumerable<Bezoeker> GeefBezoekers(string? naam, string? voornaam, string? email, string? bedrijf);
        void UpdateBezoeker(Bezoeker bezoeker);
        void VoegBezoekerToe(Bezoeker bezoeker);
    }
}