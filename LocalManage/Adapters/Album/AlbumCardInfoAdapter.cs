using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.Uwp.Models.Data.Content;

namespace PureRadio.LocalManage.Adapters
{
    class AlbumCardInfoAdapter
    {
     public static ContentInfoCard ToContentInfoCard(AlbumCardInfo album)
        {
            string Cover = "";
            if (album.LocalCover != null)
            {
                Cover = album.LocalCover.AbsoluteUri;
            }
            else
            {
                Cover = album.Cover.AbsoluteUri;
            }
            ContentInfoCard content = new ContentInfoCard
                (
                album.ContentId,
                album.Title,
                album.Podcasters,
                Cover,
                album.Description,
                ""
                );
            return content;
        }

     public static ContentInfoDetail ToContentInfoDetail(AlbumCardInfo album)
        {
            Uri Cover = null;
            if (album.LocalCover != null)
            {
                Cover = album.LocalCover;
            }
            else
            {
                Cover = album.Cover;
            }
            ContentInfoDetail detail = new ContentInfoDetail(
                Cover,
                album.ContentId,
                album.Version,
                album.Title,
                album.Description,
                0,
                "",
                album.Rating,
                album.Podcasters,
                album.CategoryId,
                album.ContentType,
                null
                );

            return detail;
        }

    public static AlbumCardInfo ToAlbumCardInfo(ContentInfoDetail detail)
        {
            AlbumCardInfo album = new AlbumCardInfo();
            album.CategoryId = detail.CategoryId;
            album.ContentId = detail.ContentId;
            album.ContentType = detail.ContentType;
            album.Cover = detail.Cover;
            album.Description = detail.Description;
            album.Podcasters = detail.Podcasters;
            album.Rating = detail.Rating;
            album.Title = detail.Title;
            album.Version = detail.Version;
            return album;
        }

    }
}
