using System;

namespace INF731_TP1_FANGUE
{
    public class Article
    {
        /* Declaration des constantes */
        public const string NON_TAXABLE = "NT";
        public const string TAXABLE     = "FP";

        enum type_taxe
        {
            NON_TAXABLE,
            TAXABLE
        };
        

        /* Declaration des attributs */
        private string numero_article;
        private string taxe_categorie;
        private string description;
        private int    quantite;
        private float  prix_uniaite;

        public Article()
        {
            
        }
        
        public Article(Article articleEntrant)
        {
            NumeroArticle = articleEntrant.NumeroArticle;
            TaxeCategorie = articleEntrant.TaxeCategorie;
            Description   = articleEntrant.Description;
            Quantite      = articleEntrant.Quantite;
            PrixUniaite   = articleEntrant.PrixUniaite;
        }
        
        /**
         * 
         */
        public Article( string numero_article_entrant, string taxe_categorie_entrant, int quantite_entrant, string description_entrant, float prix_uniaite_entrant)
        {
            NumeroArticle = numero_article_entrant;
            TaxeCategorie = taxe_categorie_entrant;
            Description   = description_entrant;
            Quantite      = quantite_entrant;
            PrixUniaite   = prix_uniaite_entrant;
            
        }

        public string NumeroArticle
        {
            get => numero_article;
            set => numero_article = (value.Contains(" ")==false)?value:"";
        }

        public string TaxeCategorie
        {
            get => taxe_categorie;
            set
            {
                Enum.IsDefined(typeof(type_taxe),value);
                if (TAXABLE == value.ToUpper() || NON_TAXABLE == value.ToUpper())
                {
                    taxe_categorie = value.ToUpper();
                }
            }
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public int Quantite
        {
            get => quantite;
            set
            {
                if (value >= 0)
                {
                    quantite = value;
                }
            }
        }

        public float PrixUniaite
        {
            get => prix_uniaite;
            set => prix_uniaite = value;
        }

        public bool estNonTaxable()
        {
            return (TaxeCategorie == NON_TAXABLE);
        }

        public string ToString()
        {
            return NumeroArticle+";"+TaxeCategorie+";"+Quantite+";"+Description+";"+PrixUniaite;
        }
    }
}