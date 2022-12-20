using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace cv8aspnet
{
    public class JsonLogger:IMyLogger
    {
        public async Task Log(Exception ex)
        {
            await File.AppendAllTextAsync("log.json", JsonSerializer.Serialize(new
            {
                date = DateTime.Now,
                message = ex.Message
            }));
        }
    }
}
