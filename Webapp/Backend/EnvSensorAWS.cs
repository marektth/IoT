using System;
using System.ComponentModel.DataAnnotations;

namespace ZadanieZCT.Backend
{
    public class EnvSensorAWS : IEnvSensor
    {


        [Required]
        private double temperature { get; set; }

        [Required]
        private double humidity { get; set; }

        [Required]
        private double pressure { get; set; }

        [Required]
        private DateTime created_at { get; set; }

        public EnvSensorAWS(double temperature, double humidity, double pressure)
        {
            this.temperature = temperature;
            this.humidity = humidity;
            this.pressure = pressure;
            this.created_at = DateTime.Now.AddHours(2);
        }

        public double GetTemperature()
        {
            return this.temperature;
        }
        public double GetHumidity()
        {
            return this.humidity;
        }

        public double GetPressure()
        {
            return this.pressure;
        }

        public DateTime GetCreated_at()
        {
            return this.created_at;
        }
    }
}