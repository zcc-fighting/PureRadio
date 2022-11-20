using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DataModelsL;

namespace PureRadio.LocalManage.Iterfaces
{
    interface IUserAlbumOperate
    {
        //某一用户下载
         Task<bool> Download(AlbumCardInfo card);

         Task<bool> Download(AlbumCardInfo card, AlbumRadioInfo radio);
        //某一用户收藏
         bool Fav(AlbumCardInfo card);

         bool Fav(AlbumCardInfo card, AlbumRadioInfo radio);

        //type 0->下载，1->收藏，2->两者
         List<AlbumCardInfo> Load(int type);

         List<AlbumRadioInfo> Load(AlbumCardInfo card, int type);

        //清空用户下载/收藏
         bool Remove(int type);
        //清空用户某一专辑下载/收藏
         bool Remove(AlbumCardInfo card, int type);
        //清空用户某一音频下载/收藏
         bool Remove(AlbumRadioInfo radio, int type);
    }
}
