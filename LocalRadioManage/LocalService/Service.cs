using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.DBBuilder.TableObj;

namespace LocalRadioManage.LocalService
{
   public partial class LocalService
    {
        /// <summary>
        /// 访问此变量需加锁，用于异步结果读取
        /// </summary>
        public MissionComplete complete = null;
        ServiceUserDown UserDownService = null;
        ServiceUserFav UserFavService = null;
        public class MissionComplete
        {
            public bool is_complete = false;
            public bool is_success = false;
        }

        public LocalService()
        {
            Start();
            complete = new MissionComplete();
            UserDownService = new ServiceUserDown();
            UserFavService = new ServiceUserFav();
        }
        private bool Start()
        {
            //暂有数据库的创建/获取->默认用户设置->默认存放文件夹
            if (SQLiteConnect.CreateLocalRadioManage())
            {
                Default.DefalutUser.SetDefaultUser();
                Default.DefalutStorage.SetDefaultStorage();
                return true;
            }
            return false;
        }
        public async void Start_Async()
        {
            lock (complete)
            {
                complete.is_complete = false;
            }
            if (await Task.Run(SQLiteConnect.CreateLocalRadioManage))
            {
                await Task.Run(Default.DefalutUser.SetDefaultUser);
                await Task.Run(Default.DefalutStorage.SetDefaultStorage);
                lock (complete)
                {
                    complete.is_complete = true;
                    complete.is_success = true;
                }

            }
            else
            {
                lock (complete)
                {
                    complete.is_complete = true;
                    complete.is_success = false;
                }
            }
        }
    }

    /// <summary>
    /// 本地下载服务
    /// </summary>
    public partial class LocalService
    {
       

        public void test()
        {

        }
        
    }

    /// <summary>
    ///远端收藏服务
    /// </summary>
    public partial class LocalService
    {
       
    }

}








