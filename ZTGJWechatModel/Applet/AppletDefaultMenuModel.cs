using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Applet
{
    /// <summary>
	/// AppletDefaultMenuModel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public partial class AppletDefaultMenuModel
    {
        public AppletDefaultMenuModel()
        { }
        #region Model
        private int _id;
        private string _img = "";
        private string _title = "";
        private string _des = "";
        private string _page = "";
        private int _pagetype = 0;
        private int _type = 0;
        private int _state = 0;
        private DateTime _createtime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 菜单封面
        /// </summary>
        public string img
        {
            set { _img = value; }
            get { return _img; }
        }
        /// <summary>
        /// 菜单名
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 菜单描述
        /// </summary>
        public string des
        {
            set { _des = value; }
            get { return _des; }
        }
        /// <summary>
        /// 页面
        /// </summary>
        public string page
        {
            set { _page = value; }
            get { return _page; }
        }
        /// <summary>
        /// 页面类型 0:switchTab(跳转到tabBar页),1:reLaunch(关闭所有跳转),2:redirectTo(关闭当前跳转页面不包括tabbar),3:navigateTo(保留当前跳转),4:navigateBack(关闭当前返回上级)
        /// </summary>
        public int pagetype
        {
            set { _pagetype = value; }
            get { return _pagetype; }
        }
        /// <summary>
        /// 菜单类型  0主程序，1附加服务，11工程师物料管理
        /// </summary>
        public int type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 菜单状态 0敬请期待,1正常,2禁用
        /// </summary>
        public int state
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

    }
}
