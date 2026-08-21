using Newtonsoft.Json;

namespace FokySdk.EFCore.Migrations.Types
{
    public class MigrationOptions
    {
        [JsonProperty("host")]
        public string Host { get; set; }
        
        [JsonProperty("port")]
        public string Port { get; set; }
        
        [JsonProperty("user")]
        public string User { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
        
        [JsonProperty("database")]
        public string Database { get; set; }
        
        [JsonProperty("schema")]
        public string Schema { get; set; } = "public";

        public string ToConnectionString()
        {
            return $"Host={Host};Port={Port};Database={Database};Username={User};Password={Password};Search Path={Schema}";
;        }
    }
}