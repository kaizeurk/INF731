using System;
using System.Net;

namespace INF731_TP1_FANGUE
{
    internal class Program
    {
        private const string DEMANDE_NOM_FACTURE = "Donnez le nom du fichier contenant les articles à facturer : ";
        private const string FIN_PROGRAMME = "--Fin du programme--";
        private const string DEMANDE_FIN_PROGRAMME = "Appuyez sur Q pour quitter ou sur une touche pour continuer...";
        private const string FACTURE_GENERE = "La facture a été produite dans le fichier Facture-{0}";
        
        public static void Main(string[] args)
        {
            bool condition_de_sortie = true;
            do
            {
               Facture facture = new Facture();
               Console.Write(DEMANDE_NOM_FACTURE);
               facture.NomFacture = Console.ReadLine();
                if (String.IsNullOrEmpty(facture.NomFacture))
                {
                    Console.WriteLine(FIN_PROGRAMME);
                }
                else
                {
                    string ligne_facture = facture.ToString();
                    Console.WriteLine(ligne_facture);
                    FichierFacture.ecrireFacture(Facture.NOM_RACINE+facture.NomFacture,ligne_facture);
                    Console.WriteLine(FACTURE_GENERE,facture.NomFacture);
                }
                Console.WriteLine(DEMANDE_FIN_PROGRAMME);
                string continu = Console.ReadLine();
                condition_de_sortie = ("Q" != continu.ToUpper());
                

            } while (condition_de_sortie);
        }
    }
}