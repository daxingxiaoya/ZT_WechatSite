using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.Common;
using ZTGJWechatModel.Common;
using ZTGJWechatUtils;

namespace ZTGJWechatBll.Common
{
    public class AddressBll
    {
        private AddressDal addrdal = new AddressDal();

        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetAddress(string uid, string type = "", string key = "", string id = "")
        {
            string res = "";
            try
            {
                AddressModel reqm = new AddressModel();
                reqm.unionid = uid;
                
                if (!string.IsNullOrEmpty(id) && id != "0")
                {
                    reqm.Id = Convert.ToInt32(id);
                }
                if (!string.IsNullOrEmpty(key))
                {
                    //判断是否为数字，是数字表示手机号反之姓名
                    if (System.Text.RegularExpressions.Regex.IsMatch(key, @"^[+-]?/d*$"))
                    {
                        reqm.phone = key;//手机号
                    }
                    else
                    {
                        reqm.name = key;//姓名
                    }
                }
                if (!string.IsNullOrEmpty(type) && type == "1")
                {
                    reqm.isdefault = 1;
                }
                
                List<AddressModel> reslist = addrdal.GetAddress(reqm);
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = reslist.Count, rows = reslist });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog("AddAddress异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 添加或修改地址
        /// </summary>
        /// <returns></returns>
        public string AddOrUpAddress(string reqdata)
        {
            string res = "";
            try
            {
                AddressModel addm = JsonConvert.DeserializeObject<AddressModel>(reqdata);
                bool resb = false;
                if (addm.Id != 0)//修改
                {
                    resb = addrdal.UpdateAddress(addm);
                }
                else//新增
                {
                    resb = addrdal.AddAddress(addm);
                }
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", addstate = resb });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog("AddAddress异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }

        // <summary>
        /// 更改地址为默认
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string UpdateDefaultAddress(string uid, string id)
        {
            string res = "";
            try
            {
                bool resb = addrdal.UpdateDefaultAddress(uid, Convert.ToInt32(id));
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", upstate = resb });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog("UpdateDefaultAddress异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        // <summary>
        /// 删除地址
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DelAddress(string uid, string id)
        {
            string res = "";
            try
            {
                bool resb = addrdal.DelAddress(uid, id);
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", upstate = resb });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog("UpdateDefaultAddress异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }

    }

}
