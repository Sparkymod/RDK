﻿using System;

namespace Dagher.ORM.SubSonic.DataProviders
{
    public interface IInterceptionStrategy
    {
        object Intercept(object objectToIntercept);

        bool Accept(Type type);
    }

}
