using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel
{
    public class ProvinceItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

    }


    public class Province
    {

        public static List<ProvinceItem> ProvinceResult()
        {
            Province item = new Province();
            List<ProvinceItem> provinceItems = new List<ProvinceItem>();
            for (int i = 0; i < item.name.Length; i++)
            {
                ProvinceItem p = new ProvinceItem();
                provinceItems.Add(p);
                provinceItems[i].Name = item.name[i];
                provinceItems[i].Id = item.id[i];
                provinceItems[i].Image = item.image[i];
            }
            return provinceItems;
        }

        string[] image = {
            "/Assets/Image/Provinces/3_beijing.png",
            "/Assets/Image/Provinces/5_tianjin.png",
            "/Assets/Image/Provinces/7_hebei.png",
            "/Assets/Image/Provinces/83_shanghai.png",
            "/Assets/Image/Provinces/19_shanxi.png",
            "/Assets/Image/Provinces/31_neimenggu.png",
            "/Assets/Image/Provinces/44_liaoning.png",
            "/Assets/Image/Provinces/59_jilin.png",
            "/Assets/Image/Provinces/69_heilongjiang.png",
            "/Assets/Image/Provinces/85_jiangsu.png",
            "/Assets/Image/Provinces/99_zhejiang.png",
            "/Assets/Image/Provinces/111_anhui.png",
            "/Assets/Image/Provinces/129_fujian.png",
            "/Assets/Image/Provinces/139_jiangxi.png",
            "/Assets/Image/Provinces/151_shandong.png",
            "/Assets/Image/Provinces/169_henan.png",
            "/Assets/Image/Provinces/187_hubei.png",
            "/Assets/Image/Provinces/202_hunan.png",
            "/Assets/Image/Provinces/217_guangdong.png",
            "/Assets/Image/Provinces/239_guangxi.png",
            "/Assets/Image/Provinces/254_hainan.png",
            "/Assets/Image/Provinces/257_chongqing.png",
            "/Assets/Image/Provinces/259_sichuan.png",
            "/Assets/Image/Provinces/281_guizhou.png",
            "/Assets/Image/Provinces/291_yunnan.png",
            "/Assets/Image/Provinces/316_shaanxi.png",
            "/Assets/Image/Provinces/327_gansu.png",
            "/Assets/Image/Provinces/351_ningxia.png",
            "/Assets/Image/Provinces/357_xinjiang.png",
            "/Assets/Image/Provinces/308_xizang.png",
            "/Assets/Image/Provinces/342_qinghai.png"};

        string[] name = {
            "北京",
            "天津",
            "河北",
            "上海",
            "山西",
            "内蒙古",
            "辽宁",
            "吉林",
            "黑龙江",
            "江苏",
            "浙江",
            "安徽",
            "福建",
            "江西",
            "山东",
            "河南",
            "湖北",
            "湖南",
            "广东",
            "广西",
            "海南",
            "重庆",
            "四川",
            "贵州",
            "云南",
            "陕西",
            "甘肃",
            "宁夏",
            "新疆",
            "西藏",
            "青海"};

        string[] id = {
            "3",
            "5",
            "7",
            "83",
            "19",
            "31",
            "44",
            "59",
            "69",
            "85",
            "99",
            "111",
            "129",
            "139",
            "151",
            "169",
            "187",
            "202",
            "217",
            "239",
            "254",
            "257",
            "259",
            "281",
            "291",
            "316",
            "327",
            "351",
            "357",
            "308",
            "342"};
    }
}
