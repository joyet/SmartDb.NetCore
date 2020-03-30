using SmartDb.NetCore;
using SmartDb.Repository.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestSmartDbRepository
{
    public interface IUserInfoRepository: ISmartDbRepository<UserInfo, int>
    {
       
        
    }
}
