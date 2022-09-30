namespace WebTest.Entity
{
    public class TrKasir
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string TipeCostumer { get; set; }
        public int PointReward { get; set; }
        public int Diskon { get; set; }
        public int TotalBelanja { get; set; }
        public int TotalBayar { get; set; }

        public DateTime TsCrt { get; set; }
    }
}
