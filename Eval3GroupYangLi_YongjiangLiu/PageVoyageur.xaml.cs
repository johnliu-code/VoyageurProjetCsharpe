using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Eval3GroupYangLi_YongjiangLiu
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
            new Voyageur ("Smith", "Anna", "Femme", "1995-11-30", "DEF-101-220", "2027-11-30"),
            new Voyageur ("Wang", "Wu", "Homme", "1980-08-05", "GHT-838-119", "2028-08-05"),
        };
        int position = 0;

        //Voyageur instance
        Voyageur voyageur;
        int find_v = 0;
        bool exist, nouveau;
        List<string> voy_list = new List<string>();
        string displaylist;

        string errmsg = "Le voyageur existe déjà, cliqez sur button Nouveau pour l'entrer à nouveau.";
        string errtitle = "Existing Errror..."; 

        string listhead = " Nom        Prenom        Sexe         Naissance       Passeport       Echeance \n "
                        + "-------------------------------------------------------------------------------\n";
        //Dispaly List in the form area:

        private void voyageurPage_Loading(FrameworkElement sender, object args)
        {
            //Add voyageur string to list variable
            for (int i = 0; i < voyageurs.Count; i++)
                displaylist += voyageurs[i].ToString();

            //Add text to textblock
            txbList.Text = listhead + displaylist;
        }


        public PageVoyageur()
        {
            this.InitializeComponent();
            this.DataContext = voyageurs[position];
        }

        private void btnPremier_Click(object sender, RoutedEventArgs e)
        {
            position = 0;
            DataContext = voyageurs[position];
            cbxSexe.SelectedValue = voyageurs[position].Sexe;
            string message = "Le premier enregistrement est affiché";

            // Initialize a new MessageDialog instance
            MessageDialog messageDialog = new MessageDialog(message, "Information!");
            // Display the message dialog
            messageDialog.ShowAsync();
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            if (position != voyageurs.Count - 1)
                position++;
            else position = 0;
            DataContext = voyageurs[position];
            cbxSexe.SelectedValue = voyageurs[position].Sexe;
        }

        private void btnPrecedent_Click(object sender, RoutedEventArgs e)
        {
            if (position != 0)
                position--;
            else position = voyageurs.Count - 1;
            DataContext = voyageurs[position];
            cbxSexe.SelectedValue = voyageurs[position].Sexe;
        }

        private void btnDernier_Click(object sender, RoutedEventArgs e)
        {
            position = voyageurs.Count - 1;
            DataContext = voyageurs[position];
            cbxSexe.SelectedValue = voyageurs[position].Sexe;
            //Afficher message
            string message = "Le dernier enregistrement est affiché";
            MessageDialog messageDialog = new MessageDialog(message, "Information!");
            messageDialog.ShowAsync();
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            cbxSexe.SelectedValue = null;
        }

        private void btnNouveau_Click(object sender, RoutedEventArgs e)
        {
            foreach (Voyageur v in voyageurs)
            {
                if (v.Passeport.Contains(txbPasseport.Text))
                    exist = true;
            }
            //Check nouveau est pas ajouter
            if ((nouveau != true) && (exist != true))
            {
                string message = "Un nouvel enregistrement non ajouté, entrez votre information et liquez sur le button Ajouter SVP!!";
                MessageDialog messageDialog = new MessageDialog(message, "Nouveau est pas ajouter!!");
                messageDialog.ShowAsync();
            }

            voyageur = new Voyageur();
            DataContext = voyageur;
            cbxSexe.SelectedValue = voyageurs[position].Sexe;
            nouveau = false;
            exist = false;
            txbMessage.Text = "Ajoutez nouveau information SVP!!";   //other method of message
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (txbPasseport.Text != "")
            {
                voyageur = new Voyageur(txbNom.Text, txbPrenom.Text, cbxSexe.SelectedValue.ToString(), txbNaissance.Text, txbPasseport.Text, txbEcheance.Text);
                //voyageur = (Voyageur)DataContext;
                for (int i = 0; i < voyageurs.Count; i++)
                {
                    if (voyageurs[i].CompareVoyageur(voyageur) == true)
                        exist = true;
                }

                //Add voyageur if it is new
                if (!exist)
                {
                    voyageurs.Add(voyageur);
                    position += 1;             //Set position to the last and display it
                    DataContext = voyageurs[position];
                    txbMessage.Text = "Ajoutez nouveau voyageur success!!";
                    txbList.Text += voyageur.ToString();
                }
                else
                {
                    txbMessage.Text = errmsg;
                    exist = false;
                    MessageDialog messageDialog = new MessageDialog(errmsg, errtitle);
                    messageDialog.ShowAsync();
                }
            }
            else
            {
                // Check if user add empty value to apply new voyageur
                MessageDialog messageDialog = new MessageDialog("Pour ajouter voyageur cliquez sur le button Nouveau avant de Entrez votre information SVP!!", "Error d'ajouter null voyageur");
                messageDialog.ShowAsync();
            }

        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            //Method 1 for print out error message with textbox
            //Check do Not to delete empty data
            if (txbPasseport.Text != "")
            {
                for (int i = 0; i < voyageurs.Count; i++)
                {
                    if (voyageurs[i].Passeport.Contains(txbPasseport.Text))
                    {
                        string deletelist = voyageurs[i].ToString();
                        voyageurs.RemoveAt(i);
                    }

                }

                DataContext = null;
                cbxSexe.SelectedValue = null;
                txbMessage.Text = "Supprimer success!!";
                position -= 1;

            }
            else
                txbMessage.Text = "Choisez element pour supperimer!!";

            //Update list


            //Method delete and show message with dialog box



        }
    }
}


