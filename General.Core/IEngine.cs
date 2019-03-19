using System;
using System.Collections.Generic;
using System.Text;

namespace General.Core
{
    public interface IEngine
    {
        T Resove<T>() where T:class;
    }
}
