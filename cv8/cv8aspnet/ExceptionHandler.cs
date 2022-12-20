using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace cv8aspnet
{
    public class ExceptionHandler
    {
        private IMyLogger logger;
        public ExceptionHandler(IMyLogger logger)
        {
            this.logger = logger;
        }

        public async Task Handle(Exception ex)
        {
            await logger.Log(ex);
            //await File.AppendAllTextAsync("log.txt", ex.Message + "\n");
        }
    }
}
