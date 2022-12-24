using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Configurations
{
    public class CustomersAccountsConfiguration
    {
        public string Section { get; set; } = "CustomersAccountsDatabase";
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CustomersCollectionName { get; set; } = null!;
        public string AccountsCollectionName { get; set; } = null!;

    }
}
