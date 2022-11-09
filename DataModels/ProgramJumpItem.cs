using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel
{
    public class ProgramJumpItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class ProgramJump
    {

        public static List<ProgramJumpItem> ProgramJumpResult()
        {
            ProgramJump item = new ProgramJump();
            List<ProgramJumpItem> programJumpItems = new List<ProgramJumpItem>();
            for (int i = 0; i < item.name.Length; i++)
            {
                ProgramJumpItem p = new ProgramJumpItem();
                programJumpItems.Add(p);
                programJumpItems[i].Name = item.name[i];
                programJumpItems[i].Id = item.id[i];
                programJumpItems[i].Image = item.image[i];
            }
            return programJumpItems;
        }

        string[] name = { "小说", "脱口秀", "相声小品", "头条", "情感", "儿童", "出版精品", "历史", "评书", "音乐", "财经", "教育", "娱乐", "影视", "文化", "外语", "汽车", "科技", "戏曲", "广播剧", "二次元", "校园", "品牌电台", "超级会员", "联合专区", "生活", "母婴" };
        string[] id = { "521", "3251", "527", "545", "529", "1599", "3636", "531", "3496", "523", "533", "537", "547", "3588", "3613", "543", "3385", "535", "3276", "3442", "3427", "1737", "3600", "3637", "3631", "3670", "3675" };
        string[] image =
        {
            "/Assets/Image/ProgramCategories/521_xiaoshuo.png",
            "/Assets/Image/ProgramCategories/3251_tuokouxiu.png",
            "/Assets/Image/ProgramCategories/527_xiangshengxiaopin.png",
            "/Assets/Image/ProgramCategories/545_toutiao.png",
            "/Assets/Image/ProgramCategories/529_qinggan.png",
            "/Assets/Image/ProgramCategories/1599_ertong.png",
            "/Assets/Image/ProgramCategories/3636_chubanjingpin.png",
            "/Assets/Image/ProgramCategories/531_lishi.png",
            "/Assets/Image/ProgramCategories/3496_pingshu.png",
            "/Assets/Image/ProgramCategories/523_yinyue.png",
            "/Assets/Image/ProgramCategories/533_caijing.png",
            "/Assets/Image/ProgramCategories/537_jiaoyu.png",
            "/Assets/Image/ProgramCategories/547_yule.png",
            "/Assets/Image/ProgramCategories/3588_yingshi.png",
            "/Assets/Image/ProgramCategories/3613_wenhua.png",
            "/Assets/Image/ProgramCategories/543_waiyu.png",
            "/Assets/Image/ProgramCategories/3385_qiche.png",
            "/Assets/Image/ProgramCategories/535_keji.png",
            "/Assets/Image/ProgramCategories/3276_xiqu.png",
            "/Assets/Image/ProgramCategories/3442_guangboju.png",
            "/Assets/Image/ProgramCategories/3427_erciyuan.png",
            "/Assets/Image/ProgramCategories/1737_xiaoyuan.png",
            "/Assets/Image/ProgramCategories/3600_pinpaidiantai.png",
            "/Assets/Image/ProgramCategories/3637_chaojihuiyuan.png",
            "/Assets/Image/ProgramCategories/3631_lianhezhuanqu.png",
            "/Assets/Image/ProgramCategories/3670_shenghuo.png",
            "/Assets/Image/ProgramCategories/3675_muying.png"
        };
    }
}
