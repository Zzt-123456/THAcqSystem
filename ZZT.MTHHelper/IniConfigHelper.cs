using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ZZT.MTHHelper
{
    /// <summary>
    /// INI 配置文件读写帮助类
    /// ==================================================================
    /// 用途：基于 Windows API（kernel32.dll）封装 INI 文件的读写操作。
    /// 核心职责：
    ///   1. 读取指定节点（Section）下某个 Key 的字符串值；
    ///   2. 向指定节点下的某个 Key 写入字符串值；
    ///   3. 读取 INI 文件中所有节点（Section）集合；
    ///   4. 读取某个节点下所有 Key 集合。
    /// 使用场景：
    ///   - 本项目使用 INI 文件存储设备配置信息，例如：
    ///       [Device] 节点保存 IP 地址、端口号；
    ///       [Recipe] 节点保存当前配方编号、配方名称；
    ///       [System] 节点保存采样周期、报警阈值等参数。
    ///   - INI 文件相对于 JSON/XML 更轻量、可读性强、便于人工编辑。
    /// 设计说明：
    ///   - 通过 P/Invoke 调用 kernel32.dll 中的 GetPrivateProfileString / WritePrivateProfileString 函数；
    ///   - 提供了"带路径"和"不带路径"两种重载，不带路径版本使用静态字段 IniPath；
    ///   - 读取 Sections / Keys 时使用字节数组接收（GetPrivateProfileStringA），通过 \0 分隔解析多个结果。
    /// 注意：
    ///   - INI 文件 API 在 .NET Core / .NET 5+ 的非 Windows 平台上不可用，本类仅适用于 Windows 平台。
    /// </summary>
    public class IniConfigHelper
    {
        /// <summary>
        /// 文件路径
        /// 静态字段：作为不带路径参数的重载方法的默认路径使用。
        /// 调用方应在程序启动时（如 Program.Main 或窗体 Load）设置此字段。
        /// </summary>
        public static string IniPath = string.Empty;


        #region API 函数声明

        /// <summary>
        /// 向 INI 文件写入字符串数据
        /// 调用 Win32 API：kernel32.dll 中的 WritePrivateProfileString 函数。
        /// </summary>
        /// <param name="section">节点名称（如 "Device"）</param>
        /// <param name="key">键名（如 "IP"）</param>
        /// <param name="val">键值（如 "192.168.1.100"）</param>
        /// <param name="filePath">INI 文件完整路径</param>
        /// <returns>非 0 表示成功，0 表示失败</returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
    string val, string filePath);

        // 需要调用 GetPrivateProfileString 的重载，用于按 StringBuilder 接收字符串结果
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// GetPrivateProfileString 的字节数组版本（A 后缀 ANSI 版本）
        /// 当 section 或 key 传 null 时，可返回所有节点名或某节点下所有 Key 名（以 \0 分隔）。
        /// </summary>
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key,
            string def, Byte[] retVal, int size, string filePath);

        #endregion

        #region 读取 INI 文件
        /// <summary>
        /// 根据节点及 Key 的值返回数据
        /// </summary>
        /// <param name="Section">节点</param>
        /// <param name="Key">键</param>
        /// <param name="NoText">默认值（当 Key 不存在时返回此值）</param>
        /// <param name="iniFilePath">INI 文件完整路径</param>
        /// <returns>返回值（Key 对应的字符串内容；文件不存在时返回 string.Empty）</returns>
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            // 先校验文件是否存在，避免 API 调用产生异常或返回不可预期结果
            if (File.Exists(iniFilePath))
            {
                // 预分配 10240 字符容量，足以容纳大部分 INI 单项值
                StringBuilder stringBuilder = new StringBuilder(10240);

                // 调用 Win32 API 读取数据，结果填充到 stringBuilder 中
                GetPrivateProfileString(Section, Key, NoText, stringBuilder, 10240, iniFilePath);

                return stringBuilder.ToString();
            }
            else
            {
                // 文件不存在时返回空字符串，便于调用方做统一判空处理
                return string.Empty;
            }
        }


        /// <summary>
        /// 根据节点及 Key 的值返回数据（使用默认路径 IniPath）
        /// </summary>
        /// <param name="Section">节点</param>
        /// <param name="Key">键</param>
        /// <param name="NoText">默认值</param>
        /// <returns>返回值</returns>
        public static string ReadIniData(string Section, string Key, string NoText)
        {
            // 转发到带路径参数的版本，使用静态字段 IniPath
            return ReadIniData(Section, Key, NoText, IniPath);
        }

        #endregion

        #region 写入 INI 文件

        /// <summary>
        /// 根据节点及 Key 的值写入数据
        /// </summary>
        /// <param name="Section">节点</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        /// <param name="path">路径</param>
        /// <returns>操作结果（true 表示成功，false 表示失败）</returns>
        public static bool WriteIniData(string Section, string Key, string Value, string path)
        {
            // 调用 Win32 API 写入数据，返回值非 0 表示成功
            long result = WritePrivateProfileString(Section, Key, Value, path);

            if (result == 0)
            {
                // 返回 0 表示写入失败（文件被占用、路径无效、权限不足等）
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 根据节点及 Key 的值写入数据（使用默认路径 IniPath）
        /// </summary>
        /// <param name="Section">节点</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        /// <returns>操作结果</returns>
        public static bool WriteIniData(string Section, string Key, string Value)
        {
            // 转发到带路径参数的版本
            return WriteIniData(Section, Key, Value, IniPath);
        }

        #endregion

        #region 读取所有的 Sections

        /// <summary>
        /// 读取所有的 Section
        /// 原理：调用 API 时 section 参数传 null，API 会返回所有节点名（以 \0 分隔，结尾两个 \0）。
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>Section 集合</returns>
        public static List<string> ReadSections(string path)
        {
            // 65536 字节缓冲区，足以容纳大型 INI 文件的所有节点名
            byte[] buffer = new byte[65536];

            // section/key 均传 null，API 返回所有 section 名称
            uint length = GetPrivateProfileStringA(null, null, null, buffer, buffer.Length, path);

            int startIndex = 0;

            List<string> sections = new List<string>();

            // API 返回的数据以 \0 分隔各个 section 名，遍历拆分
            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0)
                {
                    // 截取从 startIndex 到 i 之间的字节，按系统默认编码（中文环境下通常为 GBK）解码为字符串
                    sections.Add(Encoding.Default.GetString(buffer, startIndex, i - startIndex));
                    startIndex = i + 1;
                }
            }

            return sections;
        }

        /// <summary>
        /// 读取所有的 Section（使用默认路径 IniPath）
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>Section 集合</returns>
        public static List<string> ReadSections()
        {
            byte[] buffer = new byte[65536];

            uint length = GetPrivateProfileStringA(null, null, null, buffer, buffer.Length, IniPath);

            int startIndex = 0;

            List<string> sections = new List<string>();

            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0)
                {
                    sections.Add(Encoding.Default.GetString(buffer, startIndex, i - startIndex));
                    startIndex = i + 1;
                }
            }

            return sections;
        }



        #endregion

        #region 根据某个 Section 读取所有的 Keys

        /// <summary>
        /// 根据某个 Section 读取所有的 Keys
        /// 原理：调用 API 时 section 指定节点名、key 传 null，API 返回该节点下所有 Key（以 \0 分隔）。
        /// </summary>
        /// <param name="section">某个 section</param>
        /// <param name="path">路径</param>
        /// <returns>key 的集合</returns>
        public static List<string> ReadKeys(string section, string path)
        {
            byte[] buffer = new byte[65536];

            // section 指定节点，key 传 null，API 返回该 section 下所有 Key
            uint length = GetPrivateProfileStringA(section, null, null, buffer, buffer.Length, path);

            int startIndex = 0;

            List<string> keys = new List<string>();

            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0)
                {
                    keys.Add(Encoding.Default.GetString(buffer, startIndex, i - startIndex));
                    startIndex = i + 1;
                }
            }

            return keys;

        }

        /// <summary>
        /// 根据某个 Section 读取所有的 Keys（使用默认路径 IniPath）
        /// </summary>
        /// <param name="section">某个 section</param>
        /// <param name="path">路径</param>
        /// <returns>key 的集合</returns>
        public static List<string> ReadKeys(string section)
        {
            byte[] buffer = new byte[65536];

            uint length = GetPrivateProfileStringA(section, null, null, buffer, buffer.Length, IniPath);

            int startIndex = 0;

            List<string> keys = new List<string>();

            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0)
                {
                    keys.Add(Encoding.Default.GetString(buffer, startIndex, i - startIndex));
                    startIndex = i + 1;
                }
            }

            return keys;

        }

        #endregion

    }
}
