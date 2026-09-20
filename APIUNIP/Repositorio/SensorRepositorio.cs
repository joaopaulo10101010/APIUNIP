using APIUNIP.Models;
using MySql.Data.MySqlClient;


namespace APIUNIP.Repositorio
{
    public class SensorRepositorio
    {
        public readonly string[] _sensores = { "LDR", "MQ135", "BMP280", "DHT22" };
        public SensorRepositorio()
        {
            

        }

        public void Insert(SensorDB sensor)
        {
            try
            {

                if (_sensores.Contains(sensor.Sensor))
                {
                    Conexao con = new Conexao();

                    MySqlCommand command = con.Comando();

                    string StrSql = $"INSERT INTO TB_{sensor.Sensor}(LDR_VALORCOLETADO,LDR_VALORPROCESSADO,LDR_STATUS) values ({sensor.VALORCOLETADO},{ProcessarSensor(sensor.Sensor,sensor.VALORCOLETADO).ToString().Replace(",",".")},'{sensor.STATUS}')";

                    command.CommandText = StrSql;

                    command.ExecuteNonQuery();
                }

            }
            catch(Exception ex)
            {

            }
        }

        public List<SensorDB> Select(string entrada)
        {
            List<SensorDB> sensores = new List<SensorDB>();

            try
            {
                string sensor = entrada.ToUpper();

                if (_sensores.Contains(sensor))
                {
                    Conexao con = new Conexao();

                    MySqlCommand command = con.Comando();

                    string StrSql = $@"
                                        SELECT 
                                            {sensor}_CODIGO AS CODIGO,
                                            {sensor}_HORADOREGISTRO AS HORAREGISTRO,
                                            {sensor}_VALORCOLETADO AS VALORCOLETADO,
                                            {sensor}_VALORPROCESSADO AS VALORPROCESSADO,
                                            {sensor}_STATUS AS STATUS
                                        FROM TB_{sensor};
                                    ";

                    command.CommandText = StrSql;

                    using var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        SensorDB sensorDB = new SensorDB();

                        sensorDB.Codigo = Convert.ToInt32(reader["CODIGO"]);
                        sensorDB.HORADOREGISTRO = Convert.ToDateTime(reader["HORAREGISTRO"]);
                        sensorDB.VALORCOLETADO = Convert.ToDouble(reader["VALORCOLETADO"]);
                        sensorDB.VALORPROCESSADO = Convert.ToDouble(reader["VALORPROCESSADO"]);
                        sensorDB.STATUS = $"{Convert.ToString(reader["STATUS"])}";

                        sensores.Add(sensorDB);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return sensores;
        }

        public List<SensorDB> SelectMedia(string entrada)
        {
            List<SensorDB> sensores = new List<SensorDB>();

            try
            {
                string sensor = entrada.ToUpper();

                if (_sensores.Contains(sensor))
                {
                    Conexao con = new Conexao();

                    MySqlCommand command = con.Comando();

                    string StrSql = $@"
                                        SELECT
                                            DATE({sensor}_HORADOREGISTRO) AS DIA,
                                            AVG({sensor}_VALORCOLETADO) AS MEDIA_VALORCOLETADO,
                                            AVG({sensor}_VALORPROCESSADO) AS MEDIA_VALORPROCESSADO
                                        FROM TB_{sensor}
                                        GROUP BY DATE({sensor}_HORADOREGISTRO)
                                        ORDER BY DIA;
                                    ";

                    command.CommandText = StrSql;

                    using var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        SensorDB sensorDB = new SensorDB();

                        sensorDB.Codigo = 0;

                        sensorDB.HORADOREGISTRO = Convert.ToDateTime(reader["DIA"]);
                        sensorDB.VALORCOLETADO = Convert.ToDouble(reader["MEDIA_VALORCOLETADO"]);
                        sensorDB.VALORPROCESSADO = Convert.ToDouble(reader["MEDIA_VALORPROCESSADO"]);
                        sensorDB.STATUS = "OK";

                        sensores.Add(sensorDB);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return sensores;
        }

        private double ProcessarSensor(string sensor, double valor)
        {
            switch (sensor)
            {
                case "BMP280":
                    return ConverterBMP280(valor);

                case "DHT22":
                    return ConverterDHT22(valor);

                case "LDR":
                    return ConverterLDR(valor);

                case "MQ135":
                    return ConverterMQ135(valor);

                default:
                    return valor;
            }
        }
        private double ConverterLDR(double valor)
        {
            double porcentagem = (valor / 4095.0) * 100.0;

            return Math.Round(porcentagem, 2);
        }

        private double ConverterMQ135(double valor)
        {
            double tensao = (valor / 4095.0) * 3.3;

            return Math.Round(tensao, 2);
        }

        private double ConverterBMP280(double valor)
        {
            return Math.Round(valor, 2);
        }

        private double ConverterDHT22(double valor)
        {
            return Math.Round(valor, 2);
        }

    }
}
