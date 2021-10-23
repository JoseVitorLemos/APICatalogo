using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace APICatalogo.Logging
{
	public class CustomerLogger : ILogger
	{
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string nome, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnable(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exeception, Func<TState, Exception, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            EscreverTetoNoArquivo(mensagem);
        }

        private void EscreverTextoNoArquivo(string mensagem)
        {
            string caminhoArquivoLog = @"C:\Users\Rick\Desktop\Projetos\log\Arquivo_logger.txt";
            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
            {
                try
                {
                    streamWriter.WriteLine(mensagem);
                    streamWriter.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
