namespace In66.Authentication.Basic
{
    public class BasicEvents
    {
        public Func<BasicTokenValidatedContext, Task> OnTokenValidated { get; set; }
    }
}
