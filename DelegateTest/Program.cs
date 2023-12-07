// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");



using System.Reflection;
using System.Xml;

public class Program
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public static string ConfigFileName { get; private set; }

    /// <summary>
    /// 读取XML的委托
    /// </summary>
    /// <param name="reader">XML读取器</param>
    public delegate void ReadXmlHandler(XmlReader reader);

    /// <summary>
    /// 默认XML读取设置
    /// </summary>
    public static readonly XmlReaderSettings DefaultXmlReaderSettings;



    //交易日是否包含周末
    private static bool includeWeekends;

    /// <summary>
    /// 交易日是否不剔除周日
    /// </summary>
    public static bool IncludeWeekends { get => includeWeekends; set => includeWeekends = value; }

    //启用存档的标志
    private static bool enableBackup;

    /// <summary>
    /// 应用程序目录
    /// </summary>
    public static string AppPath { get; protected set; }

    public static void Main(string[] args)
    {
        //程序目录
        AppPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        //配置文件名
        ConfigFileName = Path.Combine(AppPath, "SubCounter.config");

        if (!ReadToPath(ConfigFileName, "SubCounter",
            delegate (System.Xml.XmlReader reader)
            {
                if (reader.IsEmptyElement)
                    throw new InvalidOperationException("配置文件\"" + Program.ConfigFileName + "\"无效！");
                reader.Read();

                string urlDataAuth = null;
                while (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "ChannelFactory":
                            urlDataAuth = reader["DataAuth"];
                            reader.Skip();
                            continue;
                        case "Backup":
                            enableBackup = ToBoolean(reader["Enable"]);
                            reader.Skip();
                            continue;
                        case "TimeMap":
                            IncludeWeekends = ToBoolean(reader["IncludeWeekends"]);
                            reader.Skip();
                            continue;
                        default:
                            reader.Skip();
                            continue;
                    }
                }
                if (string.IsNullOrEmpty(urlDataAuth))
                    throw new InvalidOperationException("配置文件缺少 DataAuth！");
                Console.WriteLine("urlDataAuth：" + urlDataAuth);
                Console.WriteLine("enableBackup：" + enableBackup);
                Console.WriteLine("IncludeWeekends：" + IncludeWeekends);
            }))

        Console.WriteLine("Hello, World!");
        Console.ReadKey();
    }

    public static bool ReadToPath(string fileName, string path, ReadXmlHandler handler)
    {
        if (fileName == null)
            throw new ArgumentNullException(nameof(fileName));
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        if (handler == null)
            throw new ArgumentNullException(nameof(handler));
        if (path == "")
            throw new ArgumentException("path不能为空！");

        string[] names = path.Split('/', '\\');

        try
        {
            using (XmlReader reader = XmlReader.Create(fileName, DefaultXmlReaderSettings))
            {
                foreach (string name in names)
                    if (!reader.ReadToDescendant(name)) return false;
                handler(reader);
                return true;
            }
        }
        catch (System.IO.FileNotFoundException)
        {
            return false;
        }
    }


    /// <summary>
    /// 将逻辑值的指定字符串表示形式转换为其等效的布尔值。
    /// </summary>
    /// <param name="Value">包含 System.Boolean.TrueString 或 System.Boolean.FalseString 值的字符串。</param>
    /// <returns>如果 Value 等于 System.Boolean.TrueString，则为 true，否则为 false。</returns>
    public static bool ToBoolean(string Value)
    {
        switch (Value)
        {
            case "0": return false;
            case "1": return true;
        }
        bool result;
        bool.TryParse(Value, out result);
        return result;
    }


}
