namespace APIUNIP.Models
{
    public class SensorDB
    {
        public int Codigo { get; set; } = 0;
        public string Sensor { get; set; } = "";
        public DateTime HORADOREGISTRO { get; set; } = DateTime.Now;
        public double VALORCOLETADO { get; set; } = 0;
        public double VALORPROCESSADO { get; set; } = 0;
        public string STATUS { get; set; } = "";
    }
}
