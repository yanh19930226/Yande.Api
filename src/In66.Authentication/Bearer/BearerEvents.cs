namespace In66.Authentication.Bearer;

public class BearerEvents
{
    public Func<BearerTokenValidatedContext, Task> OnTokenValidated { get; set; }

    public Func<BearerTokenValidFailedContext, Task> OnTokenFailed { get; set; }
}
