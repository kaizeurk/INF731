using System;

/**
 * Auteurs: Fangue Emmanuel et Fossuo Talom Hermann
 * Nom du fichier: Program.cs
 * Nom projet: INF731_TP1
 * Date de creation: 2018-03-13
 * Description: 
 */

namespace INF731_TP1
{
    internal class Program
    {
        private const string DEMANDE_NOM_FACTURE   = "Donnez le nom du fichier contenant les articles à facturer : ";
        private const string FIN_PROGRAMME         = "--Fin du programme--";
        private const string FACTURE_GENERE        = "La facture a été produite dans le fichier Facture-{0}";
        
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
                    condition_de_sortie = false;
                }
                else if(facture.NomFacture != Facture.Erreur_fichier)
                {
                    string ligne_facture = facture.ToString();
                    //Console.WriteLine(ligne_facture);
                    FichierFacture.ecrireFacture(Facture.NOM_RACINE+facture.NomFacture,ligne_facture);
                    Console.WriteLine(FACTURE_GENERE,facture.NomFacture);
                }
                

            } while (condition_de_sortie);
        }
    }
}