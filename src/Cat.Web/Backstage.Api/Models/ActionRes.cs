using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Api
{
    public class ActionRes
    {
        public ActionRes(int code, string msg, object data)
        {
            this.Code = code;
            this.Msg = msg;
            this.Data = data;
        }
        /// <summary>
        /// 返回码, 大于等于0就表示成功, 小于0表示失败
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 操作结果
        /// </summary>
        public enum Status
        {
            成功 = 0,
            失败 = -1
        }

        /// <summary>
        /// 响应结果
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ActionRes Response(Status status, string msg = "", object data = null)
        {
            return new ActionRes((int)status, msg, data);
        }

        /// <summary>
        /// 请求成功
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ActionRes Success()
        {
            return new ActionRes(0, "success", null);
        }
        /// <summary>
        /// 请求成功
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ActionRes Success(object data = null)
        {
            return new ActionRes(0, "success", data);
        }
        /// <summary>
        /// 请求成功
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ActionRes Success(int code = 0, string msg = "success", object data = null)
        {
            return new ActionRes(code, msg, data);
        }
        /// <summary>
        /// 请求失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ActionRes Fail(string msg)
        {
            return Fail(-1, msg);
        }
        /// <summary>
        /// 请求失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ActionRes Fail(Exception ex, bool saveErrLog = true)
        {
            //return Fail(-1, $"操作失败：{ex.Message}{Environment.NewLine}TraceIdentifier：{Cat.Foundation.CatContext.HttpContext.TraceIdentifier}");
            return Fail(-1, $"{ex.Message}{Environment.NewLine}");
        }
        /// <summary>
        /// 请求失败
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ActionRes Fail()
        {
            return new ActionRes(-1, "fail", null);
        }
        /// <summary>
        /// 请求失败
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ActionRes Fail(object data = null)
        {
            return new ActionRes(-1, "fail", data);
        }
        /// <summary>
        /// 请求失败
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ActionRes Fail(int code = -1, string msg = "fail", object data = null)
        {
            return new ActionRes(code, msg, data);
        }
    }
}
