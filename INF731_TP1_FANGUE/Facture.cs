using System;
using System.Collections.Generic;

namespace INF731_TP1_FANGUE
{
    public class Facture
    {
        /* Declaration des constantes */
        public const double  TPS                 = 0.05;
        public const double  TVQ                 = 0.09975;
        public const string  NOM_RACINE          = "Facture-";
        private const int    NONBRE_DE_PAS_LIGNE = 80;
        private const string AUTEURS             = "EMMANUEl FANGUE";
        
        /* Declaration des attributs */
        private List<Article> articles;
        private string        nom_facture;
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
            ligne_facture += in_article.NumeroArticle + " ";
            ligne_facture += in_article.Quantite + " ";
            ligne_facture += in_article.Description + " ";
            ligne_facture += formatMonnai(in_article.PrixUniaite) + " ";
            ligne_facture += in_article.TaxeCategorie + " ";
            ligne_facture += formatMonnai(in_article.PrixUniaite*in_article.Quantite) + " $\n";

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
            return "Sous-Total : " + formatMonnai(sous_total) + " $\n" +
                   "       TPS : " + formatMonnai(montant_tps) + " $\n" +
                   "       TVQ : " + formatMonnai(montant_tvq) + " $\n" +
                   "     Total : " + formatMonnai(montant_tps + montant_tvq  + sous_total) + " $\n";
        }
        
        public string ToString()
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