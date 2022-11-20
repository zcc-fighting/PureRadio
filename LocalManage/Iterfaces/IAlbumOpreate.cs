using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DataModelsL;


namespace PureRadio.LocalManage.Iterfaces
{
    interface IAlbumOpreate
    {

        /// <summary>
        /// 下载专辑及其封面
        /// </summary>
        Task<bool> Download(AlbumCardInfo album);
        /// <summary>
        /// 下载专辑及其中一个音频
        /// </summary>
        Task<bool> Download(AlbumCardInfo album, AlbumRadioInfo radio);

        /// <summary>
        /// 收藏专辑
        /// </summary>
         bool Fav(AlbumCardInfo album);

        /// <summary>
        /// 收藏专辑及其中一个音频
        /// </summary>
        bool Fav(AlbumCardInfo album, AlbumRadioInfo radio);
        /// <summary>
        /// 收藏专辑及其中所有音频
        /// </summary>
        int Fav(AlbumCardInfo album, List<AlbumRadioInfo> radios);

        /// <summary>
        /// 移除专辑(其中音频一并移除)
        /// </summary>
        Task<bool> Remove(AlbumCardInfo album);

        /// <summary>
        /// 移除音频
        /// </summary>
        Task<bool> Remove(AlbumRadioInfo radio);
        /// <summary>
        /// 更新专辑信息
        /// </summary>
         bool Update(AlbumCardInfo album);
        /// <summary>
        /// 更新音频信息
        /// </summary>
         bool Update(AlbumRadioInfo radio);

        /// <summary>
        /// 载入所有专辑详情页
        /// </summary>
         List<AlbumCardInfo> Load();

        /// <summary>
        /// 载入所有音频详情
        /// </summary>
         List<AlbumRadioInfo> Load(AlbumCardInfo album);

        /// <summary>
        /// 导出音频
        /// </summary>
        Task<bool> Export(AlbumCardInfo album, AlbumRadioInfo radio);
    }

    
}
