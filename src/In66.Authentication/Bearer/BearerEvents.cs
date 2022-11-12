namespace In66.Authentication.Bearer;

public class BearerEvents
{
    public Func<BearerTokenValidatedContext, Task> OnTokenValidated { get; set; }
}
