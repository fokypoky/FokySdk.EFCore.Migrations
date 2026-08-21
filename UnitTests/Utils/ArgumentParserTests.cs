using FokySdk.EFCore.Migrations.Utils;

namespace UnitTests.Utils
{
    public class ArgumentParserTests
    {
        [Theory]
        [InlineData("Host=localhost", "Port=5432", "User=postgres", "Password=12345")]
        [InlineData("Port=5432", "User=postgres", "Password=12345", "Database=postgres")]
        [InlineData("Host=localhost", "User=postgres", "Password=12345", "Database=postgres")]
        [InlineData("Host=localhost", "Port=5432", "User=postgres", "Database=postgres")]
        [InlineData("Host=localhost", "Port=5432", "Password=12345", "Database=postgres")]
        public void ParseArguments_PartialArguments_ShouldThrowArgumentException(params string[] args)
        {
            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                ArgumentParser.ParseArguments(args);
            });
        }

        [Fact]
        public void ParseArguments_AllArgumentsWithoutSchema_ShouldParseAndSchemaBeEqualsDefault()
        {
            // Arrange
            var args = new[]
            {
                "host=localhost", "port=5432", "user=postgresUSER", "password=postgresPASS", "database=postgresDB"
            };
            
            // Act
            var result = ArgumentParser.ParseArguments(args);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("localhost", result.Host);
            Assert.Equal("5432", result.Port);
            Assert.Equal("postgresUSER", result.User);
            Assert.Equal("postgresPASS", result.Password);
            Assert.Equal("postgresDB", result.Database);
            Assert.Equal("public", result.Schema);
        }
        
        [Fact]
        public void ParseArguments_AllArgumentsWithSchema_ShouldParse()
        {
            // Arrange
            var args = new[]
            {
                "host=localhost", "port=5432", "user=postgresUSER", "password=postgresPASS", "database=postgresDB", "schema=postgresSCHEMA"
            };
            
            // Act
            var result = ArgumentParser.ParseArguments(args);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("localhost", result.Host);
            Assert.Equal("5432", result.Port);
            Assert.Equal("postgresUSER", result.User);
            Assert.Equal("postgresPASS", result.Password);
            Assert.Equal("postgresDB", result.Database);
            Assert.Equal("postgresSCHEMA", result.Schema);
        }

        [Fact]
        public void ParseArguments_ValidArgumentsWithoutSchema_ShouldParse()
        {
            // Arrange
            var args = new []
            {
                "usefile=true", "filename=MigrationOptionsWithoutSchema.json"
            };
            
            // Act
            var result = ArgumentParser.ParseArguments(args);
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal("localhost", result.Host);
            Assert.Equal("5432", result.Port);
            Assert.Equal("postgresUSER", result.User);
            Assert.Equal("postgresPASS", result.Password);
            Assert.Equal("postgresDB", result.Database);
            Assert.Equal("public", result.Schema);
        }

        [Fact]
        public void ParseArguments_ValidArgumentsWithSchema_ShouldParse()
        {
            // Arrange
            var args = new []
            {
                "usefile=true", "filename=MigrationOptionsWithSchema.json"
            };
            
            // Act
            var result = ArgumentParser.ParseArguments(args);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("localhost", result.Host);
            Assert.Equal("5432", result.Port);
            Assert.Equal("postgresUSER", result.User);
            Assert.Equal("postgresPASS", result.Password);
            Assert.Equal("postgresDB", result.Database);
            Assert.Equal("postgresSCHEMA", result.Schema);
        }
    }
}