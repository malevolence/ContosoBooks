﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoBooks.Config
{
    public class BraintreeSettings
    {
		public string Environment { get; set; }
		public string MerchantId { get; set; }
		public string PublicKey { get; set; }
		public string PrivateKey { get; set; }
	}
}
