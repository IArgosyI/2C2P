using _2C2P.DAL;
using _2C2P.DAL.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2C2P.Common
{
    public static class _2C2PCommonContainerExtension
    {
        public static void RegisterCommon(this IServiceCollection services)
        {
            services.AddSingleton<ILogger, Logger>();
            services.AddSingleton<IDalClient, DalClient>();
            services.AddSingleton<IDataValidator, DataValidator>();
            services.AddSingleton<IXMLParser<Transaction>, TransactionFromXMLParser>();
            services.AddSingleton<ICSVParser<Transaction>, TransactionFromCSVParser>();
            services.AddSingleton<IFileParser<Transaction>, TransactionFileParser>();
            services.AddSingleton<IDatabaseUploader, DatabaseUploader>();
        }
    }
}
