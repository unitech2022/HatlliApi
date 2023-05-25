using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Models.BaseEntity;

namespace HattliApi.Core
{
    public interface BaseInterface 
    {

       Task<dynamic> GetItems(string UserId,int page);


        Task<dynamic> AddAsync(dynamic type);

         Task<dynamic> GitById(int typeId);


        Task<dynamic> DeleteAsync(int typeId);

        void UpdateObject(dynamic category);


         bool SaveChanges();
    }
}