using System.Runtime.Serialization;
using System.Text;
using FokySdk.EFCore.Migrations.Types;
using Newtonsoft.Json;

namespace FokySdk.EFCore.Migrations.Utils
{
    public static class ArgumentParser
    {
        private const string HOST_SELECTOR = "host";
        private const string PORT_SELECTOR = "port";
        private const string USERNAME_SELECTOR = "user";
        private const string PASSWORD_SELECTOR = "password";
        private const string DB_SELECTOR = "database";
        private const string SCHEMA_SELECTOR = "schema";
        private const string USE_FILE_SELECTOR = "usefile";
        private const string FILE_NAME_SELECTOR = "filename";
        
        public static MigrationOptions ParseArguments(string[] args)
        {
            var useFile = GetArgumentValue(args, USE_FILE_SELECTOR);
            if (!string.IsNullOrWhiteSpace(useFile) && bool.Parse(useFile))
            {
                var fileName = GetArgumentValue(args, FILE_NAME_SELECTOR);
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    throw new ArgumentException("Missing 'filename' parameter when 'usefile=true'");
                }

                var fileContent = ReadOptionsFileContent(fileName);
                return JsonConvert.DeserializeObject<MigrationOptions>(fileContent) ?? throw new SerializationException($"Can't deserialize {fileName} content as migration options");
            }

            var host = GetArgumentValue(args, HOST_SELECTOR) ?? throw new ArgumentException($"'{HOST_SELECTOR}' argument expected");
            var port = GetArgumentValue(args, PORT_SELECTOR) ?? throw new ArgumentException($"'{PORT_SELECTOR}' argument expected");
            var user = GetArgumentValue(args, USERNAME_SELECTOR) ?? throw new ArgumentException($"'{USERNAME_SELECTOR}' argument expected");
            var password = GetArgumentValue(args, PASSWORD_SELECTOR) ?? throw new ArgumentException($"'{PASSWORD_SELECTOR}' argument expected");
            var database = GetArgumentValue(args, DB_SELECTOR) ?? throw new ArgumentException($"'{DB_SELECTOR}' argument expected");
            var schema = GetArgumentValue(args, SCHEMA_SELECTOR) ?? "public";

            return new MigrationOptions()
            {
                Host = host,
                Port = port,
                User = user,
                Password = password,
                Database = database,
                Schema = schema
            };
        }

        public static string? GetArgumentValue(string[] args, string selector)
        {
            return args
                .FirstOrDefault(x => x.Split('=').First().Equals(selector, StringComparison.InvariantCultureIgnoreCase))
                ?.Split('=')
                .Last();
        }

        public static string ReadOptionsFileContent(string filename)
        {
            using var reader = new StreamReader(path: filename, encoding: Encoding.UTF8);
            return reader.ReadToEnd();
        }
    }
}