using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eval3GroupYangLi_YongjiangLiu_Final
{
    internal class Voyageur
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Sexe { get; set; }
        public string Naissance { get; set; }
        public string Passeport { get; set; }
        public string Echeance { get; set; }

        public Voyageur() { }
        public Voyageur(string nom, string prenom, string sexe, string naissance, string passeport, string echeance)
        {
            Nom = nom;
            Prenom = prenom;
            Sexe = sexe;
            Naissance = naissance;
            Passeport = passeport;
            Echeance = echeance;
        }

        public bool CompareVoyageur(Voyageur v)
        {
            if (v != null)
            {
                if (Nom == v.Nom && Prenom == v.Prenom && Sexe == v.Sexe && Naissance == v.Naissance && Passeport == v.Passeport && Echeance == v.Echeance)
                    return true;
            }
            return false;
        }
    }
}
