namespace Zarinpal.Core
{
    public class ZarinVerifyData
    {
        public int code { get; set; }
        public bool IsSuccessfulPayment => code == 100;
        
        public string message { get; set; }
        public string card_hash { get; set; }
        public string card_pan { get; set; }
        public string ref_id { get; set; }
        public string fee_type { get; set; }
        public int fee { get; set; }
    }
}