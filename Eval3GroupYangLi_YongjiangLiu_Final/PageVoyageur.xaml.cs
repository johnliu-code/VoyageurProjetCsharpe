using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Eval3GroupYangLi_YongjiangLiu_Final
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class PageVoyageur : Page
    {
        //Voyageurs list
        private List<Voyageur> voyageurs = new List<Voyageur>()
        {
            new Voyageur ("Zhang", "San", "Homme", "1990-01-20", "ABC-123-223", "2025-01-20"),
            new Voyageur ("Smith", "Johna", "Femme", "1995-11-30", "DEF-101-220", "2027-11-30"),
            new Voyageur ("Wang", "Wu", "Homme", "1980-08-05", "GHT-838-119", "2028-08-05"),
        };
        int position = 0;

        //Voyageur instance
        Voyageur voyageur;
        int exist = 0;

        public object MessageBox { get; private set; }
        public object MessageBoxButton { get; private set; }

        const string errmsg = "Voyageur a dejas existee, verifiez et reentrez SVP!!";
        const string errtitle = "Existing Errror...";


        public PageVoyageur()
        {
            this.InitializeComponent();
            this.DataContext = voyageurs[position];
        }

        private void btnPremier_Click(object sender, RoutedEventArgs e)
        {
            position = 0;
            DataContext = voyageurs[position];
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            if (position != voyageurs.Count - 1)
                position++;
            else position = 0;
            DataContext = voyageurs[position];
        }

        private void btnPrecedent_Click(object sender, RoutedEventArgs e)
        {
            if (position != 0)
                position--;
            else position = voyageurs.Count - 1;
            DataContext = voyageurs[position];
        }

        private void btnDernier_Click(object sender, RoutedEventArgs e)
        {
            position = voyageurs.Count - 1;
            DataContext = voyageurs[position];
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            DataContext = null;
        }

        private void btnNouveau_Click(object sender, RoutedEventArgs e)
        {
            voyageur = new Voyageur();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            voyageur = (Voyageur)DataContext;

            for (int i = 0; i < voyageurs.Count; i++)
            {
                if (voyageurs[i].CompareVoyageur(voyageur) == true)
                    exist++;
            }

            if (exist == 0)
                voyageurs.Add(voyageur);
            else
                txbMessage.Text = errmsg;
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                for (int i = 0; i < voyageurs.Count; i++)
                {
                    if (voyageurs[i].CompareVoyageur((Voyageur)DataContext) == true)
                    {
                        exist++;
                        voyageurs.Remove(voyageurs[i]);
                        DataContext = null;
                        txbMessage.Text = "Supprimer success!!";
                    }
                }
            }

            if (exist == 0)
            {
                txbMessage.Text = "Voyageur exist pas!!";
            }
        }
    }
}