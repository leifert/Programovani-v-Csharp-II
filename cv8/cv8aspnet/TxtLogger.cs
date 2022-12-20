using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace cv8aspnet
{
    public class TxtLogger:IMyLogger
    {
        public async Task Log(Exception ex)
        {
            await File.AppendAllTextAsync("log.txt", ex.Message + "\n");
        }
    }
}
