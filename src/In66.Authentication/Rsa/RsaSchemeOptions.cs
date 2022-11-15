namespace In66.Authentication.Rsa
{
    public class RsaSchemeOptions : AuthenticationSchemeOptions
    {
        public RsaSchemeOptions()
        {
            Events = new RsaEvents();
        }

        public new RsaEvents Events
        {
            get { return (RsaEvents)base.Events; }
            set { base.Events = value; }
        }
    }
}
