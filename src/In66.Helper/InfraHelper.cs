﻿namespace In66.Helper;

public sealed class InfraHelper
{
    private InfraHelper()
    {
    }
    static InfraHelper()
    { 
    }

    public static ISecurity Security => new Security();

    public static IHashGenerater Hash => new HashGenerater();

    public static IAccessor Accessor => new Accessor();
}