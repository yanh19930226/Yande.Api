﻿namespace In66.Authentication.Bearer;

public class BearerSchemeOptions : AuthenticationSchemeOptions
{
    public BearerSchemeOptions()
    {
        Events = new BearerEvents();
    }

    public new BearerEvents Events
    {
        get { return (BearerEvents)base.Events; }
        set { base.Events = value; }
    }
}
