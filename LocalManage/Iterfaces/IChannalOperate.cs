using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DataModelsL;

namespace PureRadio.LocalManage.Iterfaces
{
    /// <summary>
    /// 结构与IAlbumOperate一致
    /// </summary>
    public interface IChannalOperate
    {

        Task<bool> Download(ChannalCardInfo album);
        Task<bool> Download(ChannalCardInfo album, ChannalRadioInfo radio);

        bool Fav(ChannalCardInfo album);

        bool Fav(ChannalCardInfo album, ChannalRadioInfo radio);

        int Fav(ChannalCardInfo album, List<ChannalRadioInfo> radios);


        Task<bool> Remove(ChannalCardInfo album);

        Task<bool> Remove(ChannalRadioInfo radio);

        bool Update(ChannalCardInfo album);

        bool Update(ChannalRadioInfo radio);

        List<ChannalCardInfo> Load();

        List<ChannalRadioInfo> Load(ChannalCardInfo album);

       Task<bool> Export(ChannalCardInfo album, ChannalRadioInfo radio);

    }
}
