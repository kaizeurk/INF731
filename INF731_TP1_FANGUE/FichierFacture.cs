using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;   // pour utiliser les fichiers


/**
 * Auteurs: Fangue Emmanuel et Fossuo Talom Hermann
 * Nom du fichier: FichierFacture.cs
 * Nom projet: INF731_TP1_FANGUE
 * Date de creation: 2018-03-13
 * Description: permet de gerer les entrees/sortie avec les fuchiers
 */
namespace INF731_TP1
{
    static public class FichierFacture
    {
        private const  string CHEMIN_RELATIF = "../../";
        private const char    SEPARATEUR = ';';
        
        
        private static StreamWriter flux_entrant;

        private static StreamReader flux_sortant;

        static public void ecrireFacture(string nom_facture_entrant, string enregistrement)
        {
            try
            {
                flux_entrant = new StreamWriter(CHEMIN_RELATIF + nom_facture_entrant);
                flux_entrant.WriteLine(enregistrement);
                flux_entrant.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

        /**
         * 
         */

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
                double prix_unit;
                int  ligne= 0;
                int  qte;
                while (!flux_sortant.EndOfStream)
                {
                    ligne_article = flux_sortant.ReadLine();
                    element = ligne_article.Split(SEPARATEUR);
                    ligne++;
                    if (element.Length != 5)
                    {
                        throw new Exception(String.Format("Erreur dans le fichier {0} format incorrect a la linge {1}",nom_facture_sortant,ligne));
                    }
                    else if (element[0] == String.Empty)
                    {
                        throw new Exception(String.Format("Erreur dans le fichier {0} la colonne 1 est vide a la linge {1}",nom_facture_sortant,ligne));
                    }
                    else if ((Article.TAXABLE != element[1].ToUpper() && Article.NON_TAXABLE != element[1].ToUpper()))
                    {
                        throw new Exception(String.Format("Erreur dans le fichier {0} a la colonne 2 doit-etre {2} ou {3} a la linge {1}",nom_facture_sortant,ligne,Article.NON_TAXABLE,Article.TAXABLE));
                    }
                    else if (element[3] == String.Empty)
                    {
                        throw new Exception(String.Format("Erreur dans le fichier {0} la colonne 4 est vide a la linge {1}",nom_facture_sortant,ligne));
                    }
                    
                    else if (int.TryParse(element[2], out qte)==false)
                    {
                        throw new Exception(String.Format("Erreur dans le fichier {0} la colonne 3 doit-etre un entier {1}",nom_facture_sortant,ligne));
                        
                    }
                    else if (double.TryParse(element[4],NumberStyles.Float, CultureInfo.GetCultureInfo("tr-TR"), out prix_unit) == false)
                    {
                        throw new Exception(String.Format("Erreur dans le fichier {0} la colonne 5 doit-etre de type 0,00 a la ligne {1}",nom_facture_sortant,ligne));
                    }
                    else
                    {
                        prix_unit = Math.Round(prix_unit,2);
                        Article un_article = new Article(element[0], element[1], qte, element[3],
                            (float)prix_unit);
                        list.Add(un_article);
                        
                    }
                   
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