using DAL.Entity;

namespace WPF.Models
{
    public class TarifClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? PricePerMonth { get; set; }
        public int? PricePerConnection { get; set; }
        public int? PriceGorod { get; set; }
        public int? PriceMejGorod { get; set; }
        public int? PriceMejNarod { get; set; }
        public int? SMS_Per_Month { get; set; }
        public int? GB_Per_Month { get; set; }
        public int? Min_Per_Month { get; set; }
        public int TariffType { get; set; }

        public TarifClass() { }

        public TarifClass(TarifTable tarif)
        {
            Id = tarif.C__PK___Kod_Tarifa;
            Name = tarif.Name;
            PricePerMonth = tarif.Cost_PerMonth;
            PriceGorod = tarif.Price_1minn_city;
            PriceMejGorod = tarif.Price_1minn_mejgorod;
            PriceMejNarod = tarif.Price_1minn_mejdunarod;
            SMS_Per_Month = tarif.SMS_Per_Month;
            GB_Per_Month = tarif.GB_Per_Month;
            Min_Per_Month = tarif.Min_Per_Month;
            TariffType = tarif.C___FK___Kod_Type_Of_Tarif; 
            PricePerConnection = tarif.Cost_Of_Connection;
        }
    }
}