using System.Net;

namespace FBL_ATM_POSTING
{
    internal class ATM_Posting_Service
    {
        public NetworkCredential Credentials { get; set; }
        public bool PreAuthenticate { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }
}