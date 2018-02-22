using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;   // pour utiliser les fichiers

namespace INF731_TP1_FANGUE
{
    static public class FichierFacture
    {
        private const  string CHEMIN_RELATIF = "../../";
        private const char    SEPARATEUR = ';';
        
        
        private static StreamWriter flux_entrant;
        private static StreamReader flux_sortant;
        
        /**
         * 
         */
        static public void ecrireFacture(string nom_facture_entrant, string enregistrement)
        {
            flux_entrant = new StreamWriter(CHEMIN_RELATIF + nom_facture_entrant);
            flux_entrant.WriteLine(enregistrement);

            flux_entrant.Close();
        }

        static public void ecrireFacture(string enregistrement, StreamWriter _flux_entrant)
        {
            flux_entrant = _flux_entrant;
            flux_entrant.WriteLine(enregistrement);
        }

        /**
         * <param name="nom_facture_sortant">nom du fichier venant du client.</param>
         * <param name="list">Collection d'article venant du client</param>
         */
        static public List<Article> lectureFacture(string nom_facture_sortant, List<Article> list)
        {
            if(isFileExist(nom_facture_sortant))
            {
                flux_sortant = new StreamReader(CHEMIN_RELATIF + nom_facture_sortant);
                string ligne_article;
                string[] element;
                double localCultreResult;
                while (!flux_sortant.EndOfStream)
                {
                    ligne_article = flux_sortant.ReadLine();
                    element = ligne_article.Split(SEPARATEUR);
                    localCultreResult = double.Parse(element[4], CultureInfo.GetCultureInfo("tr-TR"));
                    localCultreResult = Math.Round(localCultreResult,2);
                    Article un_article = new Article(element[0], element[1], int.Parse(element[2]), element[3],
                        (float)localCultreResult);
                    list.Add(un_article);
                   
                }

                
                flux_sortant.Close();
            }
            return list;
            
        }

        /**
         * 
         */
        static public Article lectureFacture(string nom_facture_sortant, StreamReader _flux_entrant)
        {
            string ligne_article;
            string[] element;
            
            ligne_article = _flux_entrant.ReadLine();
            element = ligne_article.Split(SEPARATEUR);
            Article un_article = new Article(element[0], element[1], int.Parse(element[2]), element[3], float.Parse(element[4]));

            return un_article;
        }

        static public bool isFileExist(string in_nom_fichier)
        {
            return File.Exists(CHEMIN_RELATIF+in_nom_fichier);
        }

    }
}