using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatlliApi.ViewModels;
using HattliApi.Core;
using HattliApi.Models;

namespace HatlliApi.Serveries.AlertsServices
{
    public interface IAlertsServices :BaseInterface
    {
         Task<AlertResponse> GetAlertsByUserId(string userId);
          Task<dynamic> ViewedAlert(string userId,int alertId);
    }
}