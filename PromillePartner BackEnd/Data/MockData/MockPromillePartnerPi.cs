using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Data.MockData
{
    public class MockPromillePartnerPi
    {

        public static List<PromillePartnerPi> GetMockPies()
        {
            return
            [
               new PromillePartnerPi() {Identifier = "bob", ApiKey="bob-api-key", Ip="127.0.0.1"},
               new PromillePartnerPi() {Identifier = "kim", ApiKey="kim-api-key", Ip="127.0.0.1"}

            ];
        }

    }
}
