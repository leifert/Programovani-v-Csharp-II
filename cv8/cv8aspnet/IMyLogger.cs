using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cv8aspnet
{
    public interface IMyLogger
    {
        Task Log(Exception ex);
    }
}
