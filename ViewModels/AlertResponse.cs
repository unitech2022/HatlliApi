using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Models;

namespace HatlliApi.ViewModels
{
    public class AlertResponse
    {
        public int UnViewed { get; set; }

        public List<Alert>? Alerts { get; set; }
    }
}