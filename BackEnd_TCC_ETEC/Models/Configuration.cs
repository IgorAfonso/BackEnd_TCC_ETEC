namespace BackEndAplication.Models
{
    public class Configuration
    {
        private static IConfiguration _configuration;

        public static IConfiguration Conf
        {
            get
            {
                if(_configuration == null)
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile($"appsettings.json", true, true)
                        .Build();
                return _configuration;
            }
        }

        public static string GetSectionValue(string section, string value)
        {
            return Conf.GetSection(section).GetValue<string>(value);
        }
    }
}
