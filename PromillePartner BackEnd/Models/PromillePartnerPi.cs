namespace PromillePartner_BackEnd.Models
{
    /// <summary>
    /// A unique pi, represented by a constant identifier used to get the current ip of a pie to connect to it.
    /// </summary>
    public class PromillePartnerPi
    {

        public string Identifier { get; set; }
        public string ApiKey { get; set; }
        public string Ip { get; set; }


        public PromillePartnerPi(string identifier, string ip, string apiKey)
        {
            ApiKey = apiKey;
            Identifier = identifier;
            Ip = ip;
        }

        public PromillePartnerPi()
        {
        }

        public void ValidateIndentifier()
        {
            if (string.IsNullOrWhiteSpace(Identifier))
            {
                throw new ArgumentNullException("Identifier must have a value");
            }

        }

        public void ValidateApiKey()
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new ArgumentNullException($"{nameof(ApiKey)} can't be null");
            }
        }

        public void ValidateIp()
        {
            if (string.IsNullOrWhiteSpace(Ip))
            {
                throw new ArgumentNullException("Ip Must be set");
            }
            if (!System.Net.IPAddress.TryParse(Ip, out _))
            {
                throw new ArgumentException($"{nameof(Ip)} is not a valid IP address.");
            }
        }

        public void Validate()
        {
            ValidateIndentifier();
            ValidateApiKey();
            ValidateIp();
        }

    }
}
