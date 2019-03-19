using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace General.Core
{
    public class EngineContext
    {

        private static IEngine _engine;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(IEngine engine)
        {
            if (_engine == null)
                _engine = engine;
            return _engine;
        }
        /// <summary>
        /// 
        /// </summary>
        public static IEngine Current
        {
            get { return _engine; }
        }
    }
}
