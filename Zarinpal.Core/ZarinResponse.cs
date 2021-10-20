using System.Collections.Generic;

namespace Zarinpal.Core
{
    public class ZarinResponse
    {
        public ZarinData data { get; set; }
        public List<string> errors { get; set; }
    }
}