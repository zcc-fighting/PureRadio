using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DataModelsL;

namespace PureRadio.LocalManage.Iterfaces
{
    /// <summary>
    /// 与IUserAlbumOperate结构一致
    /// </summary>
    interface IUserChannalOperate
    {
        Task<bool> Download(ChannalCardInfo card);

        Task<bool> Download(ChannalCardInfo card, ChannalRadioInfo radio);

        bool Fav(ChannalCardInfo card);

        bool Fav(ChannalCardInfo card, ChannalRadioInfo radio);

        //type 0->下载，1->收藏，2->两者
        List<ChannalCardInfo> Load(int type);

        List<ChannalRadioInfo> Load(ChannalCardInfo card, int type);

        //清空用户下载/收藏
        bool Remove(int type);
        //清空用户某一专辑下载/收藏
        bool Remove(ChannalCardInfo card, int type);
        //清空用户某一音频下载/收藏
        bool Remove(ChannalRadioInfo radio, int type);
    }


}
