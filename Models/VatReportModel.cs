﻿using System;
using System.Collections.Generic;

namespace PointOfSalesSystem.Models
{
    public class VatReportModel
    {
        public string ReportName { get; set; }
        public decimal Amount { get; set; }
        public List<VatReturn> VatReturns { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
