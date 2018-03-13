using System;
using System.Collections.Generic;


/**
 * Auteurs: Fangue Emmanuel et Fossuo Talom Hermann
 * Nom du fichier: Facture.cs
 * Nom projet: INF731_TP1_FANGUE
 * Date de creation: 2018-03-13
 * Description: 
 */
namespace INF731_TP1
{
    public class Facture
    {
        /* Declaration des constantes */
        public  const double TPS                 = 0.05;
        public  const double TVQ                 = 0.09975;
        public  const string NOM_RACINE          = "Facture-";
        private const int    NONBRE_DE_PAS_LIGNE = 80;
        private const string AUTEURS             = "EMMANUEl FANGUE";
        public  const string Erreur_fichier      = "erreur 404";
        private const int colonne1 = 13;
        private const int colonne2 = 2;
        private const int colonne3 = 36;
        private const int colonne4 = 7;
        private const int colonne5 = 5;
        private const int colonne6 = 7;
        private const int ligne_total = 72;
        
        /* Declaration des attributs */
        private List<Article> articles;
        private string nom_facture;
        private double montant_tps;
        private double montant_tvq;
        private double sous_total;

        public Facture()
        {
            articles = new List<Article>();
        }

        public List<Article> Articles
        {
            get => articles;
            set => articles = value;
        }

        public string NomFacture
        {
            get => nom_facture;
            set
            {
                if (FichierFacture.isFileExist(value))
                {
                    nom_facture = value;
                    genererFacture();
                }
            }
        }

        public void ajouterArticle(Article in_article)
        {
            if ((in_article.GetType() == typeof(Article)))
            {
                articles.Add(in_article); 
            }
        }

        public void ajouterArticles(List<Article> in_articles)
        {
          articles.AddRange(in_articles);
        }

        private void genererFacture()
        {
            Articles = FichierFacture.lectureFacture(NomFacture, articles);

        }

        public void calculerTaxe(Article in_article)
        {
            if (in_article.estNonTaxable()==false)
            {
                montant_tps += formatMonnai(in_article.Quantite * (TPS * in_article.PrixUniaite ));
                montant_tvq += formatMonnai(in_article.Quantite * (TVQ * in_article.PrixUniaite));
            }

        }

        private string entete()
        {
            string entete = new string('_',NONBRE_DE_PAS_LIGNE);
            entete += '\n';
            entete += "   Facture produite pour le fichier "+ NOM_RACINE + nom_facture +'\n';
            entete += "   par "+ AUTEURS +'\n';
            entete += new string('_',NONBRE_DE_PAS_LIGNE) +'\n';

            return entete;

        }

        private string ligneFacture(Article in_article)
        {
            string ligne_facture = "";
            ligne_facture += in_article.NumeroArticle.PadRight(colonne1, ' ');
            ligne_facture += (in_article.Quantite + "").PadRight(colonne2, ' ');
            ligne_facture += in_article.Description.PadRight(colonne3, ' ');
            ligne_facture += (formatMonnai(in_article.PrixUniaite) + " ").PadLeft(colonne4, ' ');
            ligne_facture += ((in_article.TaxeCategorie == Article.TAXABLE)?in_article.TaxeCategorie + " ":" ").PadRight(colonne5, ' ');
            ligne_facture += (formatMonnai(in_article.PrixUniaite*in_article.Quantite)+" $").PadLeft(colonne6, ' '); ;
            ligne_facture = ligne_facture.PadLeft(ligne_total, ' ');

            ligne_facture += "\n";

            return ligne_facture;
        }

        private string lignesFacture(List<Article> in_articles)
        {
            string chaineDeSortie = "";
            foreach (Article article in in_articles)
            {
                chaineDeSortie += ligneFacture(article);
            }

            return chaineDeSortie;
        }

        private string getBlockSousTotal()
        {
            double sous_total = sousTotal(); 
            return ("Sous-Total : " + (formatMonnai(sous_total) + " $").PadLeft(11,' ')).PadLeft(ligne_total,' ') + "\n"+
                   ("       TPS : " + (formatMonnai(montant_tps) + " $").PadLeft(11,' ')).PadLeft(ligne_total,' ') + "\n" +
                   ("       TVQ : " + (formatMonnai(montant_tvq) + " $").PadLeft(11,' ')).PadLeft(ligne_total,' ') + "\n" +
                   ("     Total : " + (formatMonnai(montant_tps + montant_tvq  + sous_total) + " $").PadLeft(11,' ')).PadLeft(ligne_total,' ') + "\n";
        }
        
        public override string ToString()
        {
            string chaineDeSortie = entete();
           
            chaineDeSortie += lignesFacture(Articles);
            chaineDeSortie += new string('-',NONBRE_DE_PAS_LIGNE) +'\n';

            chaineDeSortie += getBlockSousTotal();
            return chaineDeSortie;
        }

        public double sousTotal()
        {
            double sous_total = 0;
            montant_tps = 0;
            montant_tvq = 0;
            foreach (Article article in Articles)
            {
                sous_total += article.PrixUniaite * article.Quantite;
                calculerTaxe(article);
            }

            return formatMonnai(sous_total);

        }

        private double formatMonnai(double montant)
        {
            return Math.Round(montant, 2);
        }
    }
}