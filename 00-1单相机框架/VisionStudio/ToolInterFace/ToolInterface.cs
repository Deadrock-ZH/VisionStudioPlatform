using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolInterFace
{
    /// <summary>
    /// 内 容：工具接口
    /// 作 者：王雅鹏
    /// 时 间：2020/3/3
    /// </summary>
    public interface ToolInterface
    {
        /// <summary>
        /// 输入图像
        /// </summary>
        HObject InputImage
        {
            get;
            set;
        }

        /// <summary>
        /// 运行时间
        /// </summary>
        double RunTime
        {
            get;
            set;
        }

        /// <summary>
        /// 运行消息
        /// </summary>
        string RunMsg
        {
            get;
            set;
        }

        /// <summary>
        /// 模块form显示名
        /// </summary>
        string ModualFormName
        {
            get;
            set;
        }

        /// <summary>
        /// 存储指定颜色的Hobject
        /// </summary>
        List<GVS.HalconDisp.ViewROI.Config.HObjectWithColor> ListReg
        {
            get;
            set;
        }

        /// <summary>
        /// 保存成功
        /// </summary>
        /// <param name="obj">保存的对象</param>
        /// <param name="filename">保存路径</param>
        /// <returns>true:保存成功</returns>
        bool Save(object obj, string filename);

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="type">加载的对象的类型</param>
        /// <param name="filename">加载路径</param>
        /// <returns>true：加载成功</returns>
        object Load(Type type, string filename);

        /// <summary>
        /// 运行方法
        /// </summary>
        /// <returns>true:运行成功</returns>
        bool Run();
    }
}
